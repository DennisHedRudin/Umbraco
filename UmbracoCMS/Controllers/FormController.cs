using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoCMS.Services;
using UmbracoCMS.ViewModels;

namespace UmbracoCMS.Controllers;

public class FormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, FormSubmissionService formSubmissions) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
{
    private readonly FormSubmissionService _formSubmissions = formSubmissions;


public IActionResult HandleCallbackForm(CallbackFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = _formSubmissions.SaveCallbackRequest(model);
        if(!result) 
        {
            TempData["FormError"] = "Something went wrong while submitting your request. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }

        TempData["FormSuccess"] = "Thank you! Your request has been received and we'll get back to you soon.";

        return RedirectToCurrentUmbracoPage();
    }

    public IActionResult HandleQuestionForm(QuestionFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = _formSubmissions.SaveQuestionRequest(model);
        if (!result)
        {
            TempData["QuestionFormError"] = "Something went wrong while submitting your question. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }

        TempData["QuestionFormSuccess"] = "Thank you! Your question has been received and we'll get back to you soon.";

        return RedirectToCurrentUmbracoPage();

    }

    public IActionResult HandleSupportForm(SupportFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return CurrentUmbracoPage();
        }

        var result = _formSubmissions.SaveSupportRequest(model);
        if (!result)
        {
            TempData["SupportFormError"] = "Something went wrong while submitting your email. Please try again later.";
            return RedirectToCurrentUmbracoPage();
        }

        TempData["SupportFormSuccess"] = "Thank you! We'll get back to you soon.";

        return RedirectToCurrentUmbracoPage();

    }
}
