using Document.Models;
using Document.Repositories;
using Microsoft.AspNetCore.Components.Forms;

namespace Document.Services
{
    public class FileService: IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IFileRepository fileRepository, IWebHostEnvironment webHostEnvironment)
        {
            _fileRepository = fileRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Files> CreateFile(IFormFile file)
        {
            if (file.FileName == null || file.FileName.Length == 0)
            {
                 throw new ArgumentNullException("File not exsist", nameof(CreateUpdateFile));
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/", file.FileName);
            await using FileStream fs = new(path, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);
            /*using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Close();
            }*/
            Files files = new Files() { ImagePath = path, ImageName = file.FileName };
            var createdFile = await _fileRepository.AddAsync(files);
            return createdFile;
        }

        public async Task<Files> DeleteFiles(Guid file)
        {
            var createdFile = await _fileRepository.GetByIdAsync(file);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/", createdFile.ImageName);

            //await using FileStream fileStream = new(path, FileMode.Truncate);
            //await file.OpenReadStream().DeleteAsync(fileStream);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var deletedFile = await _fileRepository.RemoveAsync(file);
            return deletedFile;
        }

        public async Task<Files> UpdateFile(Guid file, Guid document)
        {
            var esistingFile = await _fileRepository.GetByIdAsync(file);
            if (esistingFile == null) { throw new ArgumentNullException("File not exsist", nameof(CreateUpdateFile)); }
            esistingFile.DocumentID = document;
            var updatedFile = await _fileRepository.UpdateAsync(esistingFile);
            return updatedFile;
        }
    }
}
