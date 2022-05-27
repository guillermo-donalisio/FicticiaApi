using System.ComponentModel.DataAnnotations;

namespace Api_Bitsion.Core.Models.Auth;

public class UserLoginRequestModelDTO
{
    [Required]
	[MinLength(6)]
	public string Username {set;get;}
    
	[Required]
	[MinLength(6)]
	public string Password {set;get;}
}
