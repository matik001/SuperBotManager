using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Services
{
    public interface ISeleniumProvider
    {
        public IWebDriver GetDriver();
    }
    public class SeleniumProvider : ISeleniumProvider
    {
        private readonly bool local;
        private readonly bool headless;
        private readonly string hostname;
        private readonly int port;
        private readonly string proxy;
        public SeleniumProvider(IConfigurationManager configurationManager)
        {
            var section = configurationManager.GetSection("Selenium");
            if(section == null)
            {
                throw new Exception("No selenium section in configuration");
            }
            headless = section.GetValue<bool>("Headless");
            local = section.GetValue<bool>("Local");
            hostname = section.GetValue<string>("Hostname");
            port = section.GetValue<int>("Port");
            proxy = section.GetValue<string>("Proxy");
        }

        public IWebDriver GetDriver()
        {

            var options = new ChromeOptions();
            if(headless)
                options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--window-size=1295,825");
            if(!string.IsNullOrEmpty(proxy)) {
                var httpproxy = new Proxy
                {
                    Kind = ProxyKind.Manual,
                    HttpProxy = proxy,
                    SslProxy = proxy
                };
                options.Proxy = httpproxy;
            }


            if(local)
            {
                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;


                return new ChromeDriver(service, options);
            }
            else
            {

                return new RemoteWebDriver(new Uri($"http://{hostname}:{port}"), options);
            }
        }
    }
}
