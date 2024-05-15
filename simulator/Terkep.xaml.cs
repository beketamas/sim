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
using System.Windows.Shapes;

namespace simulator
{
    /// <summary>
    /// Interaction logic for Terkep.xaml
    /// </summary>
    public partial class Terkep : Window
    {
        public Terkep()
        {
            InitializeComponent();
        }
        public Terkep(string[,] terkep, int sorSzam, HashSet<string> whiteList)
        {
            InitializeComponent();
            Title = $"{sorSzam}. player || {String.Join("||", whiteList)}";
            Uri iconUri = new("turbo.jpeg", UriKind.RelativeOrAbsolute);
            Icon = BitmapFrame.Create(iconUri);
            int xSor = terkep.GetLength(0);
            int yOszlop = terkep.GetLength(1);
            for (int oszlopIndex = 0; oszlopIndex < yOszlop; oszlopIndex++)
            {
                ColumnDefinition ujOszlop = new();
                gdTerkep.ColumnDefinitions.Add(ujOszlop);

            }
            for (int sorIndex = 0; sorIndex < xSor; sorIndex++)
            {
                RowDefinition ujSor = new();
                gdTerkep.RowDefinitions.Add(ujSor);
            }
            for (int i = 0; i < xSor; i++)
            {
                for (int j = 0; j < yOszlop; j++)
                {
                    Label lbl = new()
                    {
                        Content = terkep[i, j] == "jatekos" ? "jatekos"  : terkep[i, j] == "semmi" ? "semmi" : "?",
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetRow(lbl, i);
                    Grid.SetColumn(lbl, j);
                    gdTerkep.Children.Add(lbl);
                    Border border = new()
                    {
                        BorderBrush = new SolidColorBrush(Colors.Red),
                        BorderThickness = new Thickness(1)
                    };
                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    gdTerkep.Children.Add(border);
                }
            }
        }
    }
}
