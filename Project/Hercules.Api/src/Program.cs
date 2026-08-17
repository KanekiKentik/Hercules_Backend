using Scalar.AspNetCore;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var jwtSection = builder.Configuration.GetSection(nameof(JwtOptions));

builder.Services.Configure<JwtOptions>(jwtSection);
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

if (builder.Environment.IsDevelopment()) 
    builder.Services.AddOpenApi(options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

var jwtOptions = jwtSection.Get<JwtOptions>();
if (jwtOptions == default)
    throw new NullReferenceException("Cannot parse jwt options");

string? connString = builder.Configuration.GetSection("ConnectionStrings")["psql"];
if (string.IsNullOrEmpty(connString))
    throw new NullReferenceException("Cannot get connection string");

builder.Services.AddAuth(jwtOptions);
builder.Services.AddDbAndDependencies(connString);
builder.Services.AddHasher();

builder.Services.AddScoped<ICurrentUser, CurrentUserProvider>(p =>
{
    var accessor = p.GetRequiredService<IHttpContextAccessor>();

    return new(accessor.HttpContext!.User);
});
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<TemplateService>();
builder.Services.AddScoped<WorkoutService>();

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(typeof(UserCredentialsValidator).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Theme = ScalarTheme.Kepler;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.Services.MigrateDb();
app.Services.SeedData(Path.Combine(AppContext.BaseDirectory, "seedingdata.json"));
app.UseExceptionHandling();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();