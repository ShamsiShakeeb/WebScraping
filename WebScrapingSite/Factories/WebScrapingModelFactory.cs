using Microsoft.AspNetCore.Hosting;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static WebScrapingSite.Models.ScrapingInformationModel;

namespace WebScrapingSite.Factories
{
    public class WebScrapingModelFactory : IWebScrapingModelFactory
    {
        private readonly IWebScrapingService _webScrapingService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public WebScrapingModelFactory(IWebScrapingService webScrapingService, IWebHostEnvironment hostingEnvironment)
        {
            _webScrapingService = webScrapingService;
            _hostingEnvironment = hostingEnvironment;
        }
        public (byte[] excelData , bool isSuccess , string error) ScrapZenithSiteDataAsExcel(DateTime StartDate , DateTime EndDate)
        {
            var zenithSite = new Zenith();
            string navigationLinkFormat = string.Format(zenithSite.NavigationLink, StartDate.ToString("dd/MM/yyyy"),
                EndDate.ToString("dd/MM/yyyy"));
            zenithSite.ScarpingInformation.Add("NavigationLink", navigationLinkFormat);

            var result = _webScrapingService.LoadExcel(zenithSite.LoginForm, zenithSite.ScarpingInformation);
            
            return (result.excelInByte,result.isSuccess,result.error);
        }

        public void ScrapFlyNovoAirSite(DateTime StartDate, DateTime EndDate)
        {
            Process p = new Process();
            string webRootPath = _hostingEnvironment.WebRootPath;
            var path = webRootPath + "/SeleniumWebScrapApp/WebScrapingDesktopApplications.exe";
            p.StartInfo.FileName = path;
            p.StartInfo.ArgumentList.Add(StartDate.ToString("dd-MMM-yyyy"));
            p.StartInfo.ArgumentList.Add(EndDate.ToString("dd-MMM-yyyy"));
            p.Start();
            Thread.Sleep(50000);
        }
    }
}
