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
using HRiS.Model;
using MahApps.Metro.Controls.Dialogs;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for LeaveMenu.xaml
    /// </summary>
    public partial class LeaveMenu : MetroWindow
    {
        public LeaveMenu()
        {
            InitializeComponent();
        }

        private void tileOnProcess_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveOnProcess x = new LeaveOnProcess();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileApproved_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveApproved x = new LeaveApproved();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileCancelled_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveDisapproved x = new LeaveDisapproved();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileLeaveBalance_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveBalance x = new LeaveBalance();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileLeaveUsage_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveUsage x = new LeaveUsage();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileTransactionReport_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveTransaction x = new LeaveTransaction();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileLeaveHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            LeaveHistory x = new LeaveHistory();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void tileFileLeave_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AddLeave x = new AddLeave();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }
    }
}
