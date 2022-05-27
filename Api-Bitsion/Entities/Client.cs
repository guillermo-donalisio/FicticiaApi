using System.ComponentModel.DataAnnotations;

namespace Api_Bitsion.Entities;

public class Client : BaseEntity
{
    public string FullName {set;get;}
    public int Age {set;get;}
    public string Gender {set;get;}
    public bool WearGlasses {set;get;}
    public bool IsDiabetic {set;get;}
    public bool IsSick {set;get;}
    public string Illness {set;get;}
}
