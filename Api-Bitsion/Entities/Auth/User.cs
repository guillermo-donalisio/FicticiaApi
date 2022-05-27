using Microsoft.AspNetCore.Identity;

namespace Api_Bitsion.Entities.Auth;

public class User : IdentityUser
{
    public bool IsActive {set;get;}
}
