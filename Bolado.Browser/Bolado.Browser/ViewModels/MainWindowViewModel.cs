using Bolado.Browser.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Web;

namespace Bolado.Browser.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    public event Action? BackEvent;
    public event Action? ForwardEvent;
    public event Action? ReloadEvent;
    public event Action? CancelEvent;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(BackCommand), nameof(ForwardCommand))]
    private int _pageIndex = -1;

    private readonly List<string> _pageHistory = [];
    private readonly Subject<string> _goEventSubject = new();

    public IObservable<string> GoEvent => _goEventSubject.AsObservable();

    [RelayCommand(CanExecute = nameof(CanExecuteBack))]
    private void Back()
    {
        PageIndex--;
        BackEvent?.Invoke();
    }

    private bool CanExecuteBack() => PageIndex > 0;

    [RelayCommand(CanExecute = nameof(CanExecuteForward))]
    private void Forward()
    {
        PageIndex++;
        ForwardEvent?.Invoke();
    }

    private bool CanExecuteForward() => PageIndex > -1 && PageIndex < _pageHistory.Count - 1;

    [RelayCommand]
    private void Reload()
    {
        ReloadEvent?.Invoke();
    }

    [RelayCommand]
    private void Cancel()
    {
        CancelEvent?.Invoke();
    }

    [RelayCommand]
    private void Go(string url)
    {
        if (url.IsValidUrl())
        {
            var validUrl = url.FormatUrl();
            _goEventSubject.OnNext(validUrl);
            _pageHistory.Add(validUrl);
        }
        else
        {
            var validUrl = $"https://duckduckgo.com/?q={HttpUtility.UrlEncode(url)}";
            _goEventSubject.OnNext(validUrl);
            _pageHistory.Add(validUrl);
        }

        PageIndex++;
    }
}
