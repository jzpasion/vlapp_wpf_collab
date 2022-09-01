﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using vlapp.Models;
using vlapp.Control;

namespace vlapp
{
    /// <summary>
    /// Interaction logic for Upload_Page.xaml
    /// </summary>
    public partial class Upload_Page : Page
    {
        ObservableCollection<VideoListItem> vList = new ObservableCollection<VideoListItem>();
        bool dateCheckStatus = false;
        public Upload_Page()
        {
            InitializeComponent();
            Database_Functions db = new Database_Functions();
            checkBoxStatus();
            DataContext = db.getSchedule();
        }




        private void btn_add_video_Click(object sender, RoutedEventArgs e)
        {
            popup_message.IsOpen = true;
        }

        private void btn_file_open_Click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Media Files|*.wav;*.aac;*.wma;*.wmv;*.avi;*.mpg;*.mpeg;*.m1v;*.mp2;*.mp3;*.mpa;*.mpe;*.m3u;*.mp4;*.mov;*.3g2;*.3gp2;*.3gp;*.3gpp;*.m4a;*.cda;*.aif;*.aifc;*.aiff;*.mid;*.midi;*.rmi;*.mkv;*.WAV;*.AAC;*.WMA;*.WMV;*.AVI;*.MPG;*.MPEG;*.M1V;*.MP2;*.MP3;*.MPA;*.MPE;*.M3U;*.MP4;*.MOV;*.3G2;*.3GP2;*.3GP;*.3GPP;*.M4A;*.CDA;*.AIF;*.AIFC;*.AIFF;*.MID;*.MIDI;*.RMI;*.MKV";
            if(openFileDialog.ShowDialog() == true)
            {
                txt_filename.Text = openFileDialog.SafeFileName;
                //txt_path.Text = openFileDialog.FileName;
            }
        }

        private void listbox_card_video_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txt_filename.Text = "video_"+(vList.Count + 1).ToString();
                //txt_path.Text = files[0];
                popup_message.IsOpen = true;
            }
            
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            txt_filename.Text = "";
            //txt_path.Text = "";
            popup_message.IsOpen = false;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (date_fromDate.SelectedDate != null && date_toDate.SelectedDate != null)
            {

                dateChecker((DateTime)date_fromDate.SelectedDate, (DateTime)date_toDate.SelectedDate);

                if (dateCheckStatus)
                {
                    MessageBox.Show("From Date Should Be Less Than To Date");
                    dateCheckStatus = false;
                }
                else
                {
                    if(timeCheckResponse() == "No Days is Checked!")
                    {
                        MessageBox.Show(timeCheckResponse());
                    }
                    else if (timeCheckResponse() == ""){
                        Database_Functions db = new Database_Functions();
                        long dayID = 0;
                        long schedLastId = db.saveSchedule("manghula kamuna", getTimestampDate((DateTime)date_fromDate.SelectedDate), getTimestampDate((DateTime)date_toDate.SelectedDate));
                        List<int> days = checkBoxStatus();
                        foreach (var item in days)
                        {
                            dayID = db.saveDay(item, (int)schedLastId);
                        }

                        foreach (var item in days)
                        {
                            switch (item)
                            {
                                case (1):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_monday.SelectedTime), getTimestampTime((DateTime)time_toTime_monday.SelectedTime), (int)dayID);
                                    break;
                                case (2):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_tuesday.SelectedTime), getTimestampTime((DateTime)time_toTime_tuesday.SelectedTime), (int)dayID);
                                    break;
                                case (3):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_wednesday.SelectedTime), getTimestampTime((DateTime)time_toTime_wednesday.SelectedTime), (int)dayID);
                                    break;
                                case (4):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_thursday.SelectedTime), getTimestampTime((DateTime)time_toTime_thursday.SelectedTime), (int)dayID);
                                    break;
                                case (5):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_friday.SelectedTime), getTimestampTime((DateTime)time_toTime_friday.SelectedTime), (int)dayID);
                                    break;
                                case (6):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_saturday.SelectedTime), getTimestampTime((DateTime)time_toTime_saturday.SelectedTime), (int)dayID);
                                    break;
                                case (7):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_sunday.SelectedTime), getTimestampTime((DateTime)time_toTime_sunday.SelectedTime), (int)dayID);
                                    break;
                                case (8):
                                    db.saveTime(getTimestampTime((DateTime)time_fromTime_all.SelectedTime), getTimestampTime((DateTime)time_toTime_all.SelectedTime), (int)dayID);
                                    break;
                            }
                        }

                        txt_filename.Text = "";
                        popup_message.IsOpen = false;
                    }
                    else
                    {
                        MessageBox.Show("Please fill in fromTime/toTime at " + timeCheckResponse() + "Time");
                    }
                }


            }
            else
            {
                MessageBox.Show("Please fill in fromDate/toDate");
            }

        

            }

        private string timeCheckResponse()
        {
            string response = "";
            List<int> days = checkBoxStatus();

            if(days.Count == 0)
            {
                response = "No Days is Checked!";
            }
            else
            {
                foreach (int day in days)
                {
                    switch (day)
                    {
                        case (1):
                            if (!timeChecker(time_fromTime_monday.SelectedTime, time_toTime_monday.SelectedTime))
                            {
                                response += "Monday ";
                            }
                            break;
                        case (2):
                            if (!timeChecker(time_fromTime_tuesday.SelectedTime, time_toTime_tuesday.SelectedTime))
                            {
                                response += "Tuesday ";
                            }
                            break;
                        case (3):
                            if (!timeChecker(time_fromTime_wednesday.SelectedTime, time_toTime_wednesday.SelectedTime))
                            {
                                response += "Wednesday ";
                            }
                            break;
                        case (4):
                            if (!timeChecker(time_fromTime_thursday.SelectedTime, time_toTime_thursday.SelectedTime))
                            {
                                response += "Thursday ";
                            }
                            break;
                        case (5):
                            if (!timeChecker(time_fromTime_friday.SelectedTime, time_toTime_friday.SelectedTime))
                            {
                                response += "Friday ";
                            }
                            break;
                        case (6):
                            if (!timeChecker(time_fromTime_saturday.SelectedTime, time_toTime_saturday.SelectedTime))
                            {
                                response += "Saturday ";
                            }
                            break;
                        case (7):
                            if (!timeChecker(time_fromTime_sunday.SelectedTime, time_toTime_sunday.SelectedTime))
                            {
                                response += "Sunday ";
                            }
                            break;
                        case (8):
                            if (!timeChecker(time_fromTime_all.SelectedTime, time_toTime_all.SelectedTime))
                            {
                                response += "All days ";
                            }
                            break;
                    }
                }
            }
            

            return response;

        }

        private string getTimestampDate(DateTime date)
        {
            string formatDate = date.ToString("yyyy-MM-dd");
            //string formatTime = time.ToString("hh:mm tt");


            string combine = formatDate;
            DateTime combineDateTime = DateTime.Parse(combine);
            var unixTimeSeconds = new DateTimeOffset(combineDateTime).ToUnixTimeSeconds();
            //System.Diagnostics.Trace.WriteLine(unixTimeSeconds);


            return unixTimeSeconds.ToString();

        }

        private string getTimestampTime(DateTime time)
        {
            //string formatDate = date.ToString("yyyy-MM-dd");
            string formatTime = time.ToString("hh:mm tt");


            string combine = formatTime;
            DateTime combineDateTime = DateTime.Parse(combine);
            var unixTimeSeconds = new DateTimeOffset(combineDateTime).ToUnixTimeSeconds();
            //System.Diagnostics.Trace.WriteLine(unixTimeSeconds);


            return unixTimeSeconds.ToString();

        }

        private void date_fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //string formatDate = date_fromDate.SelectedDate.Value.ToString("yyyy-MM-dd");

            //System.Diagnostics.Trace.WriteLine(formatDate);
        }

        private void dateChecker( DateTime fromDate, DateTime toDate)
        {
            dateCheckStatus = false;

            if(fromDate > toDate)
            {
                dateCheckStatus = true;
            }
        }

        private bool timeChecker(DateTime? fromTime , DateTime? toTime)
        {
            bool status = false;
            if(fromTime != null && toTime != null)
            {
                status = true;
            }

            return status;
        }


        //Days Clicked
        private void check_allDays_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_monday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_tuesday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_wednesday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_thursday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_friday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_saturday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private void check_sunday_Click(object sender, RoutedEventArgs e)
        {
            checkBoxStatus();
        }

        private List<int> checkBoxStatus()
        {
            List<int> checkedDays = new List<int>();
            //all
            if (check_allDays.IsChecked == true)
            {
                checkedDays.Add(8);
                disableAllDays();
                time_fromTime_all.IsEnabled = true;
                time_toTime_all.IsEnabled = true;
            }
            else
            {
                enableAllDays();
                time_fromTime_all.IsEnabled = false;
                time_toTime_all.IsEnabled = false;
            }

            //monday
            if(check_monday.IsChecked == true)
            {
                checkedDays.Add(1);
                time_fromTime_monday.IsEnabled = true;
                time_toTime_monday.IsEnabled= true;
            }
            else
            {
                time_fromTime_monday.IsEnabled = false;
                time_toTime_monday.IsEnabled = false;
            }

            //tuesday
            if (check_tuesday.IsChecked == true)
            {
                checkedDays.Add(2);
                time_fromTime_tuesday.IsEnabled = true;
                time_toTime_tuesday.IsEnabled = true;
            }
            else
            {
                time_fromTime_tuesday.IsEnabled = false;
                time_toTime_tuesday.IsEnabled = false;
            }

            //wednesday
            if (check_wednesday.IsChecked == true)
            {
                checkedDays.Add(3);
                time_fromTime_wednesday.IsEnabled = true;
                time_toTime_wednesday.IsEnabled = true;
            }
            else
            {
                time_fromTime_wednesday.IsEnabled = false;
                time_toTime_wednesday.IsEnabled = false;
            }

            //thursday
            if (check_thursday.IsChecked == true)
            {
                checkedDays.Add(4);
                time_fromTime_thursday.IsEnabled = true;
                time_toTime_thursday.IsEnabled = true;
            }
            else
            {
                time_fromTime_thursday.IsEnabled = false;
                time_toTime_thursday.IsEnabled = false;
            }

            //friday
            if (check_friday.IsChecked == true)
            {
                checkedDays.Add(5);
                time_fromTime_friday.IsEnabled = true;
                time_toTime_friday.IsEnabled = true;
            }
            else
            {
                time_fromTime_friday.IsEnabled = false;
                time_toTime_friday.IsEnabled = false;
            }

            //satruday
            if (check_saturday.IsChecked == true)
            {
                checkedDays.Add(6);
                time_fromTime_saturday.IsEnabled = true;
                time_toTime_saturday.IsEnabled = true;
            }
            else
            {
                time_fromTime_saturday.IsEnabled = false;
                time_toTime_saturday.IsEnabled = false;
            }

            //sunday
            if (check_thursday.IsChecked == true)
            {
                checkedDays.Add(7);
                time_fromTime_sunday.IsEnabled = true;
                time_toTime_sunday.IsEnabled = true;
            }
            else
            {
                time_fromTime_sunday.IsEnabled = false;
                time_toTime_sunday.IsEnabled = false;
            }

            return checkedDays;
        }

        private void disableAllDays()
        {
            day_monday.IsEnabled = false;
            day_monday.Visibility = Visibility.Collapsed;

            day_tuesday.IsEnabled = false;
            day_tuesday.Visibility = Visibility.Collapsed;

            day_wednesday.IsEnabled = false;
            day_wednesday.Visibility = Visibility.Collapsed;

            day_thrusday.IsEnabled = false;
            day_thrusday.Visibility = Visibility.Collapsed;

            day_friday.IsEnabled = false;
            day_friday.Visibility = Visibility.Collapsed;

            day_saturday.IsEnabled = false;
            day_saturday.Visibility = Visibility.Collapsed;

            day_sunday.IsEnabled = false;
            day_sunday.Visibility = Visibility.Collapsed;

        }

        private void enableAllDays()
        {
            day_monday.IsEnabled = true;
            day_monday.Visibility = Visibility.Visible;

            day_tuesday.IsEnabled = true;
            day_tuesday.Visibility = Visibility.Visible;

            day_wednesday.IsEnabled = true;
            day_wednesday.Visibility = Visibility.Visible;

            day_thrusday.IsEnabled = true;
            day_thrusday.Visibility = Visibility.Visible;

            day_friday.IsEnabled = true;
            day_friday.Visibility = Visibility.Visible;

            day_saturday.IsEnabled = true;
            day_saturday.Visibility = Visibility.Visible;

            day_sunday.IsEnabled = true;
            day_sunday.Visibility = Visibility.Visible;

        }

        //Time Changed All days
        private void time_fromTime_all_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_all.SelectedTime = timeValidation(time_fromTime_all.SelectedTime , time_toTime_all.SelectedTime , 1);

        }

        private void time_toTime_all_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_all.SelectedTime = timeValidation(time_fromTime_all.SelectedTime, time_toTime_all.SelectedTime , 2);
        }

        //Time Changed Monday
        private void time_fromTime_monday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_monday.SelectedTime = timeValidation(time_fromTime_monday.SelectedTime, time_toTime_monday.SelectedTime, 1);
        }

        private void time_toTime_monday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_monday.SelectedTime = timeValidation(time_fromTime_monday.SelectedTime, time_toTime_monday.SelectedTime, 2);
        }

        //Time Changed Tuesday

        private void time_fromTime_tuesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_tuesday.SelectedTime = timeValidation(time_fromTime_tuesday.SelectedTime, time_toTime_tuesday.SelectedTime, 1);
        }

        private void time_toTime_tuesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_tuesday.SelectedTime = timeValidation(time_fromTime_tuesday.SelectedTime, time_toTime_tuesday.SelectedTime, 2);
        }

        //Time Changed Wednesday

        private void time_fromTime_wednesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_wednesday.SelectedTime = timeValidation(time_fromTime_wednesday.SelectedTime, time_toTime_wednesday.SelectedTime, 1);
        }

        private void time_toTime_wednesday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_wednesday.SelectedTime = timeValidation(time_fromTime_wednesday.SelectedTime, time_toTime_wednesday.SelectedTime, 2);
        }

        //Time Changed Thursday

        private void time_fromTime_thursday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_thursday.SelectedTime = timeValidation(time_fromTime_thursday.SelectedTime, time_toTime_thursday.SelectedTime, 1);
        }

        private void time_toTime_thursday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_thursday.SelectedTime = timeValidation(time_fromTime_thursday.SelectedTime, time_toTime_thursday.SelectedTime, 2);
        }

        //Time Changed Friday

        private void time_fromTime_friday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_friday.SelectedTime = timeValidation(time_fromTime_friday.SelectedTime, time_toTime_friday.SelectedTime, 1);
        }

        private void time_toTime_friday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_friday.SelectedTime = timeValidation(time_fromTime_friday.SelectedTime, time_toTime_friday.SelectedTime, 2);
        }

        //Time Changed Saturday

        private void time_fromTime_saturday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_saturday.SelectedTime = timeValidation(time_fromTime_saturday.SelectedTime, time_toTime_saturday.SelectedTime, 1);
        }

        private void time_toTime_saturday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_saturday.SelectedTime = timeValidation(time_fromTime_saturday.SelectedTime, time_toTime_saturday.SelectedTime, 2);
        }

        //Time Changed Sunday

        private void time_fromTime_sunday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_fromTime_sunday.SelectedTime = timeValidation(time_fromTime_sunday.SelectedTime, time_toTime_sunday.SelectedTime, 1);
        }

        private void time_toTime_sunday_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            time_toTime_sunday.SelectedTime = timeValidation(time_fromTime_sunday.SelectedTime, time_toTime_sunday.SelectedTime, 2);
        }

        private DateTime timeValidation(DateTime? from, DateTime? to , int cat)
        {
            DateTime holder = DateTime.Now;

            if (from != null && to != null)
            {
                switch (cat)
                {
                    case (1):
                        if (from > to)
                        {
                            MessageBox.Show("From Time Should Be Less Than To Time");
                            holder = to.Value.AddMinutes(-1);

                        }
                        else
                        {
                            holder = (DateTime)from;

                        }
                        break;
                    case (2):
                        if (from > to)
                        {
                            MessageBox.Show("From Time Should Be Less Than To Time");
                            holder = from.Value.AddMinutes(1);
                        }
                        else
                        {
                            holder = (DateTime)to;
                        }
                        break;

                }
                return holder;
            }
            else
            {
                switch (cat)
                {
                    case (0):
                        break;
                    case(1):
                        holder =  (DateTime)from;
                        break;
                    case(2):
                        holder = (DateTime)to;
                        break;
                }
            }
            return holder;
        
        }

        
    }
}
