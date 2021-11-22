using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebScrapingSite.Models.ScrapingInformationModel;

namespace WebScrapingSite.Factories
{
    public class WebScrapingModelFactory : IWebScrapingModelFactory
    {
        private readonly IWebScrapingService _webScrapingService;
        public WebScrapingModelFactory(IWebScrapingService webScrapingService)
        {
            _webScrapingService = webScrapingService;
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
    }
}
