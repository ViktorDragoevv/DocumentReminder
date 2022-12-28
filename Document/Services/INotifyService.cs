using Document.Models;

namespace Document.Services
{
    public interface INotifyService
    {
        Task<ViewNotify> CreateNotify(CreateUpdateNotify notify);
        Task<ViewNotify> UpdateNotifyByID(CreateUpdateNotify notify, Guid id);
    }
}
