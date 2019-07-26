using AventStack.ExtentReports;
using AventStack.ExtentReports.Core;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
namespace FrameWork.Common
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest featureName; // nodo para a Feature
        private static ExtentTest scenario; // nodo para o Scenario
        private static AventStack.ExtentReports.ExtentReports extent; // objeto do ExtentReports que será criado
        private static ExtentHtmlReporter reporter; // objeto do ExtentReports que será criado
        private readonly String ScreenshotFilePath = $@"C:\Users\wncg\source\Specflow\FrameWork_Core\FrameWork\Util\Screenshot\";

        // aqui estou salvando na pasta bin/debug do projeto, o arquivo de relatório chamado ExtentReportAmazon.html
        private static readonly string PathReport = $"{AppDomain.CurrentDomain.BaseDirectory}/ExtentReport.html";

        [BeforeTestRun]
        public static void InitializeReport()
        {
            //Inicialização do ExtentReport antes dos testes iniciarem.
            string PathTargetReport = ConfigurationManager.AppSettings[PathReport];
            var htmlReporter = new ExtentHtmlReporter(PathTargetReport + @"C:\Users\wncg\source\Specflow\FrameWork_Core\FrameWork\Report\Report.html");
            //htmlReporter.Config.DocumentTitle("Automation Report"); // Tittle of Report
            htmlReporter.Config.Theme = Theme.Dark;
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AddSystemInfo("Selenium","3.141.0" );
            extent.AddSystemInfo("Specflow", "3.1.2-beta");
            extent.AddSystemInfo("NUnit", "3.12.0");
            extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            //Criação dinâmica do nome da feature no relatório de testes
            featureName = extent.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(FeatureContext.Current.FeatureInfo.Title);
        }


        [BeforeScenario]
        public void Initialize()
        {
            scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            scenario.AssignCategory(ScenarioContext.Current.ScenarioInfo.Tags);
        }



        [AfterStep]
        public void InsertReportingSteps()
        {

            // Captura de tela no momento do erro.
            //if (ScenarioContext.Current.TestError != null)
            //{
            //        Browser = ScenarioContext.Current.ScenarioInfo.Title;
            //        Util.Screen.TakeScreenshot(Browser);
            //}

            //Captura dos steps
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            PropertyInfo pInfo = typeof(ScenarioContext).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo getter = pInfo.GetGetMethod(nonPublic: true);
            object TestResult = getter.Invoke(ScenarioContext.Current, null);

            //Validação dos steps caso o mesmo seja executados com sucesso.
            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass(ScenarioContext.Current.StepContext.StepInfo.MultilineText);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Pass(ScenarioContext.Current.StepContext.StepInfo.MultilineText);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass(ScenarioContext.Current.StepContext.StepInfo.MultilineText);
                else if (stepType == "And")
                    scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Pass(ScenarioContext.Current.StepContext.StepInfo.MultilineText);
                scenario.AddScreenCaptureFromPath(ScreenshotFilePath);
            }
            //Validação dos steps caso o mesmo seja executados com insucesso.
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                //Adição da captura de tela de erro no relatório
                featureName.AddScreenCaptureFromPath(ScreenshotFilePath);
            }

            //Status da Execução do Testes Pendente
            if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == Status.Skip.ToString())
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Skipped");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Skipped");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Skipped");
            }
            else if (TestResult.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Info("Step Definition Pending");
                else if (stepType == "When")
                    scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Info("Step Definition Pending");
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Info("Step Definition Pending");
            }

        }

        public class GetScreenShot
        {
            public static string Capture(IWebDriver driver, string screenShotName)
            {
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "Util\\Screenshot\\" + screenShotName + ".png";
                string localpath = new Uri(finalpth).LocalPath;
                screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
                return localpath;
            }
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            //Flush report once test completes
            extent.Flush();
            System.Diagnostics.Process.Start(PathReport);
        }
    }
    //---------------------------------------------------------------------------------------------------------------
    public static class ScenarioExtensionMethod
    {
        // aqui é um método para criar um Scenario passando o tipo de Step
        private static ExtentTest CreateScenario(ExtentTest extent, StepDefinitionType stepDefinitionType)
        {
            // o SpecFlow nos permite pegar o nome do Step usando o ScenarioStepContext.Current
            var scenarioStepContext = ScenarioStepContext.Current.StepInfo.Text;

            switch (stepDefinitionType)
            {
                case StepDefinitionType.Given:
                    return extent.CreateNode<Given>(scenarioStepContext); // cria o nodo para Given

                case StepDefinitionType.Then:
                    return extent.CreateNode<Then>(scenarioStepContext); // cria o nodo para Then

                case StepDefinitionType.When:
                    return extent.CreateNode<When>(scenarioStepContext); // cria o nodo para When
                default:
                    throw new ArgumentOutOfRangeException(nameof(stepDefinitionType), stepDefinitionType, null);
            }
        }

        // aqui temos um método para criar um novo de falha ou erro
        private static void CreateScenarioFailOrError(ExtentTest extent, StepDefinitionType stepDefinitionType)
        {
            var error = ScenarioContext.Current.TestError;

            // se não existir exception então pega a mensagem de erro do ScenarioContext.Current
            if (error.InnerException == null)
            {
                CreateScenario(extent, stepDefinitionType).Error(error.Message);
            }
            else
            {
                // senão cria uma falha passando a exception
                CreateScenario(extent, stepDefinitionType).Fail(error.InnerException);
            }
        }

        // os métodos abaixo só facilitei as chamadas para Given, When e Then
        public static void StepDefinitionGiven(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
                CreateScenario(extent, StepDefinitionType.Given);
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.Given);
        }

        public static void StepDefinitionWhen(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
                CreateScenario(extent, StepDefinitionType.When);
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.When);
        }

        public static void StepDefinitionThen(this ExtentTest extent)
        {
            if (ScenarioContext.Current.TestError == null)
                CreateScenario(extent, StepDefinitionType.Then);
            else
                CreateScenarioFailOrError(extent, StepDefinitionType.Then);
        }
    }
    //---------------------------------------------------------------------------------------------------------------

}
