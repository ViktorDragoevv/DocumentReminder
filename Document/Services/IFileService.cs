using Document.Models;

namespace Document.Services
{
    public interface IFileService
    {
        Task<Files> CreateFile(IFormFile file);
        Task<Files> UpdateFile(Guid file, Guid document);
        Task<Files> DeleteFiles(Guid file);

    }
}
