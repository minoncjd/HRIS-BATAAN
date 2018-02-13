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
    /// Interaction logic for EmployeeShift.xaml
    /// </summary>
    public partial class UpdateEmployeeShift : MetroWindow
    {
        List<HRISShift> lShiftList = new List<HRISShift>();
        public string empnumber;
        public UpdateEmployeeShift()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetShiftDetails();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var shift = db.HRISShifts.ToList();

                    foreach (var x in shift)
                    {
                        HRISShift hrisShift = new HRISShift();
                        hrisShift.ShiftCode = x.ShiftCode;
                        lShiftList.Add(hrisShift);

                    }
                    cbShiftCode.ItemsSource = lShiftList.OrderBy(m => m.ShiftCode);
                    cbShiftCode.DisplayMemberPath = "ShiftCode";
                    cbShiftCode.SelectedValuePath = "ShiftCode";
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        private void GetShiftDetails()
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var empSchedule = (from a in db.HRISEmployeeSchedules
                                       join b in db.Employees on a.EmployeeNumber equals b.EmployeeNo
                                       join c in db.HRISShifts on a.ShiftCode equals c.ShiftCode
                                       where b.Archive == false && a.EmployeeNumber == empnumber
                                       select new { b.EmployeeNo, c.Description, a.ShiftCode, c.StartTime, c.EndTime }).FirstOrDefault();

                    txtDescription.Text = empSchedule.Description;
                    cbShiftCode.Text = empSchedule.ShiftCode;
                    txtStartTime.Text = empSchedule.StartTime;
                    txtEndTime.Text = empSchedule.EndTime;
                }
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        private void cbShiftCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var shiftCode = Convert.ToString(cbShiftCode.SelectedValue);
                    var shift = db.HRISShifts.Where(m => m.ShiftCode == shiftCode).FirstOrDefault();

                    cbShiftCode.Text = shift.ShiftCode;
                    txtDescription.Text = shift.Description;
                    txtEndTime.Text = shift.EndTime;
                    txtStartTime.Text = shift.StartTime;

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    HRISEmployeeSchedule hRISEmployeeSchedule = new HRISEmployeeSchedule();
                    var empSched = db.HRISEmployeeSchedules.Where(m => m.EmployeeNumber == empnumber).FirstOrDefault();

                    if (empSched != null)
                    {
                        empSched.ShiftCode = cbShiftCode.Text;
                        db.SaveChanges();
                        MessageBox.Show("Successfully Updated.", "System Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Employee Schedule not found.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
