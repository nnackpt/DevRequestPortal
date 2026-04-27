using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;

namespace DevRequestPortal.Services.Interfaces
{
    public interface IProposalService
    {
        Task<ProposalDetailResponse> CreateAsync(SubmitProposalRequest request);
        Task<List<ProposalSummaryResponse>> GetAllAsync(string? status, string? urgency);
        Task<ProposalDetailResponse?> GetByIdAsync(int id);
        Task<bool> UpdateStatusAsync(int id, UpdateProposalStatusRequest request);
        Task<AttachmentResponse?> AddAttachmentAsync(int proposalId, string category, IFormFile file);
    }
}