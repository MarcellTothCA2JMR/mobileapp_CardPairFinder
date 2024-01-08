using System;
using System.Collections.Generic;
using System.Linq;

namespace CardPair.Model
{
    public class CardPairModel
    {
        #region Fields

        private CardtableCell[,] _cardtable;
        
        public int playerIdentifier;
        public int numberOfPicks;
        public int firstPicksId;
        public int secondPicksId;
        public int firstPickX;
        public int firstPickY;
        public int secondPickX;
        public int secondPickY;
        public int pairsLeft;

        #endregion

        #region Properties

        public bool IsGameOver { get; private set; }
        public int Width { get => _cardtable.GetLength(0); }
        public int Height { get => _cardtable.GetLength(1); }
        
        public int PlayerIdentifier { get; private set; }
        public int NumberOfPicks { get; private set; }
        public int FirstPicksId { get; private set; }
        public int SecondPicksId { get; private set; }
        public int FirstPickX { get; private set; }
        public int FirstPickY { get; private set; }
        public int SecondPickX { get; private set; }
        public int SecondPickY { get; private set; }
        public int PairsLeft { get; private set; }


        #endregion

        #region Events

        public event EventHandler<CellChangedEventArgs> CellChanged;
        public event EventHandler CardtableChanged;
        public event EventHandler GameOver;
        
        #endregion

        #region Constructors

        public CardPairModel()
        {
            NewGame();
        }

        #endregion

        #region Private Methods
        
        private void OnCellChanged(int x, int y) => CellChanged?.Invoke(this, new CellChangedEventArgs(x, y, _cardtable[x, y]));
        private void OnCardtableChanged() => CardtableChanged?.Invoke(this, EventArgs.Empty);
        private void OnGameOver() => GameOver?.Invoke(this, EventArgs.Empty);

        private void IsPairFoundNow()
        {
            if (NumberOfPicks > 1)
            {
                if (FirstPicksId == SecondPicksId)
                {
                    if (PlayerIdentifier == 1)
                    {
                        _cardtable[FirstPickX, FirstPickY].Owner = 1;
                        _cardtable[SecondPickX, SecondPickY].Owner = 1;
                    }
                    else
                    {
                        _cardtable[FirstPickX, FirstPickY].Owner = 2;
                        _cardtable[SecondPickX, SecondPickY].Owner = 2;
                    }

                    PairsLeft--;
                    OnCardtableChanged();
                }

                PlayerIdentifier = PlayerIdentifier == 1 ? 2 : 1;
                NumberOfPicks -= 2;

                if (PairsLeft == 0)
                {
                    IsGameOver = true;
                    OnGameOver();
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start a parameterless new game, pre defined parameters: 2 width and 3 height
        /// </summary>
        public void NewGame() => NewGame(2, 3);

        /// <summary>
        /// Start a new game with parameters
        /// </summary>
        /// <param name="width">Width of the table</param>
        /// <param name="height">Height of the table</param>
        public void NewGame(int width, int height)
        {
            _cardtable = new CardtableCell[width, height];

            int numberOfACards = (width * height) / 2;
            List<int> cards = new List<int>();
            for (int i = 0; i < numberOfACards; i++)
            {
                cards.Add(i);
                cards.Add(i);
            }
            List<int> shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToList();
            int counter = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _cardtable[i, j] = CardtableCell.CreateCard(shuffledcards[counter]);
                    counter++;
                }
            }

            IsGameOver = false;
            PlayerIdentifier = 1;
            NumberOfPicks = 0;
            FirstPicksId = -3;
            SecondPicksId = -2;
            FirstPickX = -1;
            FirstPickY = -1;
            SecondPickX = -2;
            SecondPickY = -2;
            PairsLeft = (width * height) / 2;
            OnCardtableChanged();
        }

        /// <summary>
        /// Get the selected field of the table
        /// </summary>
        /// <param name="x">Width index of the cell</param>
        /// <param name="y">Height index of the cell</param>
        /// <returns>The selected cell</returns>
        public CardtableCell GetField(int x, int y)
             => _cardtable[x, y];

        /// <summary>
        /// Turn up a card at the selected cell of the table
        /// </summary>
        /// <param name="x">Width index of the cell</param>
        /// <param name="y">Height index of the cell</param>
        public void TurnUpCard(int x, int y)
        {
            if (!IsGameOver)
            {
                if (!_cardtable[x, y].IsTurnedUp)
                {
                    if (NumberOfPicks == 0 && FirstPicksId != -3 && SecondPicksId != -2 && (FirstPicksId != SecondPicksId))
                    {
                        _cardtable[FirstPickX, FirstPickY].TurnBackDown();
                        _cardtable[SecondPickX, SecondPickY].TurnBackDown();
                        OnCardtableChanged();
                    }

                    _cardtable[x, y].TurnUp();
                    NumberOfPicks++;
                    if (NumberOfPicks == 1)
                    {
                        FirstPicksId = _cardtable[x, y].PictureNumber;
                        FirstPickX = x;
                        FirstPickY = y;
                    }
                    else
                    {
                        SecondPicksId = _cardtable[x, y].PictureNumber;
                        SecondPickX = x;
                        SecondPickY = y;
                    }
                    OnCellChanged(x, y);
                }
                IsPairFoundNow();
            }
        }

        

        #endregion
    }
}
