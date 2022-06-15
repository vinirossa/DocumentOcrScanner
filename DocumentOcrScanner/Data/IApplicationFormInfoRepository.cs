using DocumentOcrScanner.Models;

namespace DocumentOcrScanner.Data;

public interface IApplicationFormInfoRepository
{
    Task Insert(ApplicationFormInfo model);
}
