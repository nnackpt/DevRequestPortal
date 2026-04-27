namespace DevRequestPortal.Models
{
    public class Proposal
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "new"; // new | reviewing | revision | confirmed | signoff | inprogress | done | rejected
        
        // Screening (Step 0)
        public string? ScreeningCategory { get; set; } // dashboard | form | workflow | newapp | migrate | integration | other
        public int? ScreeningScore { get; set; }
        public string? ScreeningRecommendation { get; set; } // excel | nocode | consult | app

        // Step 1: Requester
        public string ProjectName { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string RequesterEmail { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;
        public string ManagerEmail { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; }

        // Step 2: Problem and Goals
        public string Problem { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string UserGroupsJson { get; set; } = "[]";
        public string? Benefits { get; set; }

        // Step 3: Features and Workflow
        public string FeaturesJson { get; set; } = "[]";
        public string WorkflowDescription { get; set; } = string.Empty;
        public string? BusinessRules { get; set; }

        // Step 4: Screens and Data
        public string RequiredScreensJson { get; set; } = "[]";
        public string DataFieldsJson { get; set; } = "[]";
        public string? UIDraftDescription { get; set; }
        public string? SampleDataDescription { get; set; }

        // Step 5: Timeline
        public string UrgencyLevel { get; set; } = "medium"; // low | medium | high
        public DateTime DesiredDeadline { get; set; }
        public DateTime? HardDeadline { get; set; }
        public string? DeadlineReason { get; set; }
        public string ChecklistJson { get; set; } = "[]";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<ProposalAttachment> Attachments { get; set; } = new List<ProposalAttachment>(); 
    }

    public class ProposalAttachment
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public string Category { get; set; } = string.Empty; // workflow | ui | data
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Proposal Proposal { get; set; } = null!;

    }
}