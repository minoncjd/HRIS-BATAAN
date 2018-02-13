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
    /// Interaction logic for AccountManagement.xaml
    /// </summary>
    public partial class AccountManagement : MetroWindow
    {
        List<HRiSClass.AccountManagementList> AdminList = new List<HRiSClass.AccountManagementList>();
        List<HRiSClass.AccountManagementList> EmpList = new List<HRiSClass.AccountManagementList>();
        List<HRiSClass.AccountManagementList> FacultyList = new List<HRiSClass.AccountManagementList>();
        List<HRiSClass.AccountManagementList> FacultyHeadList = new List<HRiSClass.AccountManagementList>();
        List<HRiSClass.AccountManagementList> GuidanceList = new List<HRiSClass.AccountManagementList>();
        List<HRiSClass.AccountManagementList> HRList = new List<HRiSClass.AccountManagementList>();
        List<HRiSClass.AccountManagementList> RegistrarList = new List<HRiSClass.AccountManagementList>();
        public AccountManagement()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAccount();
        }

        public void GetAccount()
        {
            try
            {
                AdminList = new List<HRiSClass.AccountManagementList>();
                EmpList = new List<HRiSClass.AccountManagementList>();
                FacultyList = new List<HRiSClass.AccountManagementList>();
                FacultyHeadList = new List<HRiSClass.AccountManagementList>();
                GuidanceList = new List<HRiSClass.AccountManagementList>();
                HRList = new List<HRiSClass.AccountManagementList>();
                RegistrarList = new List<HRiSClass.AccountManagementList>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    //Adminstrator
                    var admin = (from a in db.AspNetUserRoles
                                 join b in db.Employees on a.AspNetUser.UserName equals b.EmployeeNo
                                 where b.Archive == false && a.RoleId == "2"
                                 select new { a.UserId, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), b.AcademicDepartment.AcaDepartmentName, b.PrimaryEmail, b.EmployeePosition.EmployeePositionName }).ToList();
                    foreach (var x in admin)
                    {
                        HRiSClass.AccountManagementList y = new HRiSClass.AccountManagementList();
                        y.AccountID = x.UserId;
                        y.EmployeeNumber = x.EmployeeNo;
                        y.EmployeeName = x.EmployeeName;
                        y.Department = x.AcaDepartmentName;
                        y.EmployeePosition = x.EmployeePositionName;
                        y.Email = x.PrimaryEmail;
                        AdminList.Add(y);
                    }
                    dgAdmin.ItemsSource = AdminList.OrderBy(m => m.EmployeeName);

                    //Employee
                    var emp = (from a in  db.AspNetUserRoles
                               join b in db.Employees on a.AspNetUser.UserName equals b.EmployeeNo
                               where b.Archive == false && a.RoleId == "4"
                               select new { a.UserId, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), b.AcademicDepartment.AcaDepartmentName, b.PrimaryEmail, b.EmployeePosition.EmployeePositionName }).ToList();
                    foreach (var x in emp)
                    {
                        HRiSClass.AccountManagementList y = new HRiSClass.AccountManagementList();
                        y.AccountID = x.UserId;
                        y.EmployeeNumber = x.EmployeeNo;
                        y.EmployeeName = x.EmployeeName;
                        y.Department = x.AcaDepartmentName;
                        y.EmployeePosition = x.EmployeePositionName;
                        y.Email = x.PrimaryEmail;
                        EmpList.Add(y);
                    }
                    dgEmployee.ItemsSource = EmpList.OrderBy(m => m.EmployeeName);

                    //Faculty

                    var fac = (from a in db.AspNetUserRoles
                               join b in db.Employees on a.AspNetUser.UserName equals b.EmployeeNo
                               where b.Archive == false && a.RoleId == "5"
                               select new { a.UserId, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), b.AcademicDepartment.AcaDepartmentName, b.PrimaryEmail, b.EmployeePosition.EmployeePositionName }).ToList();
                    foreach (var x in fac)
                    {
                        HRiSClass.AccountManagementList y = new HRiSClass.AccountManagementList();
                        y.AccountID = x.UserId;
                        y.EmployeeNumber = x.EmployeeNo;
                        y.EmployeeName = x.EmployeeName;
                        y.Department = x.AcaDepartmentName;
                        y.EmployeePosition = x.EmployeePositionName;
                        y.Email = x.PrimaryEmail;
                        FacultyList.Add(y);
                    }
                    dgFaculty.ItemsSource = FacultyList.OrderBy(m => m.EmployeeName);

                    //Faculty Head

                    var fachead = (from a in db.AspNetUserRoles
                                   join b in db.Employees on a.AspNetUser.UserName equals b.EmployeeNo
                                   where b.Archive == false && a.RoleId == "3"
                                   select new { a.UserId, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), b.AcademicDepartment.AcaDepartmentName, b.PrimaryEmail, b.EmployeePosition.EmployeePositionName }).ToList();

                    foreach (var x in fachead)
                    {
                        HRiSClass.AccountManagementList y = new HRiSClass.AccountManagementList();
                        y.AccountID = x.UserId;
                        y.EmployeeNumber = x.EmployeeNo;
                        y.EmployeeName = x.EmployeeName;
                        y.Department = x.AcaDepartmentName;
                        y.EmployeePosition = x.EmployeePositionName;
                        y.Email = x.PrimaryEmail;
                        FacultyHeadList.Add(y);
                    }

                    dgFacultyHead.ItemsSource = FacultyHeadList.OrderBy(m => m.EmployeeName);

                    //Guidance

                    var guidance = (from a in db.AspNetUserRoles
                                    join b in db.Employees on a.AspNetUser.UserName equals b.EmployeeNo
                                    where b.Archive == false && a.RoleId == "10"
                                    select new { a.UserId, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), b.AcademicDepartment.AcaDepartmentName, b.PrimaryEmail, b.EmployeePosition.EmployeePositionName }).ToList();

                    foreach (var x in guidance)
                    {
                        HRiSClass.AccountManagementList y = new HRiSClass.AccountManagementList();
                        y.AccountID = x.UserId;
                        y.EmployeeNumber = x.EmployeeNo;
                        y.EmployeeName = x.EmployeeName;
                        y.Department = x.AcaDepartmentName;
                        y.EmployeePosition = x.EmployeePositionName;
                        y.Email = x.PrimaryEmail;
                        GuidanceList.Add(y);
                    }

                    dgGuidance.ItemsSource = GuidanceList.OrderBy(m => m.EmployeeName);

                    //HR

                    var hr = (from a in db.AspNetUserRoles
                              join b in db.Employees on a.AspNetUser.UserName equals b.EmployeeNo
                              where b.Archive == false && a.RoleId == "8"
                              select new { a.UserId, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), b.AcademicDepartment.AcaDepartmentName, b.PrimaryEmail, b.EmployeePosition.EmployeePositionName }).ToList();

                    foreach (var x in hr)
                    {
                        HRiSClass.AccountManagementList y = new HRiSClass.AccountManagementList();
                        y.AccountID = x.UserId;
                        y.EmployeeNumber = x.EmployeeNo;
                        y.EmployeeName = x.EmployeeName;
                        y.Department = x.AcaDepartmentName;
                        y.EmployeePosition = x.EmployeePositionName;
                        y.Email = x.PrimaryEmail;
                        HRList.Add(y);
                    }
                    dgHR.ItemsSource = HRList.OrderBy(m => m.EmployeeName);

                    //Registrar

                    var reg = (from a in db.AspNetUserRoles
                               join b in db.Employees on a.AspNetUser.UserName equals b.EmployeeNo
                               where b.Archive == false && a.RoleId == "9"
                               select new { a.UserId, b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), b.AcademicDepartment.AcaDepartmentName, b.PrimaryEmail, b.EmployeePosition.EmployeePositionName }).ToList();

                    foreach (var x in reg)
                    {
                        HRiSClass.AccountManagementList y = new HRiSClass.AccountManagementList();
                        y.AccountID = x.UserId;
                        y.EmployeeNumber = x.EmployeeNo;
                        y.EmployeeName = x.EmployeeName;
                        y.Department = x.AcaDepartmentName;
                        y.EmployeePosition = x.EmployeePositionName;
                        y.Email = x.PrimaryEmail;
                        RegistrarList.Add(y);
                    }
                    dgRegistrar.ItemsSource = RegistrarList.OrderBy(m => m.EmployeeName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong. " + ex.Message, "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = new LetranIntegratedSystemEntities())
                {
                    AddNewAccount x = new AddNewAccount();
                    x.Owner = this;
                    x.ShowDialog();
                    GetAccount();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        public void ResetPassword()
        {
            try
            {
                using(var db = new LetranIntegratedSystemEntities())
                {
                    string userid = "";
                    var a = ((HRiSClass.AccountManagementList)dgAdmin.SelectedItem);
                    var b = ((HRiSClass.AccountManagementList)dgEmployee.SelectedItem);
                    var c = ((HRiSClass.AccountManagementList)dgFaculty.SelectedItem);
                    var d = ((HRiSClass.AccountManagementList)dgFacultyHead.SelectedItem);
                    var f = ((HRiSClass.AccountManagementList)dgGuidance.SelectedItem);
                    var g = ((HRiSClass.AccountManagementList)dgHR.SelectedItem);
                    var h = ((HRiSClass.AccountManagementList)dgRegistrar.SelectedItem);

                    if(a != null)
                    {
                        userid = a.AccountID;
                    }
                    else if (b != null)
                    {
                        userid = b.AccountID;
                    }
                    else if(c != null)
                    {
                        userid = c.AccountID;
                    }
                    else if (d != null)
                    {
                        userid = d.AccountID;
                    }
                    else if (f != null)
                    {
                        userid = f.AccountID;
                    }
                    else if(g != null)
                    {
                        userid = g.AccountID;
                    }
                    else if(h != null)
                    {
                        userid = h.AccountID;
                    }

                    if(!String.IsNullOrEmpty(userid))
                    {
                        AspNetUser search = db.AspNetUsers.Where(m => m.Id == userid).FirstOrDefault();
                        var passwordHasher = new Microsoft.AspNet.Identity.PasswordHasher();
                        search.PasswordHash = passwordHasher.HashPassword("letran1620");
                        db.SaveChanges();
                        MessageBox.Show("Password has been reset.", "Successful", MessageBoxButton.OK,MessageBoxImage.Information);
                        GetAccount();
                    }

                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetPassword();
        }

        private void tcAccountManagement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                //do work when tab is changed
                dgAdmin.SelectedItem = null;
                dgEmployee.SelectedItem = null;
                dgFaculty.SelectedItem = null;
                dgFacultyHead.SelectedItem = null;
                dgGuidance.SelectedItem = null;
                dgHR.SelectedItem = null;
                dgRegistrar.SelectedItem = null;
            }
           
        }

        //public void ChangeRole()
        //{
        //    try
        //    {
        //        using(var db = new LetranIntegratedSystemEntities())
        //        {
                    
        //            string userid = "";
        //            string empno = "";
        //            var a = ((HRiSClass.AccountManagementList)dgAdmin.SelectedItem);
        //            var b = ((HRiSClass.AccountManagementList)dgEmployee.SelectedItem);
        //            var c = ((HRiSClass.AccountManagementList)dgFaculty.SelectedItem);
        //            var d = ((HRiSClass.AccountManagementList)dgFacultyHead.SelectedItem);
        //            var f = ((HRiSClass.AccountManagementList)dgGuidance.SelectedItem);
        //            var g = ((HRiSClass.AccountManagementList)dgHR.SelectedItem);
        //            var h = ((HRiSClass.AccountManagementList)dgRegistrar.SelectedItem);

        //            if(a != null)
        //            {
        //                userid = a.AccountID;
        //                empno = a.EmployeeNumber;
        //            }
        //            else if (b != null)
        //            {
        //                userid = b.AccountID;
        //                empno = b.EmployeeNumber;
        //            }
        //            else if(c != null)
        //            {
        //                userid = c.AccountID;
        //                empno = c.EmployeeNumber;
        //            }
        //            else if (d != null)
        //            {
        //                userid = d.AccountID;
        //                empno = d.EmployeeNumber;
        //            }
        //            else if (f != null)
        //            {
        //                userid = f.AccountID;
        //                empno = f.EmployeeNumber;
        //            }
        //            else if(g != null)
        //            {
        //                userid = g.AccountID;
        //                empno = g.EmployeeNumber;
        //            }
        //            else if(h != null)
        //            {
        //                userid = h.AccountID;
        //                empno = g.EmployeeNumber;
        //            }

        //            if (!String.IsNullOrEmpty(userid)&& !String.IsNullOrEmpty(empno))
        //            {
        //                ChangeAccountRole x = new ChangeAccountRole();
        //                x.Owner = this;
        //                x.userid = userid;
        //                x.empno = empno;
        //                x.ShowDialog();
        //                GetAccount();
        //            }
        //        }
        //    }
        //    catch(Exception)
        //    {
        //        MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
        //    }
        //}

        //private void miChangeRole_Click(object sender, RoutedEventArgs e)
        //{
        //    ChangeRole();
        //}
    }
}
