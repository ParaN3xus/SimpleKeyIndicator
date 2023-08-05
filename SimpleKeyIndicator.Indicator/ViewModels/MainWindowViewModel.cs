using Avalonia;
using Avalonia.Controls;
using SharpHook.Native;
using SimpleKeyIndicator.Indicator.Controls;
using SimpleKeyIndicator.Indicator.Views;

namespace SimpleKeyIndicator.Indicator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public Control[] Keys => new Control[]
    {
        new KeyIndicator(MainWindow.MainListener,KeyCode.VcW)
        {
            Width = 50,
            Height = 50,
            Margin = new Thickness(50,50),
        }
    };

}