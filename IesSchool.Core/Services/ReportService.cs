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
				worksheet.Range["A1:BE1"].Merge();
				worksheet.Range["A1:BE1"].Text = "IDEAL EDUCATION SCHOOL";
				worksheet.Range["A1:BE1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
				worksheet.Range["A1:BE1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
				worksheet.Range["A2:AQ2"].Merge();
				worksheet.Range["A2:AQ2"].Text = "LESSON PLAN (LP)";
				worksheet.Range["A2:AQ2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
				worksheet.Range["A2:AQ2"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
				worksheet.Range["A2:AQ2"].CellStyle.Color = Color.FromArgb(42, 118, 189);
				worksheet.Range["AR2:AU2"].Text = "YEAR:";
				worksheet.Range["AR2:AU2"].CellStyle.Color = Color.FromArgb(42, 118, 189);









				//Apply row height and column width to look good
				worksheet.Range["A1:BE1"].ColumnWidth = 1;
				worksheet.Range["A1:BE1"].RowHeight = 20;
				worksheet.Range["A2:AQ2"].RowHeight = 20;















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