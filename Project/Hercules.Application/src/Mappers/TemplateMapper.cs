public static class TemplateMapper
{
    public static TemplateResponse ToResponse(this TemplateEntity template)
    {
        if(template.Exercises is not { Count: > 0}) 
            throw new ArgumentException($"Template id: {template.Id} does not contain exercises");

        var response = new TemplateResponse(template.Id, template.Name, template.Exercises.Select(e => e.Id).ToArray());
        return response;
    }
}   