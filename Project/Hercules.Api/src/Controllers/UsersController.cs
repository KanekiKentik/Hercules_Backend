using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/users")]
public class UsersController : ControllerBase
{
    private readonly UsersService _uService;
    public UsersController(UsersService usersService) => _uService = usersService;

    #region Guest
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserCredentialsDTO cred)
    {
        Result result = await _uService.Register(cred);
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserCredentialsDTO cred)
    {
        Result<string> result = await _uService.Login(cred);
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return Ok(result.Value);
    }
    #endregion

    #region User
    [Authorize]
    [HttpPatch("change-pass")]
    public async Task<IActionResult> ChangePassword([FromBody] PasswordRequest password)
    {
        Result result = await _uService.ChangePassword(password);
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [Authorize]
    [HttpPatch("change-name")]
    public async Task<IActionResult> ChangeUsername([FromBody] UsernameRequest username)
    {
        Result result = await _uService.ChangeUsername(username);
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [Authorize]
    [HttpGet("get-self")]
    public async Task<IActionResult> GetSelf()
    {
        var self = await _uService.GetSelf();
        if (self == null) return NotFound();
        
        return Ok(self);
    }
    #endregion

    #region Admin
    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpGet("get-by-username")]
    public async Task<IActionResult> GetByUsername([FromQuery] UsernameRequest request)
    {
        if (request == null)
            return BadRequest();

        var user = await _uService.Get(request);
        if (user == null) return NotFound();
        
        return Ok(user);
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpGet("get-by-id")]
    public async Task<IActionResult> GetById([FromQuery] int userId)
    {
        var user = await _uService.Get(userId);
        if (user == null) return NotFound();

        return Ok(user);
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpPatch("change-privilege")]
    public async Task<IActionResult> ChangePrivilege([FromQuery] int userId, [FromQuery] Privilege privilege)
    {
        Result result = await _uService.ChangePrivilege(userId, privilege);
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int userId)
    {
        Result result = await _uService.Delete(userId);
        if (result.IsFailure)
            this.HandleErrorResult(result);

        return NoContent();
    }

    [Authorize(Roles = nameof(Privilege.Admin))]
    [HttpPatch("admin/change-pass")]
    public async Task<IActionResult> AdminChangePass([FromQuery] int userId, [FromQuery] PasswordRequest request)
    {
        var result = await _uService.ChangePassword(request, userId);
        if (result.IsFailure) 
            return this.HandleErrorResult(result);

        return NoContent();
    }
    #endregion
}