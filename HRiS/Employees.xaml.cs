﻿using HRiS.Model;
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
    /// Interaction logic for Employees.xaml
    /// </summary>
    public partial class Employees : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        List<EmployeeClass> emplist = new List<EmployeeClass>();
        public Employees()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllEmployeeList();
            PopulateComboBox();

        }

        public void GetAllEmployeeList()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                emplist = new List<EmployeeClass>();

                int no = 1;
                var emp = db.Employees.OrderBy(m => m.LastName).ToList();

                foreach (var x in emp)
                {                                    
                    EmployeeClass el = new EmployeeClass();                      
                    el.Department = x.AcademicDepartment == null ? " " : x.AcademicDepartment.AcaAcronym;                    
                    el.EmployeeListID = no;
                    el.EmployeeName = x.LastName.ToUpper() + ", " + x.FirstName.ToUpper();
                    el.EmployeeNumber = x.EmployeeNo;
                    el.Position = x.EmployeePosition == null ? " " : x.EmployeePosition.EmployeePositionName;                
                    el.Status = x.EmployeeStatu.EmployeeStatusName;
                    el.EmployeePostionID = x.EmployeePosition == null ? 0 : x.EmployeePosition.EmployeePositionID;
                    el.Archive = x.Archive;
                    emplist.Add(el);
                    no++;
                          
                }

                dgEmployeeList.ItemsSource = emplist.OrderBy(m => m.EmployeeName);

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        public void SearchEmployee()
        {
            try
            {
                emplist = new List<EmployeeClass>();
                db = new LetranIntegratedSystemEntities();
                int no = 1;
                var emp = db.Employees.OrderBy(m => m.LastName);

                foreach (var x in emp)
                {
                   
                    EmployeeClass el = new EmployeeClass();

                    var middleName = x.MiddleName == null ? " " : x.MiddleName;
                    el.Department = x.AcademicDepartment == null ? " " : x.AcademicDepartment.AcaAcronym;
                    el.EmployeeListID = no;
                    el.EmployeeName = x.LastName.ToUpper() + ", " + x.FirstName.ToUpper();
                    el.EmployeeNumber = x.EmployeeNo;
                    el.Position = x.EmployeePosition == null ? " " : x.EmployeePosition.EmployeePositionName;
                    el.Status = x.EmployeeStatu.EmployeeStatusName;      
                    el.EmployeePostionID = x.EmployeePosition == null ? 0 : x.EmployeePosition.EmployeePositionID;
                    el.DepartmentID = x.AcademicDepartment == null ? 0 : x.AcademicDepartment.AcaDeptID;
                    el.StatusID = x.EmployeeStatu.EmployeeStatusID == 0 ? 0 : x.EmployeeStatu.EmployeeStatusID;
                    no++;
                    emplist.Add(el);
                    

                }

                if (emplist.Count() == 0)
                {
                    MessageBox.Show("Collection is empty.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                if (rbEmpname.IsChecked == true)
                {
                    emplist = emplist.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                }
                if (rbEmpnum.IsChecked == true)
                {
                    emplist = emplist.Where(m => m.EmployeeNumber.Contains(txtSearch.Text)).ToList();
                }           
                if (!String.IsNullOrEmpty(cbDepartment.Text))
                {
                    var dep = Convert.ToInt32(cbDepartment.SelectedValue);
                    emplist = emplist.Where(m => m.DepartmentID == dep).ToList();
                }

                if (!String.IsNullOrEmpty(cbPosition.Text))
                {
                    var position = Convert.ToInt32(cbPosition.SelectedValue);
                    emplist = emplist.Where(m => m.EmployeePostionID == position).ToList();
                }

                if (!String.IsNullOrEmpty(cbStatus.Text))
                {
                    var status = Convert.ToInt32(cbStatus.SelectedValue);
                    emplist = emplist.Where(m => m.StatusID == status).ToList();
                }
                if (!String.IsNullOrEmpty(cbStatus.Text))
                {
                    var stat = Convert.ToInt32(cbStatus.SelectedValue);
                    emplist = emplist.Where(m => m.StatusID == stat).ToList();
                }
                dgEmployeeList.ItemsSource = emplist.OrderBy(m => m.EmployeeName);
                cbDepartment.SelectedIndex = -1;             
                cbStatus.SelectedIndex = -1;
                cbPosition.SelectedIndex = -1;

            }
            catch (Exception n)
            {
                MessageBox.Show("Something went wrong." + n.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void PopulateComboBox()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();

                cbDepartment.ItemsSource = db.AcademicDepartments.OrderBy(m => m.AcaDepartmentName).ToList();
                cbDepartment.DisplayMemberPath = "AcaDepartmentName";
                cbDepartment.SelectedValuePath = "AcaDeptID";

                cbPosition.ItemsSource = db.EmployeePositions.OrderBy(m => m.EmployeePositionName).ToList();
                cbPosition.DisplayMemberPath = "EmployeePositionName";
                cbPosition.SelectedValuePath = "EmployeePositionID";

                cbStatus.ItemsSource = db.EmployeeStatus.OrderBy(m => m.EmployeeStatusName).ToList();
                cbStatus.DisplayMemberPath = "EmployeeStatusName";
                cbStatus.SelectedValuePath = "EmployeeStatusID";


            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void RefreshGrid()
        {
            txtSearch.Text = "";
           
            rbEmpname.IsChecked = false;
            rbEmpnum.IsChecked = false;
           

            cbDepartment.Text = "";
            cbPosition.Text = "";
            cbStatus.Text = "";

        }
        public class EmployeeClass
        {
            public int EmployeeListID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
            public string Position { get; set; }
            public bool? Archive { get; set; }
            public int EmployeePostionID { get; set; }
            public string Department { get; set; }
            public int DepartmentID { get; set; }
            public string Designation { get; set; }
            public string Status { get; set; }
            public int StatusID { get; set; }

        }


        private void rbEmpnum_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void rbEmpname_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void rbPosition_Checked(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            SearchEmployee();
            this.Cursor = Cursors.Arrow;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            AddNewEmployee x = new AddNewEmployee();
            x.Owner = this;
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            RefreshGrid();
            GetAllEmployeeList();
            this.Cursor = Cursors.Arrow;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var x = ((EmployeeClass)dgEmployeeList.SelectedItem);
                var emp = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNumber).FirstOrDefault();
                if (emp != null)
                {
                    UpdateEmployee ue = new UpdateEmployee();
                    ue.EmpID = emp.EmployeeID;
                    ue.Owner = this;
                    ue.ShowDialog();
                    GetAllEmployeeList();

                }
                else
                {
                    MessageBox.Show("Please select an employee.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void cbPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void editEmp_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var x = ((EmployeeClass)dgEmployeeList.SelectedItem);
                var emp = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNumber).FirstOrDefault();
                if (emp != null)
                {
                    UpdateEmployee ue = new UpdateEmployee();
                    ue.EmpID = emp.EmployeeID;
                    ue.Owner = this;
                    ue.ShowDialog();
                    GetAllEmployeeList();

                }
                else
                {
                    MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

            }
            catch (Exception n)
            {
                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void addBioNumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var x = ((EmployeeClass)dgEmployeeList.SelectedItem);
                var emp = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNumber).FirstOrDefault();
                if (emp != null)
                {
                    BiometricsAddBioNumber ba = new BiometricsAddBioNumber();
                    ba.EmpId = emp.EmployeeID;
                    ba.Owner = this;
                    ba.ShowDialog();
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        
        }

        private void addFaculty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var x = ((EmployeeClass)dgEmployeeList.SelectedItem);

                    if (db.Faculties.Where(m => m.EmpNo == x.EmployeeNumber).FirstOrDefault() == null)
                    {
                        Faculty f = new Faculty();
                        f.EmpNo = x.EmployeeNumber;
                        f.EmployeeID = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNumber).FirstOrDefault().EmployeeID;
                        f.FacultyCode = x.EmployeeNumber;
                        db.Faculties.Add(f);
                        db.SaveChanges();
                        MessageBox.Show("Faculty successfully added.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Faculty already exists.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void addAcct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                string roleid = "";
                AspNetUser aspuser = new AspNetUser();
                AspNetUserRole asproleuser = new AspNetUserRole();
                var x = ((EmployeeClass)dgEmployeeList.SelectedItem);
                var user = db.AspNetUsers.Where(m => m.UserName == x.EmployeeNumber).FirstOrDefault();
                if (user != null)
                {
                    MessageBox.Show("User account for this person already exists!", "System Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    var emp = db.Employees.Where(m => m.EmployeeNo == x.EmployeeNumber).FirstOrDefault();

                    roleid = "4";

                    //if (emp.EmployeeDesignation == 1 || emp.EmployeeDepartmentID == 19)
                    //{
                    //    roleid = "2";
                    //}
                    //if (emp.EmployeeDesignation == 2)
                    //{
                    //    roleid = "5";
                    //}
                    //if (emp.EmployeeDepartmentID == 34)
                    //{
                    //    roleid = "9";
                    //}
                    //else if (emp.EmployeeDepartmentID == 35)
                    //{
                    //    roleid = "10";
                    //}
                    //else if (emp.EmployeeDepartmentID == 18)
                    //{
                    //    roleid = "8";
                    //}

                    if (String.IsNullOrEmpty(roleid))
                    {
                        MessageBox.Show("Role is not specified", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    if (String.IsNullOrEmpty(emp.PrimaryEmail))
                    {
                        MessageBox.Show("Email cannot be empty.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
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
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
