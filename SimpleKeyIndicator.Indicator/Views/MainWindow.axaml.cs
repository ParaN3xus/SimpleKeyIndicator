using Avalonia.Controls;
using Avalonia.Interactivity;
using SimpleKeyIndicator.Indicator.Models;

namespace SimpleKeyIndicator.Indicator.Views;

public partial class MainWindow : Window
{
    public static KeyListener MainListener = new();
    
    public MainWindow()
    {
        InitializeComponent();
        
        MainListener.Run();
    }
}