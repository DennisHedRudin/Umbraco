using Umbraco.Cms.Core.Services;
using UmbracoCMS.ViewModels;

namespace UmbracoCMS.Services;

public class FormSubmissionService(IContentService contentService, ICommunicationService communicationService)
{
    private readonly IContentService _contentService = contentService;
    private readonly ICommunicationService _communicationService = communicationService;

    public async Task<bool> SaveCallbackRequest (CallbackFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "callbackRequest");

            request.SetValue("callbackRequestName", model.Name);
            request.SetValue("callbackRequestEmail", model.Email);
            request.SetValue("callbackRequestPhone", model.Phone);
            request.SetValue("callbackRequestOption", model.SelectedOption);

            var saveResult = _contentService.Save(request);
            if (!saveResult.Success) return false;

            await _communicationService.SendCallbackEmailAsync(model);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }


    }

    public async Task<bool> SaveQuestionRequest (QuestionFormViewModel model)
    {
       

        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "questionRequest");

            request.SetValue("questionRequestName", model.Name);
            request.SetValue("questionRequestEmail", model.QuestionEmail);
            request.SetValue("questionRequestQuestion", model.Question);


            var saveResult = _contentService.Save(request);
            if (!saveResult.Success) return false;
            
            await _communicationService.SendQuestionEmailAsync(model);

            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> SaveSupportRequest(SupportFormViewModel model)
    {


        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.SupportEmail}";
            var request = _contentService.Create(requestName, container, "supportRequest");
            
            request.SetValue("supportRequestEmail", model.SupportEmail);



            var saveResult = _contentService.Save(request);
            if (!saveResult.Success) return false;

            
            await _communicationService.SendSupportEmailAsync(model);

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}
