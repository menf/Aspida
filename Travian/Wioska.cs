using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travian
{
    class Wioska
    {
        private int x;
        private int y;
        private string nazwa;
        private string href;
        private bool aktywna;

        public bool Aktywna { get => aktywna; set => aktywna = value; }
        public string Href { get => href; set => href = value; }
        public string Nazwa { get => nazwa; set => nazwa = value; }
        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }

        public Wioska(bool aktywna, string href, string nazwa, int y, int x)
        {
            Aktywna = aktywna;
            Href = href;
            Nazwa = nazwa;
            Y = y;
            X = x;
        }
    }
}
