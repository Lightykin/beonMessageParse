using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace BeonParse_SeleniumConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int incomplete = 3258;

            while (incomplete != 1)
            {
                Console.Write(incomplete);
                //var o = new FirefoxOptions();
                // o.AddArgument("-headless");
                var FFdriver = new FirefoxDriver("C:/bible/");
                FFdriver.Manage().Window.Maximize();
                //     getHtml(FFdriver);
                LogIn(FFdriver);
                //downloadHtml(FFdriver);                     //proba + udalic reklamy
                Console.Write(incomplete);
                DownloadAllTheHtmls(FFdriver, incomplete);
                incomplete = DownloadAllTheHtmls(FFdriver, incomplete);
                
                FFdriver.Close();
            }
        }
        static void downloadHtml(FirefoxDriver driver)
        {
            driver.Url = "http://beon.ru/messages/";

            var gaypidor = driver.FindElement(By.XPath("//*[@id=\"fl_btw_ad_centercenter\"]"));

            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].style='display: none;'", gaypidor);
            gaypidor = driver.FindElement(By.XPath("//*[@id=\"btw_ad_188002\"]"));
            js.ExecuteScript("arguments[0].style='display: none;'", gaypidor);
            //var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 1));
            //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("\"fl_btw_ad_centercenter\"")));
            string pagesource = driver.PageSource;
            System.IO.File.WriteAllText(@"C:\yoursite2.htm", pagesource, new UTF8Encoding(true));
        }
        static void LogIn(FirefoxDriver driver)
        {
           // while (!driver.FindElement(By.LinkText("Войти…")).isEmpty())
           // {
                driver.Url = "http://beon.ru/messages/";
                var js = (IJavaScriptExecutor)driver;
                driver.FindElement(By.LinkText("Войти…")).Click();
                var bitch = driver.FindElement(By.Name("login"));
                var nigga = driver.FindElement(By.Name("pass"));
                driver.FindElement(By.Name("pass")).Click();
                bitch.SendKeys("iibui");
                nigga.SendKeys("RdXwyJXtS4U");
                nigga.SendKeys(Keys.Enter);
           // }
        }

        static void getHtml(FirefoxDriver driver)
        {
            driver.Url = "http://beon.ru/";
            //driver.GetAttribute("innerHTML");
            //FirefoxWebElement element = driver.GetAttribute("innerHTML"); 
            //string pagesource = driver.PageSource;
            //Console.Write(pagesource);
        }

        static int FindTable(FirefoxDriver driver)
        {
            int index = 1;
            int row = 1;
            IWebElement baseTable = driver.FindElement(By.ClassName("rcurrent"));       //Vydelic'
            //Console.Write(baseTable.GetAttribute("id"));                              //Proveric cislo
            if (Int32.TryParse(baseTable.GetAttribute("id").Remove(0, 3), out row))     //Povucic rjad
            {
                return row;
            }
            else
                return 0;

        }


        static int DownloadAllTheHtmls(FirefoxDriver driver, int row)
        {
            for (int i = 0; i < 20; i=i+1)
            {
                row = row - 1;
                driver.Url = "http://beon.ru/messages/incoming/?c=" + row;
                var gaypidor = driver.FindElement(By.XPath("//*[@id=\"fl_btw_ad_centercenter\"]"));
                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].style='display: none;'", gaypidor); ////*[@id="btw_ad_188002"]
                js.ExecuteScript("arguments[0].style='display: none;'", driver.FindElement(By.XPath("//*[@id = \"btw_ad_188002\"]")));
                string pagesource = driver.PageSource;
                System.IO.Directory.CreateDirectory(@"C:/beon");
                System.IO.File.WriteAllText(@"C:\beon\ls_nomer_" + row + ".htm", pagesource, new UTF8Encoding(true));
                Console.Write("boope");
                if (row == 1)
                {
                    return 0;
                }
            }
            Console.Write(row);
            return row;
            }

        }
    }
    
//
