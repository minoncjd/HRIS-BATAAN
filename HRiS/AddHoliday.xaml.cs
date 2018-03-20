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
    /// Interaction logic for AddHoliday.xaml
    /// </summary>
    public partial class AddHoliday : MetroWindow
    {
        public AddHoliday()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    HRISHoliday ho = new HRISHoliday();
                    ho.Date = hodate.SelectedDate.Value;
                    ho.Description = txtDescription.Text;
                    db.HRISHolidays.Add(ho);
                    db.SaveChanges();
                    MessageBox.Show("Successfully added.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    clear();
                }
             }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }   

        private void clear()
        {
            txtDescription.Text = "";
            hodate.SelectedDate = null;
        }

    }
}
