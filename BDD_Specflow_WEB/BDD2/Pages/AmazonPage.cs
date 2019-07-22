using FrameWork.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Pages
{
    public class AmazonPage
    {
        private readonly IWebDriver _webDriver;
        private IWebElement _inputSearchBox;

        // aqui recebo o Webdriver como parâmetro do construtor
        public AmazonPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        // abrir a página da Amazon.com
        public void OpenAmazonWebSite()
        {
            _webDriver.Navigate().GoToUrl("http://www.amazon.com");
        }

        // aqui irei informar o que quero pesquisar na caixa de texto
        public void SearchBy(string text)
        {
            _inputSearchBox = _webDriver.FindElement(By.Id("twotabsearchtextbox"));
            _inputSearchBox.SendKeys(text);
        }

        // adiono o SUBMIT para enviar a informação (como se fosse pressionar a teclar ENTER)
        public void PressEnterKeyForSubmit()
        {
            _inputSearchBox.Submit();
        }

        // pego o primeiro item da lista de produtos encontrados
        public void ChooseTheFirstItemOfList()
        {
            _webDriver.FindElement(By.XPath("//*[@id=\"result_0\"]//a")).Click();
        }

        public void ClickInAddToCartButton()
        {
            _webDriver.FindElement(By.Id("add-to-cart-button")).Click();
        }

        // obtenho a quantidade de itens que estão no carrinho
        public int GetItemsInCart()
        {
            var count = _webDriver.FindElement(By.Id("nav-cart-count")).Text;
            return string.IsNullOrEmpty(count) ? 0 : Convert.ToInt32(count);
        }
    }
}
