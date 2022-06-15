namespace DocumentOcrScanner.Dtos;

public class IdentificationDocumentDto
{
    public string FullName { get; set; }
    public string MotherFullName { get; set; }

    public string Gender { get; set; }
    public string Nationality { get; set; }


    public string BirthDate { get; set; }
    public string BirthCity { get; set; }
    public string BirthState { get; set; }
    public string BirthCountry { get; set; }

    public string Rg { get; set; }
    public string RgIssuingAgency { get; set; }
    public string RgIssuanceDate { get; set; }
    public string RgIssuingState { get; set; }

    public string Cpf { get; set; }

    public string DegreeIssuedAbroad { get; set; }
}
