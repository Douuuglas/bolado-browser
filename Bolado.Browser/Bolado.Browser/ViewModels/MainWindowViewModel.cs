using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Bolado.Browser.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{

    public event Action? BackEvent;
    public event Action? ForwardEvent;
    public event Action? ReloadEvent;
    public event Action? CancelEvent;

    private readonly Subject<string> _goEventSubject = new();
    public IObservable<string> GoEvent => _goEventSubject.AsObservable();

    [RelayCommand]
    private void Back()
    {
        BackEvent?.Invoke();
    }

    [RelayCommand]
    private void Forward()
    {
        ForwardEvent?.Invoke();
    }

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
        var validUrl = string.Empty;

        if (string.IsNullOrEmpty(url))
            return;

        if (!url.StartsWith("https://"))
        {
            validUrl = $"https://{url!.Trim()}";
        }

        _goEventSubject.OnNext(validUrl);
    }
}
