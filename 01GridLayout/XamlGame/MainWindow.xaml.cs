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
using System.Windows.Threading;
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
        private long  score;
        private DispatcherTimer pendulumClock;
        private TimeSpan playtime;
        private Stopwatch stopwatch;
        private List<long> listRactionTimes;

        public MainWindow()
        {
            InitializeComponent();

            ButtonStart.IsEnabled = true;
            ButtonNo.IsEnabled = false;
            ButtonYes.IsEnabled = false;

            score =  0;
            playtime = TimeSpan.FromSeconds(0);

            pendulumClock = new DispatcherTimer(
                TimeSpan.FromSeconds(1),        // 1 mp-kent valt ki esemenyt
                DispatcherPriority.Normal,
                ClockShock,                     // esemenyvezerlo, amit az ingaora minden mp-ben meghiv
                Application.Current.Dispatcher);// 
            //Mivel ez az ora azonnal elindul, allitsuk is meg: Majd a Start gombra kell elinduljon
            pendulumClock.Stop();

            //Stoperora letrehozasa az egyes reakcioidok meresere
            stopwatch = new Stopwatch();

            //Az osszes reakcioidot tartalmazo lista lettehozasa, az atlagos reakcioido szamitasahoz
            listRactionTimes = new List<long>();


        UjKartyaHuzasa();
        }

        /// <summary>
        /// Itt tudjuk a jatekidot szamitani, ezt a fgv-t hivja az ingaorank masodercenkent egyszer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClockShock(object sender, EventArgs e)
        {
            playtime = playtime + TimeSpan.FromSeconds(1);
            LablePlaytime.Content = $"{playtime.Minutes:00}:{playtime.Seconds:00}"; //00: Nulla helykitolto formatumstring
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

                //elinditjuk az idozitot:
                pendulumClock.Start();
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

            Scoring(false);
            VisszajelzesEltuntetese();
        }

         private void JoValasz()
        {
            Debug.WriteLine("A valasz helyes volt.");
            CardLeft.Icon = FontAwesomeIcon.Check;
            CardLeft.Foreground = Brushes.Green;

            Scoring(true);
            VisszajelzesEltuntetese();
        }

        private void Scoring(bool isGoodAnswer)
        {
            //stopwatch.Stop(); //nem szukseges, mert egszer olvassuk le az erteket...
            listRactionTimes.Add(stopwatch.ElapsedMilliseconds);

            //Atlagszamitas a.)
            var average1 = listRactionTimes.Sum() / listRactionTimes.Count;

            //Atlagszamitas b.)
            long sum2 = 0;
            foreach (var reactiontTime in listRactionTimes)
            {
                sum2 += reactiontTime;
            }
            var average2 = sum2 / listRactionTimes.Count;

            //Atlagszamitas c.)
            long sum3 = 0;
            for (int i = 0; i < listRactionTimes.Count; i++)
            {
                sum3 += listRactionTimes[i];
            }
            var average3 = sum3 / listRactionTimes.Count;

            //Atlagszamitas d.)
            long sum4 = 0;
            int j = 0;
            while (j < listRactionTimes.Count)
            {
                sum4 += listRactionTimes[j];
                j++;
            }
            var average4 = sum4 / j;

            LabelReactionTime.Content = $"{listRactionTimes.Last()}/{(long)listRactionTimes.Average()}/{average1}/{average2}/{average3}/{average4}";

            if (isGoodAnswer) score += 100000/listRactionTimes.Last();
            else score -= listRactionTimes.Last()/10;

            LabelScore.Content = score;
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

            stopwatch.Restart();

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
