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
using System.Windows.Shapes;
using HRiS.Model;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for AddNewAccount.xaml
    /// </summary>
    public partial class AddNewAccount : MetroWindow
    {

        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();

        public AddNewAccount()
        {
            InitializeComponent();
        }

        public void LoadComboBox()
        {
            try
            {
                EList = new List<HRiSClass.EmpCombo>();
                using (var db = new LetranIntegratedSystemEntities())
                {


                    var listbal = db.AspNetUsers.ToList();

                    var listemp = db.Employees.Where(m => m.Archive == false).ToList();

                    var x = (from a in listemp
                             where !listbal.Select(m => m.UserName).Contains(a.EmployeeNo)
                             select a).ToList();

                    foreach (var i in x)
                    {
                        HRiSClass.EmpCombo ec = new HRiSClass.EmpCombo();
                        ec.EmployeeID = i.EmployeeID;
                        ec.EmployeeNumber = i.EmployeeNo;
                        ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                        EList.Add(ec);
                    }

                    cbEmp.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                    cbEmp.DisplayMemberPath = "EmployeeName";
                    cbEmp.SelectedValuePath = "EmployeeID";

                    var roles = db.AspNetRoles.Where(m => m.Id != "6" && m.Id != "1" && m.Id != "7").ToList();

                    cbRole.ItemsSource = roles.OrderBy(m => m.Name);
                    cbRole.DisplayMemberPath = "Name";
                    cbRole.SelectedValuePath = "Id";
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = new LetranIntegratedSystemEntities())
                {
                    AspNetUser aspuser = new AspNetUser();
                    AspNetUserRole asproleuser = new AspNetUserRole();
                    int EmpID = Convert.ToInt32(cbEmp.SelectedValue);
                    var emp = db.Employees.Find(EmpID);
                    var user = db.AspNetUsers.Where(m => m.UserName == emp.EmployeeNo).FirstOrDefault();
                    if (user != null)
                    {
                        MessageBox.Show("User account for this person already exists!", "System Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(cbRole.Text))
                        {
                            MessageBox.Show("Role is not specified", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        if (String.IsNullOrEmpty(txtEmail.Text))
                        {
                            MessageBox.Show("Email cannot be empty.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        string roleid = cbRole.SelectedValue.ToString();

                        aspuser.Id = Guid.NewGuid().ToString();
                        aspuser.UserName = emp.EmployeeNo;
                        aspuser.Email = emp.PrimaryEmail;
                        aspuser.EmailConfirmed = true;
                        aspuser.PhoneNumberConfirmed = false;
                        aspuser.TwoFactorEnabled = false;
                        aspuser.LockoutEnabled = true;
                        aspuser.AccessFailedCount = 0;
                        aspuser.SecurityStamp = Guid.NewGuid().ToString();
                        var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
                        aspuser.PasswordHash = passwordHasher.HashPassword("letran1620");
                        var adduser = db.AspNetUsers.Add(aspuser);
                        asproleuser.UserId = adduser.Id;
                        asproleuser.RoleId = roleid;
                        db.AspNetUserRoles.Add(asproleuser);

                        if (roleid == "5")
                        {
                            if (db.Faculties.Where(m => m.EmpNo == emp.EmployeeNo).FirstOrDefault() == null)
                            {
                                Faculty f = new Faculty();
                                f.FacultyCode = emp.EmployeeNo;
                                f.EmpNo = emp.EmployeeNo;
                                db.Faculties.Add(f);
                            }
                        }
                        db.SaveChanges();
                        MessageBox.Show("Account successfully created.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void cbEmp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    int EmpID = Convert.ToInt32(cbEmp.SelectedValue);
                    txtEmail.Text = db.Employees.Find(EmpID).PrimaryEmail;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
