using DevRequestPortal.Data;
using DevRequestPortal.Models;
using DevRequestPortal.Services.Interfaces;
using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;
using Microsoft.EntityFrameworkCore;

namespace DevRequestPortal.Services
{
    public class AnnotationService : IAnnotationService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public AnnotationService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<AnnotationResponse> CreateAsync(SubmitAnnotationRequest req)
        {
            var no = (await _db.Annotations.CountAsync()) + 1;
            var ann = new Annotation
            {
                TrackingNumber = $"ANN-{DateTime.Now:yyyy}-{no:D4}",
                RequesterName = req.RequesterName,
                RequesterEmail = req.RequesterEmail,
                SystemName = req.SystemName,
                SubmissionDate = req.SubmissionDate,
                ManagerName = req.ManagerName,
                ManagerEmail = req.ManagerEmail,
                ChangeType = req.ChangeType,
                Location = req.Location,
                AsIs = req.AsIs,
                ToBe = req.ToBe,
                ScreenShotDescription = req.ScreenshotDescription,
                ChangeLogRef = req.ChangeLogRef,
                ChangeLogDate = req.ChangeLogDate,
                UrgencyLevel = req.UrgencyLevel,
                DesiredDeadline = req.DesiredDeadline,
                AffectedUsers = req.AffectedUsers
            };
            _db.Annotations.Add(ann);
            await _db.SaveChangesAsync();
            return ToResponse(ann);
        }

        public async Task<List<AnnotationResponse>> GetAllAsync(string? status)
        {
            var q = _db.Annotations.AsQueryable();
            if (!string.IsNullOrEmpty(status)) q = q.Where(a => a.Status == status);
            return await q.OrderByDescending(a => a.CreatedAt)
                            .Select(a => ToResponse(a)).ToListAsync();
        }

        public async Task<AnnotationResponse?> GetByIdAsync(int id)
        {
            var a = await _db.Annotations.Include(x => x.Attachments)
                                            .FirstOrDefaultAsync(x => x.Id == id);
            return a == null ? null : ToResponse(a);
        }

        public async Task<AttachmentResponse?> AddAttachmentAsync(int annotationId, IFormFile file)
        {
            if (await _db.Annotations.FindAsync(annotationId) == null) return null;

            var dir = Path.Combine(_env.WebRootPath, "uploads", "annotations", $"{annotationId}");
            Directory.CreateDirectory(dir);
            var saved = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            await using var fs = new FileStream(Path.Combine(dir, saved), FileMode.Create);
            await file.CopyToAsync(fs);

            var att = new AnnotationAttachment
            {
                AnnotationId = annotationId,
                FileName = file.FileName,
                FilePath = Path.Combine(dir, saved),
                ContentType = file.ContentType,
                FileSize = file.Length
            };
            _db.AnnotationAttachments.Add(att);
            await _db.SaveChangesAsync();
            return new AttachmentResponse
            {
                Id = att.Id,
                Category = "screenshot",
                FileName = file.FileName,
                FileSize = file.Length,
                UploadedAt = att.UploadedAt
            };
        }
        
        private static AnnotationResponse ToResponse(Annotation a) => new()
        {
            Id = a.Id,
            TrackingNumber = a.TrackingNumber,
            Status = a.Status,
            RequesterName = a.RequesterName,
            RequesterEmail = a.RequesterEmail,
            SystemName = a.SystemName,
            SubmissionDate = a.SubmissionDate,
            ManagerName = a.ManagerName,
            ManagerEmail = a.ManagerEmail,
            ChangeType = a.ChangeType,
            Location = a.Location,
            AsIs = a.AsIs,
            ToBe = a.ToBe,
            ScreeningDescription = a.ScreenShotDescription,
            ChangeLogRef = a.ChangeLogRef,
            ChangeLogDate = a.ChangeLogDate,
            UrgencyLevel = a.UrgencyLevel,
            DesiredDeadline = a.DesiredDeadline,
            AffectedUsers = a.AffectedUsers,
            CreatedAt = a.CreatedAt,
            Attachments = a.Attachments.Select(x => new AttachmentResponse
            {
                Id = x.Id,
                Category = "screenshot",
                FileName = x.FileName,
                FileSize = x.FileSize,
                UploadedAt = x.UploadedAt
            }).ToList()
        };
    }
}