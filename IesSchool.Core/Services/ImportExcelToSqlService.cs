using ExcelDataReader;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class ImportExcelToSqlService : IImportExcelToSqlService
    {
        private readonly IUnitOfWork _uow;

        public ImportExcelToSqlService(IUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }
        public ResponseDto ImportStrandsExcel(IFormFile file)
        {
            try
            {
                if (file != null && (Path.GetExtension(file.FileName) == ".xlsx" || Path.GetExtension(file.FileName) == ".csv"))
                {

                    List<Strand> strandsToImport = new List<Strand>();
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    using (var stream = file.OpenReadStream())
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            while (reader.Read()) //Each row of the file
                            {
                                while (reader.Depth >= 0 && reader.Read()) //to skip header
                                {
                                    if (reader.GetValue(0) != null)
                                    {
                                        strandsToImport.Add(new Strand
                                        {
                                            Name = reader.GetValue(0) == null ? null : reader.GetValue(0).ToString(),
                                            NameAr = reader.GetValue(1) == null ? null : reader.GetValue(1).ToString(),
                                            AreaId = reader.GetValue(2) == null ? null : int.Parse(reader.GetValue(2).ToString()),
                                            DisplayOrder = reader.GetValue(3) == null ? null : int.Parse(reader.GetValue(3).ToString()),
                                            Code = reader.GetValue(4) == null ? null : int.Parse(reader.GetValue(4).ToString())
                                        });
                                    }
                                }
                            }
                        }
                    }
                    _uow.GetRepository<Strand>().Add(strandsToImport);
                    _uow.SaveChanges();

                }


                return new ResponseDto { Status = 1, Message = " Country Added  Seccessfuly" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }

        }
    }
}
