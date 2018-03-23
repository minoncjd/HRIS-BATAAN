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
    /// Interaction logic for UpdateEmployeesSchedule.xaml
    /// </summary>
    public partial class UpdateEmployeeSchedule : MetroWindow
    {
        public int empid;
        public UpdateEmployeeSchedule()
        {
            InitializeComponent();
           
        }

        private void GetEmployeeSchedule(int employeeid) {
            try
            {
                List<EmployeeSchedule> lEmployeeSchedule = new List<EmployeeSchedule>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var empschedmon = db.HRISEmployeesShifts.Where(m => m.EmployeeID == employeeid && m.DayID == 1).FirstOrDefault();
                    var empschedtue = db.HRISEmployeesShifts.Where(m => m.EmployeeID == employeeid && m.DayID == 2).FirstOrDefault();
                    var empschedwed = db.HRISEmployeesShifts.Where(m => m.EmployeeID == employeeid && m.DayID == 3).FirstOrDefault();
                    var empschedthu = db.HRISEmployeesShifts.Where(m => m.EmployeeID == employeeid && m.DayID == 4).FirstOrDefault();
                    var empschedfri = db.HRISEmployeesShifts.Where(m => m.EmployeeID == employeeid && m.DayID == 5).FirstOrDefault();
                    var empschedsat = db.HRISEmployeesShifts.Where(m => m.EmployeeID == employeeid && m.DayID == 6).FirstOrDefault();
                    var empschedsun = db.HRISEmployeesShifts.Where(m => m.EmployeeID == employeeid && m.DayID == 7).FirstOrDefault();
                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();

                    tbEmp.Text = emp.LastName + ", " + emp.FirstName;
                    tbShiftCode.Text = empschedmon.EmployeeShiftCode;

                    EmployeeSchedule es = new EmployeeSchedule();
                    if (empschedmon != null)
                    {
                        monStartTime.Text = empschedmon.StartTime;
                        monEndTime.Text = empschedmon.EndTime;
                        if (empschedmon.IsNoBreak_ == true)
                        {
                            moncbNoBreak.IsChecked = true;
                        }
                  
                    }

                    if (empschedtue != null)
                    {
                        tueStartTime.Text = empschedtue.StartTime;
                        tueEndTime.Text = empschedtue.EndTime;
                        if (empschedtue.IsNoBreak_ == true)
                        {
                            tuescbNoBreak.IsChecked = true;
                        }
                    }

                    if (empschedwed != null)
                    {
                        wedStartTime.Text = empschedwed.StartTime;
                        wedEndTime.Text = empschedwed.EndTime;
                        if (empschedwed.IsNoBreak_ == true)
                        {
                            wedcbNoBreak.IsChecked = true;
                        }
                    }

                    if (empschedthu != null)
                    {
                        thuStartTime.Text = empschedthu.StartTime;
                        thuEndTime.Text = empschedthu.EndTime;
                        if (empschedthu.IsNoBreak_ == true)
                        {
                            thucbNoBreak.IsChecked = true;
                        }
                    }

                    if (empschedfri != null)
                    {
                        friStartTime.Text = empschedfri.StartTime;
                        friEndTime.Text = empschedfri.EndTime;
                        if (empschedfri.IsNoBreak_ == true)
                        {
                            fricbNoBreak.IsChecked = true;
                        }
                    }

                    if (empschedsat != null)
                    {
                        satStartTime.Text = empschedsat.StartTime;
                        satEndTime.Text = empschedsat.EndTime;
                        if (empschedsat.IsNoBreak_ == true)
                        {
                           satcbNoBreak.IsChecked = true;
                        }
                    }

                    if (empschedsun != null)
                    {
                        sunStartTime.Text = empschedsun.StartTime;
                        sunEndTime.Text = empschedsun.EndTime;
                        if (empschedsun.IsNoBreak_ == true)
                        {
                            suncbNoBreak.IsChecked = true;
                        }

                    }
                  



                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetEmployeeSchedule(empid);
        }

        private void cbDefault_Checked(object sender, RoutedEventArgs e)
        {
            monStartTime.Text = "8:00 AM";
            tueStartTime.Text = "8:00 AM";
            wedStartTime.Text = "8:00 AM";
            thuStartTime.Text = "8:00 AM";
            friStartTime.Text = "8:00 AM";

            monEndTime.Text = "5:00 PM";
            tueEndTime.Text = "5:00 PM";
            wedEndTime.Text = "5:00 PM";
            thuEndTime.Text = "5:00 PM";
            friEndTime.Text = "5:00 PM";

            tbShiftCode.Text = "8:00 AM - 5:00 PM";
        }

        private void cbDefault_Unchecked(object sender, RoutedEventArgs e)
        {
            monEndTime.Text = "";
            monStartTime.Text = "";
            tueEndTime.Text = "";
            tueStartTime.Text = "";
            wedEndTime.Text = "";
            wedStartTime.Text = "";
            thuEndTime.Text = "";
            thuStartTime.Text = "";
            friStartTime.Text = "";
            friEndTime.Text = "";
            tbShiftCode.Text = "";
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {

                    var empschedmon = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid && m.DayID == 1).FirstOrDefault();
                    var empschedtue = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid && m.DayID == 2).FirstOrDefault();
                    var empschedwed = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid && m.DayID == 3).FirstOrDefault();
                    var empschedthu = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid && m.DayID == 4).FirstOrDefault();
                    var empschedfri = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid && m.DayID == 5).FirstOrDefault();
                    var empschedsat = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid && m.DayID == 6).FirstOrDefault();
                    var empschedsun = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid && m.DayID == 7).FirstOrDefault();

                    if (tbShiftCode.Text != empschedmon.EmployeeShiftCode)
                    {
                        empschedmon.EmployeeShiftCode = tbShiftCode.Text;
                        empschedtue.EmployeeShiftCode = tbShiftCode.Text;
                        empschedwed.EmployeeShiftCode = tbShiftCode.Text;
                        empschedthu.EmployeeShiftCode = tbShiftCode.Text;
                        empschedfri.EmployeeShiftCode = tbShiftCode.Text;
                        empschedsat.EmployeeShiftCode = tbShiftCode.Text;
                        empschedsun.EmployeeShiftCode = tbShiftCode.Text;
                        db.SaveChanges();
                    }

                    if (empschedmon.StartTime != monStartTime.Text)
                    {
                        empschedmon.StartTime = monStartTime.Text;
                        db.SaveChanges();
                    }
                    if (empschedmon.EndTime != monEndTime.Text)
                    {
                        empschedmon.EndTime = monEndTime.Text;
                        db.SaveChanges();
                    }
                    if (moncbNoBreak.IsChecked == true)
                    {
                        empschedmon.IsNoBreak_ = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        empschedmon.IsNoBreak_ = false;
                        db.SaveChanges();
                    }
            
                    if (empschedtue.StartTime != tueStartTime.Text)
                    {
                        empschedtue.StartTime = tueStartTime.Text;
                        db.SaveChanges();
                    }
                    if (empschedtue.EndTime != tueEndTime.Text)
                    {
                        empschedtue.EndTime = tueEndTime.Text;
                        db.SaveChanges();
                    }
                    if (tuescbNoBreak.IsChecked == true)
                    {
                        empschedtue.IsNoBreak_ = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        empschedtue.IsNoBreak_ = false;
                        db.SaveChanges();
                    }


                    if (empschedwed.StartTime != wedStartTime.Text)
                    {
                        empschedwed.StartTime = wedStartTime.Text;
                        db.SaveChanges();
                    }
                    if (empschedwed.EndTime != wedEndTime.Text)
                    {
                        empschedwed.EndTime = wedEndTime.Text;
                        db.SaveChanges();
                    }
                    if (wedcbNoBreak.IsChecked == true)
                    {
                        empschedwed.IsNoBreak_ = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        empschedwed.IsNoBreak_ = false;
                        db.SaveChanges();
                    }



                    if (empschedthu.StartTime != thuStartTime.Text)
                    {
                        empschedthu.StartTime = thuStartTime.Text;
                        db.SaveChanges();
                    }
                    if (empschedthu.EndTime != thuEndTime.Text)
                    {
                        empschedthu.EndTime = thuEndTime.Text;
                        db.SaveChanges();
                    }
                    if (thucbNoBreak.IsChecked == true)
                    {
                        empschedthu.IsNoBreak_ = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        empschedthu.IsNoBreak_ = false;
                        db.SaveChanges();
                    }


                    if (empschedfri.StartTime != friStartTime.Text)
                    {
                        empschedfri.StartTime = friStartTime.Text;
                        db.SaveChanges();
                    }
                    if (empschedfri.EndTime != friEndTime.Text)
                    {
                        empschedfri.EndTime = friEndTime.Text;
                        db.SaveChanges();
                    }
                    if (fricbNoBreak.IsChecked == true)
                    {
                        empschedfri.IsNoBreak_ = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        empschedfri.IsNoBreak_ = false;
                        db.SaveChanges();
                    }

                    if (empschedsat.StartTime != satStartTime.Text)
                    {
                        empschedsat.StartTime = satStartTime.Text;
                        db.SaveChanges();
                    }
                    if (empschedsat.EndTime != satEndTime.Text)
                    {
                        empschedsat.EndTime = satEndTime.Text;
                        db.SaveChanges();
                    }
                    if (satcbNoBreak.IsChecked == true)
                    {
                        empschedsat.IsNoBreak_ = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        empschedsat.IsNoBreak_ = false;
                        db.SaveChanges();
                    }

                    if (empschedsun.StartTime != sunStartTime.Text)
                    {
                        empschedsun.StartTime = sunStartTime.Text;
                        db.SaveChanges();
                    }
                    if (empschedsun.EndTime != sunEndTime.Text)
                    {
                        empschedsun.EndTime = sunEndTime.Text;
                        db.SaveChanges();
                    }
                    if (suncbNoBreak.IsChecked == true)
                    {
                        empschedsun.IsNoBreak_ = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        empschedsun.IsNoBreak_ = false;
                        db.SaveChanges();
                    }

                    MessageBox.Show("Success.", "System Sucess!", MessageBoxButton.OK, MessageBoxImage.Information);
                    clear();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clear()
        {
            monEndTime.Text = "";
            monStartTime.Text = "";
            tueEndTime.Text = "";
            tueStartTime.Text = "";
            wedEndTime.Text = "";
            wedStartTime.Text = "";
            thuEndTime.Text = "";
            thuStartTime.Text = "";
            friStartTime.Text = "";
            friEndTime.Text = "";
            tbEmp.Text = "";
            tbShiftCode.Text = "";
            satEndTime.Text = "";
            satStartTime.Text = "";
            sunEndTime.Text = "";
            sunStartTime.Text = "";
        }



        public class EmployeeSchedule
        {
            public string ShiftCode { get; set; }
            public string MonStartTime { get; set; }
            public string TuesStartTime { get; set; }
            public string WedStartTime { get; set; }
            public string ThuStartTime { get; set; }
            public string FriStartTime { get; set; }
            public string SatStartTime { get; set; }
            public string SunStartTime { get; set; }

            public string MonEndTime { get; set; }
            public string TuesEndTime { get; set; }
            public string WedEndTime { get; set; }
            public string ThuEndTime { get; set; }
            public string FriEndTime { get; set; }
            public string SatEndTime { get; set; }
            public string SunEndTime { get; set; }
        }
    }
}
