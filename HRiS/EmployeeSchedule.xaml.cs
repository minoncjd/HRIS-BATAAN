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
using static HRiS.Model.HRiSClass;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for EmployeeSchedule.xaml
    /// </summary>
    public partial class EmployeeSchedule : MetroWindow
    {
        List<HRiSClass.EmployeeShiftList> lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();

        public EmployeeSchedule()
        {
            InitializeComponent();
        }

        private void editSched_Click(object sender, RoutedEventArgs e)
        {
            var x = ((EmployeeShiftList)dgEmployeeScheduleList.SelectedItem);
            UpdateEmployeeSchedule ue = new UpdateEmployeeSchedule();
            ue.empid = x.EmployeeID;
            ue.Owner = this;
            ue.ShowDialog();
        }

        public void LoadComboBox()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    EList = new List<HRiSClass.EmpCombo>();

                    var emp = db.Employees.Where(m => m.Archive == false).ToList();

                    foreach (var i in emp)
                    {
                        HRiSClass.EmpCombo ec = new HRiSClass.EmpCombo();
                        ec.EmployeeID = i.EmployeeID;
                        ec.EmployeeNumber = i.EmployeeNo;
                        ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                        EList.Add(ec);
                    }

                    cbEmployee.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                    cbEmployee.DisplayMemberPath = "EmployeeName";
                    cbEmployee.SelectedValuePath = "EmployeeID";

                    var dept = db.AcademicDepartments.ToList();
                    cbDepartment.ItemsSource = dept.OrderBy(m => m.AcaDepartmentName);
                    cbDepartment.DisplayMemberPath = "AcaDepartmentName";
                    cbDepartment.SelectedValuePath = "AcaDeptID";


                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


        //public void GetEmployeeScheduleList()
        //{
        //    try
        //    {
        //        using (var db = new LetranIntegratedSystemEntities())
        //        {
        //            lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
        //            var empSchedule = (from a in db.HRISEmployeeSchedules
        //                               join b in db.Employees on a.EmployeeNumber equals b.EmployeeNo
        //                               join c in db.HRISShifts on a.ShiftCode equals c.ShiftCode
        //                               where b.Archive == false
        //                               select new { b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), a.ShiftCode, c.StartTime, c.EndTime }).ToList();

        //            foreach (var x in empSchedule)
        //            {

        //                HRiSClass.EmployeeShiftList employeeShift = new HRiSClass.EmployeeShiftList();
        //                employeeShift.EmployeeNo = x.EmployeeNo;
        //                employeeShift.EmployeeName = x.EmployeeName;
        //                employeeShift.ShiftCode = x.ShiftCode;
        //                employeeShift.StartTime = x.StartTime;
        //                employeeShift.EndTime = x.EndTime;

        //                lEmployeeShiftList.Add(employeeShift);
        //            }

        //            dgEmployeeScheduleList.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);

        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void GetEmployeeSchedules()
        {
            try
            {
               

                using (var db = new LetranIntegratedSystemEntities())
                {
                    lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
                    var empSchedule = db.GetEmployeeSchedules().ToList().OrderBy(m => m.Name);

                    foreach (var x in empSchedule)
                    {

                        HRiSClass.EmployeeShiftList employeeShift = new HRiSClass.EmployeeShiftList();

                        employeeShift.EmployeeNo = x.Employeeno;
                        employeeShift.EmployeeName = x.Name;
                        employeeShift.ShiftCode = x.EmployeeShiftCode;
                        employeeShift.EmployeeID = x.EmployeeID;
                        employeeShift.Department = x.AcaAcronym;
                        lEmployeeShiftList.Add(employeeShift);
                    }

                    dgEmployeeScheduleList.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void rbEmpnum_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbEmpname_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbEmpn_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbShiftCode_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            //try
            //{
            //    using (var db = new LetranIntegratedSystemEntities())
            //    {
                   
            //        lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
            //        var empSchedule = db.GetEmployeeSchedules().ToList().OrderBy(m => m.Name);

            //        foreach (var x in empSchedule)
            //        {

            //            HRiSClass.EmployeeShiftList employeeShift = new HRiSClass.EmployeeShiftList();

            //            employeeShift.EmployeeNo = x.Employeeno;
            //            employeeShift.EmployeeName = x.Name;
            //            employeeShift.ShiftCode = x.EmployeeShiftCode;
            //            employeeShift.EmployeeID = x.EmployeeID;

            //            lEmployeeShiftList.Add(employeeShift);
            //        }

            //        dgEmployeeScheduleList.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);
                  


            //        if (rbEmpname.IsChecked == true)
            //        {
            //            lEmployeeShiftList = lEmployeeShiftList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
            //        }
            //        else if (rbShiftCode.IsChecked == true)
            //        {
            //            lEmployeeShiftList = lEmployeeShiftList.Where(m => m.ShiftCode.Contains(txtSearch.Text)).ToList();

            //        }
            //        else if (rbEmpn.IsChecked == true)
            //        {
            //            lEmployeeShiftList = lEmployeeShiftList.Where(m => m.EmployeeNo.Contains(txtSearch.Text)).ToList();
            //        }

            //        dgEmployeeScheduleList.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);

            //    }
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

           
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeSchedules x = new AddEmployeeSchedules();
            x.Owner = this;
            x.ShowDialog();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
            GetEmployeeSchedules();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
    
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeeSchedules();

        }

        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var empid = Convert.ToInt32(cbEmployee.SelectedValue);
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
                    var empSchedule = db.GetEmployeeSchedules().Where(m => m.EmployeeID == empid).ToList().OrderBy(m => m.Name);

                    foreach (var x in empSchedule)
                    {

                        HRiSClass.EmployeeShiftList employeeShift = new HRiSClass.EmployeeShiftList();

                        employeeShift.EmployeeNo = x.Employeeno;
                        employeeShift.EmployeeName = x.Name;
                        employeeShift.ShiftCode = x.EmployeeShiftCode;
                        employeeShift.EmployeeID = x.EmployeeID;

                        lEmployeeShiftList.Add(employeeShift);
                    }

                    dgEmployeeScheduleList.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var deptid = Convert.ToInt32(cbDepartment.SelectedValue);
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
                    var empSchedule = db.GetEmployeeSchedules().Where(m => m.AcaDeptID == deptid).ToList().OrderBy(m => m.Name);

                    foreach (var x in empSchedule)
                    {

                        HRiSClass.EmployeeShiftList employeeShift = new HRiSClass.EmployeeShiftList();

                        employeeShift.EmployeeNo = x.Employeeno;
                        employeeShift.EmployeeName = x.Name;
                        employeeShift.ShiftCode = x.EmployeeShiftCode;
                        employeeShift.EmployeeID = x.EmployeeID;
                        employeeShift.Department = x.AcaAcronym;

                        lEmployeeShiftList.Add(employeeShift);
                    }

                    dgEmployeeScheduleList.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
