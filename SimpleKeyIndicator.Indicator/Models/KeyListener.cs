using System;
using System.Collections.Generic;
using SharpHook;
using SharpHook.Native;

namespace SimpleKeyIndicator.Indicator.Models;

public class KeyListener
{
    private readonly TaskPoolGlobalHook _globalHook;
    private readonly Dictionary<KeyCode, EventHandler> _keyboardNotifies = new();
    private readonly Dictionary<MouseButton, EventHandler> _mouseNotifies = new();

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

    public void BindEvent(KeyCode keyCode, EventHandler func)
    {
        _keyboardNotifies[keyCode] = func;
    }

    public void BindEvent(MouseButton mouseButton, EventHandler func)
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
                    notify(sender, kArgs);
                }
                
                break;
            }
            case MouseHookEventArgs mArgs:
            {
                if (_mouseNotifies.TryGetValue(mArgs.Data.Button, out var notify))
                {
                    notify(sender, mArgs);
                }
                
                break;
            }
        }
    }
}