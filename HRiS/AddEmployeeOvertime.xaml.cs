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
    /// Interaction logic for AddEmployeeOvertime.xaml
    /// </summary>
    public partial class AddEmployeeOvertime : MetroWindow
    {
        List<HRiSClass.EmpCombo> EList = new List<HRiSClass.EmpCombo>();
        public AddEmployeeOvertime()
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
                if (cbEmp.Text != null && startTime.Text != null && endTime.Text != null && dateFiled.SelectedDate != null && otdate.SelectedDate != null && tbCalendarDay.Text != null )
                {              
                    using (var db = new LetranIntegratedSystemEntities())
                    {
                        var empid = Convert.ToInt32(cbEmp.SelectedValue);
                        //var empid = 3029;
                        HRISOvertime ho = new HRISOvertime();
                        HRISOvertimeDetail hd = new HRISOvertimeDetail();
                        ho.EmployeeID = empid;
                        ho.DateFiled = dateFiled.SelectedDate.Value;
                        db.HRISOvertimes.Add(ho);
                        ho.HRISOvertimeDetails.Add(new HRISOvertimeDetail() { Date = otdate.SelectedDate.Value.Date, CalendarDay = tbCalendarDay.Text, Reason = tbReason.Text, StartTime = startTime.Text, EndTime = endTime.Text});
                        db.SaveChanges();
                        MessageBox.Show("OT successfully filed.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        Clear();

                    }
                }

                else
                {
                    MessageBox.Show("Complete the required data.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        private void Clear()
        {
            tbReason.Text = "";
            cbEmp.Text = "";
            dateFiled.SelectedDate = null;
            tbCalendarDay.Text = "";
            startTime.Text = "";
            endTime.Text = "";
            otdate.SelectedDate = null;
        }
    }
}
