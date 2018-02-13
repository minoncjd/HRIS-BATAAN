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
    /// Interaction logic for AddEmployeeSchedule.xaml
    /// </summary>
    public partial class AddEmployeeSchedule : MetroWindow
    {
        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();
        List<HRISShift> lShiftList = new List<HRISShift>();
        List<HRiSClass.EmployeeShiftList> lEmployeeShiftList = new List<HRiSClass.EmployeeShiftList>();
        public AddEmployeeSchedule()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }


        public void LoadComboBox()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    EList = new List<HRiSClass.EmpCombo>();
                    lShiftList = new List<HRISShift>();
                    var emp = db.Employees.Where(m => m.Archive == false && m.EmployeeScheduleId == null).ToList();

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

                    var shift = db.HRISShifts.ToList();

                    foreach (var x in shift)
                    {
                        HRISShift hrisShift = new HRISShift();
                        hrisShift.ShiftCode = x.ShiftCode;
                        lShiftList.Add(hrisShift);

                    }
                    cbShiftCode.ItemsSource = lShiftList.OrderBy(m => m.ShiftCode);
                    cbShiftCode.DisplayMemberPath = "ShiftCode";
                    cbShiftCode.SelectedValuePath = "ShiftCode";

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
        private void cbEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbShiftCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var shiftCode = Convert.ToString(cbShiftCode.SelectedValue);
                    if (shiftCode != "")
                    {
                        var shift = db.HRISShifts.Where(m => m.ShiftCode == shiftCode).FirstOrDefault();
                        txtDescription.Text = shift.Description;
                        txtEndTime.Text = shift.EndTime;
                        txtStartTime.Text = shift.StartTime;
                    }
                    else
                    {
                        return;
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
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (String.IsNullOrEmpty(cbEmployee.Text))
                    {
                        MessageBox.Show("Employee cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (String.IsNullOrEmpty(cbShiftCode.Text))
                    {
                        MessageBox.Show("Shift cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var empId = Convert.ToInt32(cbEmployee.SelectedValue);
                    var emp = db.Employees.Where(m => m.EmployeeID == empId).FirstOrDefault();
                    var empSchedule = db.HRISEmployeeSchedules.Where(m => m.EmployeeNumber == emp.EmployeeNo).FirstOrDefault();
                    var shiftCode = Convert.ToString(cbShiftCode.SelectedValue);
                    if (emp != null)
                    {
                        HRISEmployeeSchedule hrisEmployeeSchedule = new HRISEmployeeSchedule();
                        hrisEmployeeSchedule.EmployeeNumber = emp.EmployeeNo;
                        hrisEmployeeSchedule.ShiftCode = shiftCode;
                        db.HRISEmployeeSchedules.Add(hrisEmployeeSchedule);    
                        db.SaveChanges();
                        emp.EmployeeScheduleId = hrisEmployeeSchedule.EmployeeScheduleId;
                        db.SaveChanges();
                        clear();
                        MessageBox.Show("Add Success.", "System Sucess!", MessageBoxButton.OK, MessageBoxImage.Information);
                       
                    }
                    else
                    {
                        MessageBox.Show("Employee not found.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }



                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        public void clear()
        {
            txtDescription.Text = "";
            txtEndTime.Text = "";
            txtStartTime.Text = "";
            cbShiftCode.SelectedIndex = -1;
            cbEmployee.Text = "";

        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         
        }
    }
}
