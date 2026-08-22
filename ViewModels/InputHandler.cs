using System;
using System.Collections.Generic;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Input;

namespace SynthInstrumentModellerMVVM.ViewModels;

public partial class InputHandler
{
    private readonly HashSet<Key> _pressedKeys = new();
    
    [RelayCommand] 
    public void OnKeyDown(KeyEventArgs e)
    {
        bool hasBeenPressed = _pressedKeys.Add(e.Key);
        if (!hasBeenPressed) return;
        
        MainViewModel.PlayNoteBIND(e);
    }
    
    [RelayCommand] 
    public void OnKeyUp(KeyEventArgs e)
    {
        _pressedKeys.Remove(e.Key);
        
        MainViewModel.StopNoteBIND(e);
    }
}