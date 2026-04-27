using DevRequestPortal.Services.Interfaces;
using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("api/[controller]"), Authorize]
public class ProposalController : ControllerBase
{
    private readonly IProposalService _svc;
    public ProposalController(IProposalService svc) => _svc = svc;

    // POST api/proposals
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubmitProposalRequest req)
    {
        if (!ModelState.IsValid) return BadRequest(ApiResponse<object>.Fail("Validation failed"));
        var result = await _svc.CreateAsync(req);
        return CreatedAtAction(nameof(GetById), new
        {
            id = result.Id
        }, ApiResponse<ProposalDetailResponse>.Ok(result, $"Submitting - Tracking: {result.TrackingNumber}"));
    } 

    // GET api/proposals?status=&urgency=
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, [FromQuery] string? urgency)
        => Ok(ApiResponse<List<ProposalSummaryResponse>>.Ok(await _svc.GetAllAsync(status, urgency)));

    // GET api/proposals/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _svc.GetByIdAsync(id);
        return r == null 
            ? NotFound(ApiResponse<object>.Fail("Not found"))
            : Ok(ApiResponse<ProposalDetailResponse>.Ok(r));
    }

    // PATCH api/proposals/{id}/status
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateProposalStatusRequest req)
    {
        var ok = await _svc.UpdateStatusAsync(id, req);
        return ok 
            ? Ok(ApiResponse<object>.Ok(null, "Status updated"))
            : NotFound(ApiResponse<object>.Fail("Not found"));
    }
    
    // PATCH api/proposals/{id}/attachments/{category}
    [HttpPatch("{id}/attachments/{category}")]
    public async Task<IActionResult> Upload(int id, string category, IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest(ApiResponse<object>.Fail("No file"));
        if (!new[] { "workflow", "ui", "data" }.Contains(category))
            return BadRequest(ApiResponse<object>.Fail("category must be: workflow | ui | data"));
        var r = await _svc.AddAttachmentAsync(id, category, file);
        return r == null
            ? NotFound(ApiResponse<object>.Fail("Proposal not found"))
            : Ok(ApiResponse<AttachmentResponse>.Ok(r));
    }
}