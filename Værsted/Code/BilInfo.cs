namespace Værsted.Code;

class BilInfo
{
    // her er Attributterne der repræsenterer bilens info

    public string? Nummerplade { get; set; }
    public KundeInfo? BilKunde { get; set; } 
    public string? Mærke { get; set; }
    public string? Model { get; set; }
    public DateTime FørsteRegistrering { get; set; }
    public float? MotorStørrelse { get; set; } 
    public DateTime SidsteSyn { get; set; }
}
