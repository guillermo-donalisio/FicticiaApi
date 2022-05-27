namespace Api_Bitsion.Core.Models.Clients;

public class ClientGetModelDTO
{
    public int ID {set;get;}
    public bool IsActive {set;get;}
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string FullName {set;get;}
    public int Age {set;get;}
    public string Gender {set;get;}
    public bool WearGlasses {set;get;}
    public bool IsDiabetic {set;get;}
    public bool IsSick {set;get;}
    public string Illness {set;get;}
}
