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
                Debug.WriteLine("A valasz helyes volt.");
            }
            else
            {
                Debug.WriteLine("A valasz hibas volt.");
            }
        }


        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Nem gombot nyomtunk");
            UjKartyaHuzasa();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start gombot nyomtunk");
            UjKartyaHuzasa();

            ButtonStart.IsEnabled = false;
            ButtonNo.IsEnabled = true;
            ButtonYes.IsEnabled = true;
        }

        private void UjKartyaHuzasa()
        {
            FontAwesome.WPF.FontAwesomeIcon[] kartyapakli = new FontAwesome.WPF.FontAwesomeIcon[6];
            kartyapakli[0] = FontAwesome.WPF.FontAwesomeIcon.Car;
            kartyapakli[1] = FontAwesome.WPF.FontAwesomeIcon.FighterJet;
            kartyapakli[2] = FontAwesome.WPF.FontAwesomeIcon.Female;
            kartyapakli[3] = FontAwesome.WPF.FontAwesomeIcon.Scissors;
            kartyapakli[4] = FontAwesome.WPF.FontAwesomeIcon.Rocket;
            kartyapakli[5] = FontAwesome.WPF.FontAwesomeIcon.Child;
            
            var dobokocka = new Random();
            var dobas = dobokocka.Next(0, 5);

            elozoKartya = CardRight.Icon;

            CardRight.Icon = kartyapakli[dobas];
            Debug.WriteLine($"A lap sorszama: { dobas }, a lap neve: { CardRight.Icon}");
        }

    }
}
