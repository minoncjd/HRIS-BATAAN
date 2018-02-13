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
    /// Interaction logic for AcademicDepartmentModule.xaml
    /// </summary>
    public partial class AcademicDepartmentModule : MetroWindow
    {
        List<AcademicDepartmentList> ADList = new List<AcademicDepartmentList>();
        List<EmployeeDepartmentList> EDList = new List<EmployeeDepartmentList>();

        public AcademicDepartmentModule()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAcaDepartment();
        }

        public void GetAcaDepartment()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    ADList = new List<AcademicDepartmentList>();
                    var dept = db.AcademicDepartments.ToList();

                    foreach (var x in dept)
                    {
                        AcademicDepartmentList adl = new AcademicDepartmentList();
                        adl.DepartmentID = x.AcaDeptID;
                        adl.Department = x.AcaDepartmentName;
                        adl.DepartmentAcro = x.AcaAcronym;
                        adl.IsAcad = x.IsAcad;
                        ADList.Add(adl);
                    }
                    dgAcaDept.ItemsSource = ADList.OrderBy(m => m.Department);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgAcaDept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = true;
                btnAdd.IsEnabled = false;
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgAcaDept.SelectedItem != null)
                    {
                        EDList = new List<EmployeeDepartmentList>();
                        var selectedItem = (AcademicDepartmentList)(dgAcaDept.SelectedItem);

                        lblPos.Content = "List of Current Employees in the Department of: " + selectedItem.Department;
                        //Academic Department
                        var dept = db.AcademicDepartments.Find(selectedItem.DepartmentID);

                        txtDeptName.Text = dept.AcaDepartmentName;
                        txtDeptAcro.Text = dept.AcaAcronym;
                        if (dept.IsAcad == true)
                            cbIsAcad.Text = "True";
                        else
                            cbIsAcad.Text = "False";
                        //Employee in the Department

                        var empdept = db.Employees.Where(m => m.AcademicDepartment.AcaDeptID == selectedItem.DepartmentID && m.Archive == false).ToList();

                        foreach (var x in empdept)
                        {
                            EmployeeDepartmentList edl = new EmployeeDepartmentList();
                            edl.EmployeeID = x.EmployeeID;
                            edl.EmployeeNo = x.EmployeeNo;
                            edl.EmployeeName = x.LastName.ToUpper() + ", " + x.FirstName.ToUpper();
                            edl.EmployeePosition = x.EmployeePosition.EmployeePositionName;
                            edl.EmployeeDesignation = x.EmployeeDesignation1.EmployeeDesignationName;
                            edl.EmployeeStatus = x.EmployeeStatu.EmployeeStatusName;
                            EDList.Add(edl);
                        }
                        dgEmpDept.ItemsSource = EDList.OrderBy(m => m.EmployeeName);


                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgAcaDept.SelectedItem != null)
                {
                    using (var db = new LetranIntegratedSystemEntities())
                    {
                        var selectedItem = (AcademicDepartmentList)(dgAcaDept.SelectedItem);

                        var finddep = db.AcademicDepartments.Find(selectedItem.DepartmentID);

                        finddep.AcaDepartmentName = txtDeptName.Text;
                        finddep.AcaAcronym = txtDeptAcro.Text;
                        if (cbIsAcad.Text == "True")
                            finddep.IsAcad = true;
                        else
                            finddep.IsAcad = false;
                        db.SaveChanges();
                        MessageBox.Show("Update Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetAcaDepartment();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = new LetranIntegratedSystemEntities())
                {
                    if(!String.IsNullOrEmpty(txtDeptName.Text) || !String.IsNullOrEmpty(cbIsAcad.Text))
                    {
                        AcademicDepartment ad = new AcademicDepartment();
                        ad.AcaDepartmentName = txtDeptName.Text;
                        ad.AcaAcronym = txtDeptAcro.Text;
                        if (cbIsAcad.Text == "True")
                            ad.IsAcad = true;
                        else
                            ad.IsAcad = false;
                        db.AcademicDepartments.Add(ad);
                        db.SaveChanges();
                        MessageBox.Show("Add Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetAcaDepartment();
                    }
                    else
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetAcaDepartment();
            TextClear();
        }

        public void TextClear()
        {
            dgAcaDept.SelectedItem = null;
            cbIsAcad.Text = "";
            txtDeptAcro.Text = "";
            txtDeptName.Text = "";
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
        }

        public class AcademicDepartmentList
        {
            public int DepartmentID { get; set; }
            public string Department { get; set; }
            public string DepartmentAcro { get; set; }
            public bool IsAcad { get; set; }
        }

        public class EmployeeDepartmentList
        {

            public int EmployeeID { get; set; }
            public string EmployeeNo { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeePosition { get; set; }
            public string EmployeeDesignation { get; set; }
            public string EmployeeStatus { get; set; }
        }


    }
}
