namespace DocumentOcrScanner.Models;

public class ApplicationFormInfo
{
    public ApplicationFormInfo(string name, string lastName, DateTime birthDate, 
        string motherName, string gender, string nationality, string countryOrigin, 
        string stateOrigin, string cityOrigin, string identityCard, string documentDispatcher, 
        DateTime documentShippingDate, string documentStateDispatcher, bool degreeOutsideBrazil)
    {
        Name = name;
        LastName = lastName;
        BirthDate = birthDate;
        MotherName = motherName;
        Gender = gender;
        Nationality = nationality;
        CountryOrigin = countryOrigin;
        StateOrigin = stateOrigin;
        CityOrigin = cityOrigin;
        IdentityCard = identityCard;
        DocumentDispatcher = documentDispatcher;
        DocumentShippingDate = documentShippingDate;
        DocumentStateDispatcher = documentStateDispatcher;
        DegreeOutsideBrazil = degreeOutsideBrazil;
    }

    public string Name { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string MotherName { get; set; }
    public string Gender { get; set; }
    public string Nationality { get; set; }
    public string CountryOrigin { get; set; }
    public string StateOrigin { get; set; }
    public string CityOrigin { get; set; }
    public string IdentityCard { get; set; }
    public string DocumentDispatcher { get; set; }
    public DateTime DocumentShippingDate { get; set; }
    public string DocumentStateDispatcher { get; set; }
    public bool DegreeOutsideBrazil { get; set; }
}
