using Microsoft.AspNetCore.Mvc;

using Moq;

using MuHub.Api.Controllers.V1;
using MuHub.Application.Services.Interfaces;

namespace MuHub.Api.Tests.Controllers;

public class InfoControllerTest
{
    private readonly InfoController _infoController;

    public InfoControllerTest()
    {
        var infoService = new Mock<IInfoService>();
        _infoController = new InfoController(infoService.Object);
    }

    [Fact]
    public void Index_Test()
    {
        // Act
        var result = _infoController.Index();

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}

