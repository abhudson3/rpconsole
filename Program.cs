using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;




string menuReturn = "";
while(menuReturn != "-1"){
    System.Console.WriteLine(@"Welcome to RegiPro! Would you like for the program to register you for a class or notify you when it opens?
1. Register for a class
2. Notify me when a class opens
3. Exit RegiPro");
    menuReturn = Console.ReadLine();
    if(menuReturn == "1")
    {
        Console.Clear();
        RegMethod();
        
    }
    else if(menuReturn == "2")
    {
        Console.Clear();
        
        EmailMethod();

    }else
    {
        Console.Clear();
        System.Console.WriteLine("Thank you for using RegiPro!");
        Environment.Exit(0);
    }
}

static void RegMethod()
{
    System.Console.WriteLine("What is your mybama username?");
    string mbUsername = Console.ReadLine();
    System.Console.WriteLine("What is your mybama password?");
    string mbPassword = Console.ReadLine();
    System.Console.WriteLine("What crn would you like to check?");
    string crnInput = Console.ReadLine();
    
    WebDriver driver = new ChromeDriver();
    driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/ua_bwckschd.p_disp_detail_sched?term_in=202310&crn_in=" + crnInput);

    regChecker(mbUsername, mbPassword, crnInput, driver);

    static void fileChecker(string mbUsername, string mbPassword, string crnInput, WebDriver driver){

    StreamReader inFile = new StreamReader("output.txt");
    string line = inFile.ReadLine();
    if(int.Parse(line) > 0)
    {
        driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/bwskfreg.P_AltPin");
        System.Threading.Thread.Sleep(3000);
        driver.FindElement(By.Id("UserID")).SendKeys(mbUsername);
        System.Threading.Thread.Sleep(500);
        driver.FindElement(By.Id("PIN")).SendKeys(mbPassword);
        driver.FindElement(By.XPath("/html/body/div[3]/form/p/input")).Click();
        System.Threading.Thread.Sleep(500);
        driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/bwskfreg.P_AltPin");
        driver.FindElement(By.XPath("/html/body/div[3]/form/input")).Click();
        driver.FindElement(By.XPath("/html/body/div[3]/form/table[3]/tbody/tr[2]/td[1]/input[2]")).SendKeys(crnInput);//crn
        System.Threading.Thread.Sleep(500);
        driver.FindElement(By.XPath("/html/body/div[3]/form/input[19]")).Click();
        System.Threading.Thread.Sleep(5000);
        driver.Close();
        System.Console.WriteLine("Thanks for using RegiPro! You should have been registered!");
        Environment.Exit(0);
    }else
    {
        regChecker(mbUsername, mbPassword, crnInput, driver);
    }
}


static void regChecker(string mbUsername, string mbPassword, string crnInput, WebDriver driver)
{
    var content = driver.FindElement(By.XPath("/html/body/div[3]/table[1]/tbody/tr[2]/td/table[1]/tbody/tr[2]/td[3]"));
    StreamWriter outFile = new StreamWriter("output.txt");
    outFile.WriteLine(content.Text);
    outFile.Close();
    driver.Navigate().Refresh();
    
    System.Threading.Thread.Sleep(60000);
    fileChecker(mbUsername, mbPassword, crnInput, driver);
}
}


static void EmailMethod()
{
    System.Console.WriteLine("What crn would you like to check?");
    string crnInput = Console.ReadLine();

    System.Console.WriteLine("Type in the email you would like to use and hit enter");
    string toEmail = Console.ReadLine();
    WebDriver driver = new ChromeDriver();
    driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/ua_bwckschd.p_disp_detail_sched?term_in=202310&crn_in=" + crnInput);

    regChecker(toEmail, driver);
    static void fileChecker(string toEmail, WebDriver driver){
    StreamReader inFile = new StreamReader("output.txt");
    string line = inFile.ReadLine();
    if(int.Parse(line) > 0)
    {
        SendMail(toEmail);
        driver.Close();
        System.Console.WriteLine("Thanks for using RegiPro!");
        Environment.Exit(0);
    }else
    {
        regChecker(toEmail, driver);
    }
}


static void regChecker(string toEmail, WebDriver driver)
{
    var content = driver.FindElement(By.XPath("/html/body/div[3]/table[1]/tbody/tr[2]/td/table[1]/tbody/tr[2]/td[3]"));
    StreamWriter outFile = new StreamWriter("output.txt");
    outFile.WriteLine(content.Text);
    outFile.Close();
    driver.Navigate().Refresh();
    
    System.Threading.Thread.Sleep(60000);
    fileChecker(toEmail, driver);
}
}



static void SendMail(string toEmail)
{
    MailMessage mail = new MailMessage();
    
    mail.From = new MailAddress("regiprotest2@gmail.com");
    mail.To.Add(toEmail);
    mail.Subject = "Class drop notification";
    mail.Body = "Class has an open slot";
    
    SmtpClient SmtpServer = new SmtpClient();
    SmtpServer.Host = "smtp.gmail.com";
    SmtpServer.Port = 587;
    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
    SmtpServer.Credentials = new NetworkCredential("regiprotest2@gmail.com", "stsjpserbidjwwby");
    SmtpServer.Timeout = 20000;
    SmtpServer.EnableSsl = true;

    SmtpServer.Send(mail);
}
    


