using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HabrCareerAddFriends
{
    public partial class Form1 : Form
    {
        int page;
        public Form1()
        {
            InitializeComponent();
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            if (StartPageTextBox.Text == "")
            {
                page = 1;
            }
            else
            {
                page = Convert.ToInt32(StartPageTextBox.Text);
            }

            if (LoginTextBox.Text != "" && PassTextBox2.Text != "" && URLtextBox3.Text != "")
            {
                if (LoginTextBox.Text != "" && PassTextBox2.Text != "")
                {
                    Properties.Settings.Default.login = LoginTextBox.Text;
                    Properties.Settings.Default.pass = PassTextBox2.Text;
                    Properties.Settings.Default.Save();

                }
                IWebDriver driver = new ChromeDriver(); ;
                //driver.Url = @"https://account.habr.com/login/?state=2b560e9ae7302456aa935e9f7f9bc042&consumer=habr&hl=ru_RU";
                driver.Url = "https://career.habr.com/";
                Thread.Sleep(2000);
                //await Task.Delay(2000);
                driver.FindElement(By.PartialLinkText("Войти")).Click();
                //await Task.Delay(2000);
                Thread.Sleep(2000);

                driver.FindElement(By.XPath(@"//input[contains(@name,'email')]")).SendKeys(LoginTextBox.Text.Trim());
                driver.FindElement(By.XPath(@"//input[contains(@type,'password')]")).SendKeys(PassTextBox2.Text.Trim());
                driver.FindElement(By.XPath(@"//button[contains(@type,'submit')]")).Click();
                //await Task.Delay(5000);
                Thread.Sleep(5000);

                //https://career.habr.com/elle-shabli7/friends
                //driver.Url = URLtextBox3.Text.Trim();
                //await Task.Delay(2000);
                Thread.Sleep(2000);
                bool reload = false;
                bool select = true;
                while (select==true)
                {
                    reload = false;
                    string x ="";
                    driver.Url = $"{URLtextBox3.Text}?page={page}";

                    Thread.Sleep(3000);
                    int count = 0;
                    IList<IWebElement> addFriends = driver.FindElements(By.ClassName("dropdown-target"));
                    for (int i = 0; i < addFriends.Count; i++)
                    {
                        if (count==3)
                        {
                            driver.Url = $"{URLtextBox3.Text}?page={page}";
                            reload = true;
                            break;
                        }
                        if (addFriends[i].Text == "В друзья")
                        {
                            try
                            {
                                addFriends[i].Click();
                                //await Task.Delay(12000);
                                Thread.Sleep(12000);

                            }
                            catch (Exception ex)
                            {
                                count++;
                            }
                        }
                    }
                    if (select==false)
                    {
                        break;
                    }
                    //if (reload == false)
                    //{
                    //    page++;
                    //    Thread.Sleep(2000);
                    //}
                    page++;
                    Thread.Sleep(2000);


                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.login.ToString() != "")
            {
                LoginTextBox.Text = Properties.Settings.Default.login;
                PassTextBox2.Text = Properties.Settings.Default.pass;
            }
        }
    }
}
