using Calendar_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Calendar_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IList<CellModel> getCellModels(int year, int month)
        {
            DateTime newDT = new DateTime(year, month, 1);
            int startDayOfWeek = (int)newDT.DayOfWeek + 1;
            int dayIncrement = 1;
            int lastDDofMonth = DateTime.DaysInMonth(newDT.Year, newDT.Month);
            IList<CellModel> cells = new List<CellModel>();
            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    CellModel cell = new CellModel() { Id = "id_" + i + j };
                    if (j == startDayOfWeek && dayIncrement == 1)
                    {
                        cell.dateDD = dayIncrement;
                        dayIncrement++;
                    }
                    else if (dayIncrement > 1 && dayIncrement <= lastDDofMonth)
                    {
                        cell.dateDD = dayIncrement;
                        dayIncrement++;
                    }
                    cells.Add(cell);
                }
            }
            return cells;
        }

        public async Task<IActionResult> Index(int year, int month)
        {
            IList<CellModel> cells;
            if(year == 0 && month == 0) 
            {
                cells = getCellModels(DateTime.Now.Year, DateTime.Now.Month);
                ViewBag.Year = DateTime.Now.Year;
                ViewBag.Month = DateTime.Now.Month;
            }
            else
            {
                cells = getCellModels(year, month);
                ViewBag.Year = year;
                ViewBag.Month = month;
            }
            
            return View(cells);
        }

        public IActionResult Previous(int year, int month)
        {
            if(month == 1)
            {
                month = 12;
                year = year - 1;
            }
            else
            {
                month = month - 1;
            }
            
            return RedirectToAction("Index", new { year, month });
        }

        public IActionResult Next(int year, int month)
        {
            if(month == 12)
            {
                month = 1;
                year = year + 1;
            }
            else
            {
                month = month + 1;
            }
            
            return RedirectToAction("Index", new { year, month });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}