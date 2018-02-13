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
    /// Interaction logic for ReportMenu.xaml
    /// </summary>
    public partial class ReportMenu : MetroWindow
    {
        public ReportMenu()
        {
            InitializeComponent();
        }

        private void tileEmpStatus_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByStatus x = new EmployeeByStatus();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileEmpDepartment_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByDepartment x = new EmployeeByDepartment();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileBirthday_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByBirthday x = new EmployeeByBirthday();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileServiceAwardee_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeServiceAwardee x = new EmployeeServiceAwardee();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileEmpAge_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByAge x = new EmployeeByAge();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileEmpGender_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeByGender x = new EmployeeByGender();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }
    }
}
