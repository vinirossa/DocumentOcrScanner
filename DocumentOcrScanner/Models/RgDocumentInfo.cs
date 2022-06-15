namespace DocumentOcrScanner.Models;

public class RgDocumentInfo
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MotherName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Nationality { get; set; }
    public string CityOrigin { get; set; }
    public string StateOrigin { get; set; }
    public string CountryOrigin { get; set; }

    public string Rg { get; set; }
    public string RgIssuingAgency { get; set; }
    public DateTime RgIssuanceDate { get; set; }
    public string RgIssuingState { get; set; }

    public string WordCloud { get; set; }
}
