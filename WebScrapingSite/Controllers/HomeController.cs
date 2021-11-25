using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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
            var model = new ScarpingFilterViewModel();
            model.HtmlTable = ScarpingFilterViewModel.HtmlTableResponse;
            return View(model);
        }

        [HttpPost]
        public IActionResult WebScraping(ScarpingFilterViewModel scarpingFilterViewModel, IFormCollection form, string submitBtn)
        {
            if (submitBtn.Equals("Zenith Load as Excel"))
            {
                var result = _webScrapingModelFactory.ScrapZenithSiteDataAsExcel(scarpingFilterViewModel.StartDate,
                    scarpingFilterViewModel.EndDate);

                if (result.isSuccess)
                {
                    return File(result.excelData, MimeType.xlsx, "report.xlsx");
                }
            }
             //Selenium WebScraping
            _webScrapingModelFactory.WebScrap(PrepareCustomPropertyFromIFormCollections(form), submitBtn);

            return RedirectToAction("WebScraping");
        }
        [HttpPost]
        [Route("webscrap/htmltable")]
        public IActionResult SeleniumWebScrapResponse([FromBody] SeleniumWebScrapResponseModel seleniumWebScrapResponseModel)
        {
            ScarpingFilterViewModel.HtmlTableResponse = seleniumWebScrapResponseModel.HtmlTable;
            return Ok();
        }

        private List<string> PrepareCustomPropertyFromIFormCollections(IFormCollection form)
        {
            List<string> customProperties = new List<string>();
            int i = 0;
            foreach (var data in form)
            {

                if (!data.Key.Equals("__RequestVerificationToken") && !data.Key.Equals("submitBtn"))
                {
                    if (i == 0 || i == 1)
                    {
                        customProperties.Add(Convert.ToDateTime(data.Value).ToString("dd-MMM-yyyy"));
                    }
                    else
                    {
                        customProperties.Add(data.Value);
                    }

                }
                i++;
            }
            return customProperties;
        }
    }
}
