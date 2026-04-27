using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;

namespace DevRequestPortal.Services.Interfaces
{
    public interface IAnnotationService
    {
        Task<AnnotationResponse> CreateAsync(SubmitAnnotationRequest request);
        Task<List<AnnotationResponse>> GetAllAsync(string? status);
        Task<AnnotationResponse?> GetByIdAsync(int id);
        Task<AttachmentResponse?> AddAttachmentAsync(int annotationId, IFormFile file);
    }
}