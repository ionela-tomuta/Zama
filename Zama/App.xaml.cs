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
>>>>>>> f102f00ce96dbe2ce991080af9bd591939b460c1
