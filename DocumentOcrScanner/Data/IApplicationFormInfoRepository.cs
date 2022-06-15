using DocumentOcrScanner.Models;

namespace DocumentOcrScanner.Data;

public interface IApplicationFormInfoRepository
{
    Task Insert(ApplicationFormInfo model);
    Task<ApplicationFormInfo> Get(string rg);
    Task<IEnumerable<ApplicationFormInfo>> GetList();
}
