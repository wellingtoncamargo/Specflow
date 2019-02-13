using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BDD_SpecFlow_API.Common
{
    [Binding]
    public class Hooks
    {
        private static ExtentTest _feature; // nodo para a Feature
        private static ExtentTest _scenario; // nodo para o Scenario
        private static ExtentReports _extent; // objeto do ExtentReports que será criado
        private static Status logstatus;

        // aqui estou salvando na pasta bin/debug do projeto, o arquivo de relatório chamado ExtentReportAmazon.html
        private static readonly string PathReport = $"{AppDomain.CurrentDomain.BaseDirectory}ExtentReport.html";

        [BeforeTestRun]
        public static void ConfigureReport()
        {
            // aqui informo o caminho do arquivo que será gerado criando um objeto ExtentHtmlReporter
            var reporter = new ExtentHtmlReporter("C:/Users/wncg/source/repos/BDD_SpecFlow_API/BDD_SpecFlow_API/Report/");

            reporter.Config.Theme = Theme.Dark;

            // instancio o objeto ExtentReports
            _extent = new ExtentReports();

            // aqui dou attach no ExtentHtmlReporter
            _extent.AttachReporter(reporter);
        }

        [BeforeFeature]
        public static void CreateFeature()
        {
            // antes de iniciar uma Feature, crie o meu novo de Feature
            // o SpecFlow permite pegar o nome da Feature usando o FeatureContext
            // se não permitisse teríamos que adicionar o nome da nossa feature
            _feature = _extent.CreateTest<AventStack.ExtentReports.Gherkin.Model.Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void CreateScenario()
        {
            // antes de iniciar um cenário, crie o meu nodo de Scenario
            _scenario = _feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        public void Initialize()
        {

            //Create dynamic scenario name
            _scenario = _feature.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }


        [AfterStep]
        public void InsertReportingSteps()
        {

            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();

            if (ScenarioContext.Current.TestError == null)
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "When")
                    _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text); 
                else if (stepType == "And")
                    _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "When")
                    _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
            }

            //Pending Status
            if (ScenarioContext.Current.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                if (stepType == "Given")
                    _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "When")
                    _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");
                else if (stepType == "Then")
                    _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Skip("Step Definition Pending");

            }

        }


        [AfterTestRun]
        public static void FlushExtent()
        {
            // depois de rodar os testes, finalize o objeto do ExtentReports
            // essa função destrói o objeto e cria o arquivo html
            _extent.Flush();

            // aqui abro o arquivo HTML após criá-lo
            System.Diagnostics.Process.Start(PathReport);
        }
    }
}