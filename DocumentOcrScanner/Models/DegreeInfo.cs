namespace DocumentOcrScanner.Models;

public class DegreeInfo
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Rg { get; set; }

    public DateTime DegreeIssuanceDate { get; set; }
    public bool DegreeIssuedAbroad { get; set; }

    public string WordCloud { get; set; }
}
