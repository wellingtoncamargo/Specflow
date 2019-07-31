using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Concurrent;
using System.Threading;
using TechTalk.SpecFlow;
using static FrameWork.Common.Hooks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace FrameWork.Steps.WebSteps
{

    [Binding]
    public class CalculoDoValorDoIRSteps
    {
        IWebDriver Driver;
        string path = @"C:\Users\wncg\source\Specflow\FrameWork_Core\FrameWork\bin\Debug\netcoreapp2.1\";
        private readonly string uri = "https://www.calcule.net/trabalhista/calculo-imposto-de-renda-irrf/";
        private static Screenshot ss;
        private static ConcurrentDictionary<string, string> _returnVariables = new ConcurrentDictionary<string, string>();
        AventStack.ExtentReports.ExtentReports extent;
        ExtentTest test;


        [BeforeScenario]
        public void Init()
        {
           this.Driver = new ChromeDriver(path);
        }

        [AfterScenario]
        public void Close()
        {
            this.Driver.Close();
            this.Driver.Dispose();
        }

        [Given(@"que acesso a pagina '(.*)'")]
        public void DadoQueEuAcessoAPagina(string p0)
        {
            this.Driver.Navigate().GoToUrl(p0);
            this.Driver.Manage().Window.Maximize();
            GetScreenShot.Capture(this.Driver, p0);
            
        }

        [Given(@"preencho o campo '(.*)' com o valor '(.*)'")]
        public void DadoPreenchoOCampoComOValor_(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.Id(p0)).SendKeys(p1);

        }

        [When(@"preencho o campo '(.*)' com o valor '(.*)'")]
        public void DadoPreenchoOCampoComOValor(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            //this.Driver.FindElement(By.Id(p0)).Clear();
            this.Driver.FindElement(By.Id(p0)).SendKeys(p1);
            GetScreenShot.Capture(this.Driver, p0);
        }

        [When(@"clico em '(.*)'")]
        public void QuandoClicoEmCalcular(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.XPath(p0)).Click();
            GetScreenShot.Capture(this.Driver, p0);
        }

        [Then(@"clico em '(.*)'")]
        public void EntaoClicoEmCalcular(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.XPath(p0)).Click();
            GetScreenShot.Capture(this.Driver, p0);
        }

        [Given(@"clico em '(.*)'")]
        public void DadoClicoEmCalcular(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.XPath(p0)).Click();
            GetScreenShot.Capture(this.Driver, p0);
        }


        [Then(@"vejo '(.*)'")]
        public void EntaoVejo(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            string valor = this.Driver.FindElement(By.XPath(p0)).Text;
            //Thread.Sleep(1000);
            Console.WriteLine("Conclusão");
            Assert.AreEqual(valor, "Conclusão");
            GetScreenShot.Capture(this.Driver, p0);
        }

        [Given(@"Eu salvo '(.*)' com o valor '(.*)'")]
        public void GivenEuSalvoOValorComo(string p0, string p1)
        {
            _returnVariables.TryAdd(p0, p1);

        }

        public void _Screenshot(string p0)
        {
            Thread.Sleep(1000);
            ss = ((ITakesScreenshot)this.Driver).GetScreenshot();
            //Use it as you want now
            string screenshot = ss.AsBase64EncodedString;
            byte[] screenshotAsByteArray = ss.AsByteArray;
            ss.SaveAsFile($@"C:\Users\wncg\source\Specflow\FrameWork_Core\FrameWork\Util\Screenshot\{p0}", ScreenshotImageFormat.Png); //use any of the built in image formating  DateTime.Now.ToString("ddMMyyyyHHmmssffff") 
            ss.ToString();//same as string screenshot = ss.AsBase64EncodedString;
            Thread.Sleep(1000);
        }

        //public void CaptureScreenshot()
        //{
        //    test = extent.Stats("CaptureScreenshot");
        //   // this.Driver = new ChromeDriver();
        //    this.Driver.Navigate().GoToUrl();
        //    string title = this.Driver.Title;
        //    Assert.AreEqual("Home - Automation Test", title);
        //    test.Log(Status.Pass, "Test Passed");
        //}
    }
}
