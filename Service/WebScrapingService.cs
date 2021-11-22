using System;
using ScrapySharp.Html;
using ScrapySharp.Network;
using System.Linq;
using TowerSoft.HtmlToExcel;
using System.Collections.Generic;
using ScrapySharp.Html.Forms;

namespace Service
{
    public class WebScrapingService : IWebScrapingService
    {

        public (bool isSuccess , byte[] excelInByte , string error ) LoadExcel (Dictionary<string,string> loginForm , Dictionary<string,string> scarpingInformation)
        {
            try
            {
                var browser = new ScrapingBrowser();

                var homepage = browser.NavigateToPage(new Uri(scarpingInformation["LoginLink"])); 

                var form = homepage.FindFormById(scarpingInformation["FromName"]); 

                form.Method = HttpVerb.Post;

                foreach (var formInfo in loginForm)
                {
                    form[formInfo.Key] = formInfo.Value;
                }

                var resultPage = form.Submit();

                var loggedInPage = browser.NavigateToPage(new Uri(scarpingInformation["NavigationLink"]));

                var data = loggedInPage.Find(scarpingInformation["htmlTag"], By.Class(scarpingInformation["htmlClass"])).Select(x => x.OuterHtml);

                if (!data.Any())
                {
                    return (false, null, "Empty Table No Results");
                }

                string htmlTable = "<table>";

                foreach (var s in data)
                {
                    htmlTable += s;
                }

                htmlTable += "</table>";

                byte[] fileData = new WorkbookGenerator().FromHtmlString(htmlTable);

                return (true, fileData,null);

            }
            catch(Exception e)
            {
                return(false,null,e.Message);
            }

        }
    }
}
