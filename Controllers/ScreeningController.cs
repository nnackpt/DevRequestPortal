using DevRequestPortal.Services.Interfaces;
using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("api/[controller]"), Authorize]
public class ScreeningController : ControllerBase
{
    private readonly IScreeningService _svc;
    public ScreeningController(IScreeningService svc) => _svc = svc;

    // POST api/screening/evaluate
    [HttpPost("evaluate")]
    public IActionResult Evaluate([FromBody] ScreeningRequest req)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail("Invalid request"));
        if (req.Answers.Any(a => a is < 0 or > 2))
            return BadRequest(ApiResponse<object>.Fail("Each answer must be 0, 1, or 2"));
        return Ok(ApiResponse<ScreeningResponse>.Ok(_svc.Evaluate(req)));
    }
}