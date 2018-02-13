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
    /// Interaction logic for LeaveTypeModule.xaml
    /// </summary>
    public partial class LeaveTypeModule : MetroWindow
    {
        public LeaveTypeModule()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetLeave();
        }

        public void GetLeave()
        {
            using (var db = new LetranIntegratedSystemEntities())
            {
                try
                {
                    var leave = db.LeaveTypes.OrderBy(m => m.LeaveCode).ToList();
                    dgLeave.ItemsSource = leave;

                    var sickleave = db.SickLeaveTypes.OrderBy(m => m.SickLeaveReasonType).ToList();
                    dgSickLeave.ItemsSource = sickleave;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void TextClear()
        {
            dgLeave.SelectedItem = null;
            dgSickLeave.SelectedItem = null;
            txtLeaveCode.Text = "";
            txtLeaveName.Text = "";
            txtSickLeave.Text = "";
            btnAddSL.IsEnabled = true;
            btnSaveSL.IsEnabled = false;
            btnAdd.IsEnabled = true;
            btnSave.IsEnabled = false;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (!String.IsNullOrEmpty(txtLeaveName.Text) || !String.IsNullOrEmpty(txtLeaveCode.Text))
                    {
                        LeaveType lt = new LeaveType();
                        lt.LeaveCode = txtLeaveCode.Text;
                        lt.LeaveDescription = txtLeaveName.Text;
                        db.LeaveTypes.Add(lt);
                        db.SaveChanges();
                        MessageBox.Show("Add Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetLeave();
                    }
                    else
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    if (dgLeave.SelectedItem != null)
                    {
                        var selectedItem = (LeaveType)(dgLeave.SelectedItem);

                        var find = db.LeaveTypes.Find(selectedItem.LeaveTypeID);

                        find.LeaveCode = txtLeaveCode.Text;
                        find.LeaveDescription = txtLeaveName.Text;

                        db.SaveChanges();
                        MessageBox.Show("Update Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetLeave();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetLeave();
            TextClear();
        }

        private void btnAddSL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (!String.IsNullOrEmpty(txtSickLeave.Text))
                    {
                        SickLeaveType sl = new SickLeaveType();
                        sl.SickLeaveReasonType = txtSickLeave.Text;
                        db.SaveChanges();
                        MessageBox.Show("Add Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetLeave();
                    }
                    else
                    {
                        MessageBox.Show("Required fields cannot be empty.", "System Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSaveSL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgSickLeave.SelectedItem != null)
                    {
                        var selectedItem = (SickLeaveType)(dgSickLeave.SelectedItem);

                        var find = db.SickLeaveTypes.Find(selectedItem.SickLeaveReasonID);

                        find.SickLeaveReasonType = txtSickLeave.Text;
                        db.SaveChanges();
                        MessageBox.Show("Update Successful", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                        TextClear();
                        GetLeave();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefreshSL_Click(object sender, RoutedEventArgs e)
        {
            GetLeave();
            TextClear();
        }

        private void dgLeave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSave.IsEnabled = true;
                btnAdd.IsEnabled = false;
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgLeave.SelectedItem != null)
                    {
                        var selectedItem = (LeaveType)(dgLeave.SelectedItem);
                        var find = db.LeaveTypes.Find(selectedItem.LeaveTypeID);

                        txtLeaveName.Text = find.LeaveDescription;
                        txtLeaveCode.Text = find.LeaveCode;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgSickLeave_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                btnSaveSL.IsEnabled = true;
                btnAddSL.IsEnabled = false;
                using (var db = new LetranIntegratedSystemEntities())
                {
                    if (dgSickLeave.SelectedItem != null)
                    {
                        var selectedItem = (SickLeaveType)(dgSickLeave.SelectedItem);
                        var find = db.SickLeaveTypes.Find(selectedItem.SickLeaveReasonID);

                        txtSickLeave.Text = find.SickLeaveReasonType;
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
