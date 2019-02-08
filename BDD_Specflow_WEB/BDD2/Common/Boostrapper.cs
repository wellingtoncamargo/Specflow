using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDD2.Common
{
    public class Bootstrapper : IDisposable
    {
        protected IWebDriver Driver { get; }

        public Bootstrapper(IObjectContainer objectContainer)
        {
            objectContainer.RegisterInstanceAs(new ChromeDriver(), typeof(IWebDriver));
            Driver = objectContainer.Resolve<IWebDriver>();
        }

        public void Dispose()
        {
            Driver?.Quit();
            Driver?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
