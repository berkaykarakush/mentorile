// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Mentorile.IdentityServer.Enums;
using Microsoft.AspNetCore.Identity;

namespace Mentorile.IdentityServer.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public UserStatus Status { get; set; }

    public void Active() => Status = UserStatus.Active;
    public void ConfirmEmail() => EmailConfirmed = true;

}
