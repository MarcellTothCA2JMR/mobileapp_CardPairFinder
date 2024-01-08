using System;
using System.Collections.Generic;
using System.Text;

namespace CardPair.Model
{
    public struct CardtableCell
    {
        
        public int Owner { get; set; }
        public int PictureNumber { get; private set; }
        public bool IsTurnedUp { get; private set; }

        
        internal static CardtableCell CreateCard(int pictureNumber) => new CardtableCell()
        {
            Owner = 0,
            PictureNumber = pictureNumber,
            IsTurnedUp = false,
        };
        
        internal void TurnUp() => IsTurnedUp = true;
        internal void TurnBackDown() => IsTurnedUp = false;

    }
}
