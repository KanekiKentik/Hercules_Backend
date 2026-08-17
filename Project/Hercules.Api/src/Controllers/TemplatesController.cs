using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("/templates")]
public class TemplatesController : ControllerBase
{
    private readonly TemplateService _tService;
    public TemplatesController(TemplateService tService) => _tService = tService;

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var templates = await _tService.GetUsersTemplates();
        return Ok(templates);
    }

    [HttpPost("post")]
    public async Task<IActionResult> Post([FromBody] TemplateRequest request)
    {
        var result = await _tService.Post(request);
        if (result.IsFailure)
            return this.HandleErrorResult(result);
        
        return Created();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] int templateId)
    {
        var result = await _tService.Delete(templateId);
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromQuery] int templateId, [FromBody] TemplateRequest request)
    {
        var result = await _tService.Update(templateId, request);
        if (result.IsFailure)
            return this.HandleErrorResult(result);

        return NoContent();
    }
}