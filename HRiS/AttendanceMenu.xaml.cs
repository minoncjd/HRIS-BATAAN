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
    /// Interaction logic for AttendanceMenu.xaml
    /// </summary>
    public partial class AttendanceMenu : MetroWindow
    {
        public AttendanceMenu()
        {
            InitializeComponent();
        }

        private void tileTurnstile_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AttendanceTurnstile x = new AttendanceTurnstile();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileBiometrics_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
