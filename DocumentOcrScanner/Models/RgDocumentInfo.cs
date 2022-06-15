using Google.Cloud.Firestore;

namespace DocumentOcrScanner.Models;

public class RgDocumentInfo
{
    [FirestoreProperty]
    public string Name { get; set; }

    [FirestoreProperty]
    public string LastName { get; set; }

    [FirestoreProperty]
    public string MotherName { get; set; }

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
    public string RgNumber { get; set; }

    [FirestoreProperty]
    public string RgIssuingAgency { get; set; }

    [FirestoreProperty]
    public DateTime RgIssuingDate { get; set; }

    [FirestoreProperty]
    public string RgIssuingState { get; set; }

    [FirestoreProperty]
    public string WordCloud { get; set; }
}
