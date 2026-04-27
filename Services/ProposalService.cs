using System.Text.Json;
using DevRequestPortal.Data;
using DevRequestPortal.Models;
using DevRequestPortal.Services.Interfaces;
using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;
using Microsoft.EntityFrameworkCore;

namespace DevRequestPortal.Services
{
    public class ProposalService : IProposalService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ProposalService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<ProposalDetailResponse> CreateAsync(SubmitProposalRequest req)
        {
            var no = (await _db.Proposals.CountAsync()) + 1;
            var p = new Proposal
            {
                TrackingNumber = $"DRP-{DateTime.Now:yyyy}-{no:D4}",
                ScreeningCategory = req.Screening?.Category,
                ScreeningScore = req.Screening?.Score,
                ScreeningRecommendation = req.Screening?.Recommendation,
                ProjectName = req.RequesterInfo.ProjectName,
                RequesterName = req.RequesterInfo.Name,
                RequesterEmail = req.RequesterInfo.Email,
                Department = req.RequesterInfo.Department,
                ManagerName = req.RequesterInfo.ManagerName,
                ManagerEmail = req.RequesterInfo.ManagerEmail,
                SubmissionDate = req.RequesterInfo.SubmissionDate,
                Problem = req.ProblemGoals.Problem,
                Objective = req.ProblemGoals.Objective,
                UserGroupsJson = JsonSerializer.Serialize(req.ProblemGoals.UserGroups),
                Benefits = req.ProblemGoals.Benefits,
                FeaturesJson = JsonSerializer.Serialize(req.featuresWorkflow.Features),
                WorkflowDescription = req.featuresWorkflow.WorkflowDescription,
                BusinessRules = req.featuresWorkflow.BusinessRules,
                RequiredScreensJson = JsonSerializer.Serialize(req.ScreensData.RequiredScreens),
                DataFieldsJson = JsonSerializer.Serialize(req.ScreensData.DataFields),
                UIDraftDescription = req.ScreensData.UIDraftDescription,
                SampleDataDescription = req.ScreensData.SampleDataDescription,
                UrgencyLevel = req.Timeline.UrgencyLevel,
                DesiredDeadline = req.Timeline.DesiredDeadline,
                HardDeadline = req.Timeline.HardDeadline,
                DeadlineReason = req.Timeline.DeadlineReason,
                ChecklistJson = JsonSerializer.Serialize(req.Timeline.Checklist)   
            };
            _db.Proposals.Add(p);
            await _db.SaveChangesAsync();
            return ToDetail(p);
        }

        public async Task<List<ProposalSummaryResponse>> GetAllAsync(string? status, string? urgency)
        {
            var q = _db.Proposals.AsQueryable();
            if (!string.IsNullOrEmpty(status)) q = q.Where(p => p.Status == status);
            if (!string.IsNullOrEmpty(urgency)) q = q.Where(p => p.UrgencyLevel == urgency);
            return await q.OrderByDescending(p => p.CreatedAt)
                            .Select(p => ToSummary(p)).ToListAsync(); 
        }

        public async Task<ProposalDetailResponse?> GetByIdAsync(int id)
        {
            var p = await _db.Proposals.Include(x => x.Attachments)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            return p == null ? null : ToDetail(p);
        }

        public async Task<bool> UpdateStatusAsync(int id, UpdateProposalStatusRequest req)
        {
            var p = await _db.Proposals.FindAsync(id);
            if (p == null) return false;
            p.Status = req.Status;
            p.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<AttachmentResponse?> AddAttachmentAsync(int proposalId, string category, IFormFile file)
        {
            if (await _db.Proposals.FindAsync(proposalId) == null) return null;

            var dir = Path.Combine(_env.WebRootPath, "uploads", "proposals", $"{proposalId}", category);
            Directory.CreateDirectory(dir);
            var saved = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            await using var fs = new FileStream(Path.Combine(dir, saved), FileMode.Create);
            await file.CopyToAsync(fs);

            var att = new ProposalAttachment
            {
                ProposalId = proposalId,
                Category = category,
                FileName = file.FileName,
                FilePath = Path.Combine(dir, saved),
                ContentType = file.ContentType,
                FileSize = file.Length
            };
            _db.ProposalAttachments.Add(att);
            await _db.SaveChangesAsync();
            return new AttachmentResponse
            {
                Id = att.Id,
                Category = category,
                FileName = file.FileName,
                FileSize = file.Length,
                UploadedAt = att.UploadedAt
            };
        }

        private static ProposalSummaryResponse ToSummary(Proposal p) => new()
        {
            Id = p.Id,
            TrackingNumber = p.TrackingNumber,
            Status = p.Status,
            ProjectName = p.ProjectName,
            RequesterName = p.RequesterName,
            Department = p.Department,
            UrgencyLevel = p.UrgencyLevel,
            DesiredDeadline = p.DesiredDeadline,
            CreatedAt = p.CreatedAt,
            ScreeningRecommendation = p.ScreeningRecommendation
        };

        private static ProposalDetailResponse ToDetail(Proposal p) => new()
        {
            Id = p.Id,
            TrackingNumber = p.TrackingNumber,
            Status = p.Status,
            ProjectName = p.ProjectName,
            RequesterName = p.RequesterName,
            RequesterEmail = p.RequesterEmail,
            Department = p.Department,
            ManagerName = p.ManagerName,
            ManagerEmail = p.ManagerEmail,
            SubmissionDate = p.SubmissionDate,
            UrgencyLevel = p.UrgencyLevel,
            DesiredDeadline = p.DesiredDeadline,
            HardDeadline = p.HardDeadline,
            DeadlineReason = p.DeadlineReason,
            CreatedAt = p.CreatedAt,
            ScreeningCategory = p.ScreeningCategory,
            ScreeningScore = p.ScreeningScore,
            ScreeningRecommendation = p.ScreeningRecommendation,
            Problem = p.Problem,
            Objective = p.Objective,
            Benefits = p.Benefits,
            WorkflowDescription = p.WorkflowDescription,
            BusinessRules = p.BusinessRules,
            UIDraftDescription = p.UIDraftDescription,
            SampleDataDescription = p.SampleDataDescription,
            UserGroups = JsonSerializer.Deserialize<List<string>>(p.UserGroupsJson) ?? new(),
            Features = JsonSerializer.Deserialize<List<string>>(p.FeaturesJson) ?? new(),
            RequiredScreens = JsonSerializer.Deserialize<List<string>>(p.RequiredScreensJson) ?? new(),
            DataFields = JsonSerializer.Deserialize<List<DataFieldResponse>>(p.DataFieldsJson) ?? new(),
            Checklist = JsonSerializer.Deserialize<List<bool>>(p.ChecklistJson) ?? new(),
            Attachments = p.Attachments.Select(a => new AttachmentResponse
            {
                Id = a.Id,
                Category = a.Category,
                FileName = a.FileName,
                FileSize = a.FileSize,
                UploadedAt = a.UploadedAt
            }).ToList()
        };
    }
}