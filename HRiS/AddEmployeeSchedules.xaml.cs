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
    /// Interaction logic for AddEmployeeSchedules.xaml
    /// </summary>
    public partial class AddEmployeeSchedules : MetroWindow
    {
        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();

        public AddEmployeeSchedules()
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
            
                    var emp = db.Employees.Where(m => m.Archive == false).ToList();

                    foreach (var i in emp)
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
                   
                    var empid = Convert.ToInt32(cbEmp.SelectedValue);
                    var exist = db.HRISEmployeesShifts.Where(m => m.EmployeeID == empid).FirstOrDefault();

                    if (exist == null)
                    {
                        HRISEmployeesShift sd1 = new HRISEmployeesShift();
                        sd1.DayName = "Mon";
                        sd1.DayID = 1;
                        sd1.EmployeeID = empid;
                        sd1.EmployeeShiftCode = tbShiftCode.Text == null ? " " : tbShiftCode.Text; ;
                        sd1.StartTime = monStartTime.Text == null ? null : monStartTime.Text; ;
                        sd1.EndTime = monEndTime.Text == null ? null : monEndTime.Text; 

                        if (moncbNoBreak.IsChecked == true)
                        {
                            sd1.IsNoBreak_ = true;
                        }
                        else
                        {
                            sd1.IsNoBreak_ = false;
                        }
                        
                        db.HRISEmployeesShifts.Add(sd1);
                        db.SaveChanges();

                        HRISEmployeesShift sd2 = new HRISEmployeesShift();
                        sd2.DayName = "Tue";
                        sd2.DayID = 2;
                        sd2.EmployeeID = empid;
                        sd2.EmployeeShiftCode = tbShiftCode.Text == null ? " " : tbShiftCode.Text; ;
                        sd2.StartTime = tueStartTime.Text == null ? null : tueStartTime.Text; ;
                        sd2.EndTime = tueEndTime.Text == null ? null : tueEndTime.Text; ;

                        if (tuescbNoBreak.IsChecked == true)
                        {
                            sd2.IsNoBreak_ = true;
                        }
                        else
                        {
                            sd2.IsNoBreak_ = false;
                        }

                        db.HRISEmployeesShifts.Add(sd2);
                        db.SaveChanges();

                        HRISEmployeesShift sd3 = new HRISEmployeesShift();
                        sd3.DayName = "Wed";
                        sd3.DayID = 3;
                        sd3.EmployeeID = empid;
                        sd3.EmployeeShiftCode = tbShiftCode.Text == null ? " " : tbShiftCode.Text; ;
                        sd3.StartTime = wedStartTime.Text == null ? null : wedStartTime.Text; ;
                        sd3.EndTime = wedEndTime.Text == null ? null : wedEndTime.Text; ;

                        if (wedcbNoBreak.IsChecked == true)
                        {
                            sd3.IsNoBreak_ = true;
                        }
                        else
                        {
                            sd3.IsNoBreak_ = false;
                        }
                        db.HRISEmployeesShifts.Add(sd3);
                        db.SaveChanges();

                        HRISEmployeesShift sd4 = new HRISEmployeesShift();
                        sd4.DayName = "Thu";
                        sd4.DayID = 4;
                        sd4.EmployeeID = empid;
                        sd4.EmployeeShiftCode = tbShiftCode.Text == null ? " " : tbShiftCode.Text; ;
                        sd4.StartTime = thuStartTime.Text == null ? null : thuStartTime.Text; ;
                        sd4.EndTime = thuEndTime.Text == null ? null : thuEndTime.Text; ;

                        if (thucbNoBreak.IsChecked == true)
                        {
                            sd4.IsNoBreak_ = true;
                        }
                        else
                        {
                            sd4.IsNoBreak_ = false;
                        }
                        db.HRISEmployeesShifts.Add(sd4);
                        db.SaveChanges();

                        HRISEmployeesShift sd5 = new HRISEmployeesShift();
                        sd5.DayName = "Fri";
                        sd5.DayID = 5;
                        sd5.EmployeeID = empid;
                        sd5.EmployeeShiftCode = tbShiftCode.Text == null ? " " : tbShiftCode.Text; ;
                        sd5.StartTime = friStartTime.Text == null ? null : friStartTime.Text; ;
                        sd5.EndTime = friEndTime.Text == null ? null : friEndTime.Text;

                        if (fricbNoBreak.IsChecked == true)
                        {
                            sd5.IsNoBreak_ = true;
                        }
                        else
                        {
                            sd5.IsNoBreak_ = false;
                        }
                        db.HRISEmployeesShifts.Add(sd5);
                        db.SaveChanges();

                        HRISEmployeesShift sd6 = new HRISEmployeesShift();
                        sd6.DayName = "Sat";
                        sd6.DayID = 6;
                        sd6.EmployeeID = empid;
                        sd6.EmployeeShiftCode = tbShiftCode.Text == null ? " " : tbShiftCode.Text; ;
                        sd6.StartTime = satStartTime.Text == null ? null : satStartTime.Text; ;
                        sd6.EndTime = satEndTime.Text == null ? null : satEndTime.Text;

                        if (satcbNoBreak.IsChecked == true)
                        {
                            sd6.IsNoBreak_ = true;
                        }
                        else
                        {
                            sd6.IsNoBreak_ = false;
                        }

                        db.HRISEmployeesShifts.Add(sd6);
                        db.SaveChanges();

                        HRISEmployeesShift sd7 = new HRISEmployeesShift();
                        sd7.DayName = "Sun";
                        sd7.DayID = 7;
                        sd7.EmployeeID = empid;
                        sd7.EmployeeShiftCode = tbShiftCode.Text == null ? " " : tbShiftCode.Text;
                        sd7.StartTime = sunStartTime.Text == null ? null : sunStartTime.Text;
                        sd7.EndTime = sunEndTime.Text == null ? null : sunEndTime.Text;


                        if (suncbNoBreak.IsChecked == true)
                        {
                            sd7.IsNoBreak_ = true;
                        }
                        else
                        {
                            sd7.IsNoBreak_ = false;
                        }

                        db.HRISEmployeesShifts.Add(sd7);
                        db.SaveChanges();

                        MessageBox.Show("Success.", "System Sucess!", MessageBoxButton.OK, MessageBoxImage.Information);
                        clear();
                    }
                    else
                    {
                        MessageBox.Show("Employee already have schedule.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clear() {
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
            cbEmp.Text = "";
            moncbNoBreak.IsChecked = false;
            tuescbNoBreak.IsChecked = false;
            wedcbNoBreak.IsChecked = false;
            thucbNoBreak.IsChecked = false;
            fricbNoBreak.IsChecked = false;
            satcbNoBreak.IsChecked = false;
            suncbNoBreak.IsChecked = false;

            tbShiftCode.Text = "";
            cbDefault.IsChecked = false;
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
            moncbNoBreak.IsChecked = false;
            tuescbNoBreak.IsChecked = false;
            wedcbNoBreak.IsChecked = false;
            thucbNoBreak.IsChecked = false;
            fricbNoBreak.IsChecked = false;
            satcbNoBreak.IsChecked = false;
            suncbNoBreak.IsChecked = false;
        }
    }
}
