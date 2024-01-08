using System;
using System.Collections.Generic;
using System.Text;
using CardPair.Model;

namespace CardPair.ViewModel
{
    public enum FieldType
    {
        TurnedUpAce, TurnedDownAce, FoundAce,
        TurnedUpKing, TurnedDownKing, FoundKing,
        TurnedUpQueen, TurnedDownQueen, FoundQueen,
        TurnedUpJack, TurnedDownJack, FoundJack,
        TurnedUpTen, TurnedDownTen, FoundTen,
        TurnedUpNine, TurnedDownNine, FoundNine,
        TurnedUpEight, TurnedDownEight, FoundEight,
        TurnedUpSeven, TurnedDownSeven, FoundSeven
    }

    public class CardtableCellViewModel : BindingSource
    {
        #region Fields

        private FieldType _field;
        private int _ownerId;

        #endregion

        #region Properties

        internal int X { get; private set; }
        internal int Y { get; private set; }

        public FieldType Field
        {
            get => _field;
            private set
            {
                if (value != _field)
                {
                    _field = value;
                    OnPropertyChanged();
                }
            }
        }

        
        public int OwnerId
        {
            get => _ownerId;
            private set
            {
                if (value != _ownerId)
                {
                    _ownerId = value;
                    OnPropertyChanged();
                }
            }
        }

        public DelegateCommand TurnCommand { get; private set; }

        #endregion

        #region Constructors

        public CardtableCellViewModel(int x, int y, CardtableCell cell, bool isGameOver, DelegateCommand turnCommand)
        {
            Update(x, y, cell, isGameOver);
            TurnCommand = turnCommand;
        }

        #endregion


        #region Internal Methods

        internal void Update(int x, int y, CardtableCell cell, bool isGameOver)
        {
            X = x;
            Y = y;


            if (cell.IsTurnedUp)
            {
                if (cell.Owner != 0)
                {
                    switch (cell.PictureNumber)
                    {
                        case 0: Field = FieldType.FoundAce; break;
                        case 1: Field = FieldType.FoundKing; break;
                        case 2: Field = FieldType.FoundQueen; break;
                        case 3: Field = FieldType.FoundJack; break;
                        case 4: Field = FieldType.FoundTen; break;
                        case 5: Field = FieldType.FoundNine; break;
                        case 6: Field = FieldType.FoundEight; break;
                        case 7: Field = FieldType.FoundSeven; break;
                        default: Field = FieldType.FoundAce; break;
                    }
                    OwnerId = cell.Owner;
                }
                else
                {
                    switch (cell.PictureNumber)
                    {
                        case 0: Field = FieldType.TurnedUpAce; break;
                        case 1: Field = FieldType.TurnedUpKing; break;
                        case 2: Field = FieldType.TurnedUpQueen; break;
                        case 3: Field = FieldType.TurnedUpJack; break;
                        case 4: Field = FieldType.TurnedUpTen; break;
                        case 5: Field = FieldType.TurnedUpNine; break;
                        case 6: Field = FieldType.TurnedUpEight; break;
                        case 7: Field = FieldType.TurnedUpSeven; break;
                        default: Field = FieldType.TurnedUpAce; break;
                    }
                    OwnerId = cell.Owner;
                }
            }
            else
            {
                switch (cell.PictureNumber)
                {
                    case 0: Field = FieldType.TurnedDownAce; break;
                    case 1: Field = FieldType.TurnedDownKing; break;
                    case 2: Field = FieldType.TurnedDownQueen; break;
                    case 3: Field = FieldType.TurnedDownJack; break;
                    case 4: Field = FieldType.TurnedDownTen; break;
                    case 5: Field = FieldType.TurnedDownNine; break;
                    case 6: Field = FieldType.TurnedDownEight; break;
                    case 7: Field = FieldType.TurnedDownSeven; break;
                    default: Field = FieldType.TurnedDownAce; break;
                }
                OwnerId = cell.Owner;
            }
        }

        #endregion
    }
}
