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
    /// Interaction logic for AddOffCampusActivity.xaml
    /// </summary>
    public partial class AddOffCampusActivity : MetroWindow
    {
        public AddOffCampusActivity()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            var db = new LetranIntegratedSystemEntities();
            var emp = db.Employees.Where(m => m.Archive == false).ToList();
            List<EmpCombo> EList = new List<EmpCombo>();
            foreach (var i in emp)
            {
                EmpCombo ec = new EmpCombo();
                ec.EmployeeID = i.EmployeeID;
                ec.EmployeeNumber = i.EmployeeNo;
                ec.EmployeeName = i.LastName.ToUpper() + ", " + i.FirstName.ToUpper();
                EList.Add(ec);
            }

            cbEmp.ItemsSource = EList.OrderBy(m => m.EmployeeName);
            cbEmp.DisplayMemberPath = "EmployeeName";
            cbEmp.SelectedValuePath = "EmployeeID";
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    HRISOffCampusActivity oca = new HRISOffCampusActivity();
                    oca.EmployeeID = Convert.ToInt32(cbEmp.SelectedValue);
                    oca.DateStart = startDate.SelectedDate;
                    oca.DateEnd = startDate.SelectedDate;
                    oca.DateFiled = dateFile.SelectedDate;
                    oca.Reason = txtReason.Text;
                    TimeSpan duration = DateTime.Parse(endTime.Text).Subtract(DateTime.Parse(startTime.Text));
                    oca.TotalHours = duration.ToString(@"hh\:mm");
                    oca.StartTime = startTime.Text;
                    oca.EndTime = endTime.Text;
                    db.HRISOffCampusActivities.Add(oca);
                    db.SaveChanges();
                    MessageBox.Show("Success!.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear();

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear()
        {
            cbEmp.Text = "";
            dateFile.SelectedDate = null;
            startDate.SelectedDate = null;
            startTime.Text = "";
            endTime.Text = "";
            txtReason.Text = "";
        }

        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }
    }
}
