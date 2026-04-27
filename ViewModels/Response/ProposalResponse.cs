namespace DevRequestPortal.ViewModels.Response
{
    public class ProposalSummaryResponse
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string UrgencyLevel { get; set; } = string.Empty;
        public DateTime DesiredDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ScreeningRecommendation { get; set; }
    }

    public class ProposalDetailResponse : ProposalSummaryResponse
    {
        public string RequesterEmail { get; set; } = string.Empty;
        public string ManagerName { get; set; } = string.Empty;
        public string ManagerEmail { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; }

        public string Problem { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public List<string> UserGroups { get; set; } = new();
        public string? Benefits { get; set; }

        public List<string> Features { get; set; } = new();
        public string WorkflowDescription { get; set; } = string.Empty;
        public string? BusinessRules { get; set; }

        public List<string> RequiredScreens { get; set; } = new();
        public List<DataFieldResponse> DataFields { get; set; } = new();
        public string? UIDraftDescription { get; set; }
        public string? SampleDataDescription { get; set; }

        public DateTime? HardDeadline { get; set; }
        public string? DeadlineReason { get; set; }
        public List<bool> Checklist { get; set; } = new();

        public string? ScreeningCategory { get; set; }
        public int? ScreeningScore { get; set; }
        public List<AttachmentResponse> Attachments { get; set; } = new();
    }

    public class DataFieldResponse
    {
        public string FieldName { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public string? ExampleOrOptions { get; set; }
    }

    public class AttachmentResponse
    {
        public int Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
    }

    public class ScreeningResponse
    {
        public int TotalScore { get; set; }
        public string Recommendation { get; set; } = string.Empty; // excel | nocode | consult | app
        public string RecommendationTitle { get; set; } = string.Empty;
        public string RecommendationBody { get; set; } = string.Empty;
        public string ProjectSize { get; set; } = string.Empty;
        public string EstimatedTimeline { get; set; } = string.Empty;
        public List<ScreeningReason> Reasons { get; set; } = new();
    }


    public class ScreeningReason
    {
        public string Type { get; set; } = string.Empty; // high | mid
        public string Text { get; set; } = string.Empty;
    }
}