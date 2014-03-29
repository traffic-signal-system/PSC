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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace PSC
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool CheckIPAddress(string ipAddress){
    // 如果什么没有等于无效

    if (string.IsNullOrWhiteSpace(ipAddress)){
        return false;

    }
    string ip = ipAddress.Trim();

    // 用"."把地址分开，如果分成的份数不是4份等于无效

    string[] numbers = ip.Split('.');

    if (numbers.Length != 4){
        return false;    
    }
    int num = 0;

    // 如果分成的四份中有任何一份不是整数等于无效

    if (numbers.Any(n => !int.TryParse(n, out num))){
        return false;
    }

    // 如果数字大于255或者小于0等于无效

    if (numbers.Any(n => (int.Parse(n) > 255) || (int.Parse(n) < 0))){
        return false;
    }

// 如果第一份是0等于无效

    return int.Parse(numbers[0]) != 0;
}
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
           string ipaddress = ip.Text;
           if(!CheckIPAddress(ipaddress))
           {
               return;
           }
           if (tsc.IsChecked == true)
           {
               bool boolTsc= Udp.sendUdpNoReciveData(ipaddress, 5435, Define.SET_TSC);
               string result = "Tsc模式切换：";
               if (boolTsc)
               {
                   result += "切换到Tsc模式成功。";
               }
               else
               {
                   result += "切换到Tsc模式失败，请检查信号机IP地址及网络情况！";
               }
               System.Windows.MessageBox.Show(result);
           }
           else if (psc1.IsChecked == true)
           {
               bool boolPsc1 = Udp.sendUdpNoReciveData(ipaddress, 5435, Define.SET_PSC_1);
               string str = dudOnePSC.Text;
               int greentime = int.Parse(str);
               byte[] psc1greentime = Define.SET_PSC_1_GREEN_TIME;
               psc1greentime[5] = (byte)greentime;
               bool boolPsc1time = Udp.sendUdpNoReciveData(ipaddress, 5435, psc1greentime);
               string result = "PSC模块切换：";
               if (boolPsc1)
               {
                   result += "到PSC一次过街模式成功，";
               }
               else
               {
                   result += "到PSC一次过街模式失败，请检查IP地址及网络。\n";
               }
               if (boolPsc1time)
               {
                   result += "一次过街时间为" + greentime + "秒成功。";
               }
               else
               {
                   result += "一次过街时间为" + greentime + "秒失败。";
               }
               System.Windows.MessageBox.Show(result);
           }
           else if (psc2.IsChecked == true)
           {
               bool boolPsc2 = Udp.sendUdpNoReciveData(ipaddress, 5435, Define.SET_PSC_2);
               string str1 = dudOnePSC.Text;
               int greentime1 = int.Parse(str1);
               byte[] psc1greentime = Define.SET_PSC_1_GREEN_TIME;
               psc1greentime[5] = (byte)greentime1;
               bool boolPsc1time = Udp.sendUdpNoReciveData(ipaddress, 5435, psc1greentime);
               
               string str2 = dudTwoPSC.Text;
               int greentime2 = int.Parse(str2);
               byte[] psc2greentime = Define.SET_PSC_2_GREEN_TIME;
               psc2greentime[5] = (byte)greentime2;
               bool boolPsc2time = Udp.sendUdpNoReciveData(ipaddress, 5435, psc2greentime);
               string result = "PSC模块切换：";
               if (boolPsc2)
               {
                   result += "到PSC二次过街模式成功，";
               }else
               {
                   result += "到PSC二次过街模式失败，请检查IP地址及网络。\n";
               }
               if (boolPsc1time)
               {
                   result += "一次过街时间为" + greentime1 + "秒，\n";
               }
               else
               {
                   result += "一次过街时间为" + greentime1 + "秒失败，\n";
               }
               if (boolPsc2time)
               {
                   result += "二次过街时间为" + greentime2 + "秒。";
               }
               else
               {
                   result += "二次过街时间为" + greentime2 + "秒失败。";
               }
               System.Windows.MessageBox.Show(result);
           }
           
        }

        private void psc1_Unchecked(object sender, RoutedEventArgs e)
        {
            lblOnePSC.Visibility = Visibility.Hidden;
            lblTwoPSC.Visibility = Visibility.Hidden;
            dudOnePSC.Visibility = Visibility.Hidden;
            dudTwoPSC.Visibility = Visibility.Hidden;
        }

        private void psc1_Checked(object sender, RoutedEventArgs e)
        {
            lblOnePSC.Visibility = Visibility.Visible;
            lblTwoPSC.Visibility = Visibility.Hidden;

            dudTwoPSC.Visibility = Visibility.Hidden;
            dudOnePSC.Visibility = Visibility.Visible;
        }

        private void psc2_Checked(object sender, RoutedEventArgs e)
        {
            lblOnePSC.Visibility = Visibility.Visible;
            lblTwoPSC.Visibility = Visibility.Visible;
            dudOnePSC.Visibility = Visibility.Visible;
            dudTwoPSC.Visibility = Visibility.Visible;
        }

        private void psc2_Unchecked(object sender, RoutedEventArgs e)
        {
            lblOnePSC.Visibility = Visibility.Hidden;
            lblTwoPSC.Visibility = Visibility.Hidden;
            dudOnePSC.Visibility = Visibility.Hidden;
            dudTwoPSC.Visibility = Visibility.Hidden;
        }

        private void tsc_Checked(object sender, RoutedEventArgs e)
        {
            lblOnePSC.Visibility = Visibility.Hidden;
            lblTwoPSC.Visibility = Visibility.Hidden;
            dudOnePSC.Visibility = Visibility.Hidden;
            dudTwoPSC.Visibility = Visibility.Hidden;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            lblOnePSC.Visibility = Visibility.Hidden;
            lblTwoPSC.Visibility = Visibility.Hidden;
            dudOnePSC.Visibility = Visibility.Hidden;
            dudTwoPSC.Visibility = Visibility.Hidden;
        }

    }
}
