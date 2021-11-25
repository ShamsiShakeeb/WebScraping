using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingDesktopApplications
{
    public class RequestArguments
    {
        public static string UniqueSiteTitle { set; get; }
        public static string JsonString { set; get; }
        public static string ApiLink { set; get; } = "https://localhost:44344/";
        public static string ApiRoute { set; get; } = "webscrap/htmltable";
    }
}
