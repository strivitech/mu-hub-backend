namespace MuHub.Application.Models.Requests.Info;

public class CreateInfoRequest
{
    public string Subject { get; set; } = null!;
    
    public string Text { get; set; } = null!;
}
