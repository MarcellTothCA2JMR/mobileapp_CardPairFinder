using System;
using System.Collections.Generic;
using System.Text;

namespace CardPair.Model
{
    public class CellChangedEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public CardtableCell Cell { get; private set; }

        internal CellChangedEventArgs(int x, int y, CardtableCell cell)
        {
            X = x;
            Y = y;
            Cell = cell;
        }
    }
}
