using Document.Models;

namespace Document.Repositories
{
    public interface INotifyRepository: IRepository<NotifyModel>
    {
        Task<NotifyModel> GetAllNotifyWithViewByID(Guid id);
    }
}
