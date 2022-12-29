using Azure;

namespace Document.NewFolder1
{
    public interface ISendMailServices
    {
        Task<ResponseVM> SendEmail();
    }
}
