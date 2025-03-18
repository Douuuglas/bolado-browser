using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Bolado.Browser.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _url = "https://www.duckduckgo.com";

        [RelayCommand]
        private void Go()
        {
            if (string.IsNullOrEmpty(Url))
                return;

            if (!Url!.StartsWith("https://"))
            {
                Url = $"https://{Url!.Trim()}";
            }
        }
    }
}
