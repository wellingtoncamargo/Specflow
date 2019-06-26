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

namespace BDD2
{
    [Binding]
    public class CalculoDoValorDoIRSteps
    {
        
        IWebDriver Driver;
        //private string uri = "https://www.calcule.net/trabalhista/calculo-imposto-de-renda-irrf/";
        

        [BeforeScenario]
        public void Init()
        {
            this.Driver = new ChromeDriver();
        }

        [AfterScenario]
        public void Close()
        {
            this.Driver.Close();
            this.Driver.Dispose();
        }

        [Given(@"que estou na página IR '(.*)'")]
        public void DadoQueEstouNaPaginaIR(string uri)
        {
            this.Driver.Navigate().GoToUrl(uri);
            //Thread.Sleep(1000);
        }
        
        [Given(@"preencho o campo '(.*)' com o valor '(.*)'")]
        public void DadoPreenchoOCampoComOValor_(string p0, string p1)
        {
            this.Driver.FindElement(By.ClassName(p0)).SendKeys(p1);
            //Thread.Sleep(1000);
        }
        
        [When(@"clico em '(.*)'")]
        public void QuandoClicoEmCalcular_(string p0)
        {
            this.Driver.FindElement(By.ClassName(p0)).Click();
            //Thread.Sleep(3000);
        }
        
        [Then(@"vejo '(.*)'")]
        public void EntaoVejo(string p0)
        {
            string valor = this.Driver.FindElement(By.ClassName("//*[@id='calcform']/table/tbody/tr[2]/td")).Text;
            //Thread.Sleep(1000);
            Console.WriteLine("Pelo Assert");
            Assert.AreEqual(valor, p0);
            
            //if (valor != p0)
            //{
                //  Console.WriteLine("Ok, pelo If");
            //}
            

        }   
    }
}
