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
					var iep = _uow.GetRepository<VwIep>().Single(x => x.Id == iepId && x.IsDeleted != true, null);
					var mapper = _mapper.Map<IepLPReportDto>(iep);
					var AllIepObjectives = _uow.GetRepository<Objective>().Single(x => x.IsDeleted != true && x.IepId == iepId, null, x => x.Include(s => s.Activities).Include(s => s.ObjectiveSkills));
					var mapperObj = _mapper.Map<ObjectiveDto>(AllIepObjectives);
			         mapper.ObjectiveDtos = mapperObj;

					using (ExcelEngine excelEngine = new ExcelEngine())
					{
						IApplication application = excelEngine.Excel;

						application.DefaultVersion = ExcelVersion.Excel2016;

						//Create a workbook
						IWorkbook workbook = application.Workbooks.Create(1);
						IWorksheet worksheet = workbook.Worksheets[0];

				//Adding a picture
				//FileStream imageStream = new FileStream("wwwroot/tempFiles/330292e0da554910bb875a420627c16f.png", FileMode.Open, FileAccess.Read);
				//IPictureShape shape = worksheet.Pictures.AddPicture(1, 1, imageStream);



				

				//Disable gridlines in the worksheet
				worksheet.IsGridLinesVisible = true;
				worksheet.Range["A1:BE1"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["BE1:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A1:BE1"].Merge();
				worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";
				worksheet.Range["A1:BE1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
				worksheet.Range["A1:BE1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["A2:BE3"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                worksheet.Range["A2:AQ2"].Merge();
				worksheet.Range["A2:AQ2"].Text = "LESSON PLAN (LP)";
				worksheet.Range["A2:AQ2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
				worksheet.Range["A2:AQ2"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
				worksheet.Range["A2:AQ2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A2:AQ2"].CellStyle.Color = Color.FromArgb(255, 204, 255);
				worksheet.Range["AR2:AU2"].Merge();
				worksheet.Range["AR2:AU2"].Text = "YEAR:";
				worksheet.Range["AR2:AU2"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["AR2:AU2"].CellStyle.Color = Color.FromArgb(255, 255, 153);
				worksheet.Range["AV2:BE2"].Merge();
				worksheet.Range["AV2:BE2"].Text = iep.AcadmicYearName == null? "": iep.AcadmicYearName.ToString();
                worksheet.Range["A3:BE3"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;

                worksheet.Range["A3:J3"].Merge();
				worksheet.Range["A3:J3"].Text = "STUDENT NAME:";
				worksheet.Range["A3:J4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["K3:AH3"].Merge();
				worksheet.Range["K3:AH3"].Text = iep.StudentName == null ? "" : iep.StudentName.ToString();
				worksheet.Range["AH3:Ah4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["AI3:AL3"].Merge();
				worksheet.Range["AI3:AL3"].Text = "D.O.B:";
				worksheet.Range["AL3:AL4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["AM3:AV3"].Merge();
				worksheet.Range["AM3:AV3"].Text = "dateofBirth";
				worksheet.Range["AV3:AV4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["AW3:AZ3"].Merge();
				worksheet.Range["AW3:AZ3"].Text = "REF#:";
				worksheet.Range["AZ3:AZ4"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["BA3:BE3"].Merge();
				worksheet.Range["BA3:BE3"].Text = iep.StudentCode == null ? "" : iep.StudentCode.ToString();

				worksheet.Range["A4:BE4"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A4:J4"].Merge();
				worksheet.Range["A4:J4"].Text = "TEACHER:";
				worksheet.Range["K4:AH4"].Merge();
				worksheet.Range["K4:AH4"].Text = iep.TeacherName == null ? "" : iep.TeacherName.ToString();
				worksheet.Range["AI4:AL4"].Merge();
				worksheet.Range["AI4:AL4"].Text = "DEPT:";
				worksheet.Range["AM4:AV4"].Merge();
				worksheet.Range["AM4:AV4"].Text = iep.DepartmentName == null ? "" : iep.DepartmentName.ToString();
				worksheet.Range["AW4:AZ4"].Merge();
				worksheet.Range["AW4:AZ4"].Text = "RM#:";
				worksheet.Range["BA4:BE4"].Merge();
				worksheet.Range["BA4:BE4"].Text = iep.RoomNumber == null ? "" : iep.RoomNumber.ToString();

				worksheet.Range["A6:BE6"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A6:BE6"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A7:BE7"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A8:BE8"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A9:BE9"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A10:BE10"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A11:BE11"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A12:BE12"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A13:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;


				worksheet.Range["J6:J10"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["J12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["AT12"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["BE6:BE13"].CellStyle.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
				worksheet.Range["A6:J6"].Merge();
				worksheet.Range["A4:J4"].Text = "Area/Strand/Skills:";
				worksheet.Range["K6:BE6"].Merge();
				worksheet.Range["K6:BE6"].Text = iep.TeacherName == null ? "" : iep.TeacherName.ToString();


				//Apply row height and column width to look good
				worksheet.Range["A1:BE1"].ColumnWidth = 1;
				worksheet.Range["A1:BE1"].RowHeight = 17;
				worksheet.Range["A2:AQ2"].RowHeight = 17;















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
		
	}
}