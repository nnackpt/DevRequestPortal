using System.ComponentModel.DataAnnotations;

namespace DevRequestPortal.ViewModels.Request
{
    public class SubmitAnnotationRequest
    {
        // General
        [Required] public string RequesterName { get; set; } = string.Empty;
        [Required, EmailAddress] public string RequesterEmail { get; set; } = string.Empty;
        [Required] public string SystemName { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; } = DateTime.Today;
        [Required] public string ManagerName { get; set; } = string.Empty;
        [Required, EmailAddress] public string ManagerEmail { get; set; } = string.Empty;

        // Change Detail
        [Required] public string ChangeType { get; set; } = string.Empty; // fix-ui | add-field | fix-logic | bug | add-report | other
        [Required] public string Location { get; set; } = string.Empty;
        [Required] public string AsIs { get; set; } = string.Empty;
        [Required] public string ToBe { get; set; } = string.Empty;
        public string? ScreenshotDescription { get; set; }

        // Reference
        public string? ChangeLogRef { get; set; }
        public DateTime? ChangeLogDate { get; set; }

        // Urgency
        [Required] public string UrgencyLevel { get; set; } = "medium"; // low | medium | high
        public DateTime? DesiredDeadline { get; set; }
        public string? AffectedUsers { get; set; }
    }
}