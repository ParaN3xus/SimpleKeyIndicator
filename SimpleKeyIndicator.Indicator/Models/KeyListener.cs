using System.Collections.Generic;
using SharpHook;
using SharpHook.Native;

namespace SimpleKeyIndicator.Indicator.Models;

public class KeyListener
{
    public delegate void KeyChangedNotify();
    
    private readonly TaskPoolGlobalHook _globalHook;
    private readonly Dictionary<KeyCode, KeyChangedNotify> _keyboardNotifies = new();
    private readonly Dictionary<MouseButton, KeyChangedNotify> _mouseNotifies = new();

    public KeyListener()
    {
        _globalHook = new TaskPoolGlobalHook();

        _globalHook.KeyPressed += OnKeyChanged;
        _globalHook.KeyReleased += OnKeyChanged;
        _globalHook.MousePressed += OnKeyChanged;
        _globalHook.MouseReleased += OnKeyChanged;
    }

    public void Run()
    {
        _globalHook.RunAsync();
    }

    public void BindEvent(KeyCode keyCode, KeyChangedNotify func)
    {
        _keyboardNotifies[keyCode] = func;
    }

    public void BindEvent(MouseButton mouseButton, KeyChangedNotify func)
    {
        _mouseNotifies[mouseButton] = func;
    }

    private void OnKeyChanged(object? sender, object e)
    {
        switch (e)
        {
            case KeyboardHookEventArgs kArgs:
            {
                if (_keyboardNotifies.TryGetValue(kArgs.Data.KeyCode, out var notify))
                {
                    notify();
                }
                
                break;
            }
            case MouseHookEventArgs mArgs:
            {
                if (_mouseNotifies.TryGetValue(mArgs.Data.Button, out var notify))
                {
                    notify();
                }
                
                break;
            }
        }
    }
}