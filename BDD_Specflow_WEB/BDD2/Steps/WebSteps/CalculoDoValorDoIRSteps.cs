using AventStack.ExtentReports;
using AventStack.ExtentReports.Configuration;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using AventStack.ExtentReports.Gherkin.Model;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using System.Reflection;
using System.Collections.Concurrent;

namespace FrameWork.Steps.WebSteps
{
    [Binding]
    public class CalculoDoValorDoIRSteps
    {
        IWebDriver Driver;
        private readonly string uri = "https://www.calcule.net/trabalhista/calculo-imposto-de-renda-irrf/";
        private static Screenshot ss;
        string path = @"C:\Users\wncg\source\repos\FrameWork\FrameWork\Util\";
        private static ConcurrentDictionary<string, string> _returnVariables = new ConcurrentDictionary<string, string>();


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
            _Screenshot("_Login.png");
        }

        [Given(@"preencho o campo '(.*)' com o valor '(.*)'")]
        public void DadoPreenchoOCampoComOValor_(string p0, string p1)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.Id(p0)).SendKeys(p1);
            _Screenshot($"_Texto.png");
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
            _Screenshot($"_Texto.png");
        }

        [When(@"clico em '(.*)'")]
        public void QuandoClicoEmCalcular(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.XPath(p0)).Click();
            _Screenshot($"_Click.png");
        }

        [Then(@"clico em '(.*)'")]
        public void EntaoClicoEmCalcular(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.XPath(p0)).Click();
            _Screenshot($"_Click.png");
        }

        [Given(@"clico em '(.*)'")]
        public void DadoClicoEmCalcular(string p0)
        {
            foreach (var variable in _returnVariables)
            {
                p0 = p0.Replace($"{{{variable.Key}}}", variable.Value);
            }
            this.Driver.FindElement(By.XPath(p0)).Click();
            _Screenshot($"_Click.png");
        }


        [Then(@"vejo '(.*)'")]
        public void EntaoVejo(string p0)
        {
            string valor = this.Driver.FindElement(By.XPath("//*[@id='calcform']/table/tbody/tr[2]/td")).Text;
            //Thread.Sleep(1000);
            Console.WriteLine("Pelo Assert");
            Assert.AreEqual(valor, p0);
        }

        [Given(@"Eu salvo o valor '(.*)' como '(.*)'")]
        public void GivenEuSalvoOValorComo(string p0, string p1)
        {
            _returnVariables.TryAdd(p1, p0);

        }

        public void _Screenshot(string p0)
        {
            Thread.Sleep(1000);
            ss = ((ITakesScreenshot)this.Driver).GetScreenshot();
            //Use it as you want now
            string screenshot = ss.AsBase64EncodedString;
            byte[] screenshotAsByteArray = ss.AsByteArray;
            ss.SaveAsFile($@"C:\Users\wncg\source\repos\FrameWork\FrameWork\Util\Screenshot\{DateTime.Now.ToString("ddMMyyyyHHmmssffff") + p0}", ScreenshotImageFormat.Png); //use any of the built in image formating
            ss.ToString();//same as string screenshot = ss.AsBase64EncodedString;
            Thread.Sleep(1000);
        }
    }
}
