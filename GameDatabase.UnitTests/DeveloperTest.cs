using GameDatabase.Domain.AggregatesModel.GameAggregate;

namespace GameDatabase.UnitTests;

public class DeveloperTest
{
    [Fact]
    public void CreateDeveloperInstance_Success()
    {
        // Arrange
        string name = "Test Developer";
        DateTime founded = DateTime.Now.AddYears(-2);
        string webSite = "http://site.com";

        // Act
        var developer = Developer.Factory(name, founded, webSite);

        // Assert
        Assert.Equal(name, developer.Name);
        Assert.Equal(founded, developer.Founded);
        Assert.Equal(webSite, developer.WebSite);
    }
    
    [Fact]
    public void ChangeWebSite_WhenNewWebSiteProvided_Success()
    {
        // Arrange
        var developer = Developer.Factory("Test Developer", DateTime.Now, "www.currentwebsite.com");
        string newWebSite = "www.newwebsite.com";

        // Act
        bool result = developer.ChangeWebSite(newWebSite);

        // Assert
        Assert.True(result, "Changing website failed.");
        Assert.Equal(newWebSite, developer.WebSite);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ChangeWebSite_WhenNewWebSiteNullOrEmpty_Fail(string newWebsite)
    {
        // Arrange
        var developer = Developer.Factory("Test Developer", DateTime.Now, "www.currentwebsite.com");

        // Act
        bool result = developer.ChangeWebSite(newWebsite);

        // Assert
        Assert.False(result, "Website invalid.");
    }
}