using System.Globalization;

namespace App_Compras_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = new Window(new AppShell());

            window.Height = 600;
            window.Width = 350;

            return window;
        }
    }
}