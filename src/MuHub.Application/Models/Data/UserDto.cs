namespace MuHub.Application.Models.Data;

public class UserDto
{
    /// <summary>
    /// Gets or sets the primary key for this user.
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Gets or sets the user name for this user.
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their email address.
    /// </summary>
    /// <value>True if the email address has been confirmed, otherwise false.</value>
    public bool EmailConfirmed { get; set; }

    /// <summary>
    /// Gets or sets a telephone number for the user.
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    /// Gets or sets a flag indicating if a user has confirmed their telephone address.
    /// </summary>
    /// <value>True if the telephone number has been confirmed, otherwise false.</value>
    public bool PhoneNumberConfirmed { get; set; }

    public bool IsBlocked { get; set; } = false;

    public DateTimeOffset? CreatedAt { get; set; }
}
