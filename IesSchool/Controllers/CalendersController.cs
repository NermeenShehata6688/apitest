using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CalendersController : Controller
    {
      
            private ICalendersService _calenderService;
            public CalendersController(ICalendersService calenderService)
            {
            _calenderService = calenderService;
            }
            [HttpGet]
            public IActionResult GetCalenders()
            {
                try
                {
                    var all = _calenderService.GetCalenders();
                    return Ok(all);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // GET api/<AcadmicYearsController>/5
            [HttpGet]
            public IActionResult GetCalenderById(int id)
            {
                try
                {
                    var all = _calenderService.GetCalenderById(id);
                    return Ok(all);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            // POST api/<AcadmicYearsController>
            [HttpPost]
            public IActionResult AddEditCalender(CalenderDto calender)
            {
                try
                {
                    var all = _calenderService.AddEditCalender(calender);
                    return Ok(all);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [HttpDelete]
            public IActionResult DeleteCalender(int id)
            {
                try
                {
                    var all = _calenderService.DeleteCalender(id);
                    return Ok(all);
                }
                catch (Exception)
                {
                    throw;
                }
            }





        #region CalenderCategories
        [HttpGet]
        public IActionResult GetCalendersCategories()
        {
            try
            {
                var all = _calenderService.GetCalendersCategories();
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult GetCalenderCategoryById(int id)
        {
            try
            {
                var all = _calenderService.GetCalenderCategoryById(id);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public IActionResult AddEditCalenderCategory(CalenderCategoryDto calender)
        {
            try
            {
                var all = _calenderService.AddEditCalenderCategory(calender);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        public IActionResult DeleteCalenderCategory(int id)
        {
            try
            {
                var all = _calenderService.DeleteCalenderCategory(id);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion






    }
}
