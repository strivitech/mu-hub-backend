using MuHub.Domain.Common.Entities;

namespace MuHub.Domain.Entities;

public class WatchList : BaseEntity
{
    public int CoinId { get; set; }
    
    public string UserId { get; set; }
}
