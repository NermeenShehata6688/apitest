using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace IesSchool.Core.Services
{
    public class FileService : IFileService
    {
        #region Property  
        private IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor  
        public FileService(IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region Upload File  
        public FileDto UploadFile(IFormFile file)
        {
            var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwRoot/tempFiles");

            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }
            if (file.Length <= 0) return new FileDto { };
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(target, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                 file.CopyToAsync(stream);
            }
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{fileName}";

            return new FileDto { FileName = fileName, virtualPath = fullpath, UploadedOn=DateTime.Now };
        }
        #endregion
    }
}
