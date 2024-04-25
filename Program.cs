using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WindowsInput;

class NumberMemoryTest
{
    static void Main()
    {
        // Set up the driver
        IWebDriver driver = new ChromeDriver();

        // Navigate to the number memory website
        driver.Navigate().GoToUrl("https://humanbenchmark.com/tests/number-memory");

        // Give some time for the page to load and maximize the window to full screen
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        driver.Manage().Window.Maximize();

        #region
        IWebElement loginButton = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[3]/div/div[2]/a[2]"));
        loginButton.Click();

        IWebElement inputUser = WaitForElement(driver, By.CssSelector("#root > div > div.css-1f554t4.e19owgy74 > div > div > form > p:nth-child(1) > input[type=text]"), TimeSpan.FromSeconds(10));
        inputUser.SendKeys("Made by Layth Hammad");
        Thread.Sleep(1000);
        inputUser.Clear();
        inputUser.SendKeys("BOT49");

        IWebElement inputKey = WaitForElement(driver, By.CssSelector("#root > div > div.css-1f554t4.e19owgy74 > div > div > form > p:nth-child(2) > input[type=password]"), TimeSpan.FromSeconds(10));
        inputKey.SendKeys("Layth12+");

        IWebElement confirmButton = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[4]/div/div/form/p[3]/input"));
        confirmButton.Click();

        IWebElement numberButton = WaitForElement(driver, By.XPath("//*[@id=\"root\"]/div/div[4]/div/div/div[1]/div/div[3]"), TimeSpan.FromSeconds(10));
        numberButton.Click();

        IWebElement startButton = WaitForElement(driver, By.XPath("//*[@id=\"root\"]/div/div[4]/div/div/div[2]/div[2]/div/div[4]/a"), TimeSpan.FromSeconds(10));
        startButton.Click();

        IWebElement startTest = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[4]/div[1]/div/div/div"));
        Thread.Sleep(1000);
        startTest.Click();
        #endregion

        // Automatically press the start button
        IWebElement start2Button = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[4]/div[1]/div/div/div/div[3]/button"));
        start2Button.Click();

        // Create input simulator
        InputSimulator simulator = new InputSimulator();
        Console.Clear();

        while (true)
        {
            // Get the page source
            string pageSource = driver.PageSource;

            // Use HtmlAgilityPack to parse the page source
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(pageSource);

            // Get the number needed in less than a second using XPath
            string numberXPath = "//*[@id=\"root\"]/div/div[4]/div[1]/div/div/div/div[1]";
            IWebElement numberElement = WaitForElement(driver, By.XPath(numberXPath), TimeSpan.FromHours(10));
            string number = numberElement.Text;
            Console.WriteLine(number);

            try
            {
                // Type in the number into the input field using a CSS selector
                IWebElement inputField = WaitForElement(driver, By.CssSelector("#root > div > div:nth-child(4) > div.number-memory-test.prompt.e12yaanm0.css-18qa6we.e19owgy77 > div > div > div > form > div:nth-child(2) > input[type=text]"), TimeSpan.FromHours(10));
                inputField.SendKeys(number);
                inputField.SendKeys(Keys.Enter);
                Thread.Sleep(1000);
                simulator.Keyboard.KeyPress(WindowsInput.Native.VirtualKeyCode.RETURN);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("ERROR");
                IWebElement inputField = WaitForElement(driver, By.CssSelector("#root > div > div:nth-child(4) > div.number-memory-test.prompt.e12yaanm0.css-18qa6we.e19owgy77 > div > div > div > form > div:nth-child(2) > input[type=text]"), TimeSpan.FromHours(11));
                inputField.SendKeys("49");
                inputField.SendKeys(Keys.Enter);

                IWebElement saveScore = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[4]/div[1]/div/div/div/div[2]/div/button[1]"));
                saveScore.Click();
            }
        }
    }

    // Helper method to wait for an element to be present
    static IWebElement WaitForElement(IWebDriver driver, By by, TimeSpan timeout)
    {
        WebDriverWait wait = new WebDriverWait(driver, timeout);
        return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
    }
}
