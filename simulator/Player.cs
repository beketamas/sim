using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace simulator
{
    class Player : Button
    {
        readonly int x;
        readonly int y;
        string[,] terkep;
        readonly int jatekosPozX;
        readonly int jatekosPozY;
        public int X { get => x;}
        public int Y { get => y;}
        public string[,] Terkep { get => terkep;}
        public int JatekosPozX { get => jatekosPozX;}
        public int JatekosPozY { get => jatekosPozY;}

        public Player(int x, int y, int jatekosPozX, int jatekosPozY, string[,] map) 
        {
            this.x = x;
            this.y = y;
            this.jatekosPozX = jatekosPozX;
            this.jatekosPozY = jatekosPozY;
            this.terkep = map;
            terkep[JatekosPozX, JatekosPozY] = "jatekos";
            Click += (sender, e) => 
            {
                Terkep ujTerkep = new(Terkep);
                ujTerkep.Show();
            };
        }
    }
}
