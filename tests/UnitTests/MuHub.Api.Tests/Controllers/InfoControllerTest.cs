using Microsoft.AspNetCore.Mvc;

using MuHub.Api.Controllers.V1;

namespace MuHub.Api.Tests.Controllers;

public class InfoControllerTest
{
    private readonly InfoController _infoController;

    public InfoControllerTest()
    {
        _infoController = new InfoController();
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
