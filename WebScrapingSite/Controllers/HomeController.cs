using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TowerSoft.HtmlToExcel;
using WebScrapingSite.Factories;
using WebScrapingSite.Models;
using WebScrapingSite.ViewModel;

namespace WebScrapingSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebScrapingModelFactory _webScrapingModelFactory;
        public HomeController(ILogger<HomeController> logger, IWebScrapingModelFactory webScrapingModelFactory)
        {
            _logger = logger;
            _webScrapingModelFactory = webScrapingModelFactory;
        }

        public IActionResult Index()
        {
            return View();
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

        public IActionResult WebScraping()
        {
            return View(new ScarpingFilterViewModel());
        }

        [HttpPost]
        public IActionResult WebScraping(ScarpingFilterViewModel scarpingFilterViewModel)
        {
            var result = _webScrapingModelFactory.ScrapZenithSiteDataAsExcel(scarpingFilterViewModel.StartDate, 
                scarpingFilterViewModel.EndDate);

            if (result.isSuccess)
            {
                return File(result.excelData, MimeType.xlsx, "report.xlsx");
            }

            return RedirectToAction("WebScraping");
        }
    }
}
