using BDD2.Common;
using BDD2.Pages;
using BoDi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace BDD2.Steps
{
    [Binding]
    // aqui eu herdei da classe Boostrapper, pois temos o objeto Driver criado nele
    // poderia instanciar o ChromeDriver diretamente no construtor vazio desta classe de Step, mas deixei separado
    public sealed class AmazonBuyDellXPS : Bootstrapper
    {
        // nosso objeto AmazonPage
        private readonly AmazonPage _amazonPage;

        public AmazonBuyDellXPS(IObjectContainer objectContainer) : base(objectContainer)
        {
            _amazonPage = new AmazonPage(this.Driver); // aqui o Driver que já foi criado
        }

        [Given(@"I open Amazon website")]
        public void GivenIOpenAmazonWebsite()
        {
            // abrir a página da Amazon
            _amazonPage.OpenAmazonWebSite();
        }

        [Given(@"I search by Dell XPS")]
        public void GivenISearchBySoapDove()
        {
            // pesquiso por "dell xps"
            _amazonPage.SearchBy("dell xps");
        }

        [Given(@"I press enter key for submit")]
        public void GivenIPressEnterKeyForSubmit()
        {
            // simulo o ENTER na caixa de pesquisa
            _amazonPage.PressEnterKeyForSubmit();
        }

        [Given(@"I choose the first item of list")]
        public void GivenIChooseTheFirstItemOfList()
        {
            // seleciono o primeiro item da listagem
            _amazonPage.ChooseTheFirstItemOfList();
        }

        [When(@"I click in Add to Cart button")]
        public void WhenIClickInAddToCartButton()
        {
            // clico para adicionar ao carrinho
            _amazonPage.ClickInAddToCartButton();
        }

        [Then(@"Must have one item in cart")]
        public void ThenMustHaveItemInCart()
        {
            // aqui obtenho a quantidade de itens do carrinho
            var expected = _amazonPage.GetItemsInCart();

            // aqui faço meus Asserts para validar
            Assert.IsTrue(expected > 0); // se a quantidade é > 0
            Assert.AreEqual(expected, 1); // se a quantidade é = 1
        }
    }
}
