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
    /// Interaction logic for BiometricsAddBioNumber.xaml
    /// </summary>
    public partial class BiometricsAddBioNumber : MetroWindow
    {
        public int EmpId;
        public BiometricsAddBioNumber()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GetBioNumber()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var bioNumber = db.Employees.Where(m => m.EmployeeID == EmpId).FirstOrDefault();
                    tbBionumber.Text = Convert.ToString(bioNumber.bioid);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAddBionumber_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
                var result = db.Employees.Where(m => m.EmployeeID == EmpId).FirstOrDefault();
                if (!String.IsNullOrEmpty(tbBionumber.Text))
                {
                    if (result != null)
                    {
                        if (result.bioid == null)
                        {
                            result.bioid = Convert.ToInt16(tbBionumber.Text);
                            db.SaveChanges();
                            tbBionumber.Text = "";
                            MessageBox.Show("Biometrics number successfully added.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Biometrics number already exist.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Employee not found.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Input biometrics number!", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            
            }
           
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
       
            GetBioNumber();

            if (tbBionumber.Text != "")
            {
                btnAddBionumber.Content = "SAVE";
            }
        }
    }
}
