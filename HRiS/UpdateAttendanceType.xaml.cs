using HRiS.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UpdateAttendanceType.xaml
    /// </summary>
    public partial class UpdateAttendanceType : MetroWindow
    {
        public int empid;
        //public string timein;
        //public string breakout;
        //public string breakin;
        //public DateTime timeout;
        public string remark;
        public DateTime Date;
        public int timeinID;
        public int breakoutID;
        public int breakinID;
        public int timeoutID;

        public UpdateAttendanceType()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetAttendanceDetails();
         
        }

       
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                  
                   // var attendance = db.BiometricsLogDatas.Where(m => m.Id == id).FirstOrDefault();
                    //if (rbnIn.IsChecked == true)
                    //{
                    //    attendance.MODE = "I";
                    //    db.SaveChanges();
                    //}
                    //else if (rbnBO.IsChecked == true)
                    //{
                    //    attendance.MODE = "BO";
                    //    db.SaveChanges();
                    //}
                    //else if (rbnBI.IsChecked == true)
                    //{
                    //    attendance.MODE = "BI";
                    //    db.SaveChanges();
                    //}
                    //else if (rbnO.IsChecked == true)
                    //{
                    //    attendance.MODE = "O";
                    //    db.SaveChanges();
                    //}

           
                    MessageBox.Show("Updated Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                  
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAttendanceDetails()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();

                    txtEmployee.Text = emp.LastName + ", " + emp.FirstName;
                    dpDate.SelectedDate = Convert.ToDateTime(Date);

                    if (timeinID != 0)
                    {
                        var attendance = db.BiometricsLogDatas.Where(m => m.Id == timeinID).FirstOrDefault();
                        timeIn.Value = attendance.DTIME;
                    }
                    if (breakoutID != 0)
                    {
                        var attendance = db.BiometricsLogDatas.Where(m => m.Id == breakoutID).FirstOrDefault();
                        breakOut.Value = attendance.DTIME;
                    }
                    if (breakinID != 0)
                    {
                        var attendance = db.BiometricsLogDatas.Where(m => m.Id == breakinID).FirstOrDefault();
                        breakIn.Value = attendance.DTIME;
                    }
                    if (timeoutID != 0)
                    {
                        var attendance = db.BiometricsLogDatas.Where(m => m.Id == timeoutID).FirstOrDefault();
                        timeOut.Value = attendance.DTIME;
                    }
                    txtRemark.Text = remark;
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                using (var db = new LetranIntegratedSystemEntities())
                {
                    BiometricsLogData bld = new BiometricsLogData();
                    var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();
                    var timeinentry = db.BiometricsLogDatas.Where(m => m.Id == timeinID).FirstOrDefault();
                    var breakoutentry = db.BiometricsLogDatas.Where(m => m.Id == breakoutID).FirstOrDefault();
                    var breakinentry = db.BiometricsLogDatas.Where(m => m.Id == breakinID).FirstOrDefault();
                    var timeoutentry = db.BiometricsLogDatas.Where(m => m.Id == timeoutID).FirstOrDefault();
               

                    if (timeinentry == null && !String.IsNullOrEmpty(timeIn.Text))
                    {
                        DateTime timeindate = Convert.ToDateTime(dpDate.SelectedDate);
                        DateTime timeintime = Convert.ToDateTime(timeIn.Text);
                        bld.EMPN = Convert.ToInt32(emp.bioid);
                        bld.DTIME = timeindate.Date + timeintime.TimeOfDay;
                        bld.REMARK = txtRemark.Text;
                        bld.MODE = "I";
                        bld.REMARK = txtRemark.Text = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                        db.BiometricsLogDatas.Add(bld);
                        db.SaveChanges();
                    }

                    if (timeinentry != null)
                    {
                        if (String.IsNullOrEmpty(timeIn.Text))
                        {
                            db.BiometricsLogDatas.Remove(timeinentry);                                            
                            db.SaveChanges();
                        }
                        else
                        {
                            DateTime timeindate = timeinentry.DTIME.Date;
                            DateTime timeintime = Convert.ToDateTime(timeIn.Text);
                            timeinentry.DTIME = timeindate.Date + timeintime.TimeOfDay;
                            timeinentry.REMARK = txtRemark.Text = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                            db.SaveChanges();
                        }
                    
                
                    }

                    if (breakoutentry == null && !String.IsNullOrEmpty(breakOut.Text))
                    {
                        DateTime breakoutdate = Convert.ToDateTime(dpDate.SelectedDate);
                        DateTime breakouttime = Convert.ToDateTime(breakOut.Text);
                        bld.EMPN = Convert.ToInt32(emp.bioid);
                        bld.DTIME = breakoutdate.Date + breakouttime.TimeOfDay;
                        bld.REMARK = txtRemark.Text;
                        bld.MODE = "BO";
                        bld.REMARK = txtRemark.Text = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                        db.BiometricsLogDatas.Add(bld);
                        db.SaveChanges();
                    }

                    if (breakoutentry != null)
                    {
                        if (String.IsNullOrEmpty(breakOut.Text))
                        {
                            db.BiometricsLogDatas.Remove(breakoutentry);
                            db.SaveChanges();
                        }
                        else
                        {
                            DateTime breakoutdate = timeinentry.DTIME.Date;
                            DateTime breakouttime = Convert.ToDateTime(breakOut.Text);
                            breakoutentry.DTIME = breakoutdate.Date + breakouttime.TimeOfDay;
                            breakoutentry.REMARK = txtRemark.Text = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                            db.SaveChanges();
                        }

                    }


                    if (breakinentry == null && !String.IsNullOrEmpty(breakIn.Text))
                    {
                        DateTime breakindate = Convert.ToDateTime(dpDate.SelectedDate);
                        DateTime breakintime = Convert.ToDateTime(breakIn.Text);
                        bld.EMPN = Convert.ToInt32(emp.bioid);
                        bld.DTIME = breakindate.Date + breakintime.TimeOfDay;
                        bld.REMARK = txtRemark.Text;
                        bld.MODE = "BI";
                        bld.REMARK = txtRemark.Text = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                        db.BiometricsLogDatas.Add(bld);
                        db.SaveChanges();
                    }

                    if (breakinentry != null)
                    {
                        if (String.IsNullOrEmpty(breakIn.Text))
                        {
                            db.BiometricsLogDatas.Remove(breakinentry);
                            db.SaveChanges();
                        }
                        else
                        {
                            DateTime breakindate = timeinentry.DTIME.Date;
                            DateTime breakintime = Convert.ToDateTime(breakIn.Text);
                            breakinentry.DTIME = breakindate.Date + breakintime.TimeOfDay;
                            breakinentry.REMARK = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                            db.SaveChanges();
                        }
                    }


                    if (timeoutentry == null && !String.IsNullOrEmpty(timeOut.Text))
                    {
                        DateTime timeoutdate = Convert.ToDateTime(dpDate.SelectedDate);
                        DateTime timeouttime = Convert.ToDateTime(timeOut.Text);
                        bld.EMPN = Convert.ToInt32(emp.bioid);
                        bld.DTIME = timeoutdate.Date + timeouttime.TimeOfDay;       
                        bld.MODE = "O";
                        bld.REMARK = txtRemark.Text = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                        db.BiometricsLogDatas.Add(bld);
                        db.SaveChanges();
                    }

                    if (timeoutentry != null)
                    {
                        if (String.IsNullOrEmpty(timeOut.Text))
                        {
                            db.BiometricsLogDatas.Remove(timeoutentry);
                            db.SaveChanges();
                        }
                        else
                        {
                            DateTime timeoutdate = timeinentry.DTIME.Date;
                            DateTime timeouttime = Convert.ToDateTime(timeOut.Text);
                            timeoutentry.DTIME = timeoutdate.Date + timeouttime.TimeOfDay;
                            timeoutentry.REMARK = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                            db.SaveChanges();
                        }
                    }
     
                    MessageBox.Show("Updating attendance was successful", "System Successs!", MessageBoxButton.OK, MessageBoxImage.Information);

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

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //AttendanceBiometric ab = new AttendanceBiometric();
            //ab.GetBiometrics();
          
        }
    }
}
