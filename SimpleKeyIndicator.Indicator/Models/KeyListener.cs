using System;
using System.Collections.Generic;
using Avalonia.Threading;
using SharpHook;
using SharpHook.Native;

namespace SimpleKeyIndicator.Indicator.Models;

public class KeyListener
{
    private readonly TaskPoolGlobalHook _globalHook;
    private readonly Dictionary<KeyCode, EventHandler> _keyboardPressedNotifies = new();
    private readonly Dictionary<KeyCode, EventHandler> _keyboardReleasedNotifies = new();
    private readonly Dictionary<MouseButton, EventHandler> _mousePressedNotifies = new();
    private readonly Dictionary<MouseButton, EventHandler> _mouseReleasedNotifies = new();

    public KeyListener()
    {
        _globalHook = new TaskPoolGlobalHook();

        _globalHook.KeyPressed += OnKeyPressed;
        _globalHook.KeyReleased += OnKeyReleased;
        _globalHook.MousePressed += OnKeyPressed;
        _globalHook.MouseReleased += OnKeyReleased;
    }

    public void Run()
    {
        _globalHook.RunAsync();
    }

    public void BindEvent(KeyCode keyCode, EventHandler pressedFunc, EventHandler releasedFunc)
    {
        _keyboardPressedNotifies[keyCode] = pressedFunc;
        _keyboardReleasedNotifies[keyCode] = releasedFunc;
    }

    public void BindEvent(MouseButton mouseButton, EventHandler pressedFunc, EventHandler releasedFunc)
    {
        _mousePressedNotifies[mouseButton] = pressedFunc;
        _mouseReleasedNotifies[mouseButton] = releasedFunc;
    }

    private void OnKeyPressed(object? sender, object e)
    {
        switch (e)
        {
            case KeyboardHookEventArgs kArgs:
            {
                if (_keyboardPressedNotifies.TryGetValue(kArgs.Data.KeyCode, out var notify))
                {
                    Dispatcher.UIThread.InvokeAsync(() => { notify(sender, kArgs); });
                }
                
                break;
            }
            case MouseHookEventArgs mArgs:
            {
                if (_mousePressedNotifies.TryGetValue(mArgs.Data.Button, out var notify))
                {
                    Dispatcher.UIThread.InvokeAsync(() => { notify(sender, mArgs); });
                }
                
                break;
            }
        }
    }
    
    private void OnKeyReleased(object? sender, object e)
    {
        switch (e)
        {
            case KeyboardHookEventArgs kArgs:
            {
                if (_keyboardReleasedNotifies.TryGetValue(kArgs.Data.KeyCode, out var notify))
                {
                    Dispatcher.UIThread.InvokeAsync(() => { notify(sender, kArgs); });
                }
                
                break;
            }
            case MouseHookEventArgs mArgs:
            {
                if (_mouseReleasedNotifies.TryGetValue(mArgs.Data.Button, out var notify))
                {
                    Dispatcher.UIThread.InvokeAsync(() => { notify(sender, mArgs); });
                }
                
                break;
            }
        }
    }
}