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
using System.Windows.Media.Animation;
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
        FontAwesomeIcon elozoKartya;
        FontAwesomeIcon[] kartyapakli = new FontAwesomeIcon[] {
                                    FontAwesomeIcon.Car,
                                    FontAwesomeIcon.FighterJet,
                                    FontAwesomeIcon.Female,
                                    FontAwesomeIcon.Scissors,
                                    FontAwesomeIcon.Rocket,
                                    FontAwesomeIcon.Child };            
        Random dobokocka = new Random();
        private int Score;

        public MainWindow()
        {
            InitializeComponent();

            ButtonStart.IsEnabled = true;
            ButtonNo.IsEnabled = false;
            ButtonYes.IsEnabled = false;

            Score =  0;
            UjKartyaHuzasa();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start gombot nyomtunk");
            StartGame();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Igen gombot nyomtunk");
            YesAnswer();
        }


        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Nem gombot nyomtunk");
            NoAnswer();
        }

        private void StartGame()
        {
            // Csak egyszer, az elejen lehessen elinditani!
            if (ButtonStart.IsEnabled == true)
            {
                UjKartyaHuzasa();

                ButtonStart.IsEnabled = false;
                ButtonNo.IsEnabled = true;
                ButtonYes.IsEnabled = true;
            }
        }

        private void NoAnswer()
        {
            if (ButtonStart.IsEnabled == false)
            {
                if (elozoKartya != CardRight.Icon)
                {
                    JoValasz();
                }
                else
                {
                    RosszValasz();
                }
                UjKartyaHuzasa();
            }
        }

        private void YesAnswer()
        {
            if (ButtonStart.IsEnabled == false)
            { 
                if (elozoKartya == CardRight.Icon)
                {
                    JoValasz();
                }
                else
                {
                    RosszValasz();
                }
                UjKartyaHuzasa();
            }
        }

        private void RosszValasz()
        {
            Debug.WriteLine("A valasz hibas volt.");
            CardLeft.Icon = FontAwesomeIcon.Times;
            CardLeft.Foreground = Brushes.Red;

            Scoring();
            VisszajelzesEltuntetese();
        }

         private void JoValasz()
        {
            Debug.WriteLine("A valasz helyes volt.");
            CardLeft.Icon = FontAwesomeIcon.Check;
            CardLeft.Foreground = Brushes.Green;

            Scoring();
            VisszajelzesEltuntetese();
        }

        private void Scoring()
        {
            Score++;
            LabelScore.Content = Score;
        }

        private void VisszajelzesEltuntetese()
        {
            var animation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(1000));
            CardLeft.BeginAnimation(OpacityProperty, animation);
        }

        private void UjKartyaHuzasa()
        {
            var dobas = dobokocka.Next(0, 5);

            elozoKartya = CardRight.Icon;

            //Eltuntetni az elozo kartyat
            var animationOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200));
            CardRight.BeginAnimation(OpacityProperty, animationOut);

            CardRight.Icon = kartyapakli[dobas];

            //Megjeleniteni az uj kartyat
            var animationIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
            CardRight.BeginAnimation(OpacityProperty, animationIn);

            Debug.WriteLine($"A lap sorszama: { dobas }, a lap neve: { CardRight.Icon}");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine(e.Key);

            if (e.Key == Key.Up) StartGame();            
            if (e.Key == Key.Right) NoAnswer();                       
            if (e.Key == Key.Left) YesAnswer();        
        }
    }
}
