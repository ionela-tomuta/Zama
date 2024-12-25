<<<<<<< HEAD
﻿namespace Zama;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
        Shell.Current.GoToAsync("///login");
    }
}
=======
﻿namespace Zama
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}