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
        public ResponseDto ImportAreasExcel(IFormFile file)
        {
            try
            {
                if (file != null && (Path.GetExtension(file.FileName) == ".xlsx" || Path.GetExtension(file.FileName) == ".csv"))
                {
                    List<Area> areasToImport = new List<Area>();
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
                                        areasToImport.Add(new Area
                                        {
                                            Id = reader.GetValue(0) == null ? 0 : int.Parse(reader.GetValue(0).ToString()),
                                            Name = reader.GetValue(1) == null ? null : reader.GetValue(1).ToString(),
                                            Code = reader.GetValue(2) == null ? null : int.Parse(reader.GetValue(2).ToString()),
                                            ProgramId = reader.GetValue(2) == null ? null : int.Parse(reader.GetValue(3).ToString()),
                                        });
                                    }
                                }
                            }
                        }
                    }
                    _uow.GetRepository<Area>().Add(areasToImport);
                    _uow.SaveChanges();
                }
                else
                {
                    return new ResponseDto { Status =0, Message = "File is not in the Correct Format" };
                }
                return new ResponseDto { Status = 1, Message = "Area Updated Successfuly. Check Your Areas now" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }

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
                                            Id = reader.GetValue(0) == null ? 0 : int.Parse(reader.GetValue(0).ToString()),
                                            AreaId = reader.GetValue(1) == null ? null : int.Parse(reader.GetValue(1).ToString()),
                                            Name = reader.GetValue(2) == null ? null : reader.GetValue(2).ToString(),
                                            Code = reader.GetValue(3) == null ? null : int.Parse(reader.GetValue(3).ToString())
                                        });
                                    }
                                }
                            }
                        }
                    }
                    _uow.GetRepository<Strand>().Add(strandsToImport);
                    _uow.SaveChanges();

                }
                else
                {
                    return new ResponseDto { Status = 0, Message = "File is not in the Correct Format" };
                }
                return new ResponseDto { Status = 1, Message = "Strands Updated Successfuly. Check Your Strands now" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }

        }
        public ResponseDto ImportSkillsExcel(IFormFile file)
        {
            try
            {
                if (file != null && (Path.GetExtension(file.FileName) == ".xlsx" || Path.GetExtension(file.FileName) == ".csv"))
                {
                    List<Skill> skillsToImport = new List<Skill>();
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
                                        skillsToImport.Add(new Skill
                                        {
                                            Id = reader.GetValue(0) == null ? 0 : int.Parse(reader.GetValue(0).ToString()),
                                            Name = reader.GetValue(1) == null ? null : reader.GetValue(1).ToString(),
                                            StrandId = reader.GetValue(2) == null ? null : int.Parse(reader.GetValue(2).ToString()),
                                            SkillNumber = reader.GetValue(3) == null ? null : int.Parse(reader.GetValue(3).ToString()),
                                        });
                                    }
                                }
                            }
                        }
                    }
                    _uow.GetRepository<Skill>().Add(skillsToImport);
                    _uow.SaveChanges();

                }
                else
                {
                    return new ResponseDto { Status = 0, Message = "File is not in the Correct Format" };
                }
                return new ResponseDto { Status = 1, Message = "Skils Updated Successfuly. Check Your Skills now" };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = ex.Message, Data = ex };
            }

        }

    }
}
