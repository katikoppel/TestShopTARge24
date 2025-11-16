using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using NUnit.Framework;

namespace ShopTARge24Testing
{
    public class Test1
    {
        private static void Main(string[] args)
        {
            InsertKindergartenData();
            UpdateKindergartenData();
            ViewKindergartenData();
            DeleteKindergartenData();
            CreateKindergarten_WithWrongData();
            UpdateKindergarten_WithWrongData();
        }

        private static void InsertKindergartenData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement kindergartenLink = driver.FindElement(By.Id("kindergartens"));
            kindergartenLink.Click();

            IWebElement createLink = driver.FindElement(By.Id("testIdCreate"));
            createLink.Click();

            driver.FindElement(By.Id("testIdGroupNameCU")).SendKeys("GroupOne");
            driver.FindElement(By.Id("testIdChildrenCountCU")).SendKeys("10");
            driver.FindElement(By.Id("testIdKindergartenNameCU")).SendKeys("KindergartenTestName");
            driver.FindElement(By.Id("testIdTeacherNameCU")).SendKeys("Teacher Name");

            driver.FindElement(By.Id("testIdCreateCU")).Click();
        }

        private static void ViewKindergartenData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement kindergartenLink = driver.FindElement(By.Id("kindergartens"));
            kindergartenLink.Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("GroupTwo"))
                {
                    row.FindElement(By.LinkText("Details")).Click();
                    break;
                }
            }

            IWebElement groupName = driver.FindElement(By.Id("testIdGroupNameDetails"));
            IWebElement childrenCount = driver.FindElement(By.Id("testIdChildrenCountDetails"));

            Assert.That(groupName.Text.Contains("GroupTwo"));
            Assert.That(childrenCount.Text.Contains("25"));
        }

        private static void UpdateKindergartenData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement kindergartenLink = driver.FindElement(By.Id("kindergartens"));
            kindergartenLink.Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("GroupOne"))
                {
                    row.FindElement(By.LinkText("Update")).Click();
                    break;
                }
            }

            IWebElement groupName = driver.FindElement(By.Id("testIdGroupNameCU"));
            groupName.Clear();
            groupName.SendKeys("GroupTwo");

            IWebElement childrenCount = driver.FindElement(By.Id("testIdChildrenCountCU"));
            childrenCount.Clear();
            childrenCount.SendKeys("25");

            IWebElement kindergartenName = driver.FindElement(By.Id("testIdKindergartenNameCU"));
            kindergartenName.Clear();
            kindergartenName.SendKeys("Big Kindergarten");

            IWebElement teacherName = driver.FindElement(By.Id("testIdTeacherNameCU"));
            teacherName.Clear();
            teacherName.SendKeys("Teacher Two");

            driver.FindElement(By.Id("testIdUpdateCU")).Click();

            Thread.Sleep(1000);
        }

        private static void DeleteKindergartenData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            IWebElement kindergartenLink = driver.FindElement(By.Id("kindergartens"));
            kindergartenLink.Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("GroupTwo"))
                {
                    row.FindElement(By.LinkText("Delete")).Click();
                    break;
                }
            }

            IWebElement deleteButton = driver.FindElement(By.Id("testIdDelete"));
            deleteButton.Click();

            Thread.Sleep(1000);
        }

        private static void CreateKindergarten_WithWrongData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            driver.FindElement(By.Id("kindergartens")).Click();
            driver.FindElement(By.Id("testIdCreate")).Click();

            driver.FindElement(By.Id("testIdGroupNameCU")).SendKeys("Bad Group");
            driver.FindElement(By.Id("testIdKindergartenNameCU")).SendKeys("Bad Kids");
            driver.FindElement(By.Id("testIdTeacherNameCU")).SendKeys("Bad Teacher");

            IWebElement childrenCount = driver.FindElement(By.Id("testIdChildrenCountCU"));
            childrenCount.SendKeys("a lot");

            driver.FindElement(By.Id("testIdCreateCU")).Click();

            Assert.That(driver.Url.ToLower().Contains("create"));

            Console.WriteLine("Creating kindergarten was blocked because ChildrenCount had wrong data type.");
        }

        private static void UpdateKindergarten_WithWrongData()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Url = "https://localhost:7282/";

            driver.FindElement(By.Id("kindergartens")).Click();

            var rows = driver.FindElements(By.CssSelector("table tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("GroupTwo"))
                {
                    row.FindElement(By.LinkText("Update")).Click();
                    break;
                }
            }

            IWebElement groupName = driver.FindElement(By.Id("testIdGroupNameCU"));
            groupName.Clear();
            groupName.SendKeys("UpdateTestGroup");

            IWebElement kindergartenName = driver.FindElement(By.Id("testIdKindergartenNameCU"));
            kindergartenName.Clear();
            kindergartenName.SendKeys("UpdateKindergartenName");

            IWebElement teacherName = driver.FindElement(By.Id("testIdTeacherNameCU"));
            teacherName.Clear();
            teacherName.SendKeys("UpdateTeacherName");

            IWebElement childrenCount = driver.FindElement(By.Id("testIdChildrenCountCU"));
            childrenCount.Clear();
            childrenCount.SendKeys("a lot");

            driver.FindElement(By.Id("testIdUpdateCU")).Click();

            Assert.That(driver.Url.ToLower().Contains("update"));

            Console.WriteLine("Updating kindergarten was blocked because ChildrenCount had wrong data type.");
        }
    }
}