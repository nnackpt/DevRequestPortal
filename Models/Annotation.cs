namespace DevRequestPortal.Models
{
    public class Annotation
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; } = string.Empty;
        public string Status { get; set; } = "new";

        // General
        public string RequesterName { get; set; } = string.Empty;
        public string RequesterEmail { get; set; } = string.Empty;
        public string SystemName { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public string ManagerEmail { get; set; } = string.Empty;

        // Change Detail
        public string ChangeType { get; set; } = string.Empty; // fix-ui | add-field | fix-logic | bug | add-report | other
        public string Location { get; set; } = string.Empty;
        public string AsIs { get; set; } = string.Empty;
        public string ToBe { get; set; } = string.Empty;
        public string? ScreenShotDescription { get; set; }

        // Reference
        public string? ChangeLogRef { get; set; }
        public DateTime? ChangeLogDate { get; set; }

        // Urgency
        public string UrgencyLevel { get; set; } = "medium";
        public DateTime? DesiredDeadline { get; set; }
        public string? AffectedUsers { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<AnnotationAttachment> Attachments { get; set; } = new List<AnnotationAttachment>();
    }

    public class AnnotationAttachment
    {
        public int Id { get; set; }
        public int AnnotationId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public Annotation Annotation { get; set; } = null!;   
    }
}