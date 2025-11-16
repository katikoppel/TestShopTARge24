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
            DeleteSpaceshipData();
            CreateSpaceship_WithWrongData();
            UpdateSpaceship_WithWrongData();
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
            var nameInDetails = idOfTestNameDetails.Text;

            IWebElement idOfTestClassificationDetails = driver.FindElement(By.Id("testIdClassificationDetails"));
            var classificationInDetails = idOfTestClassificationDetails.Text;

            Assert.That(nameInDetails.Contains("SpaceshipOne"));
            Assert.That(classificationInDetails.Contains("BigOne"));

            IWebElement idOfBack = driver.FindElement(By.Id("testIdIndexDetails"));
            idOfBack.Click();
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

        private static void DeleteSpaceshipData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement idOfSpaceshipLink = driver.FindElement(By.Id("spaceship"));
            idOfSpaceshipLink.Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("SpaceshipTWO"))
                {
                    row.FindElement(By.LinkText("Delete")).Click();
                    break;
                }
            }

            IWebElement deleteButton = driver.FindElement(By.Id("testIdDelete"));
            deleteButton.Click();

            Thread.Sleep(1000);
        }
        private static void CreateSpaceship_WithWrongData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            driver.FindElement(By.Id("spaceship")).Click();
            driver.FindElement(By.Id("testIdCreate")).Click();

            driver.FindElement(By.Id("testIdNameCU")).SendKeys("asd");
            driver.FindElement(By.Id("testIdClassificationCU")).SendKeys("asd");
            driver.FindElement(By.Id("testIdBuiltDateCU")).SendKeys("11/16/2025 11:11 AM");

            IWebElement crew = driver.FindElement(By.Id("testIdCrewCU"));
            crew.SendKeys("big");

            IWebElement enginePower = driver.FindElement(By.Id("testIdEnginePowerCU"));
            enginePower.SendKeys("123");

            driver.FindElement(By.Id("testIdCreateCU")).Click();

            Assert.That(driver.Url.Contains("create"));

            Console.WriteLine("Creating a new spaceship was blocked because of wrong data type in Crew field.");
        }

        private static void UpdateSpaceship_WithWrongData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            driver.FindElement(By.Id("spaceship")).Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("SpaceshipTWO"))
                {
                    row.FindElement(By.LinkText("Update")).Click();
                    break;
                }
            }

            IWebElement name = driver.FindElement(By.Id("testIdNameCU"));
            name.Clear();
            name.SendKeys("SomeShip");

            IWebElement classification = driver.FindElement(By.Id("testIdClassificationCU"));
            classification.Clear();
            classification.SendKeys("SmallShip");

            IWebElement crew = driver.FindElement(By.Id("testIdCrewCU"));
            crew.Clear();
            crew.SendKeys("small");

            driver.FindElement(By.Id("testIdUpdateCU")).Click();

            Assert.That(driver.Url.Contains("update"));

            Console.WriteLine("Updating spaceship was blocked because of wrong data type in Crew field.");
        }
    }
}