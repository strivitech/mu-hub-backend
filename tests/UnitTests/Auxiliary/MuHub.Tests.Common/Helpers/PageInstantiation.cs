﻿using Microsoft.AspNetCore.Http;
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
    /// <param name="httpCtx">Http context.</param>
    /// <typeparam name="TModel">Page type.</typeparam>
    /// <returns>Given Page of type TModel.</returns>
    public static TModel WithDefaultValues<TModel>(this TModel page, HttpContext? httpCtx = null)
        where TModel : PageModel
    {
        var httpContext = httpCtx ?? new DefaultHttpContext();
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

    public static Mock<IUrlHelper> CreateMockUrlHelper(ActionContext? context = null)
    {
        context ??= GetActionContextForPage("/Page");

        var urlHelper = new Mock<IUrlHelper>();
        urlHelper.SetupGet(h => h.ActionContext)
            .Returns(context);
        return urlHelper;
    }
    
    public static ActionContext GetActionContextForPage(string page)
    {
        return new()
        {
            ActionDescriptor = new()
            {
                RouteValues = new Dictionary<string, string>
                {
                    { "page", page },
                }!
            },
            RouteData = new()
            {
                Values =
                {
                    [ "page" ] = page
                }
            }
        };
    }
}
