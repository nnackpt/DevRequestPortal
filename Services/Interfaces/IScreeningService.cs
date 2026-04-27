using DevRequestPortal.ViewModels.Request;
using DevRequestPortal.ViewModels.Response;

namespace DevRequestPortal.Services.Interfaces
{
    public interface IScreeningService
    {
        ScreeningResponse Evaluate(ScreeningRequest request);
    }
}