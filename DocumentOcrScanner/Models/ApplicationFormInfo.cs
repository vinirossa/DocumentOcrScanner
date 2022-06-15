using DocumentOcrScanner.Enums;

namespace DocumentOcrScanner.Models;

public class ApplicationFormInfo
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MotherName { get; set; }
    public EGender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string Nationality { get; set; }
    public string CityOrigin { get; set; }
    public string StateOrigin { get; set; }
    public string CountryOrigin { get; set; }

    public string RgNumber { get; set; }
    public string RgIssuingAgency { get; set; }
    public DateTime RgIssuanceDate { get; set; }
    public string RgIssuingState { get; set; }

    public bool DegreeIssuedAbroad { get; set; }

    public EPreApprovalStatus PreApprovalStatus { get; set; }
    public RgDocumentInfo RgDocumentInfo { get; set; }
}
