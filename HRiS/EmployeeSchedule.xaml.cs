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

        public EmployeeSchedule()
        {
            InitializeComponent();
        }

        private void editSched_Click(object sender, RoutedEventArgs e)
        {
            var x = ((EmployeeShiftList)dgEmployeeScheduleList.SelectedItem);
            UpdateEmployeeShift ue = new UpdateEmployeeShift();
            ue.empnumber = x.EmployeeNo;
            ue.Owner = this;
            ue.ShowDialog();
        }


        public void GetEmployeeScheduleList()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
                    var empSchedule = (from a in db.HRISEmployeeSchedules
                                       join b in db.Employees on a.EmployeeNumber equals b.EmployeeNo
                                       join c in db.HRISShifts on a.ShiftCode equals c.ShiftCode
                                       where b.Archive == false
                                       select new { b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), a.ShiftCode, c.StartTime, c.EndTime }).ToList();

                    foreach (var x in empSchedule)
                    {
                      
                        HRiSClass.EmployeeShiftList employeeShift = new HRiSClass.EmployeeShiftList();
                        employeeShift.EmployeeNo = x.EmployeeNo;
                        employeeShift.EmployeeName = x.EmployeeName;
                        employeeShift.ShiftCode = x.ShiftCode;
                        employeeShift.StartTime = x.StartTime;
                        employeeShift.EndTime = x.EndTime;

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

            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
                    var empSchedule = (from a in db.HRISEmployeeSchedules
                                       join b in db.Employees on a.EmployeeNumber equals b.EmployeeNo
                                       join c in db.HRISShifts on a.ShiftCode equals c.ShiftCode
                                       where b.Archive == false
                                       select new { b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), a.ShiftCode, c.StartTime, c.EndTime }).ToList();

                    foreach (var x in empSchedule)
                    {

                        HRiSClass.EmployeeShiftList employeeShift = new HRiSClass.EmployeeShiftList();
                        employeeShift.EmployeeNo = x.EmployeeNo;
                        employeeShift.EmployeeName = x.EmployeeName;
                        employeeShift.ShiftCode = x.ShiftCode;
                        employeeShift.StartTime = x.StartTime;
                        employeeShift.EndTime = x.EndTime;

                        lEmployeeShiftList.Add(employeeShift);
                    }


                    if (rbEmpname.IsChecked == true)
                    {                    
                        lEmployeeShiftList = lEmployeeShiftList.Where(m => m.EmployeeName.Contains(txtSearch.Text)).ToList();
                    }
                    else if (rbShiftCode.IsChecked == true)
                    {
                        lEmployeeShiftList = lEmployeeShiftList.Where(m => m.ShiftCode.Contains(txtSearch.Text)).ToList();

                    }
                    else if (rbEmpn.IsChecked == true)
                    {
                        lEmployeeShiftList = lEmployeeShiftList.Where(m => m.EmployeeNo.Contains(txtSearch.Text)).ToList();
                    }

                    dgEmployeeScheduleList.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);

                }
            }
            catch (Exception)
            {

                throw;
            }

           
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeSchedule x = new AddEmployeeSchedule();
            x.Owner = this;
            x.ShowDialog();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetEmployeeScheduleList();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
    
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetEmployeeScheduleList();

        }
    }
}
