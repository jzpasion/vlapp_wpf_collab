using Microsoft.Win32;
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

namespace vlapp
{
    /// <summary>
    /// Interaction logic for Upload_Page.xaml
    /// </summary>
    public partial class Upload_Page : Page
    {
        ObservableCollection<VideoListItem> vList = new ObservableCollection<VideoListItem>();
        public Upload_Page()
        {

            InitializeComponent();
            DataContext = this.GetList();


        }
        public ObservableCollection<VideoListItem> GetList()
        {
            vList.Add(new VideoListItem() { id = 1, title = "1", tfrom = "1:30", tto = "1:30" });
            vList.Add(new VideoListItem() { id = 2, title = "3:23", tfrom = "3:23", tto = "2:12" });
            vList.Add(new VideoListItem() { id = 3, title = "1:12", tfrom = "1:12", tto = "2:23" });
            vList.Add(new VideoListItem() { id = 4, title = "4:32", tfrom = "4:32", tto = "12:12" });
            vList.Add(new VideoListItem() { id = 1, title = "1", tfrom = "1:30", tto = "1:30" });
            vList.Add(new VideoListItem() { id = 2, title = "3:23", tfrom = "3:23", tto = "2:12" });
            vList.Add(new VideoListItem() { id = 3, title = "1:12", tfrom = "1:12", tto = "2:23" });
            vList.Add(new VideoListItem() { id = 4, title = "4:32", tfrom = "4:32", tto = "12:12" });
            vList.Add(new VideoListItem() { id = 1, title = "1", tfrom = "1:30", tto = "1:30" });
            vList.Add(new VideoListItem() { id = 2, title = "3:23", tfrom = "3:23", tto = "2:12" });
            vList.Add(new VideoListItem() { id = 3, title = "1:12", tfrom = "1:12", tto = "2:23" });
            vList.Add(new VideoListItem() { id = 4, title = "4:32", tfrom = "4:32", tto = "12:12" });
            vList.Add(new VideoListItem() { id = 1, title = "1", tfrom = "1:30", tto = "1:30" });
            vList.Add(new VideoListItem() { id = 2, title = "3:23", tfrom = "3:23", tto = "2:12" });
            vList.Add(new VideoListItem() { id = 3, title = "1:12", tfrom = "1:12", tto = "2:23" });


            return vList;
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
                txt_path.Text = openFileDialog.FileName;
            }
        }

        private void listbox_card_video_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 0)
            {
                txt_filename.Text = "video_"+(vList.Count + 1).ToString();
                txt_path.Text = files[0];
                popup_message.IsOpen = true;
            }
            
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            txt_filename.Text = "";
            txt_path.Text = "";
            popup_message.IsOpen = false;
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            //if (date_fromDate.SelectedDate != null && date_toDate != null)
            //{
            //    if(time_fromTime.SelectedTime != null && time_toTime.SelectedTime != null)
            //    {
            //        dateChecker((DateTime)date_fromDate.SelectedDate, (DateTime)date_toDate.SelectedDate);

            //        if (ResponseModel.Status == true)
            //        {
            //            MessageBox.Show("From Date Should Be Less Than To Date");
            //            ResponseModel.Status = false;
            //        }
            //        else
            //        {
            //            txt_filename.Text = "";
            //            txt_path.Text = "";
            //            popup_message.IsOpen = false;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Please fill in fromTime/toTime");
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("Please fill in fromDate/toDate");
            //}

            System.Diagnostics.Trace.WriteLine(time_toTime.SelectedTime);
        }

        private void date_fromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //string formatDate = date_fromDate.SelectedDate.Value.ToString("MM-dd-yyyy");

            //System.Diagnostics.Trace.WriteLine(formatDate);
        }

        private void dateChecker( DateTime fromDate, DateTime toDate)
        {
            ResponseModel.Status = false;

            if(fromDate > toDate)
            {
                ResponseModel.Status = true;
            }
        }
    }
}
