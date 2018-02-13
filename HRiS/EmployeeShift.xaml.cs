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
using static HRiS.Model.HRiSClass;

namespace HRiS
{
    /// <summary>
    /// Interaction logic for EmployeeShift.xaml
    /// </summary>
    public partial class EmployeeShift : MetroWindow
    {
        List<HRISShift> lHRISShift = new List<HRISShift>();
        List<EmployeeShiftList> lEmployeeShiftList = new List<EmployeeShiftList>();
        public EmployeeShift()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetShifts();
        }

        public void Clear()
        {
            dgEmpShift.SelectedItem = null;
            txtDescription.Text = "";
            txtShiftCode.Text = "";
            startTimeComboBox.Text = "";
            endTimeComboBox.Text = "";
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;
        }
        public void GetShifts()
        {
            try
            {
                lHRISShift = new List<HRISShift>();
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var shifts = db.HRISShifts.ToList();

                    foreach (var x in shifts)
                    {
                        HRISShift hsl = new HRISShift();
                        hsl.ShiftCode = x.ShiftCode;
                        hsl.Description = x.Description;
                        hsl.StartTime = x.StartTime;
                        hsl.EndTime = x.EndTime;
                        lHRISShift.Add(hsl);
                    }
                    dgShifts.ItemsSource = lHRISShift.OrderBy(m => m.ShiftCode);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

            
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (txtShiftCode.Text != null && startTimeComboBox.SelectedItem != null && endTimeComboBox.SelectedItem != null)
                    {                
                        HRISShift hrisShift = new HRISShift();
                        hrisShift.ShiftCode = txtShiftCode.Text;
                        hrisShift.Description = txtDescription.Text;
                        hrisShift.StartTime = startTimeComboBox.Text;
                        hrisShift.EndTime = endTimeComboBox.Text;
                        db.HRISShifts.Add(hrisShift);
                        db.SaveChanges();
                        GetShifts();
                        Clear();
                    }

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
                    HRISShift hrisShfit = new HRISShift();
                    hrisShfit.Description = txtDescription.Text;
                    hrisShfit.StartTime = startTimeComboBox.Text;
                    hrisShfit.EndTime = endTimeComboBox.Text;
                    hrisShfit.ShiftCode = txtShiftCode.Text;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetShifts();
            Clear();
        }

        private void dgShifts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSave.IsEnabled = true;
            btnAdd.IsEnabled = false;
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    var selectedItem = (HRISShift)(dgShifts.SelectedItem);
                    if (dgShifts.SelectedItem != null)
                    {

                        lblShift.Content = "List of Current Employees with a shift of: " + selectedItem.ShiftCode;

                        lHRISShift = new List<HRISShift>();
                        txtDescription.Text = selectedItem.Description;
                        txtShiftCode.Text = selectedItem.ShiftCode;
                        endTimeComboBox.Text = selectedItem.EndTime;
                        startTimeComboBox.Text = selectedItem.StartTime;


                        var empShifts = (from a in db.HRISEmployeeSchedules
                                         join b in db.Employees on a.EmployeeNumber equals b.EmployeeNo
                                         join c in db.HRISShifts on a.ShiftCode equals c.ShiftCode
                                         where a.ShiftCode == selectedItem.ShiftCode && b.Archive == false
                                         select new { b.EmployeeNo, EmployeeName = b.LastName.ToUpper() + ", " + b.FirstName.ToUpper(), a.ShiftCode, c.StartTime, c.EndTime }).ToList();


                        foreach (var x in empShifts)
                        {
                            EmployeeShiftList esl = new EmployeeShiftList();
                            lEmployeeShiftList = new List<EmployeeShiftList>();

                            esl.EmployeeNo = x.EmployeeNo;
                            esl.EmployeeName = x.EmployeeName;
                            esl.ShiftCode = x.ShiftCode;
                            esl.StartTime = x.StartTime;
                            esl.EndTime = x.EndTime;

                            lEmployeeShiftList.Add(esl);
                        }

                        dgEmpShift.ItemsSource = lEmployeeShiftList.OrderBy(m => m.EmployeeName);
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
