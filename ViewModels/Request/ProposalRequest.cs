using System.ComponentModel.DataAnnotations;

namespace DevRequestPortal.ViewModels.Request
{
    public class SubmitProposalRequest
    {
        public ScreeningResultDto? Screening { get; set; }

        [Required] public RequesterInfoDto RequesterInfo { get; set; } = new();
        [Required] public ProblemGoalsDto ProblemGoals { get; set; } = new();

        [Required] public FeaturesWorkflowDto featuresWorkflow { get; set; } = new();
        [Required] public ScreensDataDto ScreensData { get; set; } = new();
        [Required] public TimelineSubmitDto Timeline { get; set; } = new();
    }

    public class ScreeningResultDto
    {
        public string Category { get; set; } = string.Empty;
        public int Score { get; set; }
        public string Recommendation { get; set; } = string.Empty;
    }

    // Step 1
    public class RequesterInfoDto
    {
        [Required] public string ProjectName { get; set; } = string.Empty;
        [Required] public string Name { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string Department { get; set; } = string.Empty;
        [Required] public string ManagerName { get; set; } = string.Empty;
        [Required, EmailAddress] public string ManagerEmail { get; set; } = string.Empty;
        public DateTime SubmissionDate { get; set; } = DateTime.Today; 
    }

    // Step 2
    public class ProblemGoalsDto
    {
        [Required] public string Problem { get; set; } = string.Empty;
        [Required] public string Objective { get; set; } = string.Empty;
        [Required, MinLength(1)] public List<string> UserGroups { get; set; } = new();
        public string? Benefits { get; set; }
    }

    // Step 3
    public class FeaturesWorkflowDto
    {
        [Required, MinLength(1)] public List<string> Features { get; set; } = new();
        [Required] public string WorkflowDescription { get; set; } = string.Empty;
        public string? BusinessRules { get; set; }
    }

    // Step 4
    public class ScreensDataDto
    {
        [Required, MinLength(1)] public List<string> RequiredScreens { get; set; } = new();
        [Required, MinLength(1)] public List<DataFieldDto> DataFields { get; set; } = new();
        public string? UIDraftDescription { get; set; }
        public string? SampleDataDescription { get; set; }
    }

    public class DataFieldDto
    {
        [Required] public string FieldName { get; set; } = string.Empty;
        [Required] public string DataType { get; set; } = string.Empty; // Text | Number | Data | Dropdown
        public string? ExampleOrOptions { get; set; }
    }

    // Step 5
    public class TimelineSubmitDto
    {
        [Required] public string UrgencyLevel { get; set; } = "medium"; // low | medium | high
        [Required] public DateTime DesiredDeadline { get; set; }
        public DateTime? HardDeadline { get; set; }
        public string? DeadlineReason { get; set; }
        public List<bool> Checklist { get; set; } = new();
    }

    public class UpdateProposalStatusRequest
    {
        [Required] public string Status { get; set; } = string.Empty;
        public string? Note { get; set; }
    }
}