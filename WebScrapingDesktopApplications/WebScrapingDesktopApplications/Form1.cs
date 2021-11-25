using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebScrapingDesktopApplications
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            try
            {
                var model = JsonConvert.DeserializeObject<WebScrapingModel>(RequestArguments.JsonString);
                Action(model);
                Environment.Exit(1);
                Application.Exit();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Action(WebScrapingModel model)
        {
            
            try
            {
                FirefoxOptions options = new FirefoxOptions();
                options.AddArguments("-headless");

                string geckoDriverPath = @"\geckodriver";

                using (var driver = new FirefoxDriver(geckoDriverPath))
                {

                    driver.Navigate().GoToUrl(model.LoginLink);

                    PerfromAction(new AutoMapper().HtmlMapper(model.LoginInfo), driver);

                    driver.Navigate().GoToUrl(model.NavigationLink);

                    PerfromAction(new AutoMapper().HtmlMapper(model.CustomProperties), driver);

                    PerfromAction(new AutoMapper().HtmlMapper(model.Elements), driver);

                    PerfromAction(new AutoMapper().HtmlMapper(model.ActionButtons), driver);

                    Thread.Sleep(5000);

                    var htmlTable = driver.FindElements(By.CssSelector(model.QueryCssSelector))
                        .ToList()[6].GetAttribute("outerHTML");

                    using (var client = new HttpClient())
                    {
                        HttpScrapTable postData = new HttpScrapTable { HtmlTable = htmlTable };
                        client.BaseAddress = new Uri(RequestArguments.ApiLink);
                        var response = client.PostAsJsonAsync(RequestArguments.ApiRoute, postData).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            Console.Write("Success");

                        }
                        else
                        {
                            Console.Write("Error");
                            MessageBox.Show("Something Went Wrong");
                        }
                    }
                    //driver.ExecuteScript("var html = document.querySelectorAll('#wrapper table')[6].outerHTML " +
                    //    "window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));");

                    driver.Close();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        private void PerfromAction(List<HtmlJsonPropsModel> model, FirefoxDriver driver)
        {
            foreach (var props in model)
            {
                if (props.HtmlTag.ToLower().Equals("button") || props.HtmlTag.ToLower().Equals("checkbox"))
                {
                    var by = props.GetElementBy.ToLower().Equals("name") ?
                        By.Name(props.Key) : By.Id(props.Key);

                    var findEle = driver.FindElement(by);
                    findEle.Click();
                }

                else if (props.HtmlTag.ToLower().Equals("select"))
                {

                    var by = props.GetElementBy.ToLower().Equals("name") ?
                        By.Name(props.Key) : By.Id(props.Key);

                    var selectBox = driver.FindElement(by);
                    var selectEle = new SelectElement(selectBox);
                    selectEle.SelectByValue(props.Value);
                }

                else
                {
                    var by = props.GetElementBy.ToLower().Equals("name") ?
                        By.Name(props.Key) : By.Id(props.Key);

                    var findEle = driver.FindElement(by);
                    findEle.SendKeys(props.Value);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
