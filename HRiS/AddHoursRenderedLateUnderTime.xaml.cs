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
    /// Interaction logic for AddHoursRenderedLateUnderTime.xaml
    /// </summary>
    public partial class AddHoursRenderedLateUnderTime : MetroWindow
    {
        public int empid;
        public string empname;
        //public string timein;
        //public string breakout;
        //public string breakin;
        //public DateTime timeout;
        public string remark;
        public DateTime Date;
        public int timeinID;
        public int breakoutID;
        public int breakinID;
        public int timeoutID;
        public string curentdate;

        public AddHoursRenderedLateUnderTime()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbDate.Text = curentdate;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (timeinID != 0)
                    {
                        var biolog = db.BiometricsLogDatas.Where(m => m.Id == timeinID).FirstOrDefault();

                        biolog.REG = tbTotalHours.Text;
                        biolog.LATE = tbLate.Text;
                        biolog.UNDERTIME = tbUnderTime.Text;

                        db.SaveChanges();
                    }

                    if (timeoutID != 0)
                    {
                        var biolog = db.BiometricsLogDatas.Where(m => m.Id == timeoutID).FirstOrDefault();

                        biolog.REG = tbTotalHours.Text;
                        biolog.LATE = tbLate.Text;
                        biolog.UNDERTIME = tbUnderTime.Text;

                        db.SaveChanges();
                    }

                    MessageBox.Show("Updating attendance was successful", "System Successs!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
