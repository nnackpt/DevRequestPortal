namespace DevRequestPortal.ViewModels.Response
{
    public class AnnotationResponse
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string RequesterName { get; set; } = string.Empty;
        public string RequesterEmail { get; set; } = string.Empty;
        public string SystemName { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public string ManagerEmail { get; set; } = string.Empty;
        public string ChangeType { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string AsIs { get; set; } = string.Empty;
        public string ToBe { get; set; } = string.Empty;
        public string? ScreeningDescription { get; set; }
        public string? ChangeLogRef { get; set; }
        public DateTime? ChangeLogDate { get; set; }
        public string UrgencyLevel { get; set; } = string.Empty;
        public DateTime? DesiredDeadline { get; set; }
        public string? AffectedUsers { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<AttachmentResponse> Attachments { get; set; } = new();
    }
}