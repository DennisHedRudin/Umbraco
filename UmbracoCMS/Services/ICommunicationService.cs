using UmbracoCMS.ViewModels;

namespace UmbracoCMS.Services
{
    public interface ICommunicationService
    {
        Task<EmailResult> SendCallbackEmailAsync(CallbackFormViewModel model);
        Task<EmailResult> SendQuestionEmailAsync(QuestionFormViewModel model);
        Task<EmailResult> SendSupportEmailAsync(SupportFormViewModel model);
    }
}