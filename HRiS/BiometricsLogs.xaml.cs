using HRiS.Model;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
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
    /// Interaction logic for BiometricsLogs.xaml
    /// </summary>
    public partial class BiometricsLogs : MetroWindow
    {
        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        private int iMachineNumber = 1;
        List<EmpCombo> EList = new List<EmpCombo>();
        List<Month> MList = new List<Month>();
        List<BiometricList> BList = new List<BiometricList>();
        LetranIntegratedSystemEntities db = new LetranIntegratedSystemEntities();
        

        public BiometricsLogs()
        {
            InitializeComponent();
       
        }    
        public void ConnectBiometrics()
        {
            try
            {
                axCZKEM1.Connect_Net("192.168.252.200", Convert.ToInt32("4370"));
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        public void InsertoDB(int empno, DateTime dtime, string mode)
        {
            try
            {
                BiometricsLogData biometricsLogData = new BiometricsLogData();
                biometricsLogData.MODE = mode;
                biometricsLogData.EMPN = empno;
                biometricsLogData.DTIME = dtime;
                db.BiometricsLogDatas.Add(biometricsLogData);
                db.SaveChanges();

            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }     
        public void GetBiometricsLogData()
        {
            try
            {

            int idwEnrollNumber = 0;
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwErrorCode = 0;
            string TimeStr = "";            
            ConnectBiometrics();                  
            BList = new List<BiometricList>();
            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device


                if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
                {
                    int count = 0;
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += (o, ea) =>
                    {
                            //int dwMachineNumber, ref int dwTMachineNumber, ref int dwEnrollNumber, ref int dwEMachineNumber, ref int dwVerifyMode, ref int dwInOutMode, ref int dwYear, ref int dwMonth, ref int dwDay, ref int dwHour, ref int dwMinute
                            while (axCZKEM1.GetGeneralLogDataStr(iMachineNumber, ref idwEnrollNumber, ref idwVerifyMode, ref idwInOutMode, ref TimeStr))//get records from the memory
                            {
                                DateTime biometricDate = Convert.ToDateTime(TimeStr);
                                BiometricList bl = new BiometricList();
                            
                                if (idwInOutMode == 1)
                                {
                                    bl.Type = "O";
                                }
                                else if (idwInOutMode == 0)
                                {
                                    bl.Type = "I";
                                }
                                else if (idwInOutMode == 2)
                                {
                                    bl.Type = "BO";
                                }
                                else if (idwInOutMode == 3)
                                {
                                    bl.Type = "BI";
                                }   
                                
                                bl.EnrollNumber = idwEnrollNumber;
                                bl.BiometricDate = biometricDate;
                                bl.DayOfWeek = biometricDate.DayOfWeek.ToString().ToUpper();
                                bl.Month = biometricDate.Date.ToString("MMMM");
                                bl.Year = biometricDate.Date.ToString("yyyy").ToUpper();
                                bl.BiometricTime = biometricDate.TimeOfDay;

                            //insert to db
                            var exists = db.BiometricsLogDatas.Where(m => m.EMPN == bl.EnrollNumber && m.DTIME == bl.BiometricDate).FirstOrDefault();
                            if (exists == null)
                            {
                                InsertoDB(bl.EnrollNumber, bl.BiometricDate, bl.Type);
                                count++;
                            }

                            Dispatcher.Invoke((System.Action)(() => bi.BusyContent = string.Format("Data Exported: "  + count)));                          
                                BList.Add(bl);                            
                             }
                    };

                    worker.RunWorkerCompleted += (o, ea) =>
                    {
                        bi.IsBusy = false;
                        lblCount.Content = "Count: " + BList.Count.ToString();
                        dgTurnstile.ItemsSource = BList.OrderByDescending(m => m.BiometricDate);
                       
                        MessageBox.Show("Export success." + " " + count + " data exported.", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    };
                    bi.IsBusy = true;
                    worker.RunWorkerAsync();                   
                 }
         
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);

                    if (idwErrorCode != 0)
                    {
                        MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
                        
                            return;

                    }
                    else
                    {
                        MessageBox.Show("No data from terminal returns!", "Error");
                        
                            return;
                    }
                }
                    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device 

                Mouse.OverrideCursor = Cursors.Arrow;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

        }

        public class BiometricList
        {
            public int EmployeeID { get; set; }
            public int EnrollNumber { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeNumber { get; set; }
            public DateTime BiometricDate { get; set; }
            public string Month { get; set; }
            public string Year { get; set; }
            public string DayOfWeek { get; set; }
            public TimeSpan BiometricTime { get; set; }
            public string Type { get; set; }
            public string Remarks { get; set; }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            GetBiometricsLogData();
            //clearLogs();


        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {

        }

        public class EmpCombo
        {
            public int EmployeeID { get; set; }
            public string EmployeeNumber { get; set; }
            public string EmployeeName { get; set; }
        }

        public class Month
        {
            public int MonthID { get; set; }
            public string MonthDescription { get; set; }
        }

        private void btnClearLogs_Click(object sender, RoutedEventArgs e)
        {
            clearLogs();
        }

        private void clearLogs()
        {
            try
            {
                GetBiometricsLogData();
                axCZKEM1.ClearGLog(iMachineNumber);           
                MessageBox.Show("Success", "System Success!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong!", "System Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

      
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnbtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to clear biometrics log? ", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                clearLogs();
            }

     
        }
    }
}
