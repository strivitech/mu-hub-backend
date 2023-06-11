namespace MuHub.Application.Models.Data.Info;

public class UpdateInfoRequest
{
    public int Id { get; set; }
    
    public string Subject { get; set; } = null!;
    
    public string Text { get; set; } = null!;
}
