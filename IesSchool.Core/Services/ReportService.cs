using AutoMapper;
using IesSchool.Context.Models;
using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using IesSchool.InfraStructure;
using IesSchool.InfraStructure.Paging;
using Microsoft.EntityFrameworkCore;
using Syncfusion.XlsIO;
using System.IO;
using Syncfusion.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace IesSchool.Core.Services
{
	internal class ReportService : IReportService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private IHostingEnvironment _hostingEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ReportService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)

		{
			_uow = unitOfWork;
			_mapper = mapper;
			_hostingEnvironment = hostingEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}
		public ResponseDto GetReporstHelper()
		{
			try
			{
				ReportsHelper reportsHelper = new ReportsHelper()
				{
					AllAreas = _uow.GetRepository<Area>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), null, 0, 100000, true),
					AllStrands = _uow.GetRepository<Strand>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.Name), null, 0, 1000000, true),
				};
				var mapper = _mapper.Map<ReportsHelperDto>(reportsHelper);
				return new ResponseDto { Status = 1, Message = "Success", Data = mapper };
			}

			catch (Exception ex)
			{
				return new ResponseDto { Status = 0, Errormessage = " Error", Data = ex };
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
								if (AllIVwSkills != null && AllIVwSkills.Count > 0)
								{
									areaName = AllIVwSkills.Items.First()?.AreaName == null ? "" : AllIVwSkills.Items.First().AreaName;
									strandName = AllIVwSkills.Items.First()?.StrandName == null ? "" : AllIVwSkills.Items.First().StrandName;
									skills = (listOfObjSkillsIds == null ? "" : string.Join(",", listOfObjSkillsIds));
								}
							}
							worksheet = workbook.Worksheets.Create(noOfObjectives + "-" + strandName + "(" + skills + ")");
							#region General
							//Disable gridlines in the worksheet
							worksheet.IsGridLinesVisible = true;
							worksheet.Range["A1:BE100"].WrapText = true;
							worksheet.Range["A1:BE100"].CellStyle.Font.Bold = true;
							worksheet.Range["A1:BE13"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
							worksheet.Range["A1:BE13"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
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
                            if (objective.ObjectiveSkills.Count() > 0 && skills!="")
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
						worksheet.Range["A1:BE1"].ColumnWidth = 1;
						worksheet.Range["A1"].RowHeight = 17;
						worksheet.Range["A1:BE100"].WrapText = true;
						worksheet.Range["A1:BE100"].CellStyle.Font.Bold = true;
						worksheet.Range["A1:BE13"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
						worksheet.Range["A1:BE13"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
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
					int currentRowForGoals = 0;
					int currentRowForObjectives = 0;
					int currentRowPara = 0;
					int currentRowExtr = 0;

					var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x=>x.Include(x=> x.Student).ThenInclude(x => x.Department)
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

					int noOfGoals = 1;
					if (iep != null)
					{
						string studentName = "";
						string	 studentTeacherName = "";
						string	 iepTeacherName = "";
						string	 iepHeadOfDepartmentName = "";
						string	 iepHeadOfEducationName = "";
						string	 acadmicYearName = "";
						string	termName = "";
						string	dateOfBirthName = "";
						string	studentCodeName = "";
						string studentDepartmentName = "";
                        if (iep.Student!=null)
                        {
							studentName = iep.Student.Name ==null ? "" : iep.Student.Name;
							studentDepartmentName = iep.Student.Department ==null ? "" : iep.Student.Department.Name == null ? "" : iep.Student.Department.Name;
							studentTeacherName = iep.Student.Teacher ==null ? "" : iep.Student.Teacher.Name == null ? "" : iep.Student.Teacher.Name;
							dateOfBirthName = iep.Student.DateOfBirth ==null ? "" : iep.Student.DateOfBirth.Value.ToShortDateString();
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
						worksheet = workbook.Worksheets.Create(acadmicYearName );
						#region General
						//Disable gridlines in the worksheet
						worksheet.IsGridLinesVisible = true;
						worksheet.Range["A1:BE1"].ColumnWidth = 1;
						worksheet.Range["A1"].RowHeight = 17;
						worksheet.Range["A1:BE300"].WrapText = true;
						worksheet.Range["A1:BE300"].CellStyle.Font.Bold = true;
						worksheet.Range["A1:BE300"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
						worksheet.Range["A1:BE300"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
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
						worksheet.Range["A1:BE300"].CellStyle.Font.Size = 10;


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
                        if (iep.IepAssistants!=null && iep.IepAssistants.Count()>0)
                        {
							var iepAssistants = iep.IepAssistants.ToList().Select(x => (x.Assistant == null ? "" : x.Assistant.Name)).ToArray();
							worksheet.Range["S5:BE5"].Text = studentTeacherName + "," +string.Join(",", iepAssistants);
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
						var iepGoals = _uow.GetRepository<Goal>().GetList(x => x.Iepid == iepId && x.IsDeleted != true, null, x => x.Include(x => x.Strand).Include(x => x.Area).Include(x => x.Objectives).ThenInclude(x => x.ObjectiveSkills).Include(x => x.Objectives).ThenInclude(x => x.ObjectiveEvaluationProcesses).ThenInclude(x => x.SkillEvaluation));
						var mapperGoals = _mapper.Map<Paginate<GoalDto>>(iepGoals).Items;

						currentRowForGoals = 8;
						currentRowForObjectives = 19;
						if (mapperGoals != null && mapperGoals.Count() > 0)
						{
							foreach (var goal in mapperGoals)
							{
								#region Goals
								worksheet.Range["A"+ currentRowForGoals + ":BE"+(currentRowForGoals + 12)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["BE" + (currentRowForGoals+3) + ":BE" + (currentRowForGoals + 12)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (currentRowForGoals+3) + ":K" + (currentRowForGoals + 3)].Merge();
								worksheet.Range["A" + (currentRowForGoals + 3) + ":K" + (currentRowForGoals + 3)].Text = "Goal Area:";
								worksheet.Range["A" + (currentRowForGoals + 3) + ":K" + (currentRowForGoals + 3)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["A" + (currentRowForGoals + 3) + ":K" + (currentRowForGoals + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["L" + (currentRowForGoals + 3) + ":AC" + (currentRowForGoals + 3)].Merge();
								worksheet.Range["L" + (currentRowForGoals + 3) + ":AC" + (currentRowForGoals + 3)].Text = goal.AreaName == null ? "" : goal.AreaName;
								worksheet.Range["L" + (currentRowForGoals + 3) + ":AC" + (currentRowForGoals + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["AD" + (currentRowForGoals + 3) + ":AN" + (currentRowForGoals + 3)].Merge();
								worksheet.Range["AD" + (currentRowForGoals + 3) + ":AN" + (currentRowForGoals + 3)].Text = "Strand#";
								worksheet.Range["AD" + (currentRowForGoals + 3) + ":AN" + (currentRowForGoals + 3)].CellStyle.Color = Color.FromArgb(255, 205, 205);
								worksheet.Range["AD" + (currentRowForGoals + 3) + ":AN" + (currentRowForGoals + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

								worksheet.Range["AO" + (currentRowForGoals + 3) + ":BE" + (currentRowForGoals + 3)].Merge();
								worksheet.Range["AO" + (currentRowForGoals + 3) + ":BE" + (currentRowForGoals + 3)].Text = (goal.StrandId == null ? "" : goal.StrandId) + "-" + (goal.StrandName == null ? "" : goal.StrandName);
								worksheet.Range["AO" + (currentRowForGoals + 3) + ":BE" + (currentRowForGoals + 3)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (currentRowForGoals + 4) + ":BE" + (currentRowForGoals + 4)].Merge();
								worksheet.Range["A" + (currentRowForGoals + 4) + ":BE" + (currentRowForGoals + 4)].Text = "Current Level";
								worksheet.Range["A" + (currentRowForGoals + 4) + ":BE" + (currentRowForGoals + 4)].CellStyle.Color = Color.FromArgb(255, 255, 200);
								worksheet.Range["A" + (currentRowForGoals + 5) + ":BE" + (currentRowForGoals + 5)].Merge();
								worksheet.Range["A" + (currentRowForGoals + 5) + ":BE" + (currentRowForGoals + 5)].Text = goal.CurrentLevel == null ? "" : goal.CurrentLevel;

								worksheet.Range["A" + (currentRowForGoals + 7) + ":BE" + (currentRowForGoals + 7)].Merge();
								worksheet.Range["A" + (currentRowForGoals + 7) + ":BE" + (currentRowForGoals + 7)].Text = "Long Term Goal";
								worksheet.Range["A" + (currentRowForGoals + 7) + ":BE" + (currentRowForGoals + 7)].CellStyle.Color = Color.FromArgb(255, 255, 200);
								worksheet.Range["A" + (currentRowForGoals + 8) + ":BE" + (currentRowForGoals + 8)].Merge();
								worksheet.Range["A" + (currentRowForGoals + 8) + ":BE" + (currentRowForGoals + 8)].Text = goal.LongTermGoal == null ? "" : goal.LongTermGoal;


								int shortTermNo = 0;
								worksheet.Range["A" + (currentRowForGoals + 10) + ":BE" + (currentRowForGoals + 10)].Merge();
								worksheet.Range["A" + (currentRowForGoals + 10) + ":BE" + (currentRowForGoals + 10)].Text = "Short Term Goal " + shortTermNo + "/" + goal.ShortTermProgressNumber.ToString() + "%";
								worksheet.Range["A" + (currentRowForGoals + 10) + ":BE" + (currentRowForGoals + 10)].CellStyle.Color = Color.FromArgb(255, 255, 200);
								worksheet.Range["A" + (currentRowForGoals +11) + ":BE" + (currentRowForGoals + 12)].Merge();
								worksheet.Range["A" + (currentRowForGoals + 11) + ":BE" + (currentRowForGoals + 12)].Text = goal.ShortTermGoal == null ? "" : goal.ShortTermGoal;

							
                                #endregion
                                #region Objectives
                                if (goal.Objectives != null && goal.Objectives.Count() > 0)
                                {
                                    worksheet.Range["A" + currentRowForObjectives + ":BE200"].CellStyle.Font.Size = 9;

                                    var goalObjectives = goal.Objectives.Where(x => x.IsDeleted != true).ToList();
                                    if (goalObjectives.Count() > 0)
                                    {
                                        int noOfObj = 1;
                                        foreach (var objective in goalObjectives)
                                        {
                                            // to calculate Percentage
                                            if (objective.IsMasterd == true)
                                            {
                                                shortTermNo = shortTermNo + (objective.ObjectiveNumber == null ? 0 : objective.ObjectiveNumber.Value);
                                                if (shortTermNo > 0)
                                                {
                                                    worksheet.Range["A" + (currentRowForGoals + 10) + ":BE" + (currentRowForGoals + 10)].Text = "Short Term Goal " + shortTermNo + "/" + goal.ShortTermProgressNumber.ToString() + "%";
                                                }
                                            }
                                            worksheet.Range["A" + (currentRowForObjectives + 2) + ":A" + (currentRowForObjectives + 5)].Merge();
                                            worksheet.Range["A" + (currentRowForObjectives + 2) + ":A" + (currentRowForObjectives + 5)].Text = noOfObj.ToString();
                                            worksheet.Range["A" + (currentRowForObjectives + 2) + ":A" + (currentRowForObjectives + 5)].CellStyle.Color = Color.FromArgb(255, 255, 200);
                                            worksheet.Range["A" + (currentRowForObjectives + 2) + ":A" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


                                            worksheet.Range["B" + (currentRowForObjectives + 2) + ":L" + (currentRowForObjectives + 2)].Merge();
                                            worksheet.Range["B" + (currentRowForObjectives + 2) + ":L" + (currentRowForObjectives + 2)].Text = "Objectives";
                                            worksheet.Range["B" + (currentRowForObjectives + 2) + ":L" + (currentRowForObjectives + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
                                            worksheet.Range["B" + (currentRowForObjectives + 2) + ":L" + (currentRowForObjectives + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                                            worksheet.Range["M" + (currentRowForObjectives + 2) + ":P" + (currentRowForObjectives + 2)].Merge();
                                            worksheet.Range["M" + (currentRowForObjectives + 2) + ":P" + (currentRowForObjectives + 2)].Text = "Skill no.";
                                            worksheet.Range["M" + (currentRowForObjectives + 2) + ":P" + (currentRowForObjectives + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);
                                            worksheet.Range["M" + (currentRowForObjectives + 2) + ":P" + (currentRowForObjectives + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                                            worksheet.Range["Q" + (currentRowForObjectives + 2) + ":S" + (currentRowForObjectives + 2)].Merge();
                                            if (objective.ObjectiveSkills != null && objective.ObjectiveSkills.Count() > 0)
                                            {
                                                //var notDeletedObjs= objective.ObjectiveSkills.Where(x => x.IsDeleted != true).ToList();
                                                var listOfObjSkillsIds = objective.ObjectiveSkills.Select(x => x.SkillId).ToArray();
                                                worksheet.Range["Q" + (currentRowForObjectives + 2) + ":S" + (currentRowForObjectives + 2)].Text = (listOfObjSkillsIds == null ? "" : string.Join(",", listOfObjSkillsIds));
                                            }
                                            worksheet.Range["S" + (currentRowForObjectives + 2) + ":S" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                                            worksheet.Range["T" + (currentRowForObjectives + 2) + ":AC" + (currentRowForObjectives + 2)].Merge();
                                            worksheet.Range["T" + (currentRowForObjectives + 2) + ":AC" + (currentRowForObjectives + 2)].Text = "Evaluation process";
                                            worksheet.Range["T" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 2)].CellStyle.Color = Color.FromArgb(255, 205, 205);

                                            worksheet.Range["AD" + (currentRowForObjectives + 2) + ":AL" + (currentRowForObjectives + 2)].Merge();
                                            worksheet.Range["AD" + (currentRowForObjectives + 2) + ":AL" + (currentRowForObjectives + 2)].Text = "Indication";

                                            worksheet.Range["AM" + (currentRowForObjectives + 2) + ":AR" + (currentRowForObjectives + 2)].Merge();
                                            worksheet.Range["AM" + (currentRowForObjectives + 2) + ":AR" + (currentRowForObjectives + 2)].Text = "Date";

                                            worksheet.Range["AS" + (currentRowForObjectives + 2) + ":AT" + (currentRowForObjectives + 2)].Merge();
                                            worksheet.Range["AS" + (currentRowForObjectives + 2) + ":AT" + (currentRowForObjectives + 2)].Text = "%";

                                            worksheet.Range["AU" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 2)].Merge();
                                            worksheet.Range["AU" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 2)].Text = "Resources required";

                                            worksheet.Range["B" + (currentRowForObjectives + 3) + ":S" + (currentRowForObjectives + 5)].Merge();
                                            worksheet.Range["B" + (currentRowForObjectives + 3) + ":S" + (currentRowForObjectives + 5)].Text = objective.ObjectiveNote == null ? "" : objective.ObjectiveNote;

                                            worksheet.Range["T" + (currentRowForObjectives + 3) + ":AC" + (currentRowForObjectives + 5)].Merge();
                                            if (objective.ObjectiveEvaluationProcesses != null && objective.ObjectiveEvaluationProcesses.Count() > 0)
                                            {
                                                var listOfObjEvaluationsNames = objective.ObjectiveEvaluationProcesses.ToList().Select(x => (x.SkillEvaluation.Name == null ? "" : x.SkillEvaluation.Name)).ToArray();
                                                worksheet.Range["T" + (currentRowForObjectives + 3) + ":AC" + (currentRowForObjectives + 5)].Text = (listOfObjEvaluationsNames == null ? "" : string.Join(Environment.NewLine, listOfObjEvaluationsNames));
                                            }
                                            worksheet.Range["AD" + (currentRowForObjectives + 3) + ":AL" + (currentRowForObjectives + 5)].Merge();
                                            worksheet.Range["AD" + (currentRowForObjectives + 3) + ":AL" + (currentRowForObjectives + 5)].Text = objective.Indication == null ? "" : objective.Indication;
                                            worksheet.Range["AM" + (currentRowForObjectives + 3) + ":AR" + (currentRowForObjectives + 5)].Merge();
                                            worksheet.Range["AM" + (currentRowForObjectives + 3) + ":AR" + (currentRowForObjectives + 5)].Text = objective.Date == null ? "" : objective.Date.Value.ToShortDateString();
                                            worksheet.Range["AS" + (currentRowForObjectives + 3) + ":AT" + (currentRowForObjectives + 5)].Merge();
                                            worksheet.Range["AS" + (currentRowForObjectives + 3) + ":AT" + (currentRowForObjectives + 5)].Text = objective.ObjectiveNumber == null ? "" : objective.ObjectiveNumber.ToString();
                                            worksheet.Range["AU" + (currentRowForObjectives + 3) + ":BE" + (currentRowForObjectives + 5)].Merge();
                                            worksheet.Range["AU" + (currentRowForObjectives + 3) + ":BE" + (currentRowForObjectives + 5)].Text = objective.ResourcesRequired == null ? "" : objective.ResourcesRequired;

                                            worksheet.Range["AC" + (currentRowForObjectives + 2) + ":AC" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                                            worksheet.Range["AL" + (currentRowForObjectives + 2) + ":AL" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                                            worksheet.Range["AR" + (currentRowForObjectives + 2) + ":AR" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                                            worksheet.Range["AT" + (currentRowForObjectives + 2) + ":AT" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                                            worksheet.Range["BE" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                                            worksheet.Range["B" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                                            worksheet.Range["A" + (currentRowForObjectives + 5) + ":BE" + (currentRowForObjectives + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                                            noOfObj++;
											currentRowForObjectives = currentRowForObjectives + 4;
										}
										currentRowForGoals = currentRowForObjectives;
										currentRowForObjectives = currentRowForGoals + 11;
									}
								}
                                else
                                {
                                    worksheet.Range["A" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 4)].Merge();
                                    worksheet.Range["A" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 4)].Text = " No Objects Found";
                                    worksheet.Range["A" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                                    worksheet.Range["A" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                                    worksheet.Range["A" + (currentRowForObjectives + 2) + ":BE" + (currentRowForObjectives + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
									currentRowForGoals = currentRowForObjectives+3;
									currentRowForObjectives = currentRowForGoals + 11;
								}
                                #endregion
                            }
						}
						else
						{
							worksheet.Range["A" + (currentRowForGoals+3) + ":BE" + (currentRowForGoals + 5)].Merge();
							worksheet.Range["A" + (currentRowForGoals+3) + ":BE" + (currentRowForGoals + 5)].Text = " No Goals Found";
							worksheet.Range["A" + (currentRowForGoals+3) + ":BE" + (currentRowForGoals + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (currentRowForGoals+3) + ":BE" + (currentRowForGoals + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
							worksheet.Range["A" + (currentRowForGoals + 3) + ":BE" + (currentRowForGoals + 5)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
							currentRowForGoals = currentRowForGoals + 6;
						}

						#region Para-Medical
						currentRowPara = currentRowForGoals;

						worksheet.Range["A" + (currentRowPara + 2) + ":BE" + (currentRowPara + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range["A" + (currentRowPara + 2) + ":BE" + (currentRowPara + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range["A" + (currentRowPara + 2) + ":BE" + (currentRowPara + 2)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range["A" + (currentRowPara + 2) + ":BE" + (currentRowPara + 2)].CellStyle.Color = Color.FromArgb(255, 255, 200);
                        worksheet.Range["A" + (currentRowPara + 2) + ":BE" + (currentRowPara + 2)].Merge();
                        worksheet.Range["A" + (currentRowPara + 2) + ":BE" + (currentRowPara + 2)].Text = "Student Involved in Para-Medical Services/Support";

                        worksheet.Range["A27:BE27"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range["A27:AD27"].Merge();
                        worksheet.Range["A27:AD27"].Text = "Service Name";
                        worksheet.Range["A27:AD27"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range["A27:AD27"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        worksheet.Range["AE27:BE27"].Merge();
                        worksheet.Range["AE27:BE27"].Text = "Refer to ITP";
                        worksheet.Range["AE27:BE27"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range["AE27:BE27"].CellStyle.Color = Color.FromArgb(255, 205, 205);
                        if (iep.IepParamedicalServices != null && iep.IepParamedicalServices.Count() > 0)
                        {
                            foreach (var Para in iep.IepParamedicalServices)
                            {
                                worksheet.Range["A28:BE28"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                                worksheet.Range["A28:AD28"].Merge();
                                worksheet.Range["A28:AD28"].Text = Para.ParamedicalService == null ? "" : Para.ParamedicalService.Name;
                                worksheet.Range["A28:AD28"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                                worksheet.Range["AE28:BE28"].Merge();
                                worksheet.Range["AE28:BE28"].Text = "Yes";
                                worksheet.Range["AE28:BE28"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                            }
                        }
                        //worksheet.Range["A1:BE9"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                        #endregion
                        //#region Extra-Curriular
                        //worksheet.Range["A30:BE30"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["A30:BE30"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["A30:BE30"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["A30:BE30"].CellStyle.Color = Color.FromArgb(255, 255, 200);
                        //worksheet.Range["A30:BE30"].Merge();
                        //worksheet.Range["A30:BE30"].Text = "Student Involved in Extra-Curriular Services/Support";

                        //worksheet.Range["A31:BE31"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["A31:AD31"].Merge();
                        //worksheet.Range["A31:AD31"].Text = "Name";
                        //worksheet.Range["A31:AD31"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["A31:AD31"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //worksheet.Range["AE31:BE31"].Merge();
                        //worksheet.Range["AE31:BE31"].Text = "Refer to ITP";
                        //worksheet.Range["AE31:BE31"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["AE31:BE31"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //if (iep.IepExtraCurriculars != null && iep.IepExtraCurriculars.Count() > 0)
                        //{

                        //	foreach (var extra in iep.IepExtraCurriculars)
                        //	{
                        //		worksheet.Range["A32:BE32"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        //		worksheet.Range["A32:AD32"].Merge();
                        //		worksheet.Range["A32:AD32"].Text = extra.ExtraCurricular == null ? "" : extra.ExtraCurricular.Name;
                        //		worksheet.Range["A32:AD32"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //		worksheet.Range["AE32:BE32"].Merge();
                        //		worksheet.Range["AE32:BE32"].Text = "Yes";
                        //		worksheet.Range["AE32:BE32"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //	}
                        //}
                        //                  //  else
                        //                  //    {
                        ////	worksheet.Range["A32:BE34"].Merge();
                        ////	worksheet.Range["A32:BE34"].Text = "No Extra Curriculars";
                        ////}
                        //#endregion
                        //#region FooterNote
                        //worksheet.Range["A34:BE36"].Merge();
                        //worksheet.Range["A34:BE36"].Text = "FooterNotes";
                        //worksheet.Range["A34:BE36"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["A34:BE36"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["BE34:BE36"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //#endregion
                        //#region Footer
                        //worksheet.Range["A37:BE42"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["Y37:Y39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["BE37:BE39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["A37:G37"].Merge();
                        //worksheet.Range["A37:G37"].Text = "Report Card";
                        //worksheet.Range["A37:G37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
                        //worksheet.Range["A37:G37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


                        //worksheet.Range["H37:I37"].Merge();
                        //worksheet.Range["H37:I37"].Text = iep.ReportCard == false ? "✘" : iep.ReportCard == true ? "✔" : "";
                        //worksheet.Range["H37:I37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


                        //worksheet.Range["J37:W37"].Merge();
                        //worksheet.Range["J37:W37"].Text = "Progress Report";
                        //worksheet.Range["J37:W37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
                        //worksheet.Range["J37:W37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["X37:Y37"].Merge();
                        //worksheet.Range["X37:Y37"].Text = iep.ProgressReport == false ? "✘" : iep.ProgressReport == true ? "✔" : "";
                        //worksheet.Range["X37:Y37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["Z37:AM37"].Merge();
                        //worksheet.Range["Z37:AM37"].Text = "Parents meeting";
                        //worksheet.Range["Z37:AM37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
                        //worksheet.Range["Z37:AM37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["AN37:AO37"].Merge();
                        //worksheet.Range["AN37:AO37"].Text = iep.ParentsMeeting == false ? "✘" : iep.ParentsMeeting == true ? "✔" : "";
                        //worksheet.Range["AN37:AO37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["AP37:BC37"].Merge();
                        //worksheet.Range["AP37:BC37"].Text = "Other";
                        //worksheet.Range["AP37:BC37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
                        //worksheet.Range["AP37:BC37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["BD37:BE37"].Merge();
                        //worksheet.Range["BD37:BE37"].Text = iep.Others == false ? "✘" : iep.Others == true ? "✔" : "";
                        //worksheet.Range["BD37:BE37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["A38:Y38"].Merge();
                        //worksheet.Range["A38:Y38"].Text = "Parents Involved in setting up suggestions";
                        //worksheet.Range["A38:Y38"].CellStyle.Color = Color.FromArgb(255, 205, 205);
                        //worksheet.Range["A38:Y38"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



                        //worksheet.Range["Z38:BE38"].Merge();
                        //worksheet.Range["Z38:BE38"].Text = iep.ParentsInvolvedInSettingUpSuggestions == false ? "" : iep.ParentsInvolvedInSettingUpSuggestions == true ? "Yes, refer parent meeting record form" : "";
                        //worksheet.Range["Z38:BE38"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["A39:Y39"].Merge();
                        //worksheet.Range["A39:Y39"].Text = "Date of Review";
                        //worksheet.Range["A39:Y39"].CellStyle.Color = Color.FromArgb(255, 205, 205);
                        //worksheet.Range["A39:Y39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



                        //worksheet.Range["Z39:BE39"].Merge();
                        //worksheet.Range["Z39:BE39"].Text = iep.LastDateOfReview == null ? "" : iep.LastDateOfReview.Value.ToShortDateString();
                        //worksheet.Range["Z39:BE39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["H41:H42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["S41:S42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["AA41:AA42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["AL41:AL42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["AT41:AT42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        //worksheet.Range["BE41:BE42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

                        //worksheet.Range["A41:H41"].Merge();
                        //worksheet.Range["A41:H41"].Text = "Teacher:";
                        //worksheet.Range["A41:H41"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //worksheet.Range["I41:S41"].Merge();
                        //worksheet.Range["I41:S41"].Text = iepTeacherName;

                        //worksheet.Range["T41:AA41"].Merge();
                        //worksheet.Range["T41:AA41"].Text = "HOD:";
                        //worksheet.Range["T41:AA41"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //worksheet.Range["AB41:AL41"].Merge();
                        //worksheet.Range["AB41:AL41"].Text = iepHeadOfDepartmentName;

                        //worksheet.Range["AM41:AT41"].Merge();
                        //worksheet.Range["AM41:AT41"].Text = "HOE:";
                        //worksheet.Range["AM41:AT41"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //worksheet.Range["AU41:BE41"].Merge();
                        //worksheet.Range["AU41:BE41"].Text = iepHeadOfEducationName;






                        //worksheet.Range["A42:H42"].Merge();
                        //worksheet.Range["A42:H42"].Text = "Signature:";
                        //worksheet.Range["A42:H42"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //worksheet.Range["I42:S42"].Merge();
                        //worksheet.Range["I42:S42"].Text = "";

                        //worksheet.Range["T42:AA42"].Merge();
                        //worksheet.Range["T42:AA42"].Text = "Signature:";
                        //worksheet.Range["T42:AA42"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //worksheet.Range["AB42:AL42"].Merge();
                        //worksheet.Range["AB42:AL42"].Text = "";

                        //worksheet.Range["AM42:AT42"].Merge();
                        //worksheet.Range["AM42:AT42"].Text = "Signature:";
                        //worksheet.Range["AM42:AT42"].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        //worksheet.Range["AU42:BE42"].Merge();
                        //worksheet.Range["AU42:BE42"].Text = "";

                        //#endregion
                    }
                    else
                    {
						MemoryStream stream1 = new MemoryStream();
						return new FileStreamResult(stream1, "application/excel");
					}


					//Saving the Excel to the MemoryStream 
					MemoryStream stream = new MemoryStream();
					workbook.SaveAs(stream);

					//Set the position as '0'.
					stream.Position = 0;
					//Download the Excel file in the browser
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = ("-PLReport" + ".xlsx");

					return fileStreamResult;
				}
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}