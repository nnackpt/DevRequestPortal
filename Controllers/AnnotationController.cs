using DevRequestPortal.Services.Interfaces;
using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("api/[controller]"), Authorize]
public class AnnotationController : ControllerBase
{
    private readonly IAnnotationService _svc;
    public AnnotationController(IAnnotationService svc) => _svc = svc;

    // POST api/annotations
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubmitAnnotationRequest req)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail("Validation failed"));
        var r = await _svc.CreateAsync(req);
        return CreatedAtAction(nameof(GetById), new { id = r.Id },
            ApiResponse<AnnotationResponse>.Ok(r, $"Submitted - Tracking: {r.TrackingNumber}"));
    }

    // GET api/annotations?status=
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status)
        => Ok(ApiResponse<List<AnnotationResponse>>.Ok(await _svc.GetAllAsync(status)));

    // GET api/annotations/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r == null
            ? NotFound(ApiResponse<object>.Fail("Not found"))
            : Ok(ApiResponse<AnnotationResponse>.Ok(r));   
    }

    // POST api/annotations/{id}/attachments
    [HttpPost("{id}/attachments")]
    public async Task<IActionResult> Upload(int id, IFormFile file)
    {
        if (file is null || file.Length == 0) return BadRequest(ApiResponse<object>.Fail("No file"));
        var r = await _svc.AddAttachmentAsync(id, file);
        return r == null
            ? NotFound(ApiResponse<object>.Fail("Annotation not found"))
            : Ok(ApiResponse<AttachmentResponse>.Ok(r));
    }
}