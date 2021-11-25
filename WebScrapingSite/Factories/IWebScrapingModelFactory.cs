using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebScrapingSite.Factories
{
    public interface IWebScrapingModelFactory
    {
        (byte[] excelData, bool isSuccess, string error) ScrapZenithSiteDataAsExcel(DateTime StartDate, DateTime EndDate);
        void WebScrap(List<string> customProperties, string uniqueSiteTitle);
    }
}
