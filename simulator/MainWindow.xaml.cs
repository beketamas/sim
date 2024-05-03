using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int SOROK_SZAMA = 10;
        const int OSZLOPOK_SZAMA = 15;
        int playerCount = 10;
        string[,] matrix = new string[SOROK_SZAMA,OSZLOPOK_SZAMA];
        string lepes = "";
        public MainWindow()
        {
            InitializeComponent();

            tabla.ColumnDefinitions.Clear();
            tabla.RowDefinitions.Clear();
            tabla.Background = new SolidColorBrush(Colors.LightGreen);
            int szamlalo = 1;

            for (int i = 0; i < SOROK_SZAMA; i++)
            {
                for (int j = 0; j < OSZLOPOK_SZAMA; j++)
                    matrix[i, j] = "semmi";
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
            for (int i = 0; i < SOROK_SZAMA; i++)
            {

                for (int j = 0; j < OSZLOPOK_SZAMA; j++)
                {
                    Border border = new Border();
                    border.BorderBrush = new SolidColorBrush(Colors.Red);
                    border.BorderThickness = new Thickness(1);
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    tabla.Children.Add(border);

                    Random rnd = new Random();
                    int szam = rnd.Next(0, 5);
                    if (szam == 1 && matrix[i,j] == "semmi")
                    {
                        Ellipse kor = new()
                        {
                            Fill = new SolidColorBrush(Colors.Orange),
                            Width = 30,
                            Height = 30,
                        };
                        Grid.SetColumn(kor, j);
                        Grid.SetRow(kor, i);
                        tabla.Children.Add(kor);
                        matrix[i, j] = "akna";
                    }
                    else if (szam == 2 && playerCount != 0)
                    {
                        Button player = new()
                        {
                            Background = new SolidColorBrush(Colors.Yellow),
                            Content = szamlalo,
                        };
                        playerCount--;
                        tabla.Children.Add(player);
                        szamlalo++;
                        Random randomSor = new();
                        Random randomOzlop = new();
                        while (true)
                        {
                            int szam1 = randomSor.Next(0, SOROK_SZAMA);
                            int szam2 = randomOzlop.Next(0, OSZLOPOK_SZAMA);
                            if (matrix[szam1,szam2] == "semmi" )
                            {
                                Grid.SetColumn(player, szam2);
                                Grid.SetRow(player, szam1);
                                matrix[szam1, szam2] = "jatekos";
                                break;
                            }
                        }

                    }
                }
            }

            btnLepes.Click += (s, r) =>
            {
                foreach (var item in tabla.Children)
                {
                    if (item is Button)
                    {
                        int sor = Grid.GetRow(item as Button);
                        int oszlop = Grid.GetColumn(item as Button);
                        if (sor+1 < SOROK_SZAMA && matrix[sor + 1, oszlop] == "semmi" && lepes != "le")
                        {
                            Grid.SetColumn(item as Button, Grid.GetColumn(item as Button));
                            Grid.SetRow(item as Button, Grid.GetRow(item as Button) + 1);
                            lepes = "le";
                        }
                        else if (sor - 1 > 0 && matrix[sor - 1, oszlop] == "semmi" && lepes != "fel")
                        {
                            Grid.SetColumn(item as Button, Grid.GetColumn(item as Button));
                            Grid.SetRow(item as Button, Grid.GetRow(item as Button) - 1);
                            lepes = "fel";
                        }
                        else if (oszlop + 1 < OSZLOPOK_SZAMA && matrix[sor, oszlop + 1] == "semmi" && lepes != "jobb")
                        {
                            Grid.SetColumn(item as Button, Grid.GetColumn(item as Button) + 1);
                            Grid.SetRow(item as Button, Grid.GetRow(item as Button));
                            lepes = "jobb";

                        }
                        else if (oszlop - 1 > 0 && matrix[sor, oszlop - 1] == "semmi" && lepes != "bal")
                        {
                            Grid.SetColumn(item as Button, Grid.GetColumn(item as Button) - 1);
                            Grid.SetRow(item as Button, Grid.GetRow(item as Button));
                            lepes = "bal";


                        }
                        Random num = new Random();
                        //item.Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(num.Next(0,255)), Convert.ToByte(num.Next(0, 255)), Convert.ToByte(num.Next(0, 255))));
                    }
                }
            };

        }
    }
}
