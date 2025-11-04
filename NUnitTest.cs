using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace ShopTARge24Testing
{
    public class Test
    {
        private static void Main(string[] args)
        {
            InsertSpaceshipData();
            UpdateSpaceshipData();
            ViewSpaceshipData();
        }

        private static void InsertSpaceshipData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement idOfSpaceshipLink = driver.FindElement(By.Id("spaceship"));
            idOfSpaceshipLink.Click();

            IWebElement idOfCreateButton = driver.FindElement(By.Id("testIdCreate"));
            idOfCreateButton.Click();

            IWebElement idOfName = driver.FindElement(By.Id("testIdNameCU"));
            idOfName.SendKeys("SpaceshipOne");

            IWebElement idOfClassification = driver.FindElement(By.Id("testIdClassificationCU"));
            idOfClassification.SendKeys("BigOne");

            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("MM-dd-yyyy HH:mm:ss");

            IWebElement idOfBuiltDate = driver.FindElement(By.Id("testIdBuiltDateCU"));
            idOfBuiltDate.SendKeys(formattedDate);

            IWebElement idOfCrew = driver.FindElement(By.Id("testIdCrewCU"));
            idOfCrew.SendKeys("12345");

            IWebElement idOfEnginePower = driver.FindElement(By.Id("testIdEnginePowerCU"));
            idOfEnginePower.SendKeys("6789");

            IWebElement idOfCreate = driver.FindElement(By.Id("testIdCreateCU"));
            idOfCreate.Click();
        }

        private static void ViewSpaceshipData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement idOfSpaceshipLink = driver.FindElement(By.Id("spaceship"));
            idOfSpaceshipLink.Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("SpaceshipOne"))
                {
                    row.FindElement(By.LinkText("Details")).Click();
                    break;
                }
            }

            IWebElement idOfTestNameDetails = driver.FindElement(By.Id("testIdNameDetails"));
            var nameindetails = idOfTestNameDetails.Text;

            IWebElement idOfTestClassificationDetails = driver.FindElement(By.Id("testIdClassificationDetails"));
            var classificationindetails = idOfTestClassificationDetails.Text;

            Assert.That(nameindetails.Contains("SpaceshipOne"));
            Assert.That(classificationindetails.Contains("BigOne"));

            Console.WriteLine("Test passed");
        }

        private static void UpdateSpaceshipData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement idOfSpaceshipLink = driver.FindElement(By.Id("spaceship"));
            idOfSpaceshipLink.Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("SpaceshipOne"))
                {
                    row.FindElement(By.LinkText("Update")).Click();
                    break;
                }
            }

            IWebElement idOfName = driver.FindElement(By.Id("testIdNameCU"));
            idOfName.Clear();
            idOfName.SendKeys("SpaceshipTWO");

            IWebElement idOfClassification = driver.FindElement(By.Id("testIdClassificationCU"));
            idOfClassification.Clear();
            idOfClassification.SendKeys("BiggestSHIP");

            IWebElement idOfBuiltDate = driver.FindElement(By.Id("testIdBuiltDateCU"));
            idOfBuiltDate.SendKeys("111111111111");

            IWebElement idOfCrew = driver.FindElement(By.Id("testIdCrewCU"));
            idOfCrew.Clear();
            idOfCrew.SendKeys("54321");

            IWebElement idOfEnginePower = driver.FindElement(By.Id("testIdEnginePowerCU"));
            idOfEnginePower.Clear();
            idOfEnginePower.SendKeys("9876");

            IWebElement idOfUpdate = driver.FindElement(By.Id("testIdUpdateCU"));
            idOfUpdate.Click();

            Thread.Sleep(1000);
        }
    }
}