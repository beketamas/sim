using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int SOROK_SZAMA = 10;
        const int OSZLOPOK_SZAMA = 15;
        const int JATEKOSOK_SZAMA = 30;
        const int AKNAK_SZAM = 10;
        static string[,] matrix = new string[SOROK_SZAMA, OSZLOPOK_SZAMA];
        public static List<string> helyek = new List<string>();
        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            sldCsuszka.Value = 250;
            tabla.ColumnDefinitions.Clear();
            tabla.RowDefinitions.Clear();
            for (int i = 0; i < SOROK_SZAMA; i++)
            {
                for (int j = 0; j < OSZLOPOK_SZAMA; j++)
                {
                    matrix[i, j] = "semmi";
                    helyek.Add($"{i};{j}");
                    //Border border = new();
                    //border.BorderBrush = new SolidColorBrush(Colors.Red);
                    //border.BorderThickness = new Thickness(1);
                    //Grid.SetColumn(border, j);
                    //Grid.SetRow(border, i);
                    //tabla.Children.Add(border);
                }
            }
            for (int oszlopIndex = 0; oszlopIndex < OSZLOPOK_SZAMA; oszlopIndex++)
            {
                ColumnDefinition ujOszlop = new();
                tabla.ColumnDefinitions.Add(ujOszlop);

            }
            for (int sorIndex = 0; sorIndex < SOROK_SZAMA; sorIndex++)
            {
                RowDefinition ujSor = new();
                tabla.RowDefinitions.Add(ujSor);
            }

            int index1 = 0;
            while (index1 < AKNAK_SZAM)
            {
                Random rnd = new();
                var sure = helyek.Where(x => x != "akna" && x != "jatekos").ToList();
                int szam = rnd.Next(0, sure.Count);
                int x = int.Parse(sure[szam].Split(";")[0]);
                int y = int.Parse(sure[szam].Split(";")[1]);
                if (matrix[x, y] == "semmi")
                {
                    Rectangle rectangle = new();
                    BitmapImage luk = new();
                    luk.BeginInit();
                    luk.UriSource = new Uri(@"hole.png", UriKind.RelativeOrAbsolute);
                    luk.EndInit();
                    rectangle.Fill = new ImageBrush(luk);
                    Grid.SetColumn(rectangle, y);
                    Grid.SetRow(rectangle, x);
                    tabla.Children.Add(rectangle);
                    helyek[szam] = "akna";
                    matrix[x, y] = "akna";
                    index1++;
                }
            }
            int index2 = 0;
            while (index2 < JATEKOSOK_SZAMA)
            {
                Random rnd = new();
                var sure = helyek.Where(x => x != "akna" && x != "jatekos").ToList();

                int szam = rnd.Next(0, sure.Count);
                int x = int.Parse(sure[szam].Split(";")[0]);
                int y = int.Parse(sure[szam].Split(";")[1]);
                if (matrix[x, y] == "semmi")
                {
                    BitmapImage babu = new();
                    babu.BeginInit();
                    babu.UriSource = new Uri(@"turbo.jpeg", UriKind.RelativeOrAbsolute);
                    Player jatekos = new(SOROK_SZAMA,OSZLOPOK_SZAMA,x,y, new string[SOROK_SZAMA,OSZLOPOK_SZAMA], index2+1) 
                    {

                        //Background = new SolidColorBrush(Colors.Yellow),
                        Background = new ImageBrush(babu),
                        //Content = index2 + 1,
                    };
                    babu.EndInit();
                    Grid.SetColumn(jatekos, y);
                    Grid.SetRow(jatekos, x);
                    tabla.Children.Add(jatekos);
                    helyek[szam] = "jatekos";
                    matrix[x, y] = "jatekos";
                    index2++;

                }
            }

            btnLepes.Click += (s, e) => Lepkedes(tabla);
            auto.Checked += (s, e) =>
            {
                btnLepes.IsEnabled = false;
                timer = new()
                {
                    Interval = TimeSpan.FromMilliseconds(sldCsuszka.Value)
                };
                timer.Tick += Timer_Tick;
                sldCsuszka.ValueChanged += Slider_ValueChanged;
                timer.Start();
            };
            manual.Checked += (s, e) =>
            {
                btnLepes.IsEnabled = true;
                timer.Stop();
            };
        }
        public static void VaneELuk(int x, int y, List<string> lista, string merre) => lista.Add(matrix[x, y] == "akna" ? "akna" : merre);
        private void Timer_Tick(object sender, EventArgs e) => Lepkedes(tabla);
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timer.Interval = TimeSpan.FromMilliseconds(sldCsuszka.Value);
            timer.Stop();
            timer.Start();
        }
        public static void Lepkedes(Grid tabla)
        {
            for (int i = 0; i < tabla.Children.Count; i++)
            {
                if (tabla.Children[i] is Button)
                {
                    Button? gomb = tabla.Children[i] as Button;
                    int sor = Grid.GetRow(gomb);
                    int oszlop = Grid.GetColumn(gomb);
                    List<string> listLehetosegek = new List<string>();
                    if (sor + 1 < SOROK_SZAMA && (matrix[sor + 1, oszlop] == "semmi" || matrix[sor + 1, oszlop] == "akna"))
                        VaneELuk(sor + 1, oszlop, listLehetosegek, "le");
                    if (sor - 1 > 0 && (matrix[sor - 1, oszlop] == "semmi" || matrix[sor - 1, oszlop] == "akna"))
                        VaneELuk(sor - 1, oszlop, listLehetosegek, "fel");
                    if (oszlop + 1 < OSZLOPOK_SZAMA && (matrix[sor, oszlop + 1] == "semmi" || matrix[sor, oszlop + 1] == "akna"))
                        VaneELuk(sor, oszlop + 1, listLehetosegek, "jobb");
                    if (oszlop - 1 >= 0 && (matrix[sor, oszlop - 1] == "semmi" || matrix[sor, oszlop - 1] == "akna"))
                        VaneELuk(sor, oszlop - 1, listLehetosegek, "bal");

                    Random numLep = new();
                    if (listLehetosegek.Count != 0 && gomb is Player player)
                    {
                        switch (listLehetosegek[numLep.Next(0, listLehetosegek.Count)])
                        {
                            case "le":
                                Grid.SetColumn(gomb, Grid.GetColumn(gomb));
                                Grid.SetRow(gomb, Grid.GetRow(gomb) + 1);
                                matrix[sor, oszlop] = "semmi";
                                matrix[sor + 1, oszlop] = "jatekos";
                                player.Terkep[sor, oszlop] = "semmi";
                                player.Terkep[sor+1, oszlop] = "jatekos";
                                player.WhiteList.Add($"{sor+1};{oszlop}");
                                break;
                            case "fel":
                                Grid.SetColumn(gomb, Grid.GetColumn(gomb));
                                Grid.SetRow(gomb, Grid.GetRow(gomb) - 1);
                                matrix[sor, oszlop] = "semmi";
                                matrix[sor - 1, oszlop] = "jatekos";
                                player.Terkep[sor, oszlop] = "semmi";
                                player.Terkep[sor -1, oszlop] = "jatekos";
                                player.WhiteList.Add($"{sor - 1};{oszlop}");
                                break;
                            case "jobb":
                                Grid.SetColumn(gomb, Grid.GetColumn(gomb) + 1);
                                Grid.SetRow(gomb, Grid.GetRow(gomb));
                                matrix[sor, oszlop] = "semmi";
                                matrix[sor, oszlop + 1] = "jatekos";
                                player.Terkep[sor, oszlop] = "semmi";
                                player.Terkep[sor, oszlop+1] = "jatekos";
                                player.WhiteList.Add($"{sor};{oszlop + 1}");
                                break;
                            case "bal":
                                Grid.SetColumn(gomb, Grid.GetColumn(gomb) - 1);
                                Grid.SetRow(gomb, Grid.GetRow(gomb));
                                matrix[sor, oszlop] = "semmi";
                                matrix[sor, oszlop - 1] = "jatekos";
                                player.Terkep[sor, oszlop] = "semmi";
                                player.Terkep[sor, oszlop-1] = "jatekos";
                                player.WhiteList.Add($"{sor};{oszlop - 1}");
                                break;
                            case "akna":
                                matrix[sor, oszlop] = "semmi";
                                UIElement? element = tabla.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == sor && Grid.GetColumn(e) == oszlop);
                                tabla.Children.Remove(element);
                                //Broderezes(tabla);
                                break;
                        }
                    }
                    //Random num = new Random();
                    //gomb.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(num.Next(0, 255)), Convert.ToByte(num.Next(0, 255)), Convert.ToByte(num.Next(0, 255))));

                }
            }
        }
        public static void Borderezes(Grid tabla)
        {
            for (int i = 0; i < SOROK_SZAMA; i++)
            {
                for (int j = 0; j < OSZLOPOK_SZAMA; j++)
                {
                    Border border = new();
                    border.BorderBrush = new SolidColorBrush(Colors.Red);
                    border.BorderThickness = new Thickness(1);
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    tabla.Children.Add(border);
                }
            }
        }
    }
}
