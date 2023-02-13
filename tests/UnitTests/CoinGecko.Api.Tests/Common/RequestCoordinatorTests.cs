using System.Net;

using CoinGecko.Api.Common;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;
using Moq.Protected;

namespace CoinGecko.Api.Tests.Common;

public class RequestCoordinatorTests
{
    private readonly Mock<HttpClient> _httpClient;
    private readonly Mock<ILogger<RequestCoordinator>> _logger;
    private readonly Mock<HttpMessageHandler> _httpMessageHandler;
    private readonly RequestCoordinator _requestCoordinator;
    private const string ValidUriString = "https://test.com";
    private readonly Uri _validUri = new("https://test.com");

    public RequestCoordinatorTests()
    {
        _httpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new Mock<HttpClient>(_httpMessageHandler.Object);
        _logger = new Mock<ILogger<RequestCoordinator>>();
        _requestCoordinator = new RequestCoordinator(_httpClient.Object, _logger.Object);
    }

    [Fact]
    public async Task GetAsyncWithUri_WhenResponseReceived_ReturnsResponse()
    {
        // Arrange
        var response =
            new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("successful") };
        MockHttpClientSendAsync(response);
        
        // Act
        var result = await _requestCoordinator.GetAsync(_validUri);

        // Assert
        result.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task GetAsyncWithStringUri_WhenResponseReceived_ReturnsResponse()
    {
        // Arrange
        var response =
            new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent("successful") };
        MockHttpClientSendAsync(response);
        
        // Act
        var result = await _requestCoordinator.GetAsync(ValidUriString);

        // Assert
        result.Should().BeEquivalentTo(response);
    }
    
    [Fact]
    public async Task GetAsyncWithUri_WhenHttpRequestException_ThrowsHttpRequestException()
    {
        // Arrange
        _httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                nameof(HttpClient.SendAsync),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException());
        
        // Act
        var act = async () => await _requestCoordinator.GetAsync(_validUri);

        // Assert
        await act.Should().ThrowAsync<HttpRequestException>();
    }
    
    [Fact]
    public async Task GetAsyncWithStringUri_WhenHttpRequestException_ThrowsHttpRequestException()
    {
        // Arrange
        _httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                nameof(HttpClient.SendAsync),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException());
        
        // Act
        var act = async () => await _requestCoordinator.GetAsync(ValidUriString);

        // Assert
        await act.Should().ThrowAsync<HttpRequestException>();
    }
    
    [Fact]
    public async Task GetAsyncWithUri_WhenException_ThrowsException()
    {
        // Arrange
        _httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                nameof(HttpClient.SendAsync),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new Exception());
        
        // Act
        var act = async () => await _requestCoordinator.GetAsync(_validUri);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
    
    [Fact]
    public async Task GetAsyncWithStringUri_WhenException_ThrowsException()
    {
        // Arrange
        _httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                nameof(HttpClient.SendAsync),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new Exception());
        
        // Act
        var act = async () => await _requestCoordinator.GetAsync(ValidUriString);

        // Assert
        await act.Should().ThrowAsync<Exception>();
    }
    
    private void MockHttpClientSendAsync(HttpResponseMessage valueFunction)
    {
        _httpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                nameof(HttpClient.SendAsync),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(valueFunction);
    }
}
