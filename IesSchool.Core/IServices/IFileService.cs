using IesSchool.Core.Dto;
using Microsoft.AspNetCore.Http;

namespace IesSchool.Core.IServices
{
    public interface IFileService
    {
        //for upload file locally
        public FileDto SaveBinary(string imageName, byte[] binary);
        public FileDto UploadFile(IFormFile files);
    }
}
