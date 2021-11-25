using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            DateFrom.Text = RequestArguments.StartDate;
            DateTo.Text = RequestArguments.EndDate;
            Action();
            Environment.Exit(1);
            Application.Exit();
        }

        public void Action()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("-headless");

            string geckoDriverPath = @"\geckodriver";
            using (var driver = new FirefoxDriver(geckoDriverPath))
            {

                driver.Navigate().GoToUrl("https://secure.flynovoair.com/agents/default.asp");
                var loginNameBox = driver.FindElement(By.Name("login"));
                loginNameBox.SendKeys("vqssintl");
                var passwordNameBox = driver.FindElement(By.Name("password"));
                passwordNameBox.SendKeys("dec2021");
                var submitButton = driver.FindElement(By.Id("defaultActionButton"));
                submitButton.Click();

                driver.Navigate().GoToUrl("https://secure.flynovoair.com/agents/res-history.asp");
                var dateRangeBox = driver.FindElement(By.Name("date_range"));
                var selectElement = new SelectElement(dateRangeBox);
                selectElement.SelectByValue("Date Range");
                var dateFormBox = driver.FindElement(By.Name("date_from"));
                dateFormBox.SendKeys(DateFrom.Text);
                var dateToBox = driver.FindElement(By.Name("date_to"));
                dateToBox.SendKeys(DateTo.Text);
                var TicketDetailsBox = driver.FindElement(By.Name("chk_display_details"));
                TicketDetailsBox.Click();
                var TaxDetailsBox = driver.FindElement(By.Name("chk_display_tax_fee_details"));
                TaxDetailsBox.Click();
                var PageSizeBox = driver.FindElement(By.Name("page_size"));
                PageSizeBox.SendKeys("100");
                var submitButtonBox = driver.FindElement(By.Name("Submit"));
                submitButtonBox.Click();

                Thread.Sleep(5000);

                var htmlTable = driver.FindElements(OpenQA.Selenium.By.CssSelector("#wrapper table"))
                    .ToList()[6].GetAttribute("outerHTML");

                using (var client = new HttpClient())
                {
                    HttpScrapTable postData = new HttpScrapTable { HtmlTable = htmlTable };
                    client.BaseAddress = new Uri("https://localhost:44344/");
                    var response = client.PostAsJsonAsync("webscrap/htmltable", postData).Result;
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
                driver.ExecuteScript("var html = document.querySelectorAll('#wrapper table')[6].outerHTML " +
                    "window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));");

                driver.Close();
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
