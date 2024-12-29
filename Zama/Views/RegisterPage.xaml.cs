using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zama.Data;
using Zama.Models;
using Zama.ViewModels;

namespace Zama.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegisterViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}