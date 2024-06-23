using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SuperBotManagerBase.Attributes;
using SuperBotManagerBase.DB;
using SuperBotManagerBase.RabbitMq.Concreate;
using SuperBotManagerBase.RabbitMq.Core;
using SuperBotManagerBase.Services;
using SuperBotManagerBase.Utils;
using System.Drawing;

namespace IntercityActionsConsumer
{
    public enum ICDiscount
    {
        None, Student
    }
    public class ICBuyTicketActionInput
    {
        public DateTime TripDate { get; set; }
        public string TicketOwner { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICDiscount Discount { get; set; }

        public ICBuyTicketActionInput(Dictionary<string, string> fromInput)
        {
            TicketOwner = fromInput["Ticket owner"];
            From = fromInput["From"];
            To = fromInput["To"];
            Login = fromInput["Login"];
            Password = fromInput["Password"];
            TripDate = DateTime.Parse(fromInput["Trip date"]);

            Enum.TryParse<ICDiscount>(fromInput["Discount"], out var _discount);
            Discount = _discount;

        }
    }
    [ServiceActionConsumer("intercity-buy-ticket")]
    public class IntercityBuyTicketActionConsumer : ActionQueueConsumer
    {
        ISeleniumProvider seleniumProvider;
        public IntercityBuyTicketActionConsumer(ILogger<ActionQueueConsumer> logger, IAppUnitOfWork uow, IActionService actionService, ISeleniumProvider seleniumProvider) : base(logger, uow, actionService)
        {
            this.seleniumProvider = seleniumProvider;
        }


        protected override async Task<Dictionary<string, string>> ExecuteAsync(SuperBotManagerBase.DB.Repositories.Action action, CancellationToken cancelToken)
        {
            var input = new ICBuyTicketActionInput(action.ActionData.Input);

            var ticketInfo = new IntercityTicketPage.TicketInfo()
            {
                DiscountCnt = input.Discount == ICDiscount.None ? 0 : 1,
                NormalCnt = input.Discount == ICDiscount.None ? 1 : 0,
                DiscountType = input.Discount == ICDiscount.Student ? "Studenci do 26 lat" : "",
                PreferredCarriageType = CarriageType.Any,
                PreferredSitType = SitType.Window
            };
            var loginData = new IntercityLoginPage.ICLoginData(input.Login, input.Password);
            while(true)
            {
                if(cancelToken.IsCancellationRequested)
                    return new Dictionary<string, string> { };

                using var driver = seleniumProvider.GetDriver();
                
                try
                {
                    IntercitySearchPage searchPage = new IntercitySearchPage(driver);
                    var page = searchPage.NavigateToSearching()
                        .SearchConnections(input.From, input.To, input.TripDate)
                        .SearchTicket(ticketInfo);


                    //Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                    //Console.WriteLine(screenshot.AsBase64EncodedString);

                    while(page.IsAnnouncement() || !page.CanSit())
                    {
                        if(cancelToken.IsCancellationRequested)
                            return new Dictionary<string, string> { };


                        driver.Navigate().Back();
                        page = new IntercityTicketPage(driver).SearchTicket(ticketInfo);
                    }

                    page.OrderTicket(input.TicketOwner)
                        .Login(loginData)
                        .PayLater()
                        .GoOn();

                    break;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    // driver.Manage().Cookies.DeleteAllCookies();
                    // (driver as IJavaScriptExecutor).ExecuteScript("sessionStorage.clear();");
                    // (driver as IJavaScriptExecutor).ExecuteScript("localStorage.clear();");
                    continue;
                }
            }


            return new Dictionary<string, string> { };
        }
    }

}
