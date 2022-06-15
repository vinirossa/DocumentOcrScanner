using DocumentOcrScanner.Data.Infra;
using DocumentOcrScanner.Enums;
using Google.Cloud.Firestore;

namespace DocumentOcrScanner.Models;

[FirestoreData]
public class ApplicationFormInfo : IFirebaseEntity
{
    public ApplicationFormInfo()
    {

    }

    public ApplicationFormInfo(string name, string lastName, string motherName, EGender gender, DateTime birthDate, string nationality, string cityOrigin, string stateOrigin, string countryOrigin, string rg, string rgIssuingAgency, DateTime rgIssuanceDate, string rgIssuingState, bool degreeIssuedAbroad, EPreApprovalStatus preApprovalStatus, RgDocumentInfo rgDocumentInfo)
    {
        Id = Guid.NewGuid().ToString("N");
        Name = name;
        LastName = lastName;
        MotherName = motherName;
        Gender = gender;
        BirthDate = birthDate;
        Nationality = nationality;
        CityOrigin = cityOrigin;
        StateOrigin = stateOrigin;
        CountryOrigin = countryOrigin;
        Rg = rg;
        RgIssuingAgency = rgIssuingAgency;
        RgIssuanceDate = rgIssuanceDate;
        RgIssuingState = rgIssuingState;
        DegreeIssuedAbroad = degreeIssuedAbroad;
        PreApprovalStatus = preApprovalStatus;
        RgDocumentInfo = rgDocumentInfo;
    }

    [FirestoreProperty]
    public string Id { get; set; }

    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string LastName { get; set; }

    [FirestoreProperty]
    public string MotherName { get; set; }

    [FirestoreProperty]
    public EGender Gender { get; set; }

    [FirestoreProperty]
    public DateTime BirthDate { get; set; }

    [FirestoreProperty]
    public string Nationality { get; set; }

    [FirestoreProperty]
    public string CityOrigin { get; set; }

    [FirestoreProperty]
    public string StateOrigin { get; set; }

    [FirestoreProperty]
    public string CountryOrigin { get; set; }

    [FirestoreProperty]
    public string Rg { get; set; }

    [FirestoreProperty]
    public string RgIssuingAgency { get; set; }

    [FirestoreProperty]
    public DateTime RgIssuanceDate { get; set; }

    [FirestoreProperty]
    public string RgIssuingState { get; set; }

    [FirestoreProperty]
    public bool DegreeIssuedAbroad { get; set; }

    [FirestoreProperty]
    public EPreApprovalStatus PreApprovalStatus { get; set; }

    public RgDocumentInfo RgDocumentInfo { get; set; }
}
