using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using FontAwesome.WPF;

namespace XamlGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FontAwesomeIcon elozoKartya;

        public MainWindow()
        {
            InitializeComponent();

            ButtonStart.IsEnabled = true;
            ButtonNo.IsEnabled = false;
            ButtonYes.IsEnabled = false;

            UjKartyaHuzasa();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Igen gombot nyomtunk");
            UjKartyaHuzasa();

            if (elozoKartya == CardRight.Icon)
            {
                JoValasz();
            }
            else
            {
                RosszValasz();
            }
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Nem gombot nyomtunk");
            UjKartyaHuzasa();

            if (elozoKartya != CardRight.Icon)
            {
                JoValasz();
            }
            else
            {
                RosszValasz();
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start gombot nyomtunk");
            UjKartyaHuzasa();

            ButtonStart.IsEnabled = false;
            ButtonNo.IsEnabled = true;
            ButtonYes.IsEnabled = true;
        }

        private void RosszValasz()
        {
            Debug.WriteLine("A valasz hibas volt.");
            CardLeft.Icon = FontAwesomeIcon.Times;
            CardLeft.Foreground = Brushes.Red;
        }

        private void JoValasz()
        {
            Debug.WriteLine("A valasz helyes volt.");
            CardLeft.Icon = FontAwesomeIcon.Check;
            CardLeft.Foreground = Brushes.Green;
        }


        private void UjKartyaHuzasa()
        {
            FontAwesome.WPF.FontAwesomeIcon[] kartyapakli = new FontAwesome.WPF.FontAwesomeIcon[6];
            kartyapakli[0] = FontAwesomeIcon.Car;
            kartyapakli[1] = FontAwesomeIcon.FighterJet;
            kartyapakli[2] = FontAwesomeIcon.Female;
            kartyapakli[3] = FontAwesomeIcon.Scissors;
            kartyapakli[4] = FontAwesomeIcon.Rocket;
            kartyapakli[5] = FontAwesomeIcon.Child;
            
            var dobokocka = new Random();
            var dobas = dobokocka.Next(0, 5);

            elozoKartya = CardRight.Icon;

            CardRight.Icon = kartyapakli[dobas];
            Debug.WriteLine($"A lap sorszama: { dobas }, a lap neve: { CardRight.Icon}");
        }

    }
}
