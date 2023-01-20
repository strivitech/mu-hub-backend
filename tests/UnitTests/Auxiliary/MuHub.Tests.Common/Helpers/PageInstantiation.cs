using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Moq;

namespace MuHub.Tests.Common.Helpers;

/// <summary>
/// Contains helper methods for unit testing Razor Pages. Used for page instantiation and model initialization.
/// </summary>
public static class PageInstantiation
{
    /// <summary>
    /// Initializes a given page with default values for correct work.
    /// </summary>
    /// <param name="page">Page.</param>
    /// <typeparam name="TModel">Page type.</typeparam>
    /// <returns>Given Page of type TModel.</returns>
    public static TModel WithDefaultValues<TModel>(this TModel page)
        where TModel : PageModel
    {
        var httpContext = new DefaultHttpContext();
        var modelState = new ModelStateDictionary();
        var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
        var modelMetadataProvider = new EmptyModelMetadataProvider();
        var viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
        var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
        var pageContext = new PageContext(actionContext)
        {
            ViewData = viewData
        };

        page.PageContext = pageContext;
        page.TempData = tempData;
        page.Url = new UrlHelper(actionContext);

        return page;
    }
}
