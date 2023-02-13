using CoinGecko.Api.Common;

using FluentAssertions;

namespace CoinGecko.Api.Tests.Common;

public class QueryBuilderTests
{
    private const string HostStringUri = "https://test.com";
    private readonly Uri _hostUri = new("https://test.com");

    [Fact]
    public void CreateWithUri_ReturnsNewQueryImplementationOfTypeIQueryPathBuilder()
    {
        // Arrange
        // Act
        var result = QueryBuilder.Create(_hostUri);
        
        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public void CreateWithStringUri_ReturnsNewQueryImplementationOfTypeIQueryPathBuilder()
    {
        // Arrange
        // Act
        var result = QueryBuilder.Create(HostStringUri);
        
        // Assert
        result.Should().NotBeNull();
    }
    
    [Fact]
    public void AddPath_AddsPathToTheUri()
    {
        // Arrange
        const string path = "path/test";
        
        // Act
        var result = QueryBuilder.Create(_hostUri).AddPath(path).Build();
        
        // Assert
        result.AbsolutePath.Should().Be($"/{path}");
        result.LocalPath.Should().Be($"/{path}");
    }
    
    [Fact]
    public void AddAddQueryParameters_AddsQueryParametersToTheUri()
    {
        // Arrange
        const string path = "path/test";
        var queryParameters = new Dictionary<string, string>
        {
            {"key1", "value1"},
            {"key2", "value2"}
        };
        
        // Act
        var result = QueryBuilder.Create(_hostUri).AddPath(path).AddQueryParameters(queryParameters).Build();
        
        // Assert
        result.Query.Should().Be("?key1=value1&key2=value2");
    }
    
    [Fact]
    public void Build_ReturnsUri()
    {
        // Arrange
        const string path = "path/test";
        var queryParameters = new Dictionary<string, string>
        {
            {"key1", "value1"},
            {"key2", "value2"}
        };
        
        // Act
        var result = QueryBuilder.Create(_hostUri).AddPath(path).AddQueryParameters(queryParameters).Build();
        
        // Assert
        result.Should().BeEquivalentTo(new Uri($"{HostStringUri}/{path}?key1=value1&key2=value2"));
    }
}
