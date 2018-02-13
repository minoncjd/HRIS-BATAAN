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
    /// Interaction logic for MaintenanceMenu.xaml
    /// </summary>
    public partial class MaintenanceMenu : MetroWindow
    {
        public MaintenanceMenu()
        {
            InitializeComponent();
        }

        private void tileJob_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            EmployeeJobPosition x = new EmployeeJobPosition();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileEmpDepartment_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AcademicDepartmentModule x = new AcademicDepartmentModule();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileEmpRank_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
