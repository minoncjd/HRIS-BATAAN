using HRiS.Model;
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

namespace HRiS
{
    /// <summary>
    /// Interaction logic for ChangeAccountRole.xaml
    /// </summary>
    public partial class ChangeAccountRole : MetroWindow
    {
        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();
        public string empno;
        public string userid;

        public ChangeAccountRole()
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
                             where listbal.Select(m => m.UserName).Contains(a.EmployeeNo)
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

                    //
                    cbEmp.SelectedValue = db.Employees.Where(m => m.EmployeeNo == empno).FirstOrDefault().EmployeeID;
                    cbRole.SelectedValue = db.AspNetUserRoles.Where(m => m.UserId == userid).FirstOrDefault().RoleId;
                    txtEmail.Text = db.Employees.Where(m => m.EmployeeNo == empno).FirstOrDefault().PrimaryEmail;
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

        public void ChangeRole()
        {
            try
            {
                using(var db = new LetranIntegratedSystemEntities())
                {
                    if(!String.IsNullOrEmpty(cbRole.Text))
                    {
                        AspNetUserRole userrole = db.AspNetUserRoles.Where(m => m.UserId == userid).FirstOrDefault();
                        AspNetUser user = db.AspNetUsers.Where(m => m.Id == userid).FirstOrDefault();

                        if (userrole != null && user != null)
                        {
                            string roleid = cbRole.SelectedValue.ToString();
                            userrole.RoleId = roleid;
                            user.Email = txtEmail.Text;
                            db.SaveChanges();
                            MessageBox.Show("Account Role changed.","System Success!",MessageBoxButton.OK,MessageBoxImage.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("User Account not found.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account Role cannot be empty.","System Warning!",MessageBoxButton.OK,MessageBoxImage.Warning);
                   
                    }
                    
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong."+ex.Message, "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ChangeRole();
        }
  
    }
}
