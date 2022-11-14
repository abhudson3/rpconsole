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

System.Console.WriteLine("What crn would you like to check?");
string crnInput = Console.ReadLine();

System.Console.WriteLine("Type in the email you would like to use and hit enter");
string toEmail = Console.ReadLine();

WebDriver driver = new ChromeDriver();
driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/ua_bwckschd.p_disp_detail_sched?term_in=202310&crn_in=" + crnInput);

regChecker(toEmail, driver);

//working registration code
// System.Console.WriteLine("What is your mybama username?");
// string mbUsername = Console.ReadLine();
// System.Console.WriteLine("What is your mybama password?");
// string mbPassword = Console.ReadLine();
// driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/bwskfreg.P_AltPin");
// System.Threading.Thread.Sleep(3000);
// driver.FindElement(By.Id("UserID")).SendKeys(mbUsername);
// System.Threading.Thread.Sleep(500);
// driver.FindElement(By.Id("PIN")).SendKeys(mbPassword);
// driver.FindElement(By.XPath("/html/body/div[3]/form/p/input")).Click();
// System.Threading.Thread.Sleep(500);
// driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/bwskfreg.P_AltPin");
// driver.FindElement(By.XPath("/html/body/div[3]/form/input")).Click();
// driver.FindElement(By.XPath("/html/body/div[3]/form/table[3]/tbody/tr[2]/td[1]/input[2]")).SendKeys(crnInput);//crn
// Environment.Exit(0);
// driver.Navigate().GoToUrl("https://ssb.ua.edu/pls/PROD/ua_bwckschd.p_disp_detail_sched?term_in=202310&crn_in=" + crnInput);

// string menuReturn = "";
// while(menuReturn != "-1"){
//     System.Console.WriteLine(@"Welcome to RegiPro! Would you like for the program to register you for a class or notify you when it opens?
//     1. Register for a class
//     2. Notify me when a class opens
//     3. Exit RegiPro");
//     menuReturn = Console.ReadLine();
//     if(menuReturn == "1")
//     {
//         Console.Clear();
//         System.Console.WriteLine(@"Register for a class - Press the corresponding number to select an option:");
//     }
//     if(menuReturn == "2")
//     {
//         Console.Clear();

//     }
// }




static void fileChecker(string toEmail, WebDriver driver){
    StreamReader inFile = new StreamReader("output.txt");
    string line = inFile.ReadLine();
    if(int.Parse(line) > 0)
    {
        SendMail(toEmail);
        driver.Close();
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





















// driver.FindElement(By.Id("login-username")).SendKeys("hudsonab123@gmail.com");
// driver.FindElement(By.Id("login-password")).SendKeys("jojwor-woMxy9-wetkix");
// driver.FindElement(By.Id("login-button")).Click();
// System.Threading.Thread.Sleep(4000);
// driver.FindElement(By.ClassName("Button-y0gtbx-0")).Click();
// System.Threading.Thread.Sleep(10000);
// //write the result of the element "link-subtle" to a text file
// string output = "";
// driver.FindElement(By.ClassName("link-subtle")).Text();



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
    


