using Bolado.Browser.ViewModels;
using System.Reactive.Linq;
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
        Loaded += MainWindow_Loaded;
        InitializeComponent();
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        webView.CoreWebView2InitializationCompleted += (s, e) =>
        {
            ViewModel.BackEvent += () =>
            {
                if (webView.CoreWebView2.CanGoBack)
                {
                    webView.CoreWebView2.GoBack();
                }
            };

            ViewModel.ForwardEvent += () =>
            {
                if (webView.CoreWebView2.CanGoForward)
                {
                    webView.CoreWebView2.GoForward();
                }
            };

            ViewModel.ReloadEvent += () =>
            {
                webView.CoreWebView2.Reload();
            };

            ViewModel.CancelEvent += () =>
            {
                webView.CoreWebView2.Stop();
            };

            ViewModel.GoEvent.Subscribe(url => webView.CoreWebView2.Navigate(url));
        };

        await webView.EnsureCoreWebView2Async();
    }
}