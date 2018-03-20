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
    /// Interaction logic for HolidayList.xaml
    /// </summary>
    public partial class HolidayList : MetroWindow
    {
        List<HRISHoliday> lHRISHoliday = new List<HRISHoliday>();
        public HolidayList()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetHolidays();
        }

        public void GetHolidays()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    lHRISHoliday = new List<HRISHoliday>();
                    var holidays = db.HRISHolidays.ToList();

                    foreach (var x in holidays)
                    {
                        HRISHoliday ho = new HRISHoliday();
                        ho.Date = x.Date;
                        ho.Description = x.Description;
                        lHRISHoliday.Add(ho);
                    }

                    dgHolidaysList.ItemsSource = lHRISHoliday.OrderBy(m => m.Date);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            AddHoliday x = new AddHoliday();
            x.ShowDialog();
            this.Cursor = Cursors.Arrow;
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
          
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetHolidays();
        }
    }
}
