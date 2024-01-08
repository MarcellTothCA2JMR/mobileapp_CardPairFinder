using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CardPair
{
    public partial class App : Application
    {
        #region Fields

        private Model.CardPairModel _model;
        private ViewModel.CardPairViewModel _viewModel;

        #endregion

        #region Constructors

        public App()
        {
            InitializeComponent();

            _model = new Model.CardPairModel();
            _viewModel = new ViewModel.CardPairViewModel(_model);

            MainPage = new MainPage();
            MainPage.BindingContext = _viewModel;
        }

        #endregion

        #region Protected Methods
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion
    }
}
