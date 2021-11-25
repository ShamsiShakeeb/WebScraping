using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebScrapingSite.Models;
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

        public void WebScrap(List<string> customProperties, string uniqueSiteTitle)
        {
            StreamReader r = new StreamReader("../ScarpingSiteInformation.json");
            string jsonString = r.ReadToEnd();
            var webScrapingModel = JsonConvert.DeserializeObject<List<WebScrapingModel>>(jsonString);
            var model = webScrapingModel.Where(x => x.UniqueTitle == uniqueSiteTitle)
                .Select(x => x).FirstOrDefault();

            if (model == null)
            {
                return;
            }

            for(int i=0;i<model.CustomProperties.Count;i++)
            {
                model.CustomProperties[i].Value = customProperties[i];
            }

            jsonString = JsonConvert.SerializeObject(model);

            Process p = new Process();
            string webRootPath = _hostingEnvironment.WebRootPath;
            var path = webRootPath + "/SeleniumWebScrapApp/WebScrapingDesktopApplications.exe";
            p.StartInfo.FileName = path;
            p.StartInfo.ArgumentList.Add(uniqueSiteTitle);
            p.StartInfo.ArgumentList.Add(jsonString);
            p.Start();
            Thread.Sleep(50000);
        }

       
       
    }
}
