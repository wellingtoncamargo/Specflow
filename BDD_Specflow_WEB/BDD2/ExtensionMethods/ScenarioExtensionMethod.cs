using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace BDD2.ExtensionMethods
{
    public static class ScenarioExtensionMethod
    {
        // aqui é um método para criar um Scenario passando o tipo de Step
        private static AventStack.ExtentReports.ExtentTest CreateScenario(ExtentTest extent, StepDefinitionType stepDefinitionType)
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
}
