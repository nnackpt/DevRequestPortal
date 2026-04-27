using DevRequestPortal.Services.Interfaces;
using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;

namespace DevRequestPortal.Services
{
    public class ScreeningService : IScreeningService
    {
        private static readonly string[] HighLabels =
        {
            "20+ users — Server-side management required",
            "Real-time concurrent editing — Excel has conflict issues",
            "Multi-step workflow — App needed to manage logic",
            "Audit Trail required — most No-Code tools lack native logging",
            "Mobile-first — Excel and some No-Code tools are not suitable"
        };
        private static readonly string[] MidLabels =
        {
            "4–20 users — consider Shared Access",
            "Occasional data sharing — Version conflicts possible",
            "Some approvals — use tools with Workflow support",
            "Audit Trail will be useful in the future",
            "Occasional mobile use — choose a Responsive tool"
        };

        public ScreeningResponse Evaluate(ScreeningRequest request)
        {
            int score = request.Answers.Sum();

            var reasons = request.Answers
                .Select((ans, i) => ans switch
                {
                    2 => new ScreeningReason
                    {
                        Type = "high",
                        Text = HighLabels[i]
                    },
                    1 => new ScreeningReason
                    {
                        Type = "mid",
                        Text = MidLabels[i]
                    },
                    _ => null
                })
                .Where(r => r != null)
                .Cast<ScreeningReason>()
                .ToList();

            return score switch
            {
                <= 2 => Build(score, "excel", "Microsoft Forms + Excel should be enough",
                            "Low complexity — use existing Microsoft 365 tools.",
                            "Small Project", "Same-day to 1 month", reasons),
                <= 4 => Build(score, "nocode", "Use your existing Microsoft 365 tools",
                            "SharePoint Lists or Forms + Teams can handle this.",
                            "Small-Medium Project", "Days (No-Code) or 1-2 months (App)", reasons),
                <= 7 => Build(score, "consult", "Consult the Developer for 15 minutes first",
                            "May work with Microsoft 365, or may need a custom app.",
                            "Medium Project", "1-3 months", reasons),
                _ => Build(score, "app", "A Custom App is truly needed",
                            "High complexity — Microsoft 365 is not sufficient.",
                            "Large Project", "2+ months", reasons)
            };
        }

        private static ScreeningResponse Build(int score, string rec, string title, 
            string body, string size, string timeline, List<ScreeningReason> reasons) => new()
        {
            TotalScore = score,
            Recommendation = rec,
            RecommendationTitle = title,
            RecommendationBody = body,
            ProjectSize = size,
            EstimatedTimeline = timeline,
            Reasons = reasons
        };
    }
}