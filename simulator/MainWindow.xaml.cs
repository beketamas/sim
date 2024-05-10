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
        const int JATEKOSOK_SZAMA = 30;
        const int AKNAK_SZAM = 10;
        string[,] matrix = new string[SOROK_SZAMA,OSZLOPOK_SZAMA];
        List<string> helyek = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            tabla.ColumnDefinitions.Clear();
            tabla.RowDefinitions.Clear();
            tabla.Background = new SolidColorBrush(Colors.LightGreen);
            for (int i = 0; i < SOROK_SZAMA; i++)
            {
                for (int j = 0; j < OSZLOPOK_SZAMA; j++)
                {
                    matrix[i, j] = "semmi";
                    helyek.Add($"{i};{j}");
                    Border border = new();
                    border.BorderBrush = new SolidColorBrush(Colors.Red);
                    border.BorderThickness = new Thickness(1);
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    tabla.Children.Add(border);
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
            for (int i = 0; i < AKNAK_SZAM; i++)
            {
                Random rnd = new();
                var sure = helyek.Where(x => x != "akna" && x != "jatekos").ToList();
                int szam = rnd.Next(0, sure.Count);
                int x = int.Parse(sure[szam].Split(";")[0]);
                int y = int.Parse(sure[szam].Split(";")[1]);
                if (matrix[x, y] == "semmi")
                {
                    matrix[x, y] = "akna";
                    Ellipse kor = new()
                    {
                        Fill = new SolidColorBrush(Colors.Orange),
                        Width = 30,
                        Height = 30,
                    };
                    Grid.SetColumn(kor, y);
                    Grid.SetRow(kor, x);
                    tabla.Children.Add(kor);
                    helyek[szam] = "akna";
                }
            }
            for (int i = 0; i < JATEKOSOK_SZAMA; i++)
            {
                Random rnd = new();
                var sure = helyek.Where(x => x != "akna" && x != "jatekos").ToList();

                int szam = rnd.Next(0, sure.Count);
                int x = int.Parse(sure[szam].Split(";")[0]);
                int y = int.Parse(sure[szam].Split(";")[1]);
                if (matrix[x, y] == "semmi")
                {

                    matrix[x, y] = "jatekos";
                    Button player = new()
                    {
                        Background = new SolidColorBrush(Colors.Yellow),
                        Content = i+1,
                    };
                    Grid.SetColumn(player, y);
                    Grid.SetRow(player, x);
                    tabla.Children.Add(player);
                    helyek[szam] = "jatekos";
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
                        List<string> listLehetosegek = new List<string>();
                        if (sor + 1 < SOROK_SZAMA && matrix[sor + 1, oszlop] == "semmi")
                            listLehetosegek.Add("le");
                        if (sor - 1 > 0 && matrix[sor - 1, oszlop] == "semmi")
                            listLehetosegek.Add("fel");
                        if (oszlop + 1 < OSZLOPOK_SZAMA && matrix[sor, oszlop + 1] == "semmi")
                            listLehetosegek.Add("jobb");
                        if (oszlop - 1 >= 0 && matrix[sor, oszlop - 1] == "semmi")
                            listLehetosegek.Add("bal");
                        Random numLep = new();
                        if (listLehetosegek.Count != 0)
                        {
                            switch (listLehetosegek[numLep.Next(0, listLehetosegek.Count)])
                            {
                                case "le":
                                    Grid.SetColumn(item as Button, Grid.GetColumn(item as Button));
                                    Grid.SetRow(item as Button, Grid.GetRow(item as Button) + 1);
                                    matrix[sor, oszlop] = "semmi";
                                    matrix[sor + 1, oszlop] = "jatekos";
                                    break;
                                case "fel":
                                    Grid.SetColumn(item as Button, Grid.GetColumn(item as Button));
                                    Grid.SetRow(item as Button, Grid.GetRow(item as Button) - 1);
                                    matrix[sor, oszlop] = "semmi";
                                    matrix[sor - 1, oszlop] = "jatekos";
                                    break;
                                case "jobb":
                                    Grid.SetColumn(item as Button, Grid.GetColumn(item as Button) + 1);
                                    Grid.SetRow(item as Button, Grid.GetRow(item as Button));
                                    matrix[sor, oszlop] = "semmi";
                                    matrix[sor, oszlop + 1] = "jatekos";
                                    break;
                                case "bal":
                                    Grid.SetColumn(item as Button, Grid.GetColumn(item as Button) - 1);
                                    Grid.SetRow(item as Button, Grid.GetRow(item as Button));
                                    matrix[sor, oszlop] = "semmi";
                                    matrix[sor, oszlop - 1] = "jatekos";
                                    break;
                            }
                        }
                        Random num = new Random();
                        (item as Button).Background = new SolidColorBrush(Color.FromRgb(Convert.ToByte(num.Next(0,255)), Convert.ToByte(num.Next(0, 255)), Convert.ToByte(num.Next(0, 255))));
                    }
                }
            };



        }
    }
}
