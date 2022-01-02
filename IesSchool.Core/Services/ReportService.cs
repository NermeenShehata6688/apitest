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
					int lastRow = 0;

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
					string studentName = "";

					if (iep != null)
					{
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

						lastRow = worksheet.Rows.Length;
						if (mapperGoals != null && mapperGoals.Count() > 0)
						{
							foreach (var goal in mapperGoals)
							{
								#region Goals
								worksheet.Range["A"+ (lastRow +1)+ ":BE"+(lastRow + 11)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["BE" + (lastRow+2) + ":BE" + (lastRow + 11)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
								worksheet.Range["A" + (lastRow+2) + ":K" + (lastRow + 2)].Merge();
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
								worksheet.Range["A" + (lastRow +3)+ ":BE" + (lastRow + 3)].Merge();
								worksheet.Range["A" + (lastRow +3)+ ":BE" + (lastRow + 3)].Text = "Current Level";
								worksheet.Range["A" + (lastRow +3)+ ":BE" + (lastRow + 3)].CellStyle.Color = Color.FromArgb(255, 255, 200);
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
								worksheet.Range["A" + (lastRow +10) + ":BE" + (lastRow + 11)].Merge();
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
                                                var listOfObjSkillsIds = objective.ObjectiveSkills.Select(x => x.SkillId).ToArray();
                                                worksheet.Range["Q" + (lastRow + 1) + ":S" + (lastRow + 1)].Text = (listOfObjSkillsIds == null ? "" : string.Join(",", listOfObjSkillsIds));
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
                                                var listOfObjEvaluationsNames = objective.ObjectiveEvaluationProcesses.ToList().Select(x => (x.SkillEvaluation.Name == null ? "" : x.SkillEvaluation.Name)).ToArray();
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
											worksheet.Range["S" + (lastRow ) + ":S" + (lastRow + 4)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

											noOfObj++;
											lastRow = lastRow + 4;
										}
									}
									lastRow = worksheet.Rows.Length;
								}
								else
                                {
                                    worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow+ 14)].Merge();
                                    worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow+ 14)].Text = " No Objects Found";
                                    worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow+ 14)].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                                    worksheet.Range["A" + (lastRow + 12) + ":BE" + (lastRow+ 14)].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
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
							lastRow = lastRow+3;
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
                        worksheet.Range["A" + (lastRow+1) + ":AD" + (lastRow + 1)].Merge();
                        worksheet.Range["A" + (lastRow+1) + ":AD" + (lastRow + 1)].Text = "Name";
                        worksheet.Range["A" + (lastRow+1) + ":AD" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                        worksheet.Range["A" + (lastRow + 1) + ":AD" + (lastRow + 1)].CellStyle.Color = Color.FromArgb(255, 205, 205);

                        worksheet.Range["AE" + (lastRow+1) + ":BE" + (lastRow + 1)].Merge();
                        worksheet.Range["AE" + (lastRow+1) + ":BE" + (lastRow + 1)].Text = "Refer to ITP";
                        worksheet.Range["AE" + (lastRow+1) + ":BE" + (lastRow + 1)].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
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
                        worksheet.Range["A" + (lastRow + 2) + ":BE" + (lastRow + 4)].Text = "FooterNotes";
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
					worksheet.Range["A1:BE" + (lastRow)].WrapText = true;
                    worksheet.Range["A1:BE" + (lastRow)].CellStyle.Font.Bold = true;
                    worksheet.Range["A1:BE" + (lastRow)].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    worksheet.Range["A1:BE" + (lastRow)].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
					worksheet.Range["A1:BE" + (lastRow)].CellStyle.Font.Size = 9;
					
					//Saving the Excel to the MemoryStream 
					MemoryStream stream = new MemoryStream();
					workbook.SaveAs(stream);

					//Set the position as '0'.
					stream.Position = 0;
					//Download the Excel file in the browser
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = (studentName  + "-IEPReport" + ".xlsx");


					return fileStreamResult;
				}
			}
			catch (Exception)
			{

				throw;
			}
		}
		public FileStreamResult BCPReport()
		{
			try
			{
				using (ExcelEngine excelEngine = new ExcelEngine())
				{
					IApplication application = excelEngine.Excel;
					application.DefaultVersion = ExcelVersion.Excel2016;
					int lastRow = 1;
					int lastCulumn = 1;

					

					IWorkbook workbook = application.Workbooks.Create(0);
					IWorksheet worksheet;
					string studentName = "";
					worksheet = workbook.Worksheets.Create("nn");

					#region General
					worksheet.IsGridLinesVisible = true;
					worksheet.Range["A1:BS1"].ColumnWidth = 2;
					worksheet.Range["A1"].RowHeight = 17;


					#endregion
					//lastCulumn = worksheet.Columns.Length;
					worksheet.Range["A1:BS1"].Merge();
					worksheet.Range["A1:BS1"].Text = "IDEAL EDUCATION SCHOOL";

					worksheet.Range["A2:BS2"].Merge();
					worksheet.Range["A2:BS2"].Text = "BCP Profile";
					worksheet.Range["A2:BS2"].CellStyle.Color = Color.FromArgb(255, 255, 200);

					worksheet.Range["A3:BS3"].Merge();
					worksheet.Range["A3:BS3"].Text = "Student Name";

					worksheet.Range["A4:BS4"].Merge();
					worksheet.Range["A4:BS4"].Text = "Level";
					worksheet.Range["A4:BS4"].CellStyle.Color = Color.FromArgb(255, 255, 200);

					worksheet.Range["A5:M5"].Merge();
					worksheet.Range["A5:M5"].Text = "Area/Strand";
					worksheet.Range["A5:M5"].CellStyle.Color = Color.FromArgb(255, 205, 205);

					worksheet.Range["N5:BS5"].Merge();
					worksheet.Range["N5:BS5"].Text = "Behaviors / Skills";
					worksheet.Range["N5:BS5"].CellStyle.Color = Color.FromArgb(255, 205, 205);

					var allAreas = _uow.GetRepository<Area>().GetList(x => x.IsDeleted != true, x => x.OrderBy(c => c.DisplayOrder), x => x.Include(n => n.Strands.Where(s => s.IsDeleted != true)).ThenInclude(n => n.Skills), 0, 100000, true);
					lastRow = worksheet.Rows.Length;
					int noOfAreas = 1;
                    if (allAreas != null && allAreas.Items.Count()>0)
                    {
						foreach (var area in allAreas.Items)
						{
							worksheet.Range["A1" + lastRow].Text = noOfAreas.ToString();
							noOfAreas++;
						}
					}
                   



					//if (iep != null)
					//{



					//Disable gridlines in the worksheet





					//}
					//else
					//{
					//	MemoryStream stream1 = new MemoryStream();
					//	return new FileStreamResult(stream1, "application/excel");
					//}


					//Saving the Excel to the MemoryStream 
					MemoryStream stream = new MemoryStream();
					workbook.SaveAs(stream);

					//Set the position as '0'.
					stream.Position = 0;
					//Download the Excel file in the browser
					FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

					fileStreamResult.FileDownloadName = (studentName + "-BCPReport" + ".xlsx");


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