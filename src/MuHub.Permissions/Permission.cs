using System.ComponentModel.DataAnnotations;

namespace MuHub.Permissions;

/// <summary>
/// Contains all permission names.
/// </summary>
public enum Permission
{
    // [Display(GroupName = "Info", Name = "Read", Description = "Allowed to read Info")]
    // InfoRead = 1,
    //
    // [Display(GroupName = "Info", Name = "Create", Description = "Allowed to create Info")]
    // InfoCreate = 2,
    //
    // [Display(GroupName = "Info", Name = "Update", Description = "Allowed to update Info")]
    // InfoUpdate = 3,
    //
    // [Display(GroupName = "Info", Name = "Delete", Description = "Allowed to delete Info")]
    // InfoDelete = 4,
    
    [Display(GroupName = "WatchList", Name = "Read", Description = "Allowed to read WatchList")]
    WatchListRead = 1,
    
    [Display(GroupName = "WatchList", Name = "Create", Description = "Allowed to create WatchList")]
    WatchListCreate = 2,
    
    [Display(GroupName = "WatchList", Name = "Update", Description = "Allowed to update WatchList")]
    WatchListUpdate = 3,
    
    [Display(GroupName = "WatchList", Name = "Delete", Description = "Allowed to delete WatchList")]
    WatchListDelete = 4,
}
