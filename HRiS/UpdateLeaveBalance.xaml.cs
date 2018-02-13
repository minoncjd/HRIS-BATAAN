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

namespace HRiS
{
    /// <summary>
    /// Interaction logic for UpdateLeaveBalance.xaml
    /// </summary>
    public partial class UpdateLeaveBalance : MetroWindow
    {
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        public int leavebalanceid;

        public UpdateLeaveBalance()
        {
            InitializeComponent();
        }

        public void GetLeaveBalance()
        {
            try
            {
                db = new LetranIntegratedSystemEntities();

                var leavebal = db.LeaveBalances.Find(leavebalanceid);

                if(leavebal != null)
                {
                    lblEmpNo.Content = leavebal.Employee.EmployeeNo;
                    lblEmpName.Content = leavebal.Employee.LastName.ToUpper() + ", " + leavebal.Employee.FirstName.ToUpper();

                    txtSL.Text = leavebal.SickLeaveBalance.Value.ToString();
                    txtVL.Text = leavebal.VacationLeaveBalance.Value.ToString();
                    //txtEL.Text = leavebal.EmergencyLeaveBalance.Value.ToString();
                    //txtBL.Text = leavebal.BereavementLeaveBalance.Value.ToString();
                    //txtSPL.Text = leavebal.SoloParentLeaveBalance.Value.ToString();
                }
                else
                {
                    MessageBox.Show("Leave balance not found", "System Warning.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.Close();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.","System Error!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLeaveBalance();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db = new LetranIntegratedSystemEntities();
                var x = db.LeaveBalances.Where(m=>m.LeaveBalanceID == leavebalanceid).FirstOrDefault();

                x.SickLeaveBalance = Convert.ToDecimal(txtSL.Text);
                x.VacationLeaveBalance = Convert.ToDecimal(txtVL.Text);
                //x.EmergencyLeaveBalance = Convert.ToDecimal(txtEL.Text);
                //x.BereavementLeaveBalance = Convert.ToDecimal(txtBL.Text);
                //x.SoloParentLeaveBalance = Convert.ToDecimal(txtSPL.Text);

                db.SaveChanges();
                MessageBox.Show("Save Successful","System Success!",MessageBoxButton.OK,MessageBoxImage.Information);
                this.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
