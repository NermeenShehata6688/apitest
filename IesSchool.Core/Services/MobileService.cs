using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO;
using Syncfusion.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IesSchool.Core.Services
{
    internal class MobileService : IMobileService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MobileService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public bool IsParentExist(string UserName, string Password)
        {
            try
            {
                if (UserName != null && Password != null)
                {
                    // obj.Password.CompareTo(pass) == 0/string.Equals
                    var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && String.Compare( x.ParentPassword,Password)==0 );
                   // var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.Equals( Password, StringComparison.Ordinal) && x.IsSuspended != true);
                   // var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.CompareTo( Password)==0 && x.IsSuspended != true);
                    if (user != null)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ResponseDto Login(string UserName, string Password)
        {
            try
            {
                if (UserName != null && Password != null)
                {
                    // obj.Password.CompareTo(pass) == 0/string.Equals
                    //var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && String.Compare(x.ParentPassword, Password) == 0 && x.IsSuspended != true);
                     var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.Equals( Password, StringComparison.Ordinal) && x.IsActive != false);
                    // var user = _uow.GetRepository<User>().Single(x => x.ParentUserName == UserName && x.ParentPassword.CompareTo( Password)==0 && x.IsSuspended != true);
                    if (user != null)
                        return new ResponseDto { Status = 1, Message = " Seccess", Data = user };
                    else
                        return new ResponseDto { Status = 0, Message = " null" };

                }
                return new ResponseDto { Status = 0, Message = " null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };

            }
        }
        public ResponseDto GetParentById(int parentId)
        {
            try
            {
                var parent = _uow.GetRepository<User>().Single(x => x.Id == parentId && x.IsDeleted != true);
                var mapper = _mapper.Map<ParentDto>(parent);

                if (parent != null)
                {
                    parent.ImageBinary = null;
                    var parentStudents = _uow.GetRepository<Student>().GetList(x => x.ParentId == parent.Id && x.IsDeleted != true);
                    if (parentStudents.Items.Count()>0)
                    {
                        var mapperStudent = _mapper.Map<PaginateDto<StudentDto>>(parentStudents).Items;
                        if (mapperStudent.Count()>0)
                        {
                            mapper.Students = mapperStudent;
                        }
                    }
                    if (mapper.Image!=null)
                    {
                        string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                        var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{mapper.Image}";
                        mapper.FullPath = fullpath;
                    }
                }

                
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetParentStudents(int parentId)
        {
            try
            {
                var students = _uow.GetRepository<Student>().GetList((x => new Student { Id = x.Id, Name = x.Name, NameAr = x.NameAr, Code = x.Code, Image = x.Image }),x => x.ParentId == parentId && x.IsDeleted != true, null,null, 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<StudentDto>>(students);

                if (mapper.Items.Count()>0&& mapper != null)
                {
                    foreach (var item in mapper.Items)
                    {
                        if (item.Image != null)
                        {
                            item.ImageBinary = null;
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.Image}";
                            item.FullPath = fullpath;
                        }
                    }
                }
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapper };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStudentsEventsByParentId(int parentId)
        {
            try
            {
                var studentsIds = _uow.GetRepository<Student>().GetList( x => x.ParentId == parentId && x.IsDeleted != true, null, null, 0, 100000, true).Items.Select(x=> x.Id).ToArray();

                var eventStudentIds = _uow.GetRepository<EventStudent>().GetList( x => studentsIds.Contains(x.StudentId.Value == null ? 0 : x.StudentId.Value) , null, null, 0, 100000, true).Items.Select(x=> x.Id).ToArray();

                var eventStudentFiles = _uow.GetRepository<EventStudentFile>().GetList(x => eventStudentIds.Contains(x.EventStudentId.Value == null ? 0 : x.EventStudentId.Value), null,
                  x => x.Include(x => x.EventStudent).ThenInclude(x => x.Event) );

                var mapper = _mapper.Map<PaginateDto<EventStudentFileDto>>(eventStudentFiles);
                if (mapper.Items.Count() > 0 && mapper != null)
                {
                    foreach (var item in mapper.Items)
                    {
                        if (item.FileName != null)
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
                        }
                    }
                }
                var mapperData = mapper.Items.GroupBy(x => x.EventName)
                                                .OrderByDescending(x => x.Key)
                                                .Select(evt => new
                                                {
                                                    EventName = evt.Key,
                                                    EventAttachement = evt.OrderBy(x => x.EventName)
                                                });
                return new ResponseDto { Status = 1, Message = " Seccess", Data = mapperData };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetEventsImageGroubedByEventId()
        {
            try
            {
                var eventsImage = _uow.GetRepository<EventAttachement>().GetList(null, null, x=> x.Include(x=>x.Event), 0, 100000, true);
                var mapper = _mapper.Map<PaginateDto<EventAttachementDto>>(eventsImage);

                if (mapper.Items.Count > 0)
                {
                    mapper = GetFullPath(mapper);

                    var mapperData = mapper.Items.GroupBy(x => x.EventName)
                                                 .OrderByDescending(x => x.Key)
                                                 .Select(evt => new
                                                 {
                                                     EventName = evt.Key,
                                                     EventAttachement = evt.OrderBy(x => x.EventId)
                                                 });
                    return new ResponseDto { Status = 1, Message = " Seccess", Data = mapperData };
                }
                else
                    return new ResponseDto { Status = 1, Message = " null" };
            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        public ResponseDto GetStudentIepsItpsIxps(int studentId)
        {
            try
            {
                IepsItpsIxpsDto iepsItpsIxpsDto = new IepsItpsIxpsDto();
                var iep = _uow.GetRepository<Iep>().GetList(x => x.StudentId == studentId && x.IsDeleted != true && x.IsPublished == true, null, x => x.Include(x => x.Student)
                    .Include(x => x.AcadmicYear).Include(x => x.Term), 0, 100000, true);
                var iepMapper = _mapper.Map<PaginateDto<GetIepDto>>(iep).Items;

                    iepsItpsIxpsDto.Ieps = iepMapper;


                var AllItps = _uow.GetRepository<Itp>().GetList(x => x.IsDeleted != true && x.StudentId == studentId && x.IsPublished == true, null,
                   x => x.Include(s => s.Student)
                    .Include(s => s.Therapist)
                    .Include(s => s.AcadmicYear)
                    .Include(s => s.Term)
                    .Include(s => s.ParamedicalService), 0, 100000, true);
                var itpsMapper = _mapper.Map<PaginateDto<ItpDto>>(AllItps).Items;

                iepsItpsIxpsDto.Itps = itpsMapper;


                var AllIxpsx = _uow.GetRepository<Ixp>().GetList(x => x.IsDeleted != true && x.StudentId == studentId && x.IsPublished == true, null,
                   x => x.Include(s => s.Student)
                    .Include(s => s.AcadmicYear)
                    .Include(s => s.Term)
                    .Include(s => s.IxpExtraCurriculars).ThenInclude(s => s.ExtraCurricular), 0, 100000, true);
                var ixpMapper = _mapper.Map<PaginateDto<IxpDto>>(AllIxpsx).Items;
               
                iepsItpsIxpsDto.Ixps = ixpMapper;

                return new ResponseDto { Status = 1, Message = " Seccess", Data = iepsItpsIxpsDto };

            }
            catch (Exception ex)
            {
                return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
            }
        }
        private PaginateDto<EventAttachementDto> GetFullPath(PaginateDto<EventAttachementDto> allEventAttachement)
        {
            try
            {
                if (allEventAttachement.Items.Count() > 0)
                {
                    foreach (var item in allEventAttachement.Items)
                    {
                        if (item.FileName != null)
                        {
                            string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                            var fullpath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{host}/tempFiles/{item.FileName}";
                            item.FullPath = fullpath;
                        }
                    }
                }
                return allEventAttachement;
            }
            catch (Exception ex)
            {
                return allEventAttachement; ;
            }
        }


		public FileStreamResult IepLpReport(int iepId)
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;

					var iep = _uow.GetRepository<VwIep>().Single(x => x.Id == iepId && x.IsDeleted != true, null);
					var mapper = _mapper.Map<IepLPReportDto>(iep);
					var AllIepObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true && x.IepId == iepId, null, x => x.Include(x => x.Activities).Include(x => x.ObjectiveSkills));

					IWorkbook workbook = application.Workbooks.Create(0);
					IWorksheet worksheet;
					int noOfObjectives = 1;
					if (mapper == null)
					{
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}
					if (AllIepObjectives != null && AllIepObjectives.Items.Count() > 0)
					{
						//var mapperObj = _mapper.Map<Paginate<ObjectiveDto>>(AllIepObjectives);
						//mapper.ObjectiveDtos = mapperObj.Items;

						foreach (var objective in AllIepObjectives.Items)
						{
							string strandName = "";
							string areaName = "";
							string skills = "";
							if (objective.ObjectiveSkills.Count() > 0)
							{
								var listOfObjSkillsIds = objective.ObjectiveSkills.Select(x => x.SkillId).ToArray();
								var AllIVwSkills = _uow.GetRepository<VwSkill>().GetList(x => listOfObjSkillsIds.Contains(x.Id) && x.IsDeleted != true, null);
								var skillsNumbers = AllIVwSkills.Items.Select(x => x.SkillNumber).ToArray();
								if (AllIVwSkills != null && AllIVwSkills.Count > 0)
								{
									areaName = AllIVwSkills.Items.First()?.AreaName == null ? "" : AllIVwSkills.Items.First().AreaName;
									strandName = AllIVwSkills.Items.First()?.StrandName == null ? "" : AllIVwSkills.Items.First().StrandName;
									skills = (skillsNumbers == null ? "" : string.Join(",", skillsNumbers));
								}
							}
							worksheet = workbook.Worksheets.Create(noOfObjectives + "-" + strandName + "(" + skills + ")");
							#region General
							//Disable gridlines in the worksheet
							worksheet.IsGridLinesVisible = true;
							worksheet.Range["A1:BF100"].WrapText = true;
							worksheet.Range["A1:BF100"].CellStyle.Font.Bold = true;
							worksheet.Range["A1:BF13"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
							worksheet.Range["A1:BF13"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
							#endregion
							#region IEP
							worksheet.Range["A1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["BE1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A1:BE1"].Merge();
							worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";
							worksheet.Range["A2:AQ2"].Merge();
							worksheet.Range["A2:AQ2"].Text = "LESSON PLAN (LP)";

							worksheet.Range["A2:AQ2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A2:AQ2"].CellStyle.Color = Color.FromArgb(255, 255, 200);
							worksheet.Range["AR2:AU2"].Merge();
							worksheet.Range["AR2:AU2"].Text = "YEAR:";
							worksheet.Range["AR2:AU2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["AR2:AU2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
							worksheet.Range["AV2:BE2"].Merge();
							worksheet.Range["AV2:BE2"].Text = iep.AcadmicYearName == null ? "" : iep.AcadmicYearName;

							worksheet.Range["A3:J3"].Merge();
							worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
							worksheet.Range["A3:J3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

							worksheet.Range["A3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["K3:AH3"].Merge();
							worksheet.Range["K3:AH3"].Text = iep.StudentName == null ? "" : iep.StudentName;
							worksheet.Range["AH3:Ah4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["AI3:AL3"].Merge();
							worksheet.Range["AI3:AL3"].Text = "D.O.B:";
							worksheet.Range["AI3:AL3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

							worksheet.Range["AL3:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["AM3:AV3"].Merge();
							worksheet.Range["AM3:AV3"].Text = iep.DateOfBirth == null ? "" : iep.DateOfBirth.Value.ToShortDateString();
							worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["AW3:AZ3"].Merge();
							worksheet.Range["AW3:AZ3"].Text = "REF#:";
							worksheet.Range["AW3:AZ3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

							worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["BA3:BE3"].Merge();
							worksheet.Range["BA3:BE3"].Text = iep.StudentCode == null ? "" : iep.StudentCode.ToString();

							worksheet.Range["A4:J4"].Merge();
							worksheet.Range["A4:J4"].Text = "TEACHER:";
							worksheet.Range["A4:J4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

							worksheet.Range["K4:AH4"].Merge();
							worksheet.Range["K4:AH4"].Text = iep.TeacherName == null ? "" : iep.TeacherName;
							worksheet.Range["AI4:AL4"].Merge();
							worksheet.Range["AI4:AL4"].Text = "DEPT:";
							worksheet.Range["AI4:AL4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

							worksheet.Range["AM4:AV4"].Merge();
							worksheet.Range["AM4:AV4"].Text = iep.DepartmentName == null ? "" : iep.DepartmentName;
							worksheet.Range["AW4:AZ4"].Merge();
							worksheet.Range["AW4:AZ4"].Text = "RM#:";
							worksheet.Range["AW4:AZ4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

							worksheet.Range["BA4:BE4"].Merge();
							worksheet.Range["BA4:BE4"].Text = iep.RoomNumber == null ? "" : iep.RoomNumber.ToString();
							#endregion
							#region Objective

							worksheet.Range["A6:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["J6:J10"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["J12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["AT12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["BE6:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A7:J10"].CellStyle.Color = Color.FromArgb(255, 205, 205);
							worksheet.Range["A12:AB12"].CellStyle.Color = Color.FromArgb(255, 205, 205);
							worksheet.Range["A6:J6"].CellStyle.Color = Color.FromArgb(255, 205, 205);
							worksheet.Range["A6:J6"].Merge();
							worksheet.Range["A6:J6"].Text = "Area/Strand/Skills:";
							worksheet.Range["K6:BE6"].Merge();
							if (objective.ObjectiveSkills.Count() > 0 && skills != "")
							{
								worksheet.Range["K6:BE6"].Text = areaName + "/" + strandName + "/" + skills;
							}

							worksheet.Range["A7:J7"].Merge();
							worksheet.Range["A7:J7"].Text = "Objective:";

							worksheet.Range["K7:BE7"].Merge();
							worksheet.Range["K7:BE7"].Text = objective.ObjectiveNote == null ? "" : objective.ObjectiveNote;
							worksheet.Range["A7:A10"].RowHeight = 35;
							worksheet.Range["A8:J8"].Merge();
							worksheet.Range["A8:J8"].Text = "Entry:";

							worksheet.Range["K8:BE8"].Merge();
							worksheet.Range["K8:BE8"].Text = objective.Entry == null ? "" : objective.Entry;

							worksheet.Range["A9:J9"].Merge();
							worksheet.Range["A9:J9"].Text = "Instruction" + "/" + Environment.NewLine + "Practice:";

							worksheet.Range["K9:BE9"].Merge();
							worksheet.Range["K9:BE9"].Text = objective.InstructionPractice == null ? "" : objective.InstructionPractice;

							worksheet.Range["A10:J10"].Merge();
							worksheet.Range["A10:J10"].Text = "Evaluation:";

							worksheet.Range["K10:BE10"].Merge();
							worksheet.Range["K10:BE10"].Text = objective.Evaluation == null ? "" : objective.Evaluation;

							worksheet.Range["A11:BE11"].Text = "Record";
							worksheet.Range["A11:BE11"].CellStyle.Color = Color.FromArgb(255, 255, 200);
							worksheet.Range["A11:BE11"].Merge();
							worksheet.Range["A11:BE11"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
							#endregion
							#region Activities
							worksheet.Range["A12:BE12"].CellStyle.Color = Color.FromArgb(255, 205, 205);
							worksheet.Range["A12:BE12"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A12:J12"].Merge();
							worksheet.Range["A12:BE12"].Text = "Date";
							worksheet.Range["A12:BE12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

							worksheet.Range["K12:AT12"].Merge();
							worksheet.Range["K12:AT12"].Text = "Activities";
							worksheet.Range["K12:AT12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

							worksheet.Range["AU12:BE12"].Merge();
							worksheet.Range["AU12:BE12"].Text = "Evaluation";
							worksheet.Range["AU12:BE12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							if (objective.Activities.Count() > 0)
							{
								var listOfObjActivities = objective.Activities.ToList();

								int row = 13;
								foreach (var item in listOfObjActivities)
								{
									worksheet.Range["A" + row + ":BE" + row].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
									worksheet.Range["A" + row + ":BE" + row].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

									worksheet.Range["A" + row].RowHeight = 35;
									worksheet.Range["A" + row + ":J" + row].Merge();
									worksheet.Range["K" + row + ":AT" + row].Merge();
									worksheet.Range["AU" + row + ":BE" + row].Merge();
									worksheet.Range["J13:J" + row].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["AT13:AT" + row].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["BE13:BE" + row].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + row + ":BE" + row].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + row + ":J" + row].Text = item.Date == null ? "" : item.Date.Value.ToShortDateString();
									worksheet.Range["K" + row + ":AT" + row].Text = item.Deatils == null ? "" : item.Deatils;
									worksheet.Range["AU" + row + ":BE" + row].Text = item.Evaluation == null ? "" : item.Evaluation == 1 ? "Not_Achieved" : item.Evaluation == 2 ? "Working_on" : item.Evaluation == 3 ? "Achieved" : "";


									row++;
								}
							}
							else
							{
								worksheet.Range["A13:BE13"].Merge();
								worksheet.Range["A13:BE13"].Text = "No Activities Found";
								worksheet.Range["A13"].RowHeight = 35;
								worksheet.Range["A13:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							}

							#endregion

							//Apply row height and column width to look good
							worksheet.Range["A1:BE1"].ColumnWidth = 1;
							worksheet.Range["A1"].RowHeight = 17;


							workbook.Worksheets.Append(worksheet);
							noOfObjectives++;
						}
					}
					else
					{
						worksheet = workbook.Worksheets.Create("Sheet1");
						#region General
						//Disable gridlines in the worksheet
						worksheet.IsGridLinesVisible = true;
						worksheet.Range["A1:BF1"].ColumnWidth = 1;
						worksheet.Range["A1"].RowHeight = 17;
						worksheet.Range["A1:BF100"].WrapText = true;
						worksheet.Range["A1:BF100"].CellStyle.Font.Bold = true;
						worksheet.Range["A1:BF13"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
						worksheet.Range["A1:BF13"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
						#endregion
						#region IEP
						worksheet.Range["A1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A1:BE1"].Merge();
						worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";

						worksheet.Range["A2:AQ2"].Merge();
						worksheet.Range["A2:AQ2"].Text = "LESSON PLAN (LP)";

						worksheet.Range["A2:AQ2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A2:AQ2"].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["AR2:AU2"].Merge();
						worksheet.Range["AR2:AU2"].Text = "YEAR:";
						worksheet.Range["AR2:AU2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AR2:AU2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AV2:BE2"].Merge();
						worksheet.Range["AV2:BE2"].Text = iep.AcadmicYearName == null ? "" : iep.AcadmicYearName;

						worksheet.Range["A3:J3"].Merge();
						worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
						worksheet.Range["A3:J3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["A3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["K3:AH3"].Merge();
						worksheet.Range["K3:AH3"].Text = iep.StudentName == null ? "" : iep.StudentName;
						worksheet.Range["AH3:Ah4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AI3:AL3"].Merge();
						worksheet.Range["AI3:AL3"].Text = "D.O.B:";
						worksheet.Range["AI3:AL3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AL3:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AM3:AV3"].Merge();
						worksheet.Range["AM3:AV3"].Text = iep.DateOfBirth == null ? "" : iep.DateOfBirth.Value.ToShortDateString();
						worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AW3:AZ3"].Merge();
						worksheet.Range["AW3:AZ3"].Text = "REF#:";
						worksheet.Range["AW3:AZ3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BA3:BE3"].Merge();
						worksheet.Range["BA3:BE3"].Text = iep.StudentCode == null ? "" : iep.StudentCode.ToString();

						worksheet.Range["A4:J4"].Merge();
						worksheet.Range["A4:J4"].Text = "TEACHER:";
						worksheet.Range["A4:J4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["K4:AH4"].Merge();
						worksheet.Range["K4:AH4"].Text = iep.TeacherName == null ? "" : iep.TeacherName;
						worksheet.Range["AI4:AL4"].Merge();
						worksheet.Range["AI4:AL4"].Text = "DEPT:";
						worksheet.Range["AI4:AL4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AM4:AV4"].Merge();
						worksheet.Range["AM4:AV4"].Text = iep.DepartmentName == null ? "" : iep.DepartmentName;
						worksheet.Range["AW4:AZ4"].Merge();
						worksheet.Range["AW4:AZ4"].Text = "RM#:";
						worksheet.Range["AW4:AZ4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["BA4:BE4"].Merge();
						worksheet.Range["BA4:BE4"].Text = iep.RoomNumber == null ? "" : iep.RoomNumber.ToString();
						worksheet.Range["A6:BE6"].Merge();
						worksheet.Range["A6:BE6"].Text = "No Objectives Found";
						worksheet.Range["A6"].RowHeight = 35;
						worksheet.Range["A6:BE6"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A6:BE6"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A6:BE6"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						#endregion
					}
					//Saving the Excel to the MemoryStream 
					MemoryStream stream = new MemoryStream();
					workbook.SaveAs(stream);

					//Set the position as '0'.
					stream.Position = 0;
					//Download the Excel file in the browser
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = (iep.StudentName == null ? "" : iep.StudentName + "-PLReport" + ".xlsx");

					return fileStreamResult;
				}
			}
			catch (Exception)
			{

				throw;
			}
		}
		public FileStreamResult IepReport(int iepId)
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;
					int lastRow = 0;

					var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x => x.Include(x => x.Student).ThenInclude(x => x.Department)
					.Include(x => x.Student).ThenInclude(x => x.Teacher)
					.Include(x => x.Teacher).Include(x => x.AcadmicYear).Include(x => x.Term)
					.Include(x => x.HeadOfDepartmentNavigation)
					.Include(x => x.HeadOfEducationNavigation)
					.Include(x => x.IepExtraCurriculars).ThenInclude(x => x.ExtraCurricular)
					.Include(x => x.IepParamedicalServices).ThenInclude(x => x.ParamedicalService)
					.Include(x => x.IepAssistants).ThenInclude(x => x.Assistant));
					var mapper = _mapper.Map<GetIepDto>(iep);

					IWorkbook workbook = application.Workbooks.Create(0);
					IWorksheet worksheet;
					string studentName = "";

					if (iep != null)
					{
						string studentTeacherName = "";
						string iepTeacherName = "";
						string iepHeadOfDepartmentName = "";
						string iepHeadOfEducationName = "";
						string acadmicYearName = "";
						string termName = "";
						string dateOfBirthName = "";
						string studentCodeName = "";
						string studentDepartmentName = "";
						if (iep.Student != null)
						{
							studentName = iep.Student.Name == null ? "" : iep.Student.Name;
							studentDepartmentName = iep.Student.Department == null ? "" : iep.Student.Department.Name == null ? "" : iep.Student.Department.Name;
							studentTeacherName = iep.Student.Teacher == null ? "" : iep.Student.Teacher.Name == null ? "" : iep.Student.Teacher.Name;
							dateOfBirthName = iep.Student.DateOfBirth == null ? "" : iep.Student.DateOfBirth.Value.ToShortDateString();
							studentCodeName = iep.Student.Code == null ? "" : iep.Student.Code.ToString();
						}

						if (iep.AcadmicYear != null)
							acadmicYearName = iep.AcadmicYear.Name == null ? "" : iep.AcadmicYear.Name;
						else
							acadmicYearName = "Sheet1";
						if (iep.Teacher != null)
						{
							iepTeacherName = iep.Teacher.Name == null ? "" : iep.Teacher.Name;
						}
						if (iep.HeadOfDepartmentNavigation != null)
						{
							iepHeadOfDepartmentName = iep.HeadOfDepartmentNavigation.Name == null ? "" : iep.HeadOfDepartmentNavigation.Name;
						}
						if (iep.HeadOfEducationNavigation != null)
						{
							iepHeadOfEducationName = iep.HeadOfEducationNavigation.Name == null ? "" : iep.HeadOfEducationNavigation.Name;
						}
						if (iep.Term != null)
						{
							termName = iep.Term.Name == null ? "" : iep.Term.Name;
						}
						worksheet = workbook.Worksheets.Create(acadmicYearName);
						#region General
						//Disable gridlines in the worksheet
						worksheet.IsGridLinesVisible = true;
						worksheet.Range["A1:BE1"].ColumnWidth = 1;
						worksheet.Range["A1"].RowHeight = 17;

						#endregion
						#region IEP
						worksheet.Range["A1:BE9"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE1:BE9"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AH2:AH4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AL2:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["J3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["R5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AS2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AW2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["R5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["A1:BE1"].Merge();
						worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";
						worksheet.Range["A2:AH2"].Merge();
						worksheet.Range["A2:AH2"].Text = "LESSON PLAN (LP)";
						worksheet.Range["A2:AH2"].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["AI2:AL2"].Merge();
						worksheet.Range["AI2:AL2"].Text = "YEAR:";
						worksheet.Range["AI2:AL2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AM2:AS2"].Merge();
						worksheet.Range["AM2:AS2"].Text = acadmicYearName;
						worksheet.Range["AT2:AW2"].Merge();
						worksheet.Range["AT2:AW2"].Text = "Term:";
						worksheet.Range["AT2:AW2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AX2:BE2"].Merge();
						worksheet.Range["AX2:BE2"].Text = termName;


						worksheet.Range["A3:J3"].Merge();
						worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
						worksheet.Range["A3:J3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["K3:AH3"].Merge();
						worksheet.Range["K3:AH3"].Text = studentName;
						worksheet.Range["AI3:AL3"].Merge();
						worksheet.Range["AI3:AL3"].Text = "D.O.B:";
						worksheet.Range["AI3:AL3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AM3:AV3"].Merge();
						worksheet.Range["AM3:AV3"].Text = dateOfBirthName;
						worksheet.Range["AW3:AZ3"].Merge();
						worksheet.Range["AW3:AZ3"].Text = "REF#:";
						worksheet.Range["AW3:AZ3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["BA3:BE3"].Merge();
						worksheet.Range["BA3:BE3"].Text = studentCodeName;

						worksheet.Range["A4:J4"].Merge();
						worksheet.Range["A4:J4"].Text = "TEACHER:";
						worksheet.Range["A4:J4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["K4:AH4"].Merge();
						worksheet.Range["K4:AH4"].Text = studentTeacherName;
						worksheet.Range["AI4:AL4"].Merge();
						worksheet.Range["AI4:AL4"].Text = "DEPT:";
						worksheet.Range["AI4:AL4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AM4:AV4"].Merge();
						worksheet.Range["AM4:AV4"].Text = studentDepartmentName;
						worksheet.Range["AW4:AZ4"].Merge();
						worksheet.Range["AW4:AZ4"].Text = "RM#:";
						worksheet.Range["AW4:AZ4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["BA4:BE4"].Merge();
						worksheet.Range["BA4:BE4"].Text = iep.RoomNumber == null ? "" : iep.RoomNumber.ToString();


						worksheet.Range["A5:R5"].Merge();
						worksheet.Range["A5:R5"].Text = "People involved in setting up IEP:";
						worksheet.Range["A5:R5"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						if (iep.IepAssistants != null && iep.IepAssistants.Count() > 0)
						{
							var iepAssistants = iep.IepAssistants.ToList().Select(x => (x.Assistant == null ? "" : x.Assistant.Name)).ToArray();
							worksheet.Range["S5:BE5"].Text = iepTeacherName + "," + string.Join(",", iepAssistants);
						}
						else
						{
							worksheet.Range["S5:BE5"].Text = iepTeacherName;
						}
						worksheet.Range["S5:BE5"].Merge();

						worksheet.Range["A6:BE6"].Merge();
						worksheet.Range["A6:BE6"].Text = "General Current Level Achievement and Functional Performance";
						worksheet.Range["A6:BE6"].CellStyle.Color = Color.FromArgb(255, 255, 200);

						worksheet.Range["A7:BE9"].Merge();
						worksheet.Range["A7:BE9"].Text = iep.StudentNotes == null ? "" : iep.StudentNotes;

						#endregion
						//var iepGoals = iep.Goals.Where(x => x.IsDeleted != true).ToList();
						var iepGoals = _uow.GetRepository<Goal>().GetList(x => x.Iepid == iepId && x.IsDeleted != true, null, x => x.Include(x => x.Strand).Include(x => x.Area).Include(x => x.Objectives).ThenInclude(x => x.ObjectiveSkills).ThenInclude(x => x.Skill).Include(x => x.Objectives).ThenInclude(x => x.ObjectiveEvaluationProcesses).ThenInclude(x => x.SkillEvaluation));
						var mapperGoals = _mapper.Map<PaginateDto<GoalDto>>(iepGoals).Items;

						lastRow = worksheet.Rows.Length;
						if (mapperGoals != null && mapperGoals.Count() > 0)
						{
							foreach (var goal in mapperGoals)
							{
								#region Goals
								worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 11)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["BE" + (lastRow + 2) + ":BE" + (lastRow + 11)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 2)].Merge();
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 2)].Text = "Goal Area:";
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["L" + (lastRow + 2) + ":AC" + (lastRow + 2)].Merge();
								worksheet.Range["L" + (lastRow + 2) + ":AC" + (lastRow + 2)].Text = goal.AreaName == null ? "" : goal.AreaName;
								worksheet.Range["L" + (lastRow + 2) + ":AC" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["AD" + (lastRow + 2) + ":AN" + (lastRow + 2)].Merge();
								worksheet.Range["AD" + (lastRow + 2) + ":AN" + (lastRow + 2)].Text = "Strand#";
								worksheet.Range["AD" + (lastRow + 2) + ":AN" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["AD" + (lastRow + 2) + ":AN" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["AO" + (lastRow + 2) + ":BE" + (lastRow + 2)].Merge();
								worksheet.Range["AO" + (lastRow + 2) + ":BE" + (lastRow + 2)].Text = (goal.StrandId == null ? "" : goal.StrandId) + "-" + (goal.StrandName == null ? "" : goal.StrandName);
								worksheet.Range["AO" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow + 3) + ":BE" + (lastRow + 3)].Merge();
								worksheet.Range["A" + (lastRow + 3) + ":BE" + (lastRow + 3)].Text = "Current Level";
								worksheet.Range["A" + (lastRow + 3) + ":BE" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(255, 255, 200);
								worksheet.Range["A" + (lastRow + 4) + ":BE" + (lastRow + 5)].Merge();
								worksheet.Range["A" + (lastRow + 4) + ":BE" + (lastRow + 5)].Text = goal.CurrentLevel == null ? "" : goal.CurrentLevel;

								worksheet.Range["A" + (lastRow + 6) + ":BE" + (lastRow + 6)].Merge();
								worksheet.Range["A" + (lastRow + 6) + ":BE" + (lastRow + 6)].Text = "Long Term Goal";
								worksheet.Range["A" + (lastRow + 6) + ":BE" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 255, 200);
								worksheet.Range["A" + (lastRow + 7) + ":BE" + (lastRow + 8)].Merge();
								worksheet.Range["A" + (lastRow + 7) + ":BE" + (lastRow + 8)].Text = goal.LongTermGoal == null ? "" : goal.LongTermGoal;


								int shortTermNo = 0;
								int shortTermPosition = lastRow + 9;

								worksheet.Range["A" + (lastRow + 9) + ":BE" + (lastRow + 9)].Merge();
								worksheet.Range["A" + (lastRow + 9) + ":BE" + (lastRow + 9)].Text = "Short Term Goal " + shortTermNo + "/" + goal.ShortTermProgressNumber.ToString() + "%";
								worksheet.Range["A" + (lastRow + 9) + ":BE" + (lastRow + 9)].CellStyle.Color = Color.FromArgb(255, 255, 200);
								worksheet.Range["A" + (lastRow + 10) + ":BE" + (lastRow + 11)].Merge();
								worksheet.Range["A" + (lastRow + 10) + ":BE" + (lastRow + 11)].Text = goal.ShortTermGoal == null ? "" : goal.ShortTermGoal;
								#endregion
								#region Objectives
								if (goal.Objectives != null && goal.Objectives.Count() > 0)
								{
									var goalObjectives = goal.Objectives.Where(x => x.IsDeleted != true).ToList();
									if (goalObjectives.Count() > 0)
									{
										int noOfObj = 1;
										lastRow = worksheet.Rows.Length;
										foreach (var objective in goalObjectives)
										{
											// to calculate Percentage
											if (objective.IsMasterd == true)
											{
												shortTermNo = shortTermNo + (objective.ObjectiveNumber == null ? 0 : objective.ObjectiveNumber.Value);
												if (shortTermNo > 0)
												{
													worksheet.Range["A" + shortTermPosition + ":BE" + shortTermPosition].Text = "Short Term Goal " + shortTermNo + "/" + goal.ShortTermProgressNumber.ToString() + "%";
												}
											}
											//worksheet.Range["A" + (lastRow + 1) + ":AE300" ].CellStyle.Font.Size = 9;
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 4)].Merge();
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 4)].Text = noOfObj.ToString();
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 4)].CellStyle.Color = Color.FromArgb(255, 255, 200);
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


											worksheet.Range["B" + (lastRow + 1) + ":L" + (lastRow + 1)].Merge();
											worksheet.Range["B" + (lastRow + 1) + ":L" + (lastRow + 1)].Text = "Objectives";
											worksheet.Range["B" + (lastRow + 1) + ":L" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
											worksheet.Range["B" + (lastRow + 1) + ":L" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

											worksheet.Range["M" + (lastRow + 1) + ":P" + (lastRow + 1)].Merge();
											worksheet.Range["M" + (lastRow + 1) + ":P" + (lastRow + 1)].Text = "Skill no.";
											worksheet.Range["M" + (lastRow + 1) + ":P" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
											worksheet.Range["M" + (lastRow + 1) + ":P" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

											worksheet.Range["Q" + (lastRow + 1) + ":S" + (lastRow + 1)].Merge();
											if (objective.ObjectiveSkills != null && objective.ObjectiveSkills.Count() > 0)
											{
												var listOfObjSkillsNumbers = objective.ObjectiveSkills.Select(x => x.SkillNumber).ToArray();
												worksheet.Range["Q" + (lastRow + 1) + ":S" + (lastRow + 1)].Text = (listOfObjSkillsNumbers == null ? "" : string.Join(",", listOfObjSkillsNumbers));
											}

											worksheet.Range["T" + (lastRow + 1) + ":AC" + (lastRow + 1)].Merge();
											worksheet.Range["T" + (lastRow + 1) + ":AC" + (lastRow + 1)].Text = "Evaluation process";
											worksheet.Range["T" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

											worksheet.Range["AD" + (lastRow + 1) + ":AL" + (lastRow + 1)].Merge();
											worksheet.Range["AD" + (lastRow + 1) + ":AL" + (lastRow + 1)].Text = "Indication";

											worksheet.Range["AM" + (lastRow + 1) + ":AR" + (lastRow + 1)].Merge();
											worksheet.Range["AM" + (lastRow + 1) + ":AR" + (lastRow + 1)].Text = "Date";

											worksheet.Range["AS" + (lastRow + 1) + ":AT" + (lastRow + 1)].Merge();
											worksheet.Range["AS" + (lastRow + 1) + ":AT" + (lastRow + 1)].Text = "%";

											worksheet.Range["AU" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
											worksheet.Range["AU" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = "Resources required";

											worksheet.Range["B" + (lastRow + 2) + ":S" + (lastRow + 4)].Merge();
											worksheet.Range["B" + (lastRow + 2) + ":S" + (lastRow + 4)].Text = objective.ObjectiveNote == null ? "" : objective.ObjectiveNote;

											worksheet.Range["T" + (lastRow + 2) + ":AC" + (lastRow + 4)].Merge();
											if (objective.ObjectiveEvaluationProcesses != null && objective.ObjectiveEvaluationProcesses.Count() > 0)
											{
												//var listOfObjEvaluationsNames = objective.ObjectiveEvaluationProcesses.ToList().Select(x => (x.SkillEvaluation.Name == null ? "" : x.SkillEvaluation.Name)).ToArray();
												var listOfObjEvaluationsNames = string.Join(Environment.NewLine, objective.EvaluationProcessName);
												worksheet.Range["T" + (lastRow + 2) + ":AC" + (lastRow + 4)].Text = (listOfObjEvaluationsNames == null ? "" : string.Join(Environment.NewLine, listOfObjEvaluationsNames));
											}
											worksheet.Range["AD" + (lastRow + 2) + ":AL" + (lastRow + 4)].Merge();
											worksheet.Range["AD" + (lastRow + 2) + ":AL" + (lastRow + 4)].Text = objective.Indication == null ? "" : objective.Indication;
											worksheet.Range["AM" + (lastRow + 2) + ":AR" + (lastRow + 4)].Merge();
											worksheet.Range["AM" + (lastRow + 2) + ":AR" + (lastRow + 4)].Text = objective.Date == null ? "" : objective.Date.Value.ToShortDateString();
											worksheet.Range["AS" + (lastRow + 2) + ":AT" + (lastRow + 4)].Merge();
											worksheet.Range["AS" + (lastRow + 2) + ":AT" + (lastRow + 4)].Text = objective.ObjectiveNumber == null ? "" : objective.ObjectiveNumber.ToString();
											worksheet.Range["AU" + (lastRow + 2) + ":BE" + (lastRow + 4)].Merge();
											worksheet.Range["AU" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = objective.ResourcesRequired == null ? "" : objective.ResourcesRequired;

											worksheet.Range["AC" + (lastRow + 1) + ":AC" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["AL" + (lastRow + 1) + ":AL" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["AR" + (lastRow + 1) + ":AR" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["AT" + (lastRow + 1) + ":AT" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["BE" + (lastRow + 1) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["B" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["A" + (lastRow + 4) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["S" + (lastRow) + ":S" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

											noOfObj++;
											lastRow = lastRow + 4;
										}
									}
									lastRow = worksheet.Rows.Length;
								}
								else
								{
									worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow + 14)].Merge();
									worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow + 14)].Text = " No Objects Found";
									worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow + 14)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow + 14)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow + 14)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									lastRow = worksheet.Rows.Length;
								}
								#endregion
							}
						}
						else
						{
							lastRow = worksheet.Rows.Length;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Merge();
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = " No Goals Found";
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							lastRow = worksheet.Rows.Length;

						}

						#region Para-Medical

						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 2)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 2)].Text = "Student Involved in Para-Medical Services/Support";

						worksheet.Range["A" + (lastRow + 3) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 3) + ":AD" + (lastRow + 3)].Merge();
						worksheet.Range["A" + (lastRow + 3) + ":AD" + (lastRow + 3)].Text = "Service Name";
						worksheet.Range["A" + (lastRow + 3) + ":AD" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 3) + ":AD" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AE" + (lastRow + 3) + ":BE" + (lastRow + 3)].Merge();
						worksheet.Range["AE" + (lastRow + 3) + ":BE" + (lastRow + 3)].Text = "Refer to ITP";
						worksheet.Range["AE" + (lastRow + 3) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AE" + (lastRow + 3) + ":BE" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						lastRow = worksheet.Rows.Length;


						if (iep.IepParamedicalServices != null && iep.IepParamedicalServices.Count() > 0)
						{
							foreach (var Para in iep.IepParamedicalServices)
							{
								worksheet.Range["A" + lastRow + ":BE" + lastRow].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + lastRow + ":AD" + lastRow].Merge();
								worksheet.Range["A" + lastRow + ":AD" + lastRow].Text = Para.ParamedicalService == null ? "" : Para.ParamedicalService.Name;
								worksheet.Range["A" + lastRow + ":AD" + lastRow].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["AE" + lastRow + ":BE" + lastRow].Merge();
								worksheet.Range["AE" + lastRow + ":BE" + lastRow].Text = "Yes";
								worksheet.Range["AE" + lastRow + ":BE" + lastRow].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								lastRow++;
							}

						}
						else
						{
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 2)].Merge();
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 2)].Text = "No Paramedical Services";
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							lastRow = lastRow + 3;
						}

						#endregion
						#region Extra-Curriular
						lastRow = lastRow + 1;

						worksheet.Range["A" + lastRow + ":BE" + lastRow].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + lastRow + ":BE" + lastRow].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + lastRow + ":BE" + lastRow].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + lastRow + ":BE" + lastRow].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["A" + lastRow + ":BE" + lastRow].Merge();
						worksheet.Range["A" + lastRow + ":BE" + lastRow].Text = "Student Involved in Extra-Curriular Services/Support";

						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":AD" + (lastRow + 1)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":AD" + (lastRow + 1)].Text = "Name";
						worksheet.Range["A" + (lastRow + 1) + ":AD" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":AD" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
						worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = "Refer to ITP";
						worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						if (iep.IepExtraCurriculars != null && iep.IepExtraCurriculars.Count() > 0)
						{

							foreach (var extra in iep.IepExtraCurriculars)
							{
								worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow + 2) + ":AD" + (lastRow + 2)].Merge();
								worksheet.Range["A" + (lastRow + 2) + ":AD" + (lastRow + 2)].Text = extra.ExtraCurricular == null ? "" : extra.ExtraCurricular.Name;
								worksheet.Range["A" + (lastRow + 2) + ":AD" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 2)].Merge();
								worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 2)].Text = "Yes";
								worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								lastRow++;
							}
						}
						else
						{
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 3)].Merge();
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 3)].Text = "No Extra Curriculars";
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							lastRow = worksheet.Rows.Length;
						}
						#endregion
						#region FooterNote
						lastRow = worksheet.Rows.Length;

						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = iep.StudentNotes;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						lastRow = worksheet.Rows.Length;
						#endregion
						#region Footer
						lastRow = worksheet.Rows.Length;
						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].Text = "Report Card";
						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["H" + (lastRow + 2) + ":I" + (lastRow + 2)].Merge();
						worksheet.Range["H" + (lastRow + 2) + ":I" + (lastRow + 2)].Text = iep.ReportCard == false ? "✘" : iep.ReportCard == true ? "✔" : "";
						worksheet.Range["H" + (lastRow + 2) + ":I" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].Merge();
						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].Text = "Progress Report";
						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["X" + (lastRow + 2) + ":Y" + (lastRow + 2)].Merge();
						worksheet.Range["X" + (lastRow + 2) + ":Y" + (lastRow + 2)].Text = iep.ProgressReport == false ? "✘" : iep.ProgressReport == true ? "✔" : "";
						worksheet.Range["X" + (lastRow + 2) + ":Y" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].Merge();
						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].Text = "Parents meeting";
						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AN" + (lastRow + 2) + ":AO" + (lastRow + 2)].Merge();
						worksheet.Range["AN" + (lastRow + 2) + ":AO" + (lastRow + 2)].Text = iep.ParentsMeeting == false ? "✘" : iep.ParentsMeeting == true ? "✔" : "";
						worksheet.Range["AN" + (lastRow + 2) + ":AO" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].Merge();
						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].Text = "Other";
						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["BD" + (lastRow + 2) + ":BE" + (lastRow + 2)].Merge();
						worksheet.Range["BD" + (lastRow + 2) + ":BE" + (lastRow + 2)].Text = iep.Others == false ? "✘" : iep.Others == true ? "✔" : "";
						worksheet.Range["BD" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].Merge();
						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].Text = "Parents Involved in setting up suggestions";
						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["Z" + (lastRow + 3) + ":BE" + (lastRow + 3)].Merge();
						worksheet.Range["Z" + (lastRow + 3) + ":BE" + (lastRow + 3)].Text = iep.ParentsInvolvedInSettingUpSuggestions == false ? "" : iep.ParentsInvolvedInSettingUpSuggestions == true ? "Yes, refer parent meeting record form" : "";
						worksheet.Range["Z" + (lastRow + 3) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].Merge();
						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].Text = "Date of Review";
						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["Z" + (lastRow + 4) + ":BE" + (lastRow + 4)].Merge();
						worksheet.Range["Z" + (lastRow + 4) + ":BE" + (lastRow + 4)].Text = iep.LastDateOfReview == null ? "" : iep.LastDateOfReview.Value.ToShortDateString();
						worksheet.Range["Z" + (lastRow + 4) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["A" + (lastRow + 6) + ":H" + (lastRow + 6)].Merge();
						worksheet.Range["A" + (lastRow + 6) + ":H" + (lastRow + 6)].Text = "Teacher:";
						worksheet.Range["A" + (lastRow + 6) + ":H" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["I" + (lastRow + 6) + ":S" + (lastRow + 6)].Merge();
						worksheet.Range["I" + (lastRow + 6) + ":S" + (lastRow + 6)].Text = iepTeacherName;

						worksheet.Range["T" + (lastRow + 6) + ":AA" + (lastRow + 6)].Merge();
						worksheet.Range["T" + (lastRow + 6) + ":AA" + (lastRow + 6)].Text = "HOD:";
						worksheet.Range["T" + (lastRow + 6) + ":AA" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AB" + (lastRow + 6) + ":AL" + (lastRow + 6)].Merge();
						worksheet.Range["AB" + (lastRow + 6) + ":AL" + (lastRow + 6)].Text = iepHeadOfDepartmentName;

						worksheet.Range["AM" + (lastRow + 6) + ":AT" + (lastRow + 6)].Merge();
						worksheet.Range["AM" + (lastRow + 6) + ":AT" + (lastRow + 6)].Text = "HOE:";
						worksheet.Range["AM" + (lastRow + 6) + ":AT" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AU" + (lastRow + 6) + ":BE" + (lastRow + 6)].Merge();
						worksheet.Range["AU" + (lastRow + 6) + ":BE" + (lastRow + 6)].Text = iepHeadOfEducationName;


						worksheet.Range["A" + (lastRow + 7) + ":H" + (lastRow + 7)].Merge();
						worksheet.Range["A" + (lastRow + 7) + ":H" + (lastRow + 7)].Text = "Signature:";
						worksheet.Range["A" + (lastRow + 7) + ":H" + (lastRow + 7)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["I" + (lastRow + 7) + ":S" + (lastRow + 7)].Merge();
						worksheet.Range["I" + (lastRow + 7) + ":S" + (lastRow + 7)].Text = "";

						worksheet.Range["T" + (lastRow + 7) + ":AA" + (lastRow + 7)].Merge();
						worksheet.Range["T" + (lastRow + 7) + ":AA" + (lastRow + 7)].Text = "Signature:";
						worksheet.Range["T" + (lastRow + 7) + ":AA" + (lastRow + 7)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AB" + (lastRow + 7) + ":AL" + (lastRow + 7)].Merge();
						worksheet.Range["AB" + (lastRow + 7) + ":AL" + (lastRow + 7)].Text = "";

						worksheet.Range["AM" + (lastRow + 7) + ":AT" + (lastRow + 7)].Merge();
						worksheet.Range["AM" + (lastRow + 7) + ":AT" + (lastRow + 7)].Text = "Signature:";
						worksheet.Range["AM" + (lastRow + 7) + ":AT" + (lastRow + 7)].CellStyle.Color = Color.FromArgb(255, 205, 205);


						worksheet.Range["AU" + (lastRow + 7) + ":BE" + (lastRow + 7)].Merge();
						worksheet.Range["AU" + (lastRow + 7) + ":BE" + (lastRow + 7)].Text = "";

						worksheet.Range["H" + (lastRow + 6) + ":H" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["S" + (lastRow + 6) + ":S" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AA" + (lastRow + 6) + ":AA" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AL" + (lastRow + 6) + ":AL" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AT" + (lastRow + 6) + ":AT" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 6) + ":BE" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["Y" + (lastRow + 2) + ":Y" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 2) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						#endregion
					}
					else
					{
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}
					lastRow = worksheet.Rows.Length;
					worksheet.Range["A1:BF" + (lastRow)].WrapText = true;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.Font.Bold = true;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.Font.Size = 9;


					MemoryStream stream = new MemoryStream();
					//workbook.SaveAsHtml(stream, Syncfusion.XlsIO.Implementation.HtmlSaveOptions.Default);
					workbook.SaveAs(stream);
					stream.Position = 0;
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = (studentName + "-IEPReport" + ".xlsx");
					//fileStreamResult.FileDownloadName = ( "-IEPReport.html");


					return fileStreamResult;
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public FileStreamResult BCPReport(int? studentId, int? iepId)
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;
					int lastRow = 1;
					string studentName = "";

					List<Skill> allSkills = new List<Skill>();
					List<Skill> iepMasteredSkills = new List<Skill>();

					string currentDateOfPreparation = "";
					string currentDateOfReview = "";
					if (studentId > 0 && studentId != null)
					{
						var studentIeps = _uow.GetRepository<Iep>().GetList(x => x.StudentId == studentId && x.IsDeleted != true && x.Status == 3, null,
							x => x.Include(s => s.Goals).ThenInclude(x => x.Objectives.Where(o => o.IsMasterd == true)).ThenInclude(os => os.ObjectiveSkills).ThenInclude(s => s.Skill).Include(x => x.Student)).Items;

						studentName = studentIeps.FirstOrDefault().Student == null ? "" : studentIeps.FirstOrDefault().Student.Name;
						if (studentIeps.Count > 0)
						{
							foreach (var studentIep in studentIeps)
							{
								iepMasteredSkills = studentIep.Goals
								 .SelectMany(x => x.Objectives)
								 .SelectMany(x => x.ObjectiveSkills)
								 .Select(x => x.Skill == null ? new Skill { Id = 0, StrandId = 0 } : x.Skill)
								  .ToList();
								if (iepMasteredSkills != null && iepMasteredSkills.Count > 0)
								{
									allSkills.AddRange(iepMasteredSkills);
								}
							}
						}
					}
					else if (iepId > 0 && iepId != null)
					{
						var currentIep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x => x.Include(x => x.Student));
						if (currentIep != null)
						{
							studentName = currentIep.Student == null ? "" : currentIep.Student.Name;
							currentDateOfPreparation = currentIep.DateOfPreparation == null ? "" : currentIep.DateOfPreparation.Value.ToShortDateString();
							currentDateOfReview = currentIep.LastDateOfReview == null ? "" : currentIep.LastDateOfReview.Value.ToShortDateString();
							var studentIeps = _uow.GetRepository<Iep>().GetList(x => x.StudentId == currentIep.StudentId && x.IsDeleted != true && x.Status == 3 && x.CreatedOn <= currentIep.CreatedOn, null,
						  x => x.Include(s => s.Goals).ThenInclude(x => x.Objectives.Where(o => o.IsMasterd == true)).ThenInclude(os => os.ObjectiveSkills).ThenInclude(s => s.Skill)).Items;

							if (studentIeps.Count > 0)
							{
								foreach (var studentIep in studentIeps)
								{
									iepMasteredSkills = studentIep.Goals
									 .SelectMany(x => x.Objectives)
									 .SelectMany(x => x.ObjectiveSkills)
									 .Select(x => x.Skill == null ? new Skill { Id = 0, StrandId = 0 } : x.Skill)
									  .ToList();
									if (iepMasteredSkills != null && iepMasteredSkills.Count > 0)
									{
										allSkills.AddRange(iepMasteredSkills);
									}
								}
							}
						}
					}
					else
					{
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}
					IWorkbook workbook = application.Workbooks.Create(0);
					IWorksheet worksheet;
					worksheet = workbook.Worksheets.Create(studentName);
					worksheet.UsedRange.AutofitColumns();

					#region General
					worksheet.IsGridLinesVisible = true;
					worksheet.Range["A1:BS1"].ColumnWidth = 2;
					worksheet.Range["A1"].RowHeight = 17;
					#endregion
					#region Header
					//lastCulumn = worksheet.Columns.Length;
					worksheet.Range["A1:BS5"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

					worksheet.Range["A1:BS1"].Merge();
					worksheet.Range["A1:BS1"].Text = "IDEAL EDUCATION SCHOOL";

					worksheet.Range["A2:BS2"].Merge();
					worksheet.Range["A2:BS2"].Text = "BCP Profile";
					worksheet.Range["A2:BS2"].CellStyle.Color = Color.FromArgb(255, 255, 200);

					worksheet.Range["A3:BS3"].Merge();
					worksheet.Range["A3:BS3"].Text = "Student Name";

					worksheet.Range["A4:M4"].Merge();
					worksheet.Range["A4:M4"].Text = "Level";
					worksheet.Range["M4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
					worksheet.Range["A4:BS4"].CellStyle.Color = Color.FromArgb(255, 255, 200);

					worksheet.Range["A5:M5"].Merge();
					worksheet.Range["A5:M5"].Text = "Area/Strand";
					worksheet.Range["A5:M5"].CellStyle.Color = Color.FromArgb(255, 205, 205);
					worksheet.Range["M5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

					worksheet.Range["N5:BS5"].Merge();
					worksheet.Range["N5:BS5"].Text = "Behaviors / Skills";
					worksheet.Range["N5:BS5"].CellStyle.Color = Color.FromArgb(255, 205, 205);
					#endregion


					#region DrowSkills
					var allAreas = _uow.GetRepository<Area>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(n => n.Strands.Where(s => s.IsDeleted != true)).ThenInclude(n => n.Skills), 0, 100000, true);
					if (allAreas != null && allAreas.Items.Count() > 0)
					{
						lastRow = worksheet.Rows.Length + 1;
						foreach (var area in allAreas.Items)
						{
							worksheet.Range["A" + (lastRow)].Number = area.Id;
							worksheet.Range["A" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 255, 200);
							worksheet.Range["A" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

							worksheet.Range["B" + (lastRow) + ":BS" + (lastRow)].Merge();
							worksheet.Range["B" + (lastRow) + ":BS" + (lastRow)].Text = area.Name == null ? "" : area.Name;
							worksheet.Range["B" + (lastRow) + ":BS" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 255, 200);
							worksheet.Range["A" + (lastRow) + ":BS" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

							var AreaStrands = area.Strands.Where(x => x.IsDeleted != true).ToList();
							if (AreaStrands != null && AreaStrands.Count() > 0)
							{
								lastRow = worksheet.Rows.Length;
								foreach (var strand in AreaStrands)
								{
									worksheet.Range["A" + (lastRow + 1)].Number = strand.Id;
									worksheet.Range["A" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["B" + (lastRow + 1) + ":M" + (lastRow + 1)].Merge();
									worksheet.Range["B" + (lastRow + 1) + ":M" + (lastRow + 1)].Text = strand.Name == null ? "" : strand.Name;
									worksheet.Range["A" + (lastRow + 1) + ":BS" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["M" + (lastRow + 1) + ":BS" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

									var strandSkills = strand.Skills.Where(x => x.IsDeleted != true).ToList();
									if (strandSkills != null && strandSkills.Count() > 0)
									{
										List<Skill> masteredStrandSkills = new List<Skill>();
										if (allSkills != null && allSkills.Count > 0)
										{
											masteredStrandSkills = allSkills.Where(x => x.StrandId == strand.Id).ToList();
										}
										int currentCulumn = 14;
										for (int i = 0; i < strandSkills.Count; i++)
										{
											worksheet.Range[(lastRow + 1), (currentCulumn + i)].CellStyle.Font.Size = 9;
											worksheet.Range[(lastRow + 1), (currentCulumn + i)].Number = (double)(strandSkills[i].SkillNumber == null ? 0 : strandSkills[i].SkillNumber);
											if (masteredStrandSkills != null && masteredStrandSkills.Count > 0)
											{
												if (masteredStrandSkills.Any(x => x.Id == strandSkills[i].Id))
												{
													worksheet.Range[(lastRow + 1), (currentCulumn + i)].CellStyle.Color = Color.FromArgb(113, 211, 110);
												}
											}
										}
									}
									lastRow++;
								}
							}
							lastRow++;
						}

					}

					#endregion
					#region Keys
					if (studentId != null && studentId > 0)
					{
						lastRow = worksheet.Rows.Length + 1;
						worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["J" + (lastRow + 1) + ":J" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 2) + ":Q" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 3) + ":Q" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 4) + ":Q" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["Q" + (lastRow + 1) + ":Q" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["A" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(113, 211, 110);
						worksheet.Range["B" + (lastRow + 1) + ":Q" + (lastRow + 1)].Merge();
						worksheet.Range["B" + (lastRow + 1) + ":Q" + (lastRow + 1)].Text = "Mastered Skills";

						worksheet.Range["A" + (lastRow + 2)].CellStyle.Color = Color.Yellow;
						worksheet.Range["B" + (lastRow + 2) + ":Q" + (lastRow + 2)].Merge();
						worksheet.Range["B" + (lastRow + 2) + ":Q" + (lastRow + 2)].Text = "Current Objective";

						worksheet.Range["A" + (lastRow + 3)].CellStyle.Color = Color.Orange;
						worksheet.Range["B" + (lastRow + 3) + ":Q" + (lastRow + 3)].Merge();
						worksheet.Range["B" + (lastRow + 3) + ":Q" + (lastRow + 3)].Text = "Regression to (so it is the current goal)";

						worksheet.Range["A" + (lastRow + 4)].CellStyle.Color = Color.Aqua;
						worksheet.Range["B" + (lastRow + 4) + ":Q" + (lastRow + 4)].Merge();
						worksheet.Range["B" + (lastRow + 4) + ":Q" + (lastRow + 4)].Text = "The last mastered skill in the previous/last assessment.";


					}
					else
					{
						lastRow = worksheet.Rows.Length + 1;
						worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["J" + (lastRow + 1) + ":J" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":J" + (lastRow + 1)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":J" + (lastRow + 1)].Text = "Date of first assessment:";
						worksheet.Range["A" + (lastRow + 1) + ":J" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 255, 200);

						worksheet.Range["K" + (lastRow + 1) + ":Q" + (lastRow + 1)].Merge();
						worksheet.Range["K" + (lastRow + 1) + ":Q" + (lastRow + 1)].Text = currentDateOfPreparation;

						worksheet.Range["A" + (lastRow + 2) + ":J" + (lastRow + 2)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":J" + (lastRow + 2)].Text = "Date of second assessment:";
						worksheet.Range["A" + (lastRow + 2) + ":J" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 255, 200);

						worksheet.Range["K" + (lastRow + 2) + ":Q" + (lastRow + 2)].Merge();
						worksheet.Range["K" + (lastRow + 2) + ":Q" + (lastRow + 2)].Text = currentDateOfReview;

						worksheet.Range["A" + (lastRow + 3) + ":Q" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 3) + ":Q" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 4) + ":Q" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 5) + ":Q" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 6) + ":Q" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 3) + ":A" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["Q" + (lastRow + 1) + ":Q" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["A" + (lastRow + 3)].CellStyle.Color = Color.Green;
						worksheet.Range["B" + (lastRow + 3) + ":Q" + (lastRow + 3)].Merge();
						worksheet.Range["B" + (lastRow + 3) + ":Q" + (lastRow + 3)].Text = "Mastered Skills";

						worksheet.Range["A" + (lastRow + 4)].CellStyle.Color = Color.Yellow;
						worksheet.Range["B" + (lastRow + 4) + ":Q" + (lastRow + 4)].Merge();
						worksheet.Range["B" + (lastRow + 4) + ":Q" + (lastRow + 4)].Text = "Current Objective";

						worksheet.Range["A" + (lastRow + 5)].CellStyle.Color = Color.Orange;
						worksheet.Range["B" + (lastRow + 5) + ":Q" + (lastRow + 5)].Merge();
						worksheet.Range["B" + (lastRow + 5) + ":Q" + (lastRow + 5)].Text = "Regression to (so it is the current goal)";

						worksheet.Range["A" + (lastRow + 6)].CellStyle.Color = Color.Aqua;
						worksheet.Range["B" + (lastRow + 6) + ":Q" + (lastRow + 6)].Merge();
						worksheet.Range["B" + (lastRow + 6) + ":Q" + (lastRow + 6)].Text = "The last mastered skill in the previous/last assessment.";



					}
					#endregion
					lastRow = worksheet.Rows.Length;
					worksheet.Range["A1:BS" + (lastRow)].WrapText = true;
					worksheet.Range["A1:BS" + (lastRow)].CellStyle.Font.Bold = true;
					worksheet.Range["A1:BS" + (lastRow)].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
					worksheet.Range["A1:BS" + (lastRow)].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

					MemoryStream stream = new MemoryStream();
					workbook.SaveAs(stream);

					stream.Position = 0;
					//Download the Excel file in the browser
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = ("BCPReport" + ".xlsx");
					return fileStreamResult;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}
		public FileStreamResult ProgressReport(int iepProgressReportId)
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;
					int lastRow = 0;

					var iepProgressReport = _uow.GetRepository<IepProgressReport>().Single(x => x.Id == iepProgressReportId, null,
						x => x.Include(x => x.ProgressReportExtraCurriculars).ThenInclude(x => x.ExtraCurricular)
						.Include(x => x.ProgressReportParamedicals).ThenInclude(x => x.ParamedicalService)
						.Include(x => x.ProgressReportStrands).ThenInclude(x => x.Strand).ThenInclude(x => x.Area)
						.Include(x => x.Teacher)
						.Include(x => x.Student).Include(x => x.Term)
						.Include(x => x.Student).ThenInclude(x => x.Department)
						.Include(x => x.AcadmicYear));

					var iepProgressReportDto = _mapper.Map<IepProgressReportDto>(iepProgressReport);

					IWorkbook workbook = application.Workbooks.Create(0);

					IWorksheet worksheet;
					IWorksheet dataWorksheet;

					if (iepProgressReportDto != null)
					{

						string acadmicYearName = iepProgressReportDto.AcadmicYearName == null ? "" : iepProgressReportDto.AcadmicYearName;
						string termName = iepProgressReportDto.TermName == null ? "" : iepProgressReportDto.TermName;
						string studentName = iepProgressReportDto.StudentName == null ? "" : iepProgressReportDto.StudentName;
						string dateOfBirthName = iepProgressReportDto.StudentBirthDay == null ? "" : iepProgressReportDto.StudentBirthDay;
						string studentCodeName = iepProgressReportDto.StudentCode == null ? "" : iepProgressReportDto.StudentCode;
						string studentDepartmentName = iepProgressReportDto.StudentDepartmentName == null ? "" : iepProgressReportDto.StudentDepartmentName;

						worksheet = workbook.Worksheets.Create(studentCodeName + "(" + acadmicYearName + ")" + "(" + termName + ")");
						dataWorksheet = workbook.Worksheets.Create("Data");
						//IChartShape chart = worksheet.Charts.Add();

						#region Strands
						worksheet.Range["A1:AH1"].Merge();
						worksheet.Range["A1:AH1"].Text = "Educational";
						worksheet.Range["A1:AH1"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A1:AH1"].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["AH1"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						lastRow = worksheet.Rows.Length + 1;

						if (iepProgressReportDto.ProgressReportStrands.Count > 0)
						{
							foreach (var strand in iepProgressReportDto.ProgressReportStrands.ToList())
							{
								worksheet.Range["A" + (lastRow) + ":AH" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["A" + (lastRow) + ":Q" + (lastRow)].Merge();
								worksheet.Range["A" + (lastRow) + ":Q" + (lastRow)].Text = strand.AreaName == null ? "" : strand.AreaName;
								worksheet.Range["Q" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["A" + (lastRow) + ":Ah" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["R" + (lastRow) + ":AH" + (lastRow)].Merge();
								worksheet.Range["R" + (lastRow) + ":AH" + (lastRow)].Text = strand.StrandName == null ? "" : strand.StrandName;

								worksheet.Range["A" + (lastRow + 1) + ":AH" + (lastRow + 1)].Merge();
								worksheet.Range["A" + (lastRow + 1) + ":AH" + (lastRow + 1)].Text = "Comment:";

								worksheet.Range["A" + (lastRow + 2) + ":AH" + (lastRow + 6)].Merge();
								worksheet.Range["A" + (lastRow + 2) + ":AH" + (lastRow + 6)].Text = strand.Comment == null ? "" : strand.Comment;
								worksheet.Range["AH" + (lastRow) + ":AH" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								lastRow = worksheet.Rows.Length + 1;
							}


						}
						#endregion
						#region General
						lastRow = worksheet.Rows.Length + 2;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Merge();
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Text = "GENERAL COMMENT:";
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 1) + ":AH" + (lastRow + 5)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":AH" + (lastRow + 5)].Text = iepProgressReportDto.GeneralComment == null ? "" : iepProgressReportDto.GeneralComment;
						worksheet.Range["A" + (lastRow + 1) + ":AH" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["A" + (lastRow + 6) + ":Q" + (lastRow + 6)].Merge();
						worksheet.Range["A" + (lastRow + 6) + ":Q" + (lastRow + 6)].Text = "Teacher's name";
						worksheet.Range["Q" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 6) + ":Ah" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["R" + (lastRow + 6) + ":AH" + (lastRow + 6)].Merge();
						worksheet.Range["R" + (lastRow + 6) + ":AH" + (lastRow + 6)].Text = "School Stamp";
						worksheet.Range["R" + (lastRow + 6) + ":AH" + (lastRow + 6)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						lastRow = worksheet.Rows.Length + 1;

						worksheet.Range["A" + (lastRow) + ":Q" + (lastRow + 4)].Merge();
						worksheet.Range["A" + (lastRow) + ":Q" + (lastRow + 4)].Text = iepProgressReportDto.TeacherName == null ? "" : iepProgressReportDto.TeacherName;
						worksheet.Range["Q" + (lastRow) + ":Q" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 4) + ":AH" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["R" + (lastRow) + ":AH" + (lastRow + 4)].Merge();
						worksheet.Range["R" + (lastRow) + ":AH" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						#endregion
						#region Extra
						lastRow = worksheet.Rows.Length + 2;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Merge();
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Text = "Extra Curricula";
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						if (iepProgressReportDto.ProgressReportExtraCurriculars.Count > 0)
						{
							foreach (var extra in iepProgressReportDto.ProgressReportExtraCurriculars)
							{
								worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].Merge();
								worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].Text = extra.ExtraCurricularName == null ? "" : extra.ExtraCurricularName;
								worksheet.Range["Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow + 1) + ":Ah" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;


								worksheet.Range["A" + (lastRow + 1) + ":Ah" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["R" + (lastRow + 1) + ":AH" + (lastRow + 1)].Merge();
								worksheet.Range["R" + (lastRow + 1) + ":AH" + (lastRow + 1)].Text = extra.ExtraCurricularNameAr == null ? "" : extra.ExtraCurricularNameAr;

								worksheet.Range["A" + (lastRow + 2) + ":AH" + (lastRow + 5)].Merge();
								worksheet.Range["A" + (lastRow + 2) + ":AH" + (lastRow + 5)].Text = extra.Comment == null ? "" : extra.Comment;
								worksheet.Range["A" + (lastRow + 5) + ":Ah" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["AH" + (lastRow) + ":Ah" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								lastRow = worksheet.Rows.Length;
							}


						}


						#endregion
						#region Paramedical
						lastRow = worksheet.Rows.Length + 2;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Merge();
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Text = "Paramedical";
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						if (iepProgressReportDto.ProgressReportParamedicals.Count > 0)
						{
							foreach (var para in iepProgressReportDto.ProgressReportParamedicals)
							{
								worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].Merge();
								worksheet.Range["A" + (lastRow + 1) + ":Q" + (lastRow + 1)].Text = para.ParamedicalServiceName == null ? "" : para.ParamedicalServiceName;
								worksheet.Range["Q" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow + 1) + ":Ah" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;


								worksheet.Range["A" + (lastRow + 1) + ":Ah" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["R" + (lastRow + 1) + ":AH" + (lastRow + 1)].Merge();
								worksheet.Range["R" + (lastRow + 1) + ":AH" + (lastRow + 1)].Text = para.ParamedicalServiceNameAr == null ? "" : para.ParamedicalServiceNameAr;

								worksheet.Range["A" + (lastRow + 2) + ":AH" + (lastRow + 5)].Merge();
								worksheet.Range["A" + (lastRow + 2) + ":AH" + (lastRow + 5)].Text = para.Comment == null ? "" : para.Comment;
								worksheet.Range["A" + (lastRow + 5) + ":Ah" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["AH" + (lastRow + 1) + ":Ah" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								lastRow = worksheet.Rows.Length;
							}


						}

						#endregion
						#region Other
						lastRow = worksheet.Rows.Length + 2;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Merge();
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].Text = "Other";
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow) + ":AH" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 1) + ":AH" + (lastRow + 9)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":AH" + (lastRow + 9)].Text = iepProgressReportDto.OtherComment == null ? "" : iepProgressReportDto.OtherComment;
						worksheet.Range["A" + (lastRow + 9) + ":Ah" + (lastRow + 9)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AH" + (lastRow) + ":AH" + (lastRow + 9)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						lastRow = worksheet.Rows.Length;
						#endregion
						#region Chart

						if (iepProgressReportDto.ProgressReportStrands.Count() > 0)
						{
							dataWorksheet.Range["A1"].Text = "Strand/Term";
							dataWorksheet.Range["A1"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

							dataWorksheet.Range["A1:D1"].CellStyle.Color = Color.FromArgb(255, 255, 200);
							dataWorksheet.Range["A1:D1"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

							dataWorksheet.Range["B1"].Text = "Goal";
							dataWorksheet.Range["B1"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

							dataWorksheet.Range["C1"].Text = "First Term";
							dataWorksheet.Range["C1"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

							dataWorksheet.Range["D1"].Text = "Second Term";
							dataWorksheet.Range["D1"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


							var progressStrands = iepProgressReportDto.ProgressReportStrands.ToList();
							for (int i = 0; i < progressStrands.Count(); i++)
							{
								dataWorksheet.Range["A" + (i + 2)].Text = progressStrands[i].StrandName;
								dataWorksheet.Range["A" + (i + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								dataWorksheet.Range["A" + (i + 2) + ":D" + (i + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

								dataWorksheet.Range["B" + (i + 2)].Number = (double)(progressStrands[i].GoalLongTermNumber == null ? 0 : progressStrands[i].GoalLongTermNumber);
								dataWorksheet.Range["B" + (i + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								dataWorksheet.Range["C" + (i + 2)].Number = (double)(progressStrands[i].FirstTermPercentage == null ? 0 : progressStrands[i].FirstTermPercentage);
								dataWorksheet.Range["C" + (i + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								dataWorksheet.Range["D" + (i + 2)].Number = (double)(progressStrands[i].SecondTermPercentage == null ? 0 : progressStrands[i].SecondTermPercentage);
								dataWorksheet.Range["D" + (i + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

							}


							IChartShape chart = worksheet.Charts.Add();
							chart.ChartType = ExcelChartType.Column_Clustered;

							IChartSerie goal = chart.Series.Add("Goal");
							goal.Values = dataWorksheet.Range["B2:B" + (iepProgressReportDto.ProgressReportStrands.Count() + 1)];
							goal.CategoryLabels = dataWorksheet.Range["A2:A" + (iepProgressReportDto.ProgressReportStrands.Count() + 1)];
							goal.SerieFormat.Fill.ForeColor = Color.FromArgb(79, 129, 189);


							IChartSerie first = chart.Series.Add("First Term");
							first.Values = dataWorksheet.Range["C2:C" + (iepProgressReportDto.ProgressReportStrands.Count() + 1)];
							first.CategoryLabels = dataWorksheet.Range["A2:A" + (iepProgressReportDto.ProgressReportStrands.Count() + 1)];
							first.SerieFormat.Fill.ForeColor = Color.FromArgb(192, 80, 77);

							IChartSerie second = chart.Series.Add("Second Term");
							second.Values = dataWorksheet.Range["D2:D" + (iepProgressReportDto.ProgressReportStrands.Count() + 1)];
							second.CategoryLabels = dataWorksheet.Range["A2:A" + (iepProgressReportDto.ProgressReportStrands.Count() + 1)];
							//second.SerieFormat.Fill.FillType = ExcelFillType.Gradient;
							second.SerieFormat.Fill.ForeColor = Color.FromArgb(155, 187, 89);
							//second.SerieFormat.CommonSerieOptions.GapWidth = 15;

							chart.PrimaryValueAxis.MaximumValue = 90;
							chart.PrimaryValueAxis.MinimumValue = 0;
							chart.PrimaryValueAxis.MajorUnit = 10;

							chart.TopRow = 2;
							chart.LeftColumn = 41;
							chart.RightColumn = 75;
							chart.BottomRow = 27;
							chart.HasTitle = false;
						}
						else
						{
							IChartShape chart = worksheet.Charts.Add();
							chart.ChartType = ExcelChartType.Column_Clustered;

							IChartSerie goal = chart.Series.Add("Goal");

							IChartSerie first = chart.Series.Add("First Term");

							IChartSerie second = chart.Series.Add("Second Term");

							chart.PrimaryValueAxis.MaximumValue = 90;
							chart.PrimaryValueAxis.MinimumValue = 0;
							chart.PrimaryValueAxis.MajorUnit = 10;

							chart.TopRow = 2;
							chart.LeftColumn = 41;
							chart.RightColumn = 75;
							chart.BottomRow = 27;
							chart.HasTitle = false;
						}
						#endregion
						#region StudentInfo
						worksheet.Range["AO30:BV32"].Merge();
						worksheet.Range["AO30:BV32"].Text = acadmicYearName;
						worksheet.Range["AO30:BV32"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AO30:BV30"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AO30:BV30"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AO30:AO47"].CellStyle.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AY30:AY47"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BV30:BV47"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AO33:AY35"].Merge();
						worksheet.Range["AO33:AY35"].Text = "Student Name:";
						worksheet.Range["AO35:BV35"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AZ33:BV35"].Merge();
						worksheet.Range["AZ33:BV35"].Text = studentName;

						worksheet.Range["AO36:AY38"].Merge();
						worksheet.Range["AO36:AY38"].Text = "Date of Birth:";
						worksheet.Range["AO36:BV38"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AZ36:BV38"].Merge();
						worksheet.Range["AZ36:BV38"].Text = dateOfBirthName;

						worksheet.Range["AO39:AY41"].Merge();
						worksheet.Range["AO39:AY41"].Text = "Ref.No:";
						worksheet.Range["AO39:BV41"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AZ39:BV41"].Merge();
						worksheet.Range["AZ39:BV41"].Text = studentCodeName;

						worksheet.Range["AO42:AY44"].Merge();
						worksheet.Range["AO42:AY44"].Text = "Department:";
						worksheet.Range["AO42:BV44"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AZ42:BV44"].Merge();
						worksheet.Range["AZ42:BV44"].Text = studentDepartmentName;

						worksheet.Range["AO45:AY47"].Merge();
						worksheet.Range["AO45:AY47"].Text = "Term:";
						worksheet.Range["AO45:BV47"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AZ45:BV47"].Merge();
						worksheet.Range["AZ45:BV47"].Text = termName;

						#endregion
						#region Images
						//Adding a picture
						FileStream SysFile = new FileStream("wwwroot/staticFiles/SysLogo.jpeg", FileMode.Open, FileAccess.Read);
						IPictureShape SysShape = worksheet.Pictures.AddPicture(50, 48, SysFile, 60, 60);

						FileStream CisFile = new FileStream("wwwroot/staticFiles/CisLogo.png", FileMode.Open, FileAccess.Read);
						IPictureShape CisShap = worksheet.Pictures.AddPicture(70, 41, CisFile, 20, 20);

						FileStream AutFile = new FileStream("wwwroot/staticFiles/AutLogo.jpg", FileMode.Open, FileAccess.Read);
						IPictureShape AutShap = worksheet.Pictures.AddPicture(69, 57, AutFile, 35, 35);

						FileStream AFile = new FileStream("wwwroot/staticFiles/ALogo.jpg", FileMode.Open, FileAccess.Read);
						IPictureShape AShap = worksheet.Pictures.AddPicture(69, 67, AFile, 20, 20);
						#endregion

					}
					else
					{
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}
					#region General
					lastRow = worksheet.Rows.Length;
					worksheet.IsGridLinesVisible = true;
					worksheet.Range["A1:BX1"].ColumnWidth = 1;
					worksheet.Range["A1:A" + (lastRow)].RowHeight = 10;
					worksheet.Range["A1:BV" + (lastRow)].WrapText = true;
					worksheet.Range["A1:BV" + (lastRow)].CellStyle.Font.Bold = true;
					worksheet.Range["A1:BV" + (lastRow)].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
					worksheet.Range["A1:BV" + (lastRow)].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
					worksheet.Range["A1:BV" + (lastRow)].CellStyle.Font.Size = 9;
					#endregion

					MemoryStream stream = new MemoryStream();
					workbook.SaveAs(stream);
					stream.Position = 0;
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = ("ProgressReport" + ".xlsx");
					return fileStreamResult;
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		public FileStreamResult ItpReport(int itpId)
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;
					int lastRow = 0;

					var itp = _uow.GetRepository<Itp>().Single(x => x.Id == itpId && x.IsDeleted != true, null,
						x => x.Include(x => x.Student).ThenInclude(x => x.Department)
					.Include(x => x.Student).ThenInclude(x => x.Teacher)
					.Include(x => x.Therapist).Include(x => x.TherapistDepartment)
					.Include(x => x.AcadmicYear).Include(x => x.Term)
					.Include(x => x.HeadOfEducation)
					.Include(x => x.ParamedicalService)
					.Include(x => x.ItpGoals.Where(x => x.IsDeleted != true)).ThenInclude(x => x.ItpGoalObjectives.Where(x => x.IsDeleted != true)));
					//var mapper = _mapper.Map<ItpDto>(itp);

					IWorkbook workbook = application.Workbooks.Create(0);
					IWorksheet worksheet;
					string studentName = "";

					if (itp != null)
					{
						string studentTeacherName = "";
						string itpTherapistName = "";
						string itpHeadOfEducationName = "";
						string acadmicYearName = "";
						string termName = "";
						string dateOfBirthName = "";
						string studentCodeName = "";
						string studentDepartmentName = "";
						string therapistDepartmentName = "";
						if (itp.Student != null)
						{
							studentName = itp.Student.Name == null ? "" : itp.Student.Name;
							studentDepartmentName = itp.Student.Department == null ? "" : itp.Student.Department.Name == null ? "" : itp.Student.Department.Name;
							studentTeacherName = itp.Student.Teacher == null ? "" : itp.Student.Teacher.Name == null ? "" : itp.Student.Teacher.Name;
							dateOfBirthName = itp.Student.DateOfBirth == null ? "" : itp.Student.DateOfBirth.Value.ToShortDateString();
							studentCodeName = itp.Student.Code == null ? "" : itp.Student.Code.ToString();
						}

						if (itp.AcadmicYear != null)
							acadmicYearName = itp.AcadmicYear.Name == null ? "" : itp.AcadmicYear.Name;
						else
							acadmicYearName = "Sheet1";
						if (itp.Therapist != null)
						{
							itpTherapistName = itp.Therapist.Name == null ? "" : itp.Therapist.Name;
							therapistDepartmentName = itp.TherapistDepartment == null ? "" : itp.TherapistDepartment.Name;
						}

						if (itp.HeadOfEducation != null)
						{
							itpHeadOfEducationName = itp.HeadOfEducation.Name == null ? "" : itp.HeadOfEducation.Name;
						}
						if (itp.Term != null)
						{
							termName = itp.Term.Name == null ? "" : itp.Term.Name;
						}
						worksheet = workbook.Worksheets.Create(acadmicYearName);
						#region General
						//Disable gridlines in the worksheet
						worksheet.IsGridLinesVisible = true;
						worksheet.Range["A1:BE1"].ColumnWidth = 1;
						worksheet.Range["A1"].RowHeight = 17;

						#endregion
						#region ITP
						worksheet.Range["A1:BE12"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE1:BE12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AH2:AH4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AL2:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["J3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AS2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AW2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["R5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["A1:BE1"].Merge();
						worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";
						worksheet.Range["A2:AH2"].Merge();
						worksheet.Range["A2:AH2"].Text = "INDIVIDUAL THERAPY PLAN (ITP)";
						worksheet.Range["A2:AH2"].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["AI2:AL2"].Merge();
						worksheet.Range["AI2:AL2"].Text = "YEAR:";
						worksheet.Range["AI2:AL2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AM2:AS2"].Merge();
						worksheet.Range["AM2:AS2"].Text = acadmicYearName;
						worksheet.Range["AT2:AW2"].Merge();
						worksheet.Range["AT2:AW2"].Text = "Term:";
						worksheet.Range["AT2:AW2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AX2:BE2"].Merge();
						worksheet.Range["AX2:BE2"].Text = termName;


						worksheet.Range["A3:J3"].Merge();
						worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
						worksheet.Range["A3:J3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["K3:AH3"].Merge();
						worksheet.Range["K3:AH3"].Text = studentName;
						worksheet.Range["AI3:AL3"].Merge();
						worksheet.Range["AI3:AL3"].Text = "D.O.B:";
						worksheet.Range["AI3:AL3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AM3:AV3"].Merge();
						worksheet.Range["AM3:AV3"].Text = dateOfBirthName;
						worksheet.Range["AW3:AZ3"].Merge();
						worksheet.Range["AW3:AZ3"].Text = "REF#:";
						worksheet.Range["AW3:AZ3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["BA3:BE3"].Merge();
						worksheet.Range["BA3:BE3"].Text = studentCodeName;

						worksheet.Range["A4:J4"].Merge();
						worksheet.Range["A4:J4"].Text = "Therapist:";
						worksheet.Range["A4:J4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["K4:AH4"].Merge();
						worksheet.Range["K4:AH4"].Text = itpTherapistName;
						worksheet.Range["AI4:AO4"].Merge();
						worksheet.Range["AI4:AO4"].Text = "Therapist Dep:";
						worksheet.Range["AI4:AO4"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AI4:AO4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["AP4:BE4"].Merge();
						worksheet.Range["AP4:BE4"].Text = therapistDepartmentName;
						worksheet.Range["AP4:BE4"].Merge();

						worksheet.Range["A5:J5"].Merge();
						worksheet.Range["A5:J5"].Text = "Date oF Preparation:";
						worksheet.Range["A5:J5"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["J5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["k5:AH5"].Merge();
						worksheet.Range["k5:AH5"].Text = itp.DateOfPreparation == null ? "" : itp.DateOfPreparation.Value.ToShortDateString();
						worksheet.Range["AH5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["AI5:AL5"].Merge();
						worksheet.Range["AI5:AL5"].Text = "Teacher:";
						worksheet.Range["AI5:AL5"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AL5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["AM5:AV5"].Merge();
						worksheet.Range["AM5:AV5"].Text = studentTeacherName;
						worksheet.Range["AV5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["Aw5:AZ5"].Merge();
						worksheet.Range["Aw5:AZ5"].Text = "Dep:";
						worksheet.Range["Aw5:AZ5"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AZ5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["BA5:BE5"].Merge();
						worksheet.Range["BA5:BE5"].Text = studentDepartmentName;


						worksheet.Range["A6:BE6"].Merge();
						worksheet.Range["A6:BE6"].Text = itp.ParamedicalService == null ? "" : itp.ParamedicalService.Name == null ? "" : itp.ParamedicalService.Name;

						worksheet.Range["A6:BE6"].CellStyle.Color = Color.FromArgb(230, 214, 242);

						worksheet.Range["A7:BE7"].Merge();
						worksheet.Range["A7:BE7"].Text = "Current Level";
						worksheet.Range["A7:BE7"].CellStyle.Color = Color.FromArgb(255, 255, 200);

						worksheet.Range["A8:BE12"].Merge();
						worksheet.Range["A8:BE12"].Text = itp.CurrentLevel == null ? "" : itp.CurrentLevel;

						#endregion

						lastRow = worksheet.Rows.Length;
						if (itp.ItpGoals.Count() > 0)
						{
							int goalCount = 1;
							foreach (var goal in itp.ItpGoals)
							{
								#region Goals
								worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["BE" + (lastRow + 2) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 3)].Merge();
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 3)].Text = goalCount + " - " + "Goal:";
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(230, 214, 242);
								worksheet.Range["A" + (lastRow + 2) + ":K" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["L" + (lastRow + 2) + ":BE" + (lastRow + 3)].Merge();
								worksheet.Range["L" + (lastRow + 2) + ":BE" + (lastRow + 3)].Text = goal.Goal == null ? "" : goal.Goal;
								goalCount++;
								#endregion
								#region Objectives
								if (goal.ItpGoalObjectives != null && goal.ItpGoalObjectives.Count() > 0)
								{
									var goalObjectives = goal.ItpGoalObjectives.Where(x => x.IsDeleted != true).ToList();
									if (goalObjectives.Count() > 0)
									{
										int noOfObj = 1;
										lastRow = worksheet.Rows.Length;
										foreach (var objective in goalObjectives)
										{

											//worksheet.Range["A" + (lastRow + 1) + ":AE300" ].CellStyle.Font.Size = 9;
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].Merge();
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].Text = noOfObj.ToString();
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].CellStyle.Color = Color.FromArgb(255, 255, 200);
											worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



											worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].Merge();
											worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].Text = "Objective  " + noOfObj;
											worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
											worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

											worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
											worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = "Approach/ Resources :";
											worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
											worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

											worksheet.Range["B" + (lastRow + 2) + ":AD" + (lastRow + 5)].Merge();
											worksheet.Range["B" + (lastRow + 2) + ":AD" + (lastRow + 5)].Text = objective.ObjectiveNote == null ? "" : objective.ObjectiveNote;
											worksheet.Range["B" + (lastRow + 2) + ":AD" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

											worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 5)].Merge();
											worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 5)].Text = objective.ResourcesRequired == null ? "" : objective.ResourcesRequired;
											worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
											worksheet.Range["A" + (lastRow + 5) + ":BE" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

											noOfObj++;
											lastRow = lastRow + 5;
										}
									}
									lastRow = worksheet.Rows.Length;
								}
								else
								{
									lastRow = worksheet.Rows.Length;

									worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].Merge();
									worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].Text = " No Objects Found";
									worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									lastRow = worksheet.Rows.Length + 3;
								}

								#endregion
								lastRow = worksheet.Rows.Length;
							}
						}
						else
						{
							lastRow = worksheet.Rows.Length;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Merge();
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = " No Goals Found";
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							lastRow = worksheet.Rows.Length;

						}

						#region FooterNote
						lastRow = worksheet.Rows.Length;

						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = itp.StudentNotes;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						lastRow = worksheet.Rows.Length;
						#endregion
						#region Footer
						lastRow = worksheet.Rows.Length;
						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].Text = "Report Card";
						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["A" + (lastRow + 2) + ":G" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["H" + (lastRow + 2) + ":I" + (lastRow + 2)].Merge();
						worksheet.Range["H" + (lastRow + 2) + ":I" + (lastRow + 2)].Text = itp.ReportCard == false ? "✘" : itp.ReportCard == true ? "✔" : "";
						worksheet.Range["H" + (lastRow + 2) + ":I" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].Merge();
						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].Text = "Progress Report";
						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["J" + (lastRow + 2) + ":W" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["X" + (lastRow + 2) + ":Y" + (lastRow + 2)].Merge();
						worksheet.Range["X" + (lastRow + 2) + ":Y" + (lastRow + 2)].Text = itp.ProgressReport == false ? "✘" : itp.ProgressReport == true ? "✔" : "";
						worksheet.Range["X" + (lastRow + 2) + ":Y" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].Merge();
						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].Text = "Parents meeting";
						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["Z" + (lastRow + 2) + ":AM" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AN" + (lastRow + 2) + ":AO" + (lastRow + 2)].Merge();
						worksheet.Range["AN" + (lastRow + 2) + ":AO" + (lastRow + 2)].Text = itp.ParentsMeeting == false ? "✘" : itp.ParentsMeeting == true ? "✔" : "";
						worksheet.Range["AN" + (lastRow + 2) + ":AO" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].Merge();
						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].Text = "Other";
						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AP" + (lastRow + 2) + ":BC" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["BD" + (lastRow + 2) + ":BE" + (lastRow + 2)].Merge();
						worksheet.Range["BD" + (lastRow + 2) + ":BE" + (lastRow + 2)].Text = itp.Others == false ? "✘" : itp.Others == true ? "✔" : "";
						worksheet.Range["BD" + (lastRow + 2) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].Merge();
						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].Text = "Parents Involved in setting up suggestions";
						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["A" + (lastRow + 3) + ":Y" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["Z" + (lastRow + 3) + ":BE" + (lastRow + 3)].Merge();
						worksheet.Range["Z" + (lastRow + 3) + ":BE" + (lastRow + 3)].Text = itp.ParentsInvolvedInSettingUpSuggestions == false ? "" : itp.ParentsInvolvedInSettingUpSuggestions == true ? "Yes, refer parent meeting record form" : "";
						worksheet.Range["Z" + (lastRow + 3) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].Merge();
						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].Text = "Date of Review";
						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["A" + (lastRow + 4) + ":Y" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["Z" + (lastRow + 4) + ":BE" + (lastRow + 4)].Merge();
						worksheet.Range["Z" + (lastRow + 4) + ":BE" + (lastRow + 4)].Text = itp.LastDateOfReview == null ? "" : itp.LastDateOfReview.Value.ToShortDateString();
						worksheet.Range["Z" + (lastRow + 4) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["A" + (lastRow + 6) + ":H" + (lastRow + 6)].Merge();
						worksheet.Range["A" + (lastRow + 6) + ":H" + (lastRow + 6)].Text = "Therapist :";
						worksheet.Range["A" + (lastRow + 6) + ":H" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["I" + (lastRow + 6) + ":S" + (lastRow + 6)].Merge();
						worksheet.Range["I" + (lastRow + 6) + ":S" + (lastRow + 6)].Text = itpTherapistName;

						worksheet.Range["T" + (lastRow + 6) + ":AA" + (lastRow + 6)].Merge();
						worksheet.Range["T" + (lastRow + 6) + ":AA" + (lastRow + 6)].Text = "Parent:";
						worksheet.Range["T" + (lastRow + 6) + ":AA" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AB" + (lastRow + 6) + ":AL" + (lastRow + 6)].Merge();
						worksheet.Range["AB" + (lastRow + 6) + ":AL" + (lastRow + 6)].Text = "";

						worksheet.Range["AM" + (lastRow + 6) + ":AT" + (lastRow + 6)].Merge();
						worksheet.Range["AM" + (lastRow + 6) + ":AT" + (lastRow + 6)].Text = "HOE:";
						worksheet.Range["AM" + (lastRow + 6) + ":AT" + (lastRow + 6)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AU" + (lastRow + 6) + ":BE" + (lastRow + 6)].Merge();
						worksheet.Range["AU" + (lastRow + 6) + ":BE" + (lastRow + 6)].Text = itpHeadOfEducationName;


						worksheet.Range["A" + (lastRow + 7) + ":H" + (lastRow + 7)].Merge();
						worksheet.Range["A" + (lastRow + 7) + ":H" + (lastRow + 7)].Text = "Signature:";
						worksheet.Range["A" + (lastRow + 7) + ":H" + (lastRow + 7)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["I" + (lastRow + 7) + ":S" + (lastRow + 7)].Merge();
						worksheet.Range["I" + (lastRow + 7) + ":S" + (lastRow + 7)].Text = "";

						worksheet.Range["T" + (lastRow + 7) + ":AA" + (lastRow + 7)].Merge();
						worksheet.Range["T" + (lastRow + 7) + ":AA" + (lastRow + 7)].Text = "Signature:";
						worksheet.Range["T" + (lastRow + 7) + ":AA" + (lastRow + 7)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AB" + (lastRow + 7) + ":AL" + (lastRow + 7)].Merge();
						worksheet.Range["AB" + (lastRow + 7) + ":AL" + (lastRow + 7)].Text = "";

						worksheet.Range["AM" + (lastRow + 7) + ":AT" + (lastRow + 7)].Merge();
						worksheet.Range["AM" + (lastRow + 7) + ":AT" + (lastRow + 7)].Text = "Signature:";
						worksheet.Range["AM" + (lastRow + 7) + ":AT" + (lastRow + 7)].CellStyle.Color = Color.FromArgb(255, 205, 205);


						worksheet.Range["AU" + (lastRow + 7) + ":BE" + (lastRow + 7)].Merge();
						worksheet.Range["AU" + (lastRow + 7) + ":BE" + (lastRow + 7)].Text = "";

						worksheet.Range["H" + (lastRow + 6) + ":H" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["S" + (lastRow + 6) + ":S" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AA" + (lastRow + 6) + ":AA" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AL" + (lastRow + 6) + ":AL" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AT" + (lastRow + 6) + ":AT" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 6) + ":BE" + (lastRow + 7)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["Y" + (lastRow + 2) + ":Y" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 2) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						#endregion
					}
					else
					{
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}
					lastRow = worksheet.Rows.Length;
					worksheet.Range["A1:BE" + (lastRow)].WrapText = true;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.Font.Bold = true;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.Font.Size = 9;


					MemoryStream stream = new MemoryStream();
					//workbook.SaveAsHtml(stream, Syncfusion.XlsIO.Implementation.HtmlSaveOptions.Default);
					workbook.SaveAs(stream);
					stream.Position = 0;
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = (studentName + "-ITPReport" + ".xlsx");
					//fileStreamResult.FileDownloadName = ( "-ITPReport.html");


					return fileStreamResult;
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}
		public FileStreamResult ItpProgressReport(int itpProgressReportId)
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;
					int lastRow = 9;

					var itpProgressReport = _uow.GetRepository<ItpProgressReport>().Single(x => x.Id == itpProgressReportId && x.IsDeleted != true, null,
						x => x.Include(x => x.Student).ThenInclude(x => x.Department)
					.Include(x => x.Student).ThenInclude(x => x.Teacher)
					.Include(x => x.Therapist).ThenInclude(x => x.Department)
					.Include(x => x.AcadmicYear).Include(x => x.Term)
					.Include(x => x.HeadOfEducation)
					.Include(x => x.ParamedicalService)
					.Include(x => x.ItpObjectiveProgressReports).ThenInclude(x => x.ItpObjective));
					//var mapper = _mapper.Map<ItpDto>(itp);

					IWorkbook workbook = application.Workbooks.Create(0);
					IWorksheet worksheet;
					string studentName = "";

					if (itpProgressReport != null)
					{
						string studentTeacherName = "";
						string itpTherapistName = "";
						string itpHeadOfEducationName = "";
						string acadmicYearName = "";
						string termName = "";
						string dateOfBirthName = "";
						string studentCodeName = "";
						string studentDepartmentName = "";
						string therapistDepartmentName = "";
						if (itpProgressReport.Student != null)
						{
							studentName = itpProgressReport.Student.Name == null ? "" : itpProgressReport.Student.Name;
							studentDepartmentName = itpProgressReport.Student.Department == null ? "" : itpProgressReport.Student.Department.Name == null ? "" : itpProgressReport.Student.Department.Name;
							studentTeacherName = itpProgressReport.Student.Teacher == null ? "" : itpProgressReport.Student.Teacher.Name == null ? "" : itpProgressReport.Student.Teacher.Name;
							dateOfBirthName = itpProgressReport.Student.DateOfBirth == null ? "" : itpProgressReport.Student.DateOfBirth.Value.ToShortDateString();
							studentCodeName = itpProgressReport.Student.Code == null ? "" : itpProgressReport.Student.Code.ToString();
						}

						if (itpProgressReport.AcadmicYear != null)
							acadmicYearName = itpProgressReport.AcadmicYear.Name == null ? "" : itpProgressReport.AcadmicYear.Name;
						else
							acadmicYearName = "Sheet1";
						if (itpProgressReport.Therapist != null)
						{
							itpTherapistName = itpProgressReport.Therapist.Name == null ? "" : itpProgressReport.Therapist.Name;
							therapistDepartmentName = itpProgressReport.Therapist.Department == null ? "" : itpProgressReport.Therapist.Department.Name == null ? "" : itpProgressReport.Therapist.Department.Name;
						}

						if (itpProgressReport.HeadOfEducation != null)
						{
							itpHeadOfEducationName = itpProgressReport.HeadOfEducation.Name == null ? "" : itpProgressReport.HeadOfEducation.Name;
						}
						if (itpProgressReport.Term != null)
						{
							termName = itpProgressReport.Term.Name == null ? "" : itpProgressReport.Term.Name;
						}
						worksheet = workbook.Worksheets.Create(acadmicYearName);
						#region General
						//Disable gridlines in the worksheet
						worksheet.IsGridLinesVisible = true;
						worksheet.Range["A1:BE1"].ColumnWidth = 1;
						worksheet.Range["A1"].RowHeight = 17;
						#endregion

						#region General Data
						FileStream AllLogo = new FileStream("wwwroot/staticFiles/AllLogos.jpg", FileMode.Open, FileAccess.Read);
						IPictureShape AllLogoShape = worksheet.Pictures.AddPicture(1, 1, AllLogo, 107, 100);

						lastRow = worksheet.Rows.Length + 9;
						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].Text = "Therapy Progress Report " + Environment.NewLine + acadmicYearName;
						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(202, 215, 238);

						worksheet.Range["A" + (lastRow + 3) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						lastRow = worksheet.Rows.Length;
						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 13)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 1) + ":BE" + (lastRow + 13)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AB" + (lastRow + 1) + ":AB" + (lastRow + 8)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":AB" + (lastRow + 8)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["A" + (lastRow + 1) + ":AB" + (lastRow + 1)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":AB" + (lastRow + 1)].Text = "Student's Name:";

						worksheet.Range["AC" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
						worksheet.Range["AC" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = studentName;

						worksheet.Range["A" + (lastRow + 2) + ":AB" + (lastRow + 2)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":AB" + (lastRow + 2)].Text = "REF#:";

						worksheet.Range["AC" + (lastRow + 2) + ":BE" + (lastRow + 2)].Merge();
						worksheet.Range["AC" + (lastRow + 2) + ":BE" + (lastRow + 2)].Text = studentCodeName;

						worksheet.Range["A" + (lastRow + 3) + ":AB" + (lastRow + 3)].Merge();
						worksheet.Range["A" + (lastRow + 3) + ":AB" + (lastRow + 3)].Text = "Date Of Birth:";

						worksheet.Range["AC" + (lastRow + 3) + ":BE" + (lastRow + 3)].Merge();
						worksheet.Range["AC" + (lastRow + 3) + ":BE" + (lastRow + 3)].Text = dateOfBirthName;

						worksheet.Range["A" + (lastRow + 4) + ":AB" + (lastRow + 4)].Merge();
						worksheet.Range["A" + (lastRow + 4) + ":AB" + (lastRow + 4)].Text = "Therapist:";

						worksheet.Range["AC" + (lastRow + 4) + ":BE" + (lastRow + 4)].Merge();
						worksheet.Range["AC" + (lastRow + 4) + ":BE" + (lastRow + 4)].Text = itpTherapistName;

						worksheet.Range["A" + (lastRow + 5) + ":AB" + (lastRow + 5)].Merge();
						worksheet.Range["A" + (lastRow + 5) + ":AB" + (lastRow + 5)].Text = "Therapist Department:";

						worksheet.Range["AC" + (lastRow + 5) + ":BE" + (lastRow + 5)].Merge();
						worksheet.Range["AC" + (lastRow + 5) + ":BE" + (lastRow + 5)].Text = therapistDepartmentName;

						worksheet.Range["A" + (lastRow + 6) + ":AB" + (lastRow + 6)].Merge();
						worksheet.Range["A" + (lastRow + 6) + ":AB" + (lastRow + 6)].Text = "AcadmicYear:";

						worksheet.Range["AC" + (lastRow + 6) + ":BE" + (lastRow + 6)].Merge();
						worksheet.Range["AC" + (lastRow + 6) + ":BE" + (lastRow + 6)].Text = acadmicYearName;

						worksheet.Range["A" + (lastRow + 7) + ":AB" + (lastRow + 7)].Merge();
						worksheet.Range["A" + (lastRow + 7) + ":AB" + (lastRow + 7)].Text = "Term:";

						worksheet.Range["AC" + (lastRow + 7) + ":BE" + (lastRow + 7)].Merge();
						worksheet.Range["AC" + (lastRow + 7) + ":BE" + (lastRow + 7)].Text = termName;

						worksheet.Range["A" + (lastRow + 8) + ":AB" + (lastRow + 8)].Merge();
						worksheet.Range["A" + (lastRow + 8) + ":AB" + (lastRow + 8)].Text = "Date:";

						worksheet.Range["AC" + (lastRow + 8) + ":BE" + (lastRow + 8)].Merge();
						worksheet.Range["AC" + (lastRow + 8) + ":BE" + (lastRow + 8)].Text = itpProgressReport.Date == null ? "" : itpProgressReport.Date.Value.ToShortDateString();

						worksheet.Range["A" + (lastRow + 9) + ":BE" + (lastRow + 9)].Merge();
						worksheet.Range["A" + (lastRow + 9) + ":BE" + (lastRow + 9)].Text = "General Comment";
						worksheet.Range["A" + (lastRow + 9) + ":BE" + (lastRow + 9)].CellStyle.Color = Color.FromArgb(255, 255, 200);

						worksheet.Range["A" + (lastRow + 10) + ":BE" + (lastRow + 13)].Merge();
						worksheet.Range["A" + (lastRow + 10) + ":BE" + (lastRow + 13)].Text = itpProgressReport.GeneralComment == null ? "" : itpProgressReport.GeneralComment;
						#endregion
						#region Objectives

						lastRow = worksheet.Rows.Length;

						if (itpProgressReport.ItpObjectiveProgressReports != null && itpProgressReport.ItpObjectiveProgressReports.Count() > 0)
						{
							var objectives = itpProgressReport.ItpObjectiveProgressReports.ToList();
							if (objectives.Count() > 0)
							{
								int noOfObj = 1;
								lastRow = worksheet.Rows.Length + 1;
								foreach (var objective in objectives)
								{
									worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].Merge();
									worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].Text = noOfObj.ToString();
									worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].CellStyle.Color = Color.FromArgb(255, 255, 200);
									worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



									worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].Merge();
									worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].Text = "Objective  " + noOfObj;
									worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
									worksheet.Range["B" + (lastRow + 1) + ":AD" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;

									worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
									worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = "Achievement :";
									worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
									worksheet.Range["AE" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

									worksheet.Range["B" + (lastRow + 2) + ":AD" + (lastRow + 5)].Merge();
									worksheet.Range["B" + (lastRow + 2) + ":AD" + (lastRow + 5)].Text = objective.ItpObjective == null ? "" : objective.ItpObjective.ObjectiveNote == null ? "" : objective.ItpObjective.ObjectiveNote;
									worksheet.Range["B" + (lastRow + 2) + ":AD" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

									worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 5)].Merge();
									worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 5)].Text = objective.Comment == null ? "" : objective.Comment;
									worksheet.Range["AE" + (lastRow + 2) + ":BE" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									worksheet.Range["A" + (lastRow + 5) + ":BE" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

									noOfObj++;
									lastRow = lastRow + 5;
								}
							}
							lastRow = worksheet.Rows.Length;
						}
						else
						{
							lastRow = worksheet.Rows.Length + 1;

							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].Merge();
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].Text = " No Objects Found";
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							lastRow = worksheet.Rows.Length + 3;
						}
						#endregion
						#region signature
						lastRow = worksheet.Rows.Length + 1;

						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["H" + (lastRow + 1) + ":H" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AL" + (lastRow + 1) + ":AL" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AD" + (lastRow + 1) + ":AD" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 1) + ":BE" + (lastRow + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 1) + ":H" + (lastRow + 1)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":H" + (lastRow + 1)].Text = "Therapist:";
						worksheet.Range["A" + (lastRow + 1) + ":H" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["I" + (lastRow + 1) + ":AD" + (lastRow + 1)].Merge();
						worksheet.Range["I" + (lastRow + 1) + ":AD" + (lastRow + 1)].Text = itpTherapistName;



						worksheet.Range["AE" + (lastRow + 1) + ":AL" + (lastRow + 1)].Merge();
						worksheet.Range["AE" + (lastRow + 1) + ":AL" + (lastRow + 1)].Text = "H.O.E :";
						worksheet.Range["AE" + (lastRow + 1) + ":AL" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AM" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
						worksheet.Range["AM" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = itpHeadOfEducationName;

						worksheet.Range["A" + (lastRow + 2) + ":H" + (lastRow + 2)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":H" + (lastRow + 2)].Text = "Signature :";
						worksheet.Range["A" + (lastRow + 2) + ":H" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["I" + (lastRow + 2) + ":AD" + (lastRow + 2)].Merge();
						worksheet.Range["I" + (lastRow + 2) + ":AD" + (lastRow + 2)].Text = "";



						worksheet.Range["AE" + (lastRow + 2) + ":AL" + (lastRow + 2)].Merge();
						worksheet.Range["AE" + (lastRow + 2) + ":AL" + (lastRow + 2)].Text = "Signature :";
						worksheet.Range["AE" + (lastRow + 2) + ":AL" + (lastRow + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AM" + (lastRow + 2) + ":BE" + (lastRow + 2)].Merge();
						worksheet.Range["AM" + (lastRow + 2) + ":BE" + (lastRow + 2)].Text = "";
						#endregion


					}
					else
					{
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}
					lastRow = worksheet.Rows.Length;
					worksheet.Range["A1:BE" + (lastRow)].WrapText = true;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.Font.Bold = true;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.Font.Size = 11;


					MemoryStream stream = new MemoryStream();
					//workbook.SaveAsHtml(stream, Syncfusion.XlsIO.Implementation.HtmlSaveOptions.Default);
					workbook.SaveAs(stream);
					stream.Position = 0;
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = (studentName + "-ITPReport" + ".xlsx");
					//fileStreamResult.FileDownloadName = ( "-ITPReport.html");


					return fileStreamResult;
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}
		public FileStreamResult IxpReport(int ixpId)
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;
					int lastRow = 0;

					var ixp = _uow.GetRepository<Ixp>().Single(x => x.Id == ixpId && x.IsDeleted != true, null,
						x => x.Include(x => x.Student).ThenInclude(x => x.Department)
						.Include(x => x.Student).ThenInclude(x => x.Teacher)
					.Include(x => x.AcadmicYear).Include(x => x.Term)
					.Include(x => x.HeadOfEducation)
					.Include(x => x.IxpExtraCurriculars).ThenInclude(x => x.ExtraCurricular)
					.Include(x => x.IxpExtraCurriculars).ThenInclude(x => x.Teacher));

					IWorkbook workbook = application.Workbooks.Create(0);
					IWorksheet worksheet;
					string studentName = "";

					if (ixp != null)
					{
						string itpHeadOfEducationName = "";
						string acadmicYearName = "";
						string termName = "";
						string dateOfBirthName = "";
						string studentCodeName = "";
						string studentDepartmentName = "";
						string studentTeacherName = "";
						if (ixp.Student != null)
						{
							studentName = ixp.Student.Name == null ? "" : ixp.Student.Name;
							studentDepartmentName = ixp.Student.Department == null ? "" : ixp.Student.Department.Name == null ? "" : ixp.Student.Department.Name;
							dateOfBirthName = ixp.Student.DateOfBirth == null ? "" : ixp.Student.DateOfBirth.Value.ToShortDateString();
							studentCodeName = ixp.Student.Code == null ? "" : ixp.Student.Code.ToString();
							studentTeacherName = ixp.Student.Teacher == null ? "" : ixp.Student.Teacher.Name == null ? "" : ixp.Student.Teacher.Name;

						}

						if (ixp.AcadmicYear != null)
							acadmicYearName = ixp.AcadmicYear.Name == null ? "" : ixp.AcadmicYear.Name;
						else
							acadmicYearName = "Sheet1";

						if (ixp.HeadOfEducation != null)
						{
							itpHeadOfEducationName = ixp.HeadOfEducation.Name == null ? "" : ixp.HeadOfEducation.Name;
						}
						if (ixp.Term != null)
						{
							termName = ixp.Term.Name == null ? "" : ixp.Term.Name;
						}
						worksheet = workbook.Worksheets.Create(acadmicYearName);
						#region General
						//Disable gridlines in the worksheet
						worksheet.IsGridLinesVisible = true;
						worksheet.Range["A1:BE1"].ColumnWidth = 1;
						worksheet.Range["A1"].RowHeight = 17;

						#endregion
						#region IXP
						worksheet.Range["A1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AH2:AH4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AL2:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["J3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AS2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AW2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["R5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["A1:BE1"].Merge();
						worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";
						worksheet.Range["A2:AH2"].Merge();
						worksheet.Range["A2:AH2"].Text = "INDIVIDUAL ExtraCurricular PLAN";
						worksheet.Range["A2:AH2"].CellStyle.Color = Color.FromArgb(255, 255, 200);
						worksheet.Range["AI2:AL2"].Merge();
						worksheet.Range["AI2:AL2"].Text = "YEAR:";
						worksheet.Range["AI2:AL2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AM2:AS2"].Merge();
						worksheet.Range["AM2:AS2"].Text = acadmicYearName;
						worksheet.Range["AT2:AW2"].Merge();
						worksheet.Range["AT2:AW2"].Text = "Term:";
						worksheet.Range["AT2:AW2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AX2:BE2"].Merge();
						worksheet.Range["AX2:BE2"].Text = termName;


						worksheet.Range["A3:J3"].Merge();
						worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
						worksheet.Range["A3:J3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["K3:AH3"].Merge();
						worksheet.Range["K3:AH3"].Text = studentName;
						worksheet.Range["AI3:AL3"].Merge();
						worksheet.Range["AI3:AL3"].Text = "D.O.B:";
						worksheet.Range["AI3:AL3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["AM3:AV3"].Merge();
						worksheet.Range["AM3:AV3"].Text = dateOfBirthName;
						worksheet.Range["AW3:AZ3"].Merge();
						worksheet.Range["AW3:AZ3"].Text = "REF#:";
						worksheet.Range["AW3:AZ3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

						worksheet.Range["BA3:BE3"].Merge();
						worksheet.Range["BA3:BE3"].Text = studentCodeName;

						worksheet.Range["A4:J4"].Merge();
						worksheet.Range["A4:J4"].Text = "Date oF Preparation:";
						worksheet.Range["A4:J4"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["k4:Y4"].Merge();
						worksheet.Range["k4:Y4"].Text = ixp.DateOfPreparation == null ? "" : ixp.DateOfPreparation.Value.ToShortDateString();
						worksheet.Range["Y4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["Z4:AH4"].Merge();
						worksheet.Range["Z4:AH4"].Text = "Teacher:";
						worksheet.Range["Z4:AH4"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AH4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["AI4:AS4"].Merge();
						worksheet.Range["AI4:AS4"].Text = studentTeacherName;
						worksheet.Range["AS4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AT4:AW4"].Merge();
						worksheet.Range["AT4:AW4"].Text = "Dep:";
						worksheet.Range["AT4:AW4"].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AW4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


						worksheet.Range["AX4:BE4"].Merge();
						worksheet.Range["AX4:BE4"].Text = studentDepartmentName;


						#endregion

						lastRow = worksheet.Rows.Length;
						if (ixp.IxpExtraCurriculars.Count() > 0)
						{
							int extraCurricularCount = 1;
							foreach (var extra in ixp.IxpExtraCurriculars)
							{
								#region ExtraCurriculars
								worksheet.Range["A" + (lastRow) + ":BE" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["BE" + (lastRow + 1) + ":BE" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["W" + (lastRow + 1) + ":W" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["AL" + (lastRow + 1) + ":AL" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["AV" + (lastRow + 1) + ":AV" + (lastRow + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].Merge();
								worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].Text = extraCurricularCount.ToString();
								worksheet.Range["A" + (lastRow + 1) + ":A" + (lastRow + 5)].CellStyle.Color = Color.FromArgb(255, 255, 200);



								worksheet.Range["B" + (lastRow + 1) + ":W" + (lastRow + 1)].Merge();
								worksheet.Range["B" + (lastRow + 1) + ":W" + (lastRow + 1)].Text = extra.ExtraCurricular == null ? "" : extra.ExtraCurricular.Name == null ? "" : extra.ExtraCurricular.Name;
								worksheet.Range["B" + (lastRow + 1) + ":W" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

								worksheet.Range["X" + (lastRow + 1) + ":AL" + (lastRow + 1)].Merge();
								worksheet.Range["X" + (lastRow + 1) + ":AL" + (lastRow + 1)].Text = "Strategies/ Resources  :";
								worksheet.Range["X" + (lastRow + 1) + ":AL" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

								worksheet.Range["AM" + (lastRow + 1) + ":AV" + (lastRow + 1)].Merge();
								worksheet.Range["AM" + (lastRow + 1) + ":AV" + (lastRow + 1)].Text = "Indication :";
								worksheet.Range["AM" + (lastRow + 1) + ":AV" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

								worksheet.Range["AW" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
								worksheet.Range["AW" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = "Date :";
								worksheet.Range["AW" + (lastRow + 1) + ":BE" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);




								worksheet.Range["B" + (lastRow + 2) + ":W" + (lastRow + 5)].Merge();
								worksheet.Range["B" + (lastRow + 2) + ":W" + (lastRow + 5)].Text = extra.Goal == null ? "" : extra.Goal;

								worksheet.Range["X" + (lastRow + 2) + ":AL" + (lastRow + 5)].Merge();
								worksheet.Range["X" + (lastRow + 2) + ":AL" + (lastRow + 5)].Text = extra.Strategy == null ? "" : extra.Strategy;

								worksheet.Range["AM" + (lastRow + 2) + ":AV" + (lastRow + 5)].Merge();
								worksheet.Range["AM" + (lastRow + 2) + ":AV" + (lastRow + 5)].Text = extra.Indication == 0 ? "Not Met" : extra.Indication == 1 ? "Partially Met" : extra.Indication == 2 ? "Fully Met" : extra.Indication == 3 ? "Exceeded" : "";

								worksheet.Range["AW" + (lastRow + 2) + ":BE" + (lastRow + 5)].Merge();
								worksheet.Range["AW" + (lastRow + 2) + ":BE" + (lastRow + 5)].Text = extra.Date == null ? "" : extra.Date.Value.ToShortDateString();

								extraCurricularCount++;
								lastRow = lastRow + 6;

								#endregion

							}
						}
						else
						{
							lastRow = worksheet.Rows.Length;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Merge();
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = " No Goals Found";
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							lastRow = worksheet.Rows.Length;

						}

						#region FooterNote
						lastRow = worksheet.Rows.Length;

						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Merge();
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = ixp.StudentNotes;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE" + (lastRow + 2) + ":BE" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						lastRow = worksheet.Rows.Length;
						#endregion
						#region Footer
						lastRow = worksheet.Rows.Length + 1;
						worksheet.Range["A" + (lastRow) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A" + (lastRow + 1) + ":W" + (lastRow + 1)].Merge();
						worksheet.Range["A" + (lastRow + 1) + ":W" + (lastRow + 1)].Text = "Parents Involved in setting up suggestions";
						worksheet.Range["A" + (lastRow + 1) + ":W" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["W" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["X" + (lastRow + 1) + ":Y" + (lastRow + 1)].Merge();
						worksheet.Range["X" + (lastRow + 1) + ":Y" + (lastRow + 1)].Text = ixp.ParentsInvolvedInSettingUpSuggestions == false ? "✘" : ixp.ParentsInvolvedInSettingUpSuggestions == true ? "✔" : ""; ;
						worksheet.Range["Y" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						worksheet.Range["Z" + (lastRow + 1) + ":AM" + (lastRow + 1)].Merge();
						worksheet.Range["Z" + (lastRow + 1) + ":AM" + (lastRow + 1)].Text = "Date Of Review";
						worksheet.Range["Z" + (lastRow + 1) + ":AM" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);
						worksheet.Range["AM" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["AN" + (lastRow + 1) + ":BE" + (lastRow + 1)].Merge();
						worksheet.Range["AN" + (lastRow + 1) + ":BE" + (lastRow + 1)].Text = ixp.LastDateOfReview == null ? "" : ixp.LastDateOfReview.Value.ToShortDateString();
						worksheet.Range["BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



						if (ixp.IxpExtraCurriculars.Count() > 0)
						{
							lastRow = worksheet.Rows.Length + 1;

							foreach (var extra in ixp.IxpExtraCurriculars)
							{
								worksheet.Range["A" + (lastRow) + ":J" + (lastRow)].Merge();
								worksheet.Range["A" + (lastRow) + ":J" + (lastRow)].Text = "Teacher Of :" + extra.ExtraCurricular == null ? "" : extra.ExtraCurricular.Name == null ? "" : extra.ExtraCurricular.Name; ;
								worksheet.Range["A" + (lastRow) + ":J" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["J" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


								worksheet.Range["K" + (lastRow) + ":Y" + (lastRow)].Merge();
								worksheet.Range["K" + (lastRow) + ":Y" + (lastRow)].Text = extra.Teacher == null ? "" : extra.Teacher.Name == null ? "" : extra.Teacher.Name; ;
								worksheet.Range["A" + (lastRow) + ":BE" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["Y" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


								worksheet.Range["Z" + (lastRow) + ":AM" + (lastRow)].Merge();
								worksheet.Range["Z" + (lastRow) + ":AM" + (lastRow)].Text = "Signature";
								worksheet.Range["Z" + (lastRow) + ":AM" + (lastRow)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["AM" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


								worksheet.Range["AN" + (lastRow) + ":BE" + (lastRow)].Merge();
								worksheet.Range["AN" + (lastRow) + ":BE" + (lastRow)].Text = "";
								worksheet.Range["BE" + (lastRow)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								lastRow++;

							}
						}
						#endregion
					}
					else
					{
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}
					lastRow = worksheet.Rows.Length;
					worksheet.Range["A1:BF" + (lastRow)].WrapText = true;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.Font.Bold = true;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
					worksheet.Range["A1:BF" + (lastRow)].CellStyle.Font.Size = 9;


					MemoryStream stream = new MemoryStream();
					workbook.SaveAs(stream);
					stream.Position = 0;
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = (studentName + "-IXPReport" + ".xlsx");
					return fileStreamResult;
				}
			}
			catch (Exception ex)
			{

				throw;
			}
		}
	}
}
