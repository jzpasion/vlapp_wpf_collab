using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Brightness_Page.xaml
    /// </summary>
    public partial class Brightness_Page : Page
    {
        ConnectionModel connection = new ConnectionModel();
        ObservableCollection<BlindListItem> blindList;
        BlindListItem? element;
        ModuleModel? ModuleElement;
        int index, moduleIndex , blindIDHolder;
        public Brightness_Page()
        {
            InitializeComponent();
            connection.sendWithResponse();
            blindList = connection.GetBlindList();
            listbox_arrange_ip.ItemsSource = blindList;
            listbox_module_1.ItemsSource = displayModule();
            listbox_module_2.ItemsSource = displayModule();
            listbox_module_3.ItemsSource = displayModule();
            listbox_module_4.ItemsSource = displayModule();
            //if (blindList.Count == 0)
            //{
            //    btn_Save_Arrangement.Visibility = Visibility.Collapsed;
            //    btn_Refresh.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    btn_Save_Arrangement.Visibility = Visibility.Visible;
            //    btn_Refresh.Visibility = Visibility.Collapsed;
            //}
        }
        private void sliderb1_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb1_red.Value);
            txtb1_red.Text = "Red: " + value;
        }

        private void sliderb1_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb1_green.Value);
            txtb1_green.Text = "Green: " + value;
        }

        private void sliderb1_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb1_blue.Value);
            txtb1_blue.Text = "Blue: " + value;
        }

        private void sliderb2_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb2_red.Value);
            txtb2_red.Text = "Red: " + value;
        }

        private void sliderb2_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb2_green.Value);
            txtb2_green.Text = "Green: " + value;
        }

        private void sliderb2_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb2_blue.Value);
            txtb2_blue.Text = "Blue: " + value;
        }

        private void sliderb3_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb3_red.Value);
            txtb3_red.Text = "Red: " + value;
        }

        private void sliderb3_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb3_green.Value);
            txtb3_green.Text = "Green: " + value;
        }

        private void sliderb3_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb3_blue.Value);
            txtb3_blue.Text = "Blue: " + value;
        }

        private void sliderb4_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb4_red.Value);
            txtb4_red.Text = "Red: " + value;
        }

        private void sliderb4_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb4_green.Value);
            txtb4_green.Text = "Green: " + value;
        }

        private void sliderb4_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(sliderb4_blue.Value);
            txtb4_blue.Text = "Blue: " + value;
        }


        private void slider_popup_red_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(slider_popup_red.Value);
            txt_popup_red.Text = "Red: " + value;
        }

        private void slider_popup_green_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(slider_popup_green.Value);
            txt_popup_green.Text = "Green: " + value;
        }

        private void slider_popup_blue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = Convert.ToInt32(slider_popup_blue.Value);
            txt_popup_blue.Text = "Blue: " + value;
        }

        private List<ModuleModel> displayModule()
        {
            List<ModuleModel> modules = new List<ModuleModel>();
            modules.Add(new ModuleModel() { ModulePosition = "Module 1"});
            modules.Add(new ModuleModel() { ModulePosition = "Module 2" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 3" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 4" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 5" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 6" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 7" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 8" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 9" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 10" });
            modules.Add(new ModuleModel() { ModulePosition = "Module 11" });

           return modules;
        }

        private void btn_esp_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;


            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    index = this.listbox_arrange_ip.Items.IndexOf(item);
                    element = (BlindListItem)this.listbox_arrange_ip.Items[index];

                    txt_EspNumber.Content = "Selected Esp ID: "+element.BlindIndex.ToString();
                    txt_BlindNumber.Content = "Number of blinds: "+ element.BlindNumber.ToString();

                    switch(element.BlindNumber)
                    {
                        case 1:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            blind_2.IsEnabled = false;
                            blind_3.IsEnabled = false;
                            blind_4.IsEnabled = false;
                        break;
                        case 2:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            blind_2.IsEnabled = true;
                            blind_3.IsEnabled = false;
                            blind_4.IsEnabled = false;
                            break;
                        case 3:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            blind_2.IsEnabled = true;
                            blind_3.IsEnabled = true;
                            blind_4.IsEnabled = false;
                            break;
                        case 4:
                            resetSliders();
                            blind_1.IsEnabled = true;
                            blind_2.IsEnabled = true;
                            blind_3.IsEnabled = true;
                            blind_4.IsEnabled = true;
                            break;
                    }

                    System.Diagnostics.Trace.WriteLine(element.BlindNumber);
                    //blindList[index].BlindIndex

                }
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            popup_BrightnessChange.IsOpen = false;
        }

        private void resetSliders()
        {
            sliderb1_blue.Value = 0;
            sliderb1_green.Value = 0;
            sliderb1_red.Value = 0;

            sliderb2_blue.Value = 0;
            sliderb2_green.Value = 0;
            sliderb2_red.Value = 0;

            sliderb3_blue.Value = 0;
            sliderb3_green.Value = 0;
            sliderb3_red.Value = 0;

            sliderb4_blue.Value = 0;
            sliderb4_green.Value = 0;
            sliderb4_red.Value = 0;
        }


        private void btn_module1_Click_1(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    moduleIndex = this.listbox_module_1.Items.IndexOf(item)+1;
                    blindIDHolder = 1;

                    txt_BlindNumber_popup.Content = "Blind ID: "+ blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: "+moduleIndex.ToString();

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }

        private void btn_module2_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    moduleIndex = this.listbox_module_2.Items.IndexOf(item) + 1;
                    blindIDHolder = 2;

                    txt_BlindNumber_popup.Content = "Blind ID: " + blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: " + moduleIndex.ToString();

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }

        private void btn_module3_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    moduleIndex = this.listbox_module_3.Items.IndexOf(item) + 1;
                    blindIDHolder = 3;

                    txt_BlindNumber_popup.Content = "Blind ID: " + blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: " + moduleIndex.ToString();

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }

        private void btn_module4_Click(object sender, RoutedEventArgs e)
        {
            Button? btn = sender as Button;

            if (btn != null)
            {
                object item = btn.DataContext;


                if (item != null)
                {
                    moduleIndex = this.listbox_module_4.Items.IndexOf(item) + 1;
                    blindIDHolder = 4;

                    txt_BlindNumber_popup.Content = "Blind ID: " + blindIDHolder.ToString();
                    txt_ModuleNumber_popup.Content = "Module Number: " + moduleIndex.ToString();

                    popup_BrightnessChange.IsOpen = true;
                }
            }
        }
    }
}
