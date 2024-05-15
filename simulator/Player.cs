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
        int numb;
        HashSet<string> whiteList = new();
        public int X { get => x;}
        public int Y { get => y;}
        public string[,] Terkep { get => terkep;}
        public int JatekosPozX { get => jatekosPozX;}
        public int JatekosPozY { get => jatekosPozY;}
        public int Numb { get => numb; set => numb = value; }
        public HashSet<string> WhiteList { get => whiteList; set => whiteList = value; }

        public Player(int x, int y, int jatekosPozX, int jatekosPozY, string[,] map, int sorSzam) 
        {
            this.x = x;
            this.y = y;
            this.jatekosPozX = jatekosPozX;
            this.jatekosPozY = jatekosPozY;
            this.terkep = map;
            numb = sorSzam;
            terkep[JatekosPozX, JatekosPozY] = "jatekos";
            Click += (sender, e) => 
            {
                Terkep ujTerkep = new(Terkep, numb, WhiteList);
                ujTerkep.Show();
            };
        }
    }
}
