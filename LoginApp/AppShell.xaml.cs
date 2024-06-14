namespace LoginApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("home", typeof(Pages.HomePage));
            Routing.RegisterRoute("procurar", typeof(Pages.PesquisaPage));
            Routing.RegisterRoute("conta", typeof(Pages.ContaPage));
        }
    }
}
