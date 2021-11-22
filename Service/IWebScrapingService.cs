using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IWebScrapingService
    {
        (bool isSuccess, byte[] excelInByte,string error) LoadExcel(Dictionary<string, string> loginForm, Dictionary<string, string> scarpingInformation);
    }
}
