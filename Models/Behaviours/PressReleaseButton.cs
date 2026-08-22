using System.Windows.Input;
using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia;

namespace SynthInstrumentModellerMVVM.Models.Behaviours;

public class PressReleaseButton : Button
{
    protected override Type StyleKeyOverride => typeof(Button);
    
    public static readonly StyledProperty<ICommand?> PressCommandProperty =
        AvaloniaProperty.Register<PressReleaseButton, ICommand?>(nameof(PressCommand));

    public static readonly StyledProperty<ICommand?> ReleaseCommandProperty =
        AvaloniaProperty.Register<PressReleaseButton, ICommand?>(nameof(ReleaseCommand));

    public ICommand? PressCommand
    {
        get => GetValue(PressCommandProperty);
        set => SetValue(PressCommandProperty, value);
    }

    public ICommand? ReleaseCommand
    {
        get => GetValue(ReleaseCommandProperty);
        set => SetValue(ReleaseCommandProperty, value);
    }

    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        base.OnPointerPressed(e);
        
        if (PressCommand?.CanExecute(CommandParameter) == true)
            PressCommand.Execute(CommandParameter);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
        if (ReleaseCommand?.CanExecute(CommandParameter) == true)
            ReleaseCommand.Execute(CommandParameter);
    }
}