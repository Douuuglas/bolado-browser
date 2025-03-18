using Bolado.Browser.ViewModels;
using System.Windows;

namespace Bolado.Browser;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowViewModel ViewModel = new();

    public MainWindow()
    {
        DataContext = ViewModel;
        InitializeComponent();
    }

    private void ButtonGo_Click(object sender, RoutedEventArgs e)
    {
        webView.CoreWebView2.Navigate(ViewModel.Url);
    }
}