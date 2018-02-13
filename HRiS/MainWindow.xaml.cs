using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HRiS.Model;
using MahApps.Metro.Controls.Dialogs;
using System.IO.IsolatedStorage;
using System.IO;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();

        public MainWindow()
        {
            InitializeComponent();
            usernametb.Focus();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var user = db.AspNetUsers.ToList();
        }

        private void usernametb_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void passwordtb_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Rememberme_Click(object sender, RoutedEventArgs e)
        {
            if (Rememberme.IsChecked == false)
            {
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                if(isolatedStorage.FileExists("login"))
                    isolatedStorage.DeleteFile("login");
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);


            IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            if (isolatedStorage.FileExists("login"))
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("login", FileMode.OpenOrCreate, isolatedStorage))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string uname = sr.ReadLine();
                        string pass = sr.ReadLine();
                        int syear = Convert.ToInt32(sr.ReadLine());
                        usernametb.Text = uname;
                        passwordtb.Password = pass;

                    }
                    Rememberme.IsChecked = true;
                }

            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            if (Rememberme.IsChecked == true)
            {
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("login", FileMode.Create, isolatedStorage))
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine(usernametb.Text);
                        sw.WriteLine(passwordtb.Password);

                    }
                }
            }


        }

        private void logbtn_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            logme();
            this.Cursor = Cursors.Arrow;
        }

        public async void logme()
        {
            try
            {
                this.Cursor = Cursors.Wait;
                db = new LetranIntegratedSystemEntities();

                if (String.IsNullOrEmpty(usernametb.Text) || String.IsNullOrEmpty(passwordtb.Password))
                {
                    await this.ShowMessageAsync("Warning", "Employee number and/or Password  cannot be empty.");
                    return;
                }
                var user = db.AspNetUsers.Where(m => m.UserName == usernametb.Text).FirstOrDefault();
                if (user != null)
                {

                    var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
                    if (passwordHasher.VerifyHashedPassword(user.PasswordHash, passwordtb.Password) == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                    {
                        var employee = (from a in db.Employees
                                        join b in db.AcademicDepartments on a.EmployeeDepartmentID equals b.AcaDeptID
                                        where a.Archive == false && a.EmployeeNo == usernametb.Text
                                        select a).FirstOrDefault();
                        if (employee == null)
                        {
                            await this.ShowMessageAsync("Warning", "Employee not found.");
                        }
                        else
                        {
                            if (employee.EmployeeDepartmentID == 1 || employee.EmployeeDepartmentID == 13)
                            {
                                 //Main Menu
                                App.EmployeeName = employee.FirstName + " " + employee.LastName;
                                App.EmployeeNumber = employee.EmployeeNo;
                                App.EmployeeID = employee.EmployeeID;
                                App.EmployeeUserName = employee.FirstName.Substring(0, 1).ToUpper() + employee.LastName.ToUpper();
                                MainMenu x = new MainMenu();
                                x.Show();
                                this.Close();
                            
                            }
                            else
                            {
                                await this.ShowMessageAsync("Warning", "Invalid login type.");
                                return;
                            }
                        }
                      
                    }
                    else
                    {


                        await this.ShowMessageAsync("Warning", "Password is incorrect.");                      
                        return;
                    }

                }
                else
                {
                    await this.ShowMessageAsync("Warning", "Employee number not found.");
                    return;
                }



                this.Cursor = Cursors.Arrow;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
