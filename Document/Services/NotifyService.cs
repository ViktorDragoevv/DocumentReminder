using Document.Models;
using Document.Repositories;
using System.Drawing.Text;

namespace Document.Services
{
    public class NotifyService : INotifyService
    {
        private readonly INotifyRepository _notifyRepository;

        public NotifyService(INotifyRepository notifyRepository)
        {
            _notifyRepository = notifyRepository;
        }

        public async Task<ViewNotify> CreateNotify(CreateUpdateNotify notify)
        {
            var notifyEntity = notify.ToEntity(Guid.NewGuid());
            var newNotify = await _notifyRepository.AddAsync(notifyEntity);
            //var createdDocument = await _notifyRepository.GetAllNotifyWithViewByID(newNotify.ID);
            return newNotify.ToModel();
        }

        public async Task<ViewNotify> UpdateNotifyByID(CreateUpdateNotify notify, Guid id)
        {
            var Viewnotify = new ViewNotify();
            if (id == null || id == Guid.Empty)
            {
                var notifyEntity = notify.ToEntity(Guid.NewGuid());
                var newNotify = await _notifyRepository.AddAsync(notifyEntity);
                Viewnotify = newNotify.ToModel();
            }
            else
            {
                var existingNotify = await _notifyRepository.GetByIdAsync(id);
                existingNotify.Copy(notify);
                var notifyModel = await _notifyRepository.UpdateAsync(existingNotify);
                //var updatednotify = await _notifyRepository.GetAllNotifyWithViewByID(id);
                //var toMNodel = updatednotify.ToModel();
                Viewnotify = notifyModel.ToModel();
            }
            return Viewnotify;
            //if (existingNotify == null) { throw new ArgumentNullException("notify not exsist", nameof(CreateUpdateNotify)); }

        }
    }
}
