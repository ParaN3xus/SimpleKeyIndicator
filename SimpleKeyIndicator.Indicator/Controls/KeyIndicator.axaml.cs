using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using SharpHook.Native;
using SimpleKeyIndicator.Indicator.Models;

namespace SimpleKeyIndicator.Indicator.Controls;

public partial class KeyIndicator : UserControl
{
    public static readonly StyledProperty<KeyListener> ListenerProperty =
        AvaloniaProperty.Register<KeyIndicator, KeyListener>(nameof(Listener));

    public KeyListener Listener
    {
        get => GetValue(ListenerProperty);
        init => SetValue(ListenerProperty, value);
    }
    
    private object _keyBind;

    public object KeyBind
    {
        get => _keyBind;
        set
        {
            _keyBind = value;
            if (value is KeyCode keyCode)
            {
                Listener.BindEvent(keyCode, KeyPressed, KeyReleased);
            }
            else if (value is MouseButton mouseButton)
            {
                Listener.BindEvent(mouseButton, KeyPressed, KeyReleased);
            }
        }
    }

    public KeyIndicator(KeyListener listener ,object keyBind)
    {
        Listener = listener;
        
        _keyBind = keyBind;
        if (keyBind is KeyCode keyCode)
        {
            Listener.BindEvent(keyCode, KeyPressed, KeyReleased);
        }
        else if (keyBind is MouseButton mouseButton)
        {
            Listener.BindEvent(mouseButton, KeyPressed, KeyReleased);
        }
        InitializeComponent(true);
    }

    private void KeyPressed(object? sender, EventArgs e)
    {
        var rect = Content as Rectangle;
        // ReSharper disable once RedundantArgumentDefaultValue
        Rect1.Fill = new SolidColorBrush(Colors.Black, 1D);
    }

    private void KeyReleased(object? sender, EventArgs e)
    {
        Rect1.Fill = new SolidColorBrush(Colors.Black, 0D);
    }
    
}