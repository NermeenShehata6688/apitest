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

namespace IesSchool.Core.Services
{
    internal class reaport_test
    {
		public FileStreamResult IepLpReport(int iepId)
		{
			using (ExcelEngine excelEngine = new ExcelEngine())
			{
				IApplication application = excelEngine.Excel;

				application.DefaultVersion = ExcelVersion.Excel2016;

				//Create a workbook
				IWorkbook workbook = application.Workbooks.Create(1);
				IWorksheet worksheet = workbook.Worksheets[0];

				//Adding a picture
				FileStream imageStream = new FileStream("wwwroot/tempFiles/330292e0da554910bb875a420627c16f.png", FileMode.Open, FileAccess.Read);
				IPictureShape shape = worksheet.Pictures.AddPicture(1, 1, imageStream);

				//Disable gridlines in the worksheet
				worksheet.IsGridLinesVisible = true;

				//Enter values to the cells from A3 to A5
				worksheet.Range["A3"].Text = "46036 Michigan Ave";
				worksheet.Range["A4"].Text = "Canton, USA";
				worksheet.Range["A5"].Text = "Phone: +1 231-231-2310";

				//Make the text bold
				worksheet.Range["A3:A5"].CellStyle.Font.Bold = true;

				//Merge cells
				worksheet.Range["D1:E1"].Merge();

				//Enter text to the cell D1 and apply formatting.
				worksheet.Range["D1"].Text = "INVOICE";
				worksheet.Range["D1"].CellStyle.Font.Bold = true;
				worksheet.Range["D1"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
				worksheet.Range["D1"].CellStyle.Font.Size = 35;

				//Apply alignment in the cell D1
				worksheet.Range["D1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
				worksheet.Range["D1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

				//Enter values to the cells from D5 to E8
				worksheet.Range["D5"].Text = "INVOICE#";
				worksheet.Range["E5"].Text = "DATE";
				worksheet.Range["D6"].Number = 1028;
				worksheet.Range["E6"].Value = "12/31/2018";
				worksheet.Range["D7"].Text = "CUSTOMER ID";
				worksheet.Range["E7"].Text = "TERMS";
				worksheet.Range["D8"].Number = 564;
				worksheet.Range["E8"].Text = "Due Upon Receipt";

				//Apply RGB backcolor to the cells from D5 to E8
				worksheet.Range["D5:E5"].CellStyle.Color = Color.FromArgb(42, 118, 189);
				worksheet.Range["D7:E7"].CellStyle.Color = Color.FromArgb(42, 118, 189);

				//Apply known colors to the text in cells D5 to E8
				worksheet.Range["D5:E5"].CellStyle.Font.Color = ExcelKnownColors.White;
				worksheet.Range["D7:E7"].CellStyle.Font.Color = ExcelKnownColors.White;

				//Make the text as bold from D5 to E8
				worksheet.Range["D5:E8"].CellStyle.Font.Bold = true;

				//Apply alignment to the cells from D5 to E8
				worksheet.Range["D5:E8"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
				worksheet.Range["D5:E5"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
				worksheet.Range["D7:E7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
				worksheet.Range["D6:E6"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

				//Enter value and applying formatting in the cell A7
				worksheet.Range["A7"].Text = "  BILL TO";
				worksheet.Range["A7"].CellStyle.Color = Color.FromArgb(42, 118, 189);
				worksheet.Range["A7"].CellStyle.Font.Bold = true;
				worksheet.Range["A7"].CellStyle.Font.Color = ExcelKnownColors.White;

				//Apply alignment
				worksheet.Range["A7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
				worksheet.Range["A7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

				//Enter values in the cells A8 to A12
				worksheet.Range["A8"].Text = "Steyn";
				worksheet.Range["A9"].Text = "Great Lakes Food Market";
				worksheet.Range["A10"].Text = "20 Whitehall Rd";
				worksheet.Range["A11"].Text = "North Muskegon,USA";
				worksheet.Range["A12"].Text = "+1 231-654-0000";

				//Create a Hyperlink for e-mail in the cell A13
				IHyperLink hyperlink = worksheet.HyperLinks.Add(worksheet.Range["A13"]);
				hyperlink.Type = ExcelHyperLinkType.Url;
				hyperlink.Address = "Steyn@greatlakes.com";
				hyperlink.ScreenTip = "Send Mail";

				//Merge column A and B from row 15 to 22
				worksheet.Range["A15:B15"].Merge();
				worksheet.Range["A16:B16"].Merge();
				worksheet.Range["A17:B17"].Merge();
				worksheet.Range["A18:B18"].Merge();
				worksheet.Range["A19:B19"].Merge();
				worksheet.Range["A20:B20"].Merge();
				worksheet.Range["A21:B21"].Merge();
				worksheet.Range["A22:B22"].Merge();

				//Enter details of products and prices
				worksheet.Range["A15"].Text = "  DESCRIPTION";
				worksheet.Range["C15"].Text = "QTY";
				worksheet.Range["D15"].Text = "UNIT PRICE";
				worksheet.Range["E15"].Text = "AMOUNT";
				worksheet.Range["A16"].Text = "Cabrales Cheese";
				worksheet.Range["A17"].Text = "Chocos";
				worksheet.Range["A18"].Text = "Pasta";
				worksheet.Range["A19"].Text = "Cereals";
				worksheet.Range["A20"].Text = "Ice Cream";
				worksheet.Range["C16"].Number = 3;
				worksheet.Range["C17"].Number = 2;
				worksheet.Range["C18"].Number = 1;
				worksheet.Range["C19"].Number = 4;
				worksheet.Range["C20"].Number = 3;
				worksheet.Range["D16"].Number = 21;
				worksheet.Range["D17"].Number = 54;
				worksheet.Range["D18"].Number = 10;
				worksheet.Range["D19"].Number = 20;
				worksheet.Range["D20"].Number = 30;
				worksheet.Range["D23"].Text = "Total";

				//Apply number format
				worksheet.Range["D16:E22"].NumberFormat = "$.00";
				worksheet.Range["E23"].NumberFormat = "$.00";

				//Apply incremental formula for column Amount by multiplying Qty and UnitPrice
				application.EnableIncrementalFormula = true;
				worksheet.Range["E16:E20"].Formula = "=C16*D16";

				//Formula for Sum the total
				worksheet.Range["E23"].Formula = "=SUM(E16:E22)";

				//Apply borders
				worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
				worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;
				worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
				worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;

				//Apply font setting for cells with product details
				worksheet.Range["A3:E23"].CellStyle.Font.FontName = "Arial";
				worksheet.Range["A3:E23"].CellStyle.Font.Size = 10;
				worksheet.Range["A15:E15"].CellStyle.Font.Color = ExcelKnownColors.White;
				worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;
				worksheet.Range["D23:E23"].CellStyle.Font.Bold = true;

				//Apply cell color
				worksheet.Range["A15:E15"].CellStyle.Color = Color.FromArgb(42, 118, 189);

				//Apply alignment to cells with product details
				worksheet.Range["A15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
				worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
				worksheet.Range["D15:E15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

				//Apply row height and column width to look good
				worksheet.Range["A1"].ColumnWidth = 36;
				worksheet.Range["B1"].ColumnWidth = 11;
				worksheet.Range["C1"].ColumnWidth = 8;
				worksheet.Range["D1:E1"].ColumnWidth = 18;
				worksheet.Range["A1"].RowHeight = 47;
				worksheet.Range["A2"].RowHeight = 15;
				worksheet.Range["A3:A4"].RowHeight = 15;
				worksheet.Range["A5"].RowHeight = 18;
				worksheet.Range["A6"].RowHeight = 29;
				worksheet.Range["A7"].RowHeight = 18;
				worksheet.Range["A8"].RowHeight = 15;
				worksheet.Range["A9:A14"].RowHeight = 15;
				worksheet.Range["A15:A23"].RowHeight = 18;


				//Saving the Excel to the MemoryStream 
				MemoryStream stream = new MemoryStream();

				workbook.SaveAs(stream);

				//Set the position as '0'.
				stream.Position = 0;


				//Download the Excel file in the browser
				FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

				fileStreamResult.FileDownloadName = "asds.xlsx";

				return fileStreamResult;
			}
			
		}
		//public FileStreamResult IepReport(int iepId)
		//{
		//	try
		//	{
		//		using (ExcelEngine excelEngine = new ExcelEngine())
		//		{
		//			IApplication application = excelEngine.Excel;
		//			application.DefaultVersion = ExcelVersion.Excel2016;

		//			var iep = _uow.GetRepository<Iep>().Single(x => x.Id == iepId && x.IsDeleted != true, null, x => x.Include(x => x.Student).ThenInclude(x => x.Department)
		//			.Include(x => x.Student).ThenInclude(x => x.Teacher)
		//			.Include(x => x.Teacher).Include(x => x.AcadmicYear).Include(x => x.Term)
		//			.Include(x => x.HeadOfDepartmentNavigation)
		//			.Include(x => x.HeadOfEducationNavigation)
		//			.Include(x => x.IepExtraCurriculars).ThenInclude(x => x.ExtraCurricular)
		//			.Include(x => x.IepParamedicalServices).ThenInclude(x => x.ParamedicalService)
		//			.Include(x => x.IepAssistants).ThenInclude(x => x.Assistant));
		//			var mapper = _mapper.Map<GetIepDto>(iep);

		//			IWorkbook workbook = application.Workbooks.Create(0);
		//			IWorksheet worksheet;

		//			int noOfGoals = 1;
		//			if (iep != null)
		//			{
		//				string studentName = "";
		//				string studentTeacherName = "";
		//				string iepTeacherName = "";
		//				string iepHeadOfDepartmentName = "";
		//				string iepHeadOfEducationName = "";
		//				string acadmicYearName = "";
		//				string termName = "";
		//				string dateOfBirthName = "";
		//				string studentCodeName = "";
		//				string studentDepartmentName = "";
		//				if (iep.Student != null)
		//				{
		//					studentName = iep.Student.Name == null ? "" : iep.Student.Name;
		//					studentDepartmentName = iep.Student.Department == null ? "" : iep.Student.Department.Name == null ? "" : iep.Student.Department.Name;
		//					studentTeacherName = iep.Student.Teacher == null ? "" : iep.Student.Teacher.Name == null ? "" : iep.Student.Teacher.Name;
		//					dateOfBirthName = iep.Student.DateOfBirth == null ? "" : iep.Student.DateOfBirth.Value.ToShortDateString();
		//					studentCodeName = iep.Student.Code == null ? "" : iep.Student.Code.ToString();
		//				}

		//				if (iep.AcadmicYear != null)
		//					acadmicYearName = iep.AcadmicYear.Name == null ? "" : iep.AcadmicYear.Name;
		//				else
		//					acadmicYearName = "Sheet1";
		//				if (iep.Teacher != null)
		//				{
		//					iepTeacherName = iep.Teacher.Name == null ? "" : iep.Teacher.Name;
		//				}
		//				if (iep.HeadOfDepartmentNavigation != null)
		//				{
		//					iepHeadOfDepartmentName = iep.HeadOfDepartmentNavigation.Name == null ? "" : iep.HeadOfDepartmentNavigation.Name;
		//				}
		//				if (iep.HeadOfEducationNavigation != null)
		//				{
		//					iepHeadOfEducationName = iep.HeadOfEducationNavigation.Name == null ? "" : iep.HeadOfEducationNavigation.Name;
		//				}
		//				if (iep.Term != null)
		//				{
		//					termName = iep.Term.Name == null ? "" : iep.Term.Name;
		//				}
		//				worksheet = workbook.Worksheets.Create(acadmicYearName);
		//				#region General
		//				//Disable gridlines in the worksheet
		//				worksheet.IsGridLinesVisible = true;
		//				worksheet.Range["A1:BE1"].ColumnWidth = 1;
		//				worksheet.Range["A1"].RowHeight = 17;
		//				worksheet.Range["A1:BE300"].WrapText = true;
		//				worksheet.Range["A1:BE300"].CellStyle.Font.Bold = true;
		//				worksheet.Range["A1:BE300"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
		//				worksheet.Range["A1:BE300"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
		//				#endregion
		//				#region IEP
		//				worksheet.Range["A1:BE9"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["BE1:BE9"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AH2:AH4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AL2:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["J3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["R5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AS2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AW2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["R5"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A1:BE9"].CellStyle.Font.Size = 10;


		//				worksheet.Range["A1:BE1"].Merge();
		//				worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";
		//				worksheet.Range["A2:AH2"].Merge();
		//				worksheet.Range["A2:AH2"].Text = "LESSON PLAN (LP)";
		//				worksheet.Range["A2:AH2"].CellStyle.Color = Color.FromArgb(255, 255, 200);
		//				worksheet.Range["AI2:AL2"].Merge();
		//				worksheet.Range["AI2:AL2"].Text = "YEAR:";
		//				worksheet.Range["AI2:AL2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["AM2:AS2"].Merge();
		//				worksheet.Range["AM2:AS2"].Text = acadmicYearName;
		//				worksheet.Range["AT2:AW2"].Merge();
		//				worksheet.Range["AT2:AW2"].Text = "Term:";
		//				worksheet.Range["AT2:AW2"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["AX2:BE2"].Merge();
		//				worksheet.Range["AX2:BE2"].Text = termName;


		//				worksheet.Range["A3:J3"].Merge();
		//				worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
		//				worksheet.Range["A3:J3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["K3:AH3"].Merge();
		//				worksheet.Range["K3:AH3"].Text = studentName;
		//				worksheet.Range["AI3:AL3"].Merge();
		//				worksheet.Range["AI3:AL3"].Text = "D.O.B:";
		//				worksheet.Range["AI3:AL3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AM3:AV3"].Merge();
		//				worksheet.Range["AM3:AV3"].Text = dateOfBirthName;
		//				worksheet.Range["AW3:AZ3"].Merge();
		//				worksheet.Range["AW3:AZ3"].Text = "REF#:";
		//				worksheet.Range["AW3:AZ3"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["BA3:BE3"].Merge();
		//				worksheet.Range["BA3:BE3"].Text = studentCodeName;

		//				worksheet.Range["A4:J4"].Merge();
		//				worksheet.Range["A4:J4"].Text = "TEACHER:";
		//				worksheet.Range["A4:J4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["K4:AH4"].Merge();
		//				worksheet.Range["K4:AH4"].Text = studentTeacherName;
		//				worksheet.Range["AI4:AL4"].Merge();
		//				worksheet.Range["AI4:AL4"].Text = "DEPT:";
		//				worksheet.Range["AI4:AL4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AM4:AV4"].Merge();
		//				worksheet.Range["AM4:AV4"].Text = studentDepartmentName;
		//				worksheet.Range["AW4:AZ4"].Merge();
		//				worksheet.Range["AW4:AZ4"].Text = "RM#:";
		//				worksheet.Range["AW4:AZ4"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["BA4:BE4"].Merge();
		//				worksheet.Range["BA4:BE4"].Text = iep.RoomNumber == null ? "" : iep.RoomNumber.ToString();


		//				worksheet.Range["A5:R5"].Merge();
		//				worksheet.Range["A5:R5"].Text = "People involved in setting up IEP:";
		//				worksheet.Range["A5:R5"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				if (iep.IepAssistants != null && iep.IepAssistants.Count() > 0)
		//				{
		//					var iepAssistants = iep.IepAssistants.ToList().Select(x => (x.Assistant == null ? "" : x.Assistant.Name)).ToArray();
		//					worksheet.Range["S5:BE5"].Text = studentTeacherName + "," + string.Join(",", iepAssistants);
		//				}
		//				else
		//				{
		//					worksheet.Range["S5:BE5"].Text = iepTeacherName;
		//				}
		//				worksheet.Range["S5:BE5"].Merge();

		//				worksheet.Range["A6:BE6"].Merge();
		//				worksheet.Range["A6:BE6"].Text = "General Current Level Achievement and Functional Performance";
		//				worksheet.Range["A6:BE6"].CellStyle.Color = Color.FromArgb(255, 255, 200);

		//				worksheet.Range["A7:BE9"].Merge();
		//				worksheet.Range["A7:BE9"].Text = iep.StudentNotes == null ? "" : iep.StudentNotes;

		//				#endregion
		//				//var iepGoals = iep.Goals.Where(x => x.IsDeleted != true).ToList();
		//				var iepGoals = _uow.GetRepository<Goal>().GetList(x => x.Iepid == iepId && x.IsDeleted != true, null, x => x.Include(x => x.Strand).Include(x => x.Area).Include(x => x.Objectives).ThenInclude(x => x.ObjectiveSkills).Include(x => x.Objectives).ThenInclude(x => x.ObjectiveEvaluationProcesses).ThenInclude(x => x.SkillEvaluation));
		//				var mapperGoals = _mapper.Map<Paginate<GoalDto>>(iepGoals).Items;

		//				int currentRow = 8;
		//				if (mapperGoals != null && mapperGoals.Count() > 0)
		//				{
		//					foreach (var goal in mapperGoals)
		//					{
		//						#region Goals
		//						worksheet.Range["A8:BE20"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//						worksheet.Range["BE8:BE20"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//						worksheet.Range["A11:K11"].Merge();
		//						worksheet.Range["A11:K11"].Text = "Goal Area:";
		//						worksheet.Range["A11:K11"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//						worksheet.Range["A11:K11"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//						worksheet.Range["L11:AC11"].Merge();
		//						worksheet.Range["L11:AC11"].Text = goal.AreaName == null ? "" : goal.AreaName;
		//						worksheet.Range["L11:AC11"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//						worksheet.Range["AD11:AN11"].Merge();
		//						worksheet.Range["AD11:AN11"].Text = "Strand#";
		//						worksheet.Range["AD11:AN11"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//						worksheet.Range["AD11:AN11"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//						worksheet.Range["AO11:BE11"].Merge();
		//						worksheet.Range["AO11:BE11"].Text = (goal.StrandId == null ? "" : goal.StrandId) + "-" + (goal.StrandName == null ? "" : goal.StrandName);
		//						worksheet.Range["AO11:BE11"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//						worksheet.Range["A12:BE12"].Merge();
		//						worksheet.Range["A12:BE12"].Text = "Current Level";
		//						worksheet.Range["A12:BE12"].CellStyle.Color = Color.FromArgb(255, 255, 200);
		//						worksheet.Range["A13:BE14"].Merge();
		//						worksheet.Range["A13:BE14"].Text = goal.CurrentLevel == null ? "" : goal.CurrentLevel;

		//						worksheet.Range["A15:BE15"].Merge();
		//						worksheet.Range["A15:BE15"].Text = "Long Term Goal";
		//						worksheet.Range["A15:BE15"].CellStyle.Color = Color.FromArgb(255, 255, 200);
		//						worksheet.Range["A16:BE17"].Merge();
		//						worksheet.Range["A16:BE17"].Text = goal.LongTermGoal == null ? "" : goal.LongTermGoal;


		//						int shortTermNo = 0;
		//						worksheet.Range["A18:BE18"].Merge();
		//						worksheet.Range["A18:BE18"].Text = "Short Term Goal " + shortTermNo + "/" + goal.ShortTermProgressNumber.ToString() + "%";
		//						worksheet.Range["A18:BE18"].CellStyle.Color = Color.FromArgb(255, 255, 200);
		//						worksheet.Range["A19:BE20"].Merge();
		//						worksheet.Range["A19:BE20"].Text = goal.ShortTermGoal == null ? "" : goal.ShortTermGoal;
		//						#endregion
		//						#region Objectives
		//						if (goal.Objectives != null && goal.Objectives.Count() > 0)
		//						{
		//							worksheet.Range["A11:BE200"].CellStyle.Font.Size = 9;

		//							var goalObjectives = goal.Objectives.Where(x => x.IsDeleted != true).ToList();
		//							if (goalObjectives.Count() > 0)
		//							{
		//								int noOfObj = 1;
		//								foreach (var objective in goalObjectives)
		//								{
		//									// to calculate Percentage
		//									if (objective.IsMasterd == true)
		//									{
		//										shortTermNo = shortTermNo + (objective.ObjectiveNumber == null ? 0 : objective.ObjectiveNumber.Value);
		//										if (shortTermNo > 0)
		//										{
		//											worksheet.Range["A18:BE18"].Text = "Short Term Goal " + shortTermNo + "/" + goal.ShortTermProgressNumber.ToString() + "%";
		//										}
		//									}
		//									worksheet.Range["A21:A24"].Merge();
		//									worksheet.Range["A21:A24"].Text = noOfObj.ToString();
		//									worksheet.Range["A21:A24"].CellStyle.Color = Color.FromArgb(255, 255, 200);
		//									worksheet.Range["A21:A24"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


		//									worksheet.Range["B21:L21"].Merge();
		//									worksheet.Range["B21:L21"].Text = "Objectives";
		//									worksheet.Range["B21:L21"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//									worksheet.Range["B21:L21"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//									worksheet.Range["M21:P21"].Merge();
		//									worksheet.Range["M21:P21"].Text = "Skill no.";
		//									worksheet.Range["M21:P21"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//									worksheet.Range["M21:P21"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//									worksheet.Range["Q21:S21"].Merge();
		//									if (objective.ObjectiveSkills != null && objective.ObjectiveSkills.Count() > 0)
		//									{
		//										//var notDeletedObjs= objective.ObjectiveSkills.Where(x => x.IsDeleted != true).ToList();
		//										var listOfObjSkillsIds = objective.ObjectiveSkills.Select(x => x.SkillId).ToArray();
		//										worksheet.Range["Q21:S21"].Text = (listOfObjSkillsIds == null ? "" : string.Join(",", listOfObjSkillsIds));
		//									}
		//									worksheet.Range["S21:S24"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//									worksheet.Range["T21:AC21"].Merge();
		//									worksheet.Range["T21:AC21"].Text = "Evaluation process";
		//									worksheet.Range["T21:BE21"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//									worksheet.Range["AD21:AL21"].Merge();
		//									worksheet.Range["AD21:AL21"].Text = "Indication";

		//									worksheet.Range["AM21:AR21"].Merge();
		//									worksheet.Range["AM21:AR21"].Text = "Date";

		//									worksheet.Range["AS21:AT21"].Merge();
		//									worksheet.Range["AS21:AT21"].Text = "%";

		//									worksheet.Range["AU21:BE21"].Merge();
		//									worksheet.Range["AU21:BE21"].Text = "Resources required";

		//									worksheet.Range["B22:S24"].Merge();
		//									worksheet.Range["B22:S24"].Text = objective.ObjectiveNote == null ? "" : objective.ObjectiveNote;

		//									worksheet.Range["T22:AC24"].Merge();
		//									if (objective.ObjectiveEvaluationProcesses != null && objective.ObjectiveEvaluationProcesses.Count() > 0)
		//									{
		//										var listOfObjEvaluationsNames = objective.ObjectiveEvaluationProcesses.ToList().Select(x => (x.SkillEvaluation.Name == null ? "" : x.SkillEvaluation.Name)).ToArray();
		//										worksheet.Range["T22:AC24"].Text = (listOfObjEvaluationsNames == null ? "" : string.Join(Environment.NewLine, listOfObjEvaluationsNames));
		//									}
		//									worksheet.Range["AD22:AL24"].Merge();
		//									worksheet.Range["AD22:AL24"].Text = objective.Indication == null ? "" : objective.Indication;
		//									worksheet.Range["AM22:AR24"].Merge();
		//									worksheet.Range["AM22:AR24"].Text = objective.Date == null ? "" : objective.Date.Value.ToShortDateString();
		//									worksheet.Range["AS22:AT24"].Merge();
		//									worksheet.Range["AS22:AT24"].Text = objective.ObjectiveNumber == null ? "" : objective.ObjectiveNumber.ToString();
		//									worksheet.Range["AU22:BE24"].Merge();
		//									worksheet.Range["AU22:BE24"].Text = objective.ResourcesRequired == null ? "" : objective.ResourcesRequired;

		//									worksheet.Range["AC21:AC24"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//									worksheet.Range["AL21:AL24"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//									worksheet.Range["AR21:AR24"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//									worksheet.Range["AT21:AT24"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//									worksheet.Range["BE21:BE24"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//									worksheet.Range["B21:BE21"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//									worksheet.Range["A24:BE24"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//									noOfObj++;
		//								}
		//							}
		//						}
		//						else
		//						{
		//							worksheet.Range["A21:BE23"].Merge();
		//							worksheet.Range["A21:BE23"].Text = " No Objects Found";
		//							worksheet.Range["A21:BE23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//							worksheet.Range["A21:BE23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
		//							worksheet.Range["A21:BE23"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//						}
		//						#endregion
		//					}
		//				}
		//				else
		//				{
		//					worksheet.Range["A11:BE13"].Merge();
		//					worksheet.Range["A11:BE13"].Text = " No Goals Found";
		//					worksheet.Range["A11:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//					worksheet.Range["A11:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
		//					worksheet.Range["A11:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				}

		//				#region Para-Medical
		//				worksheet.Range["A26:BE26"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A26:BE26"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A26:BE26"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A26:BE26"].CellStyle.Color = Color.FromArgb(255, 255, 200);
		//				worksheet.Range["A26:BE26"].Merge();
		//				worksheet.Range["A26:BE26"].Text = "Student Involved in Para-Medical Services/Support";

		//				worksheet.Range["A27:BE27"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A27:AD27"].Merge();
		//				worksheet.Range["A27:AD27"].Text = "Service Name";
		//				worksheet.Range["A27:AD27"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A27:AD27"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AE27:BE27"].Merge();
		//				worksheet.Range["AE27:BE27"].Text = "Refer to ITP";
		//				worksheet.Range["AE27:BE27"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AE27:BE27"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				if (iep.IepParamedicalServices != null && iep.IepParamedicalServices.Count() > 0)
		//				{
		//					foreach (var Para in iep.IepParamedicalServices)
		//					{
		//						worksheet.Range["A28:BE28"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//						worksheet.Range["A28:AD28"].Merge();
		//						worksheet.Range["A28:AD28"].Text = Para.ParamedicalService == null ? "" : Para.ParamedicalService.Name;
		//						worksheet.Range["A28:AD28"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//						worksheet.Range["AE28:BE28"].Merge();
		//						worksheet.Range["AE28:BE28"].Text = "Yes";
		//						worksheet.Range["AE28:BE28"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//					}
		//				}
		//				//worksheet.Range["A1:BE9"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

		//				#endregion
		//				#region Extra-Curriular
		//				worksheet.Range["A30:BE30"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A30:BE30"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A30:BE30"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A30:BE30"].CellStyle.Color = Color.FromArgb(255, 255, 200);
		//				worksheet.Range["A30:BE30"].Merge();
		//				worksheet.Range["A30:BE30"].Text = "Student Involved in Extra-Curriular Services/Support";

		//				worksheet.Range["A31:BE31"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A31:AD31"].Merge();
		//				worksheet.Range["A31:AD31"].Text = "Name";
		//				worksheet.Range["A31:AD31"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A31:AD31"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AE31:BE31"].Merge();
		//				worksheet.Range["AE31:BE31"].Text = "Refer to ITP";
		//				worksheet.Range["AE31:BE31"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AE31:BE31"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				if (iep.IepExtraCurriculars != null && iep.IepExtraCurriculars.Count() > 0)
		//				{

		//					foreach (var extra in iep.IepExtraCurriculars)
		//					{
		//						worksheet.Range["A32:BE32"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//						worksheet.Range["A32:AD32"].Merge();
		//						worksheet.Range["A32:AD32"].Text = extra.ExtraCurricular == null ? "" : extra.ExtraCurricular.Name;
		//						worksheet.Range["A32:AD32"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//						worksheet.Range["AE32:BE32"].Merge();
		//						worksheet.Range["AE32:BE32"].Text = "Yes";
		//						worksheet.Range["AE32:BE32"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//					}
		//				}
		//				//  else
		//				//    {
		//				//	worksheet.Range["A32:BE34"].Merge();
		//				//	worksheet.Range["A32:BE34"].Text = "No Extra Curriculars";
		//				//}
		//				#endregion
		//				#region FooterNote
		//				worksheet.Range["A34:BE36"].Merge();
		//				worksheet.Range["A34:BE36"].Text = "FooterNotes";
		//				worksheet.Range["A34:BE36"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["A34:BE36"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["BE34:BE36"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				#endregion
		//				#region Footer
		//				worksheet.Range["A37:BE42"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["Y37:Y39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["BE37:BE39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["A37:G37"].Merge();
		//				worksheet.Range["A37:G37"].Text = "Report Card";
		//				worksheet.Range["A37:G37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["A37:G37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


		//				worksheet.Range["H37:I37"].Merge();
		//				worksheet.Range["H37:I37"].Text = iep.ReportCard == false ? "✘" : iep.ReportCard == true ? "✔" : "";
		//				worksheet.Range["H37:I37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;


		//				worksheet.Range["J37:W37"].Merge();
		//				worksheet.Range["J37:W37"].Text = "Progress Report";
		//				worksheet.Range["J37:W37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["J37:W37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["X37:Y37"].Merge();
		//				worksheet.Range["X37:Y37"].Text = iep.ProgressReport == false ? "✘" : iep.ProgressReport == true ? "✔" : "";
		//				worksheet.Range["X37:Y37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["Z37:AM37"].Merge();
		//				worksheet.Range["Z37:AM37"].Text = "Parents meeting";
		//				worksheet.Range["Z37:AM37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["Z37:AM37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["AN37:AO37"].Merge();
		//				worksheet.Range["AN37:AO37"].Text = iep.ParentsMeeting == false ? "✘" : iep.ParentsMeeting == true ? "✔" : "";
		//				worksheet.Range["AN37:AO37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["AP37:BC37"].Merge();
		//				worksheet.Range["AP37:BC37"].Text = "Other";
		//				worksheet.Range["AP37:BC37"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["AP37:BC37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["BD37:BE37"].Merge();
		//				worksheet.Range["BD37:BE37"].Text = iep.Others == false ? "✘" : iep.Others == true ? "✔" : "";
		//				worksheet.Range["BD37:BE37"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["A38:Y38"].Merge();
		//				worksheet.Range["A38:Y38"].Text = "Parents Involved in setting up suggestions";
		//				worksheet.Range["A38:Y38"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["A38:Y38"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



		//				worksheet.Range["Z38:BE38"].Merge();
		//				worksheet.Range["Z38:BE38"].Text = iep.ParentsInvolvedInSettingUpSuggestions == false ? "" : iep.ParentsInvolvedInSettingUpSuggestions == true ? "Yes, refer parent meeting record form" : "";
		//				worksheet.Range["Z38:BE38"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["A39:Y39"].Merge();
		//				worksheet.Range["A39:Y39"].Text = "Date of Review";
		//				worksheet.Range["A39:Y39"].CellStyle.Color = Color.FromArgb(255, 205, 205);
		//				worksheet.Range["A39:Y39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;



		//				worksheet.Range["Z39:BE39"].Merge();
		//				worksheet.Range["Z39:BE39"].Text = iep.LastDateOfReview == null ? "" : iep.LastDateOfReview.Value.ToShortDateString();
		//				worksheet.Range["Z39:BE39"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["H41:H42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["S41:S42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AA41:AA42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AL41:AL42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["AT41:AT42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
		//				worksheet.Range["BE41:BE42"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;

		//				worksheet.Range["A41:H41"].Merge();
		//				worksheet.Range["A41:H41"].Text = "Teacher:";
		//				worksheet.Range["A41:H41"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["I41:S41"].Merge();
		//				worksheet.Range["I41:S41"].Text = iepTeacherName;

		//				worksheet.Range["T41:AA41"].Merge();
		//				worksheet.Range["T41:AA41"].Text = "HOD:";
		//				worksheet.Range["T41:AA41"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AB41:AL41"].Merge();
		//				worksheet.Range["AB41:AL41"].Text = iepHeadOfDepartmentName;

		//				worksheet.Range["AM41:AT41"].Merge();
		//				worksheet.Range["AM41:AT41"].Text = "HOE:";
		//				worksheet.Range["AM41:AT41"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AU41:BE41"].Merge();
		//				worksheet.Range["AU41:BE41"].Text = iepHeadOfEducationName;






		//				worksheet.Range["A42:H42"].Merge();
		//				worksheet.Range["A42:H42"].Text = "Signature:";
		//				worksheet.Range["A42:H42"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["I42:S42"].Merge();
		//				worksheet.Range["I42:S42"].Text = "";

		//				worksheet.Range["T42:AA42"].Merge();
		//				worksheet.Range["T42:AA42"].Text = "Signature:";
		//				worksheet.Range["T42:AA42"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AB42:AL42"].Merge();
		//				worksheet.Range["AB42:AL42"].Text = "";

		//				worksheet.Range["AM42:AT42"].Merge();
		//				worksheet.Range["AM42:AT42"].Text = "Signature:";
		//				worksheet.Range["AM42:AT42"].CellStyle.Color = Color.FromArgb(255, 205, 205);

		//				worksheet.Range["AU42:BE42"].Merge();
		//				worksheet.Range["AU42:BE42"].Text = "";

		//				#endregion
		//			}
		//			else
		//			{
		//				MemoryStream stream1 = new MemoryStream();
		//				return new FileStreamResult(stream1, "application/excel");
		//			}


		//			//Saving the Excel to the MemoryStream 
		//			MemoryStream stream = new MemoryStream();
		//			workbook.SaveAs(stream);

		//			//Set the position as '0'.
		//			stream.Position = 0;
		//			//Download the Excel file in the browser
		//			FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

		//			fileStreamResult.FileDownloadName = ("-PLReport" + ".xlsx");

		//			return fileStreamResult;
		//		}
		//	}
		//	catch (Exception)
		//	{

		//		throw;
		//	}
		//}
	}
}
