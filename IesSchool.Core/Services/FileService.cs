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
        public FileDto SaveBinary(string imageName,byte[] binary)
        {
            try
            {
                if (imageName != null && binary.Count() > 0)
                {
                    var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageName);

                    var target = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwRoot/tempFiles");
                    string fullpath = "";
                    if (!Directory.Exists(target))
                    {
                        Directory.CreateDirectory(target);
                    }
                    if (binary.Count() > 0)
                    {
                        System.IO.File.WriteAllBytes("wwwRoot/tempFiles/" + fileName, binary);

                        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                        fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{fileName}";
                    }

                    return new FileDto { FileName = fileName, virtualPath = fullpath, UploadedOn = DateTime.Now };
                }
                else
                    return new FileDto { };
            }
            catch (Exception)
            {

                throw;
            }

        }

        //save image from front end to temp file
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
            try
            {
                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    file.CopyToAsync(stream);
                //    stream.Close();
                //}

                System.IO.File.Copy(file.FileName, filePath, true);

            }
            catch (Exception ex)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
            }
            if (File.Exists("wwwRoot/tempFiles/" + fileName))
            {
                Console.WriteLine("Excist");
            }
            else
            {
                Console.WriteLine("notExcist");

            }
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{fileName}";

            return new FileDto { FileName = fileName, virtualPath = fullpath, UploadedOn = DateTime.Now };
        }
        #endregion
    }
}
