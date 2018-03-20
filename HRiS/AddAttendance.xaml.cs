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
    /// Interaction logic for AddAttendance.xaml
    /// </summary>
    public partial class AddAttendance : MetroWindow
    {

        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();
       
        public AddAttendance()
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

                    cbEmployee.ItemsSource = EList.OrderBy(m => m.EmployeeName);
                    cbEmployee.DisplayMemberPath = "EmployeeName";
                    cbEmployee.SelectedValuePath = "EmployeeID";

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

        private void cbMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    BiometricsLogData bld = new BiometricsLogData();
                    if (cbEmployee.SelectedItem != null && dpDate.SelectedDate != null)
                    {
                        var empid = Convert.ToInt32(cbEmployee.SelectedValue);
                        var emp = db.Employees.Where(m => m.EmployeeID == empid).FirstOrDefault();

                        DateTime date = Convert.ToDateTime(dpDate.SelectedDate);
                      


                        if (timeIn.Text != null)
                        {
                            DateTime time = Convert.ToDateTime(timeIn.Text);
                            bld.EMPN = Convert.ToInt32(emp.bioid);
                            bld.DTIME = date.Date + time.TimeOfDay;
                            bld.REMARK = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                             bld.MODE = "I";
                            db.BiometricsLogDatas.Add(bld);
                            db.SaveChanges();
                        }
                        if (breakOut.Text != null)
                        {
                            DateTime time = Convert.ToDateTime(breakOut.Text);
                            bld.EMPN = Convert.ToInt32(emp.bioid);
                            bld.DTIME = date.Date + time.TimeOfDay;
                            bld.MODE = "BO";
                            bld.REMARK = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                            db.BiometricsLogDatas.Add(bld);
                            db.SaveChanges();
                        }
                        if (breakIn.Text != null)
                        {
                            DateTime time = Convert.ToDateTime(breakIn.Text);
                            bld.EMPN = Convert.ToInt32(emp.bioid);
                            bld.DTIME = date.Date + time.TimeOfDay;
                            bld.REMARK = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                            bld.MODE = "BI";
                            db.BiometricsLogDatas.Add(bld);
                            db.SaveChanges();
                        }
                        if (timeOut.Text != null)
                        {
                            DateTime time = Convert.ToDateTime(timeOut.Text);
                            bld.EMPN = Convert.ToInt32(emp.bioid);
                            bld.DTIME = date.Date + time.TimeOfDay;
                            bld.REMARK = String.IsNullOrEmpty(txtRemark.Text) ? null : txtRemark.Text;
                            bld.MODE = "O";
                            db.BiometricsLogDatas.Add(bld);
                            db.SaveChanges();
                        }

                                           
                        MessageBox.Show("Added Successfully.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Complete the required data.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear()
        {
            
            dpDate.SelectedDate = DateTime.Now;
            cbEmployee.Text = "";
            timeIn.Text = "";
            breakOut.Text = "";
            breakIn.Text = "";
            timeOut.Text = "";
            txtRemark.Text = "";
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        
    }
    
}
