using System;
using System.Collections.ObjectModel;
using System.Linq;
using CardPair.Model;

namespace CardPair.ViewModel
{
    public class CardPairViewModel : BindingSource
    {
        #region Fields

        private CardPairModel _model;

        private CardtableCellViewModel[][] _cardtable;

        private DelegateCommand _cellTurnCommand;

        #endregion

        #region Properties

        public CardtableCellViewModel[][] Cardtable
        {
            get => _cardtable;
            private set
            {
                if (value != _cardtable)
                {
                    _cardtable = value;
                    OnPropertyChanged();
                }
            }
        }


        public DelegateCommand NewGameCommand { get; private set; }

        #endregion


        #region Constructors

        public CardPairViewModel(CardPairModel model)
        {
            _model = model ?? throw new ArgumentNullException("model");

            _model.CellChanged += _model_CellChanged;
            _model.CardtableChanged += _model_CardtableChanged;
            _model.GameOver += _model_GameOver;

            NewGameCommand = new DelegateCommand(Command_NewGame);
            _cellTurnCommand = new DelegateCommand(Command_Cell_Turn);

            _model_CardtableChanged(this, EventArgs.Empty);
        }

        #endregion

        #region Command Methods

        private void Command_NewGame(object param)
        {
            if (param != null && param is string difficulty)
            {
                switch (difficulty)
                {
                    case "easy": _model.NewGame(2, 3); break;
                    case "medium": _model.NewGame(3, 4); break;
                    case "hard": _model.NewGame(4, 4); break;
                    default: _model.NewGame(); break;
                }
            }
            else _model.NewGame();
        }


        private void Command_Cell_Turn(object param)
        {
            if (param != null && param is CardtableCellViewModel cell)
                _model.TurnUpCard(cell.X, cell.Y);
        }


        #endregion

        #region Model Event Handlers

        private void _model_CellChanged(object sender, CellChangedEventArgs e)
        {
            if (e.X < Cardtable.Length && e.Y < Cardtable[e.X].Length)
            {
                Cardtable[e.X][e.Y].Update(e.X, e.Y, e.Cell, _model.IsGameOver);
            }
            else _model_CardtableChanged(this, EventArgs.Empty);
        }
        private void _model_CardtableChanged(object sender, EventArgs e)
        {
            CardtableCellViewModel[][] cardtable = new CardtableCellViewModel[_model.Width][];

            for (int i = 0; i < _model.Width; i++)
            {
                cardtable[i] = new CardtableCellViewModel[_model.Height];
                for (int j = 0; j < _model.Height; j++)
                    cardtable[i][j] = new CardtableCellViewModel(i, j, _model.GetField(i, j), _model.IsGameOver, _cellTurnCommand);
            }

            Cardtable = cardtable;
        }
        private void _model_GameOver(object sender, EventArgs e)
        {
            for (int i = 0; i < _model.Width; i++)
                for (int j = 0; j < _model.Height; j++)
                    Cardtable[i][j].Update(i, j, _model.GetField(i, j), _model.IsGameOver);
        }

        #endregion
    }
}
