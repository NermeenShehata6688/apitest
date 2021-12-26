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
			using (ExcelEngine excelEngine = new ExcelEngine())
			{
				IApplication application = excelEngine.Excel;

				application.DefaultVersion = ExcelVersion.Excel2016;

		  var iep = _uow.GetRepository<VwIep>().Single(x => x.Id == iepId && x.IsDeleted != true, null);
			var mapper = _mapper.Map<IepLPReportDto>(iep);
			var AllIepObjectives = _uow.GetRepository<Objective>().GetList(x => x.IsDeleted != true && x.IepId == iepId, null, x => x.Include(x => x.Activities).Include(x => x.ObjectiveSkills));
				IWorkbook workbook = application.Workbooks.Create(1);
				if (AllIepObjectives != null|| AllIepObjectives.Items.Count()>0)
				{
					int noOfObjectives = 0;
					foreach (var objective in AllIepObjectives.Items)
					{
						var mapperObj = _mapper.Map<Paginate<ObjectiveDto>>(AllIepObjectives);
					   mapper.ObjectiveDtos = mapperObj.Items;

                        //IWorksheet objective = workbook.Worksheets[noOfObjectives];
                        IWorksheet worksheet = workbook.Worksheets[noOfObjectives];

						IWorksheet newWorksheet;

						//for (int i = 1; i <= 5; i++)
						//{
						//	//Add a worksheet to the workbook
						//	newWorksheet = workbook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

						//	//Name the sheet
						//	newWorksheet.Name = "New_Sheet" + i.ToString();

						//	//Get the cells collection
						//	Range cells = newWorksheet.Cells;

						//	//Input a string value to a cell of the sheet
						//	cells.set_Item(i, i, "New_Sheet" + i.ToString());
						//}










						//		var sheetName = i < sheetNames.Count
						//? sheetNames[i]
						//: String.Format("Sheet{0}", sheetNames.Count - i);
						//var datasheets = new List<workbook.Worksheets>();
						//workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);

						#region General
						//Disable gridlines in the worksheet
						worksheet.IsGridLinesVisible = true;
						worksheet.Range["A1:BE100"].WrapText = true;
						worksheet.Range["A1:BE100"].CellStyle.Font.Bold = true;
						worksheet.Range["A1:BE13"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
						worksheet.Range["A1:BE13"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
						#endregion

						#region IEP
						worksheet.Range["A1:BE1"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A1:BE1"].Merge();
						worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";

						worksheet.Range["A2:BE3"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A2:AQ2"].Merge();
						worksheet.Range["A2:AQ2"].Text = "LESSON PLAN (LP)";

						worksheet.Range["A2:AQ2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A2:AQ2"].CellStyle.Color = Color.FromArgb(255, 255, 163);
						worksheet.Range["AR2:AU2"].Merge();
						worksheet.Range["AR2:AU2"].Text = "YEAR:";
						worksheet.Range["AR2:AU2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AR2:AU2"].CellStyle.Color = Color.FromArgb(228, 201, 255);
						worksheet.Range["AV2:BE2"].Merge();
						worksheet.Range["AV2:BE2"].Text = iep.AcadmicYearName == null ? "" : iep.AcadmicYearName.ToString();
						worksheet.Range["A3:BE3"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

						worksheet.Range["A3:J3"].Merge();
						worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
						worksheet.Range["A3:J3"].CellStyle.Color = Color.FromArgb(228, 201, 255);

						worksheet.Range["A3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["K3:AH3"].Merge();
						worksheet.Range["K3:AH3"].Text = iep.StudentName == null ? "" : iep.StudentName.ToString();
						worksheet.Range["AH3:Ah4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AI3:AL3"].Merge();
						worksheet.Range["AI3:AL3"].Text = "D.O.B:";
						worksheet.Range["AI3:AL3"].CellStyle.Color = Color.FromArgb(228, 201, 255);

						worksheet.Range["AL3:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AM3:AV3"].Merge();
						worksheet.Range["AM3:AV3"].Text = "dateofBirth";
						worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AW3:AZ3"].Merge();
						worksheet.Range["AW3:AZ3"].Text = "REF#:";
						worksheet.Range["AW3:AZ3"].CellStyle.Color = Color.FromArgb(228, 201, 255);

						worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BA3:BE3"].Merge();
						worksheet.Range["BA3:BE3"].Text = iep.StudentCode == null ? "" : iep.StudentCode.ToString();

						worksheet.Range["A4:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A4:J4"].Merge();
						worksheet.Range["A4:J4"].Text = "TEACHER:";
						worksheet.Range["A4:J4"].CellStyle.Color = Color.FromArgb(228, 201, 255);

						worksheet.Range["K4:AH4"].Merge();
						worksheet.Range["K4:AH4"].Text = iep.TeacherName == null ? "" : iep.TeacherName.ToString();
						worksheet.Range["AI4:AL4"].Merge();
						worksheet.Range["AI4:AL4"].Text = "DEPT:";
						worksheet.Range["AI4:AL4"].CellStyle.Color = Color.FromArgb(228, 201, 255);

						worksheet.Range["AM4:AV4"].Merge();
						worksheet.Range["AM4:AV4"].Text = iep.DepartmentName == null ? "" : iep.DepartmentName.ToString();
						worksheet.Range["AW4:AZ4"].Merge();
						worksheet.Range["AW4:AZ4"].Text = "RM#:";
						worksheet.Range["AW4:AZ4"].CellStyle.Color = Color.FromArgb(228, 201, 255);

						worksheet.Range["BA4:BE4"].Merge();
						worksheet.Range["BA4:BE4"].Text = iep.RoomNumber == null ? "" : iep.RoomNumber.ToString();
						#endregion
						#region Objective

						worksheet.Range["A6:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["J6:J10"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["J12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["AT12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["BE6:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
						worksheet.Range["A6:J10"].CellStyle.Color = Color.FromArgb(228, 201, 255);
						worksheet.Range["A12:AB12"].CellStyle.Color = Color.FromArgb(228, 201, 255);

						if (objective.ObjectiveSkills.Count() > 0)
						{
							var listOfObjSkillsIds = objective.ObjectiveSkills.Select(x => x.SkillId).ToArray();
							var AllIVwSkills = _uow.GetRepository<VwSkill>().GetList(x => listOfObjSkillsIds.Contains(x.Id) && x.IsDeleted != true, null);
							if (AllIVwSkills != null || AllIVwSkills.Count > 0)
							{
								worksheet.Range["A6:J6"].Merge();
								worksheet.Range["A6:J6"].Text = "Area/Strand/Skills:";
								worksheet.Range["K6:BE6"].Merge();
								worksheet.Range["K6:BE6"].Text = (AllIVwSkills.Items.First()?.AreaName == null ? "" : AllIVwSkills.Items.First().AreaName) + "/" + (AllIVwSkills.Items.First()?.StrandName == null ? "" : AllIVwSkills.Items.First().StrandName) + "/" + (listOfObjSkillsIds == null ? "" : string.Join(",", listOfObjSkillsIds));
							}
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
						worksheet.Range["A11:BE11"].CellStyle.Color = Color.FromArgb(255, 255, 163);
						worksheet.Range["A11:BE11"].Merge();
						worksheet.Range["A11:BE11"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
						#endregion
						//Activities
						#region Activities
						worksheet.Range["A12:BE12"].CellStyle.Color = Color.FromArgb(228, 201, 255);
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
								//worksheet.Range["A" + row + ":BE" + row].CellStyle.WrapText = true;

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
						worksheet.

						workbook.Worksheets.Append(worksheet);
						noOfObjectives++;
					}
				}
			
				//Saving the Excel to the MemoryStream 
				MemoryStream stream = new MemoryStream();
				workbook.SaveAs(stream);

				//Set the position as '0'.
				stream.Position = 0;
				//Download the Excel file in the browser
				FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

				fileStreamResult.FileDownloadName = "PLReport.xlsx";

				return fileStreamResult;
			}

		}

	}
}