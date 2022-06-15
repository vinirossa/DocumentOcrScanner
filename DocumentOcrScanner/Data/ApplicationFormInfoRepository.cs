using DocumentOcrScanner.Models;

namespace DocumentOcrScanner.Data;

public class ApplicationFormInfoRepository : IApplicationFormInfoRepository
{
    public Task<ApplicationFormInfo> Get(string rg)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationFormInfo>> GetList()
    {
        throw new NotImplementedException();
    }

    public Task Insert(ApplicationFormInfo model)
    {
        throw new NotImplementedException();
    }
}
