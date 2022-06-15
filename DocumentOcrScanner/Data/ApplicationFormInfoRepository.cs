using DocumentOcrScanner.Data.Infra;
using DocumentOcrScanner.Models;

namespace DocumentOcrScanner.Data;

public class ApplicationFormInfoRepository : IApplicationFormInfoRepository
{
    private readonly FirestoreProvider _firestoreProvider;

    public ApplicationFormInfoRepository(FirestoreProvider firestoreProvider)
    {
        _firestoreProvider = firestoreProvider;
    }

    public async Task<ApplicationFormInfo> Get(string id)
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        return await _firestoreProvider.Get<ApplicationFormInfo>(id, cts.Token);
    }

    public async Task<IEnumerable<ApplicationFormInfo>> GetList()
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        var all = await _firestoreProvider.GetAll<ApplicationFormInfo>(cts.Token);

        return all;
    }

    public async Task Insert(ApplicationFormInfo model)
    {
        CancellationTokenSource cts = new CancellationTokenSource();

        await _firestoreProvider.AddOrUpdate(model, cts.Token);
    }
}
