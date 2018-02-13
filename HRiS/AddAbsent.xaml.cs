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
    /// Interaction logic for AddAbsent.xaml
    /// </summary>
    public partial class AddAbsent : MetroWindow
    {
        public AddAbsent()
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

        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    HRISEmployeeAbsent ea = new HRISEmployeeAbsent();
                    int empID = Convert.ToInt32(cbEmp.SelectedValue);

                    ea.EmployeeID = empID;
                    ea.Date = absentDate.SelectedDate;
                    ea.Reason = txtReason.Text;
                    db.HRISEmployeeAbsents.Add(ea);
                    db.SaveChanges();
                    MessageBox.Show("Added Succesful", "System Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
