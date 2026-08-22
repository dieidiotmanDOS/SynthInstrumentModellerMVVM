using System.Collections.Generic;
using System.Collections.ObjectModel;
using SynthInstrumentModellerMVVM.Models;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NAudio.Wave;
using Avalonia.Input;

namespace SynthInstrumentModellerMVVM.ViewModels;

public class KeyboardTemplateVM 
{
    public string Label { get; set; } = "Key";
    public double Frequency { get; set; } = 440.0;

    public ICommand? PressCommand { get; set; }
    public ICommand? ReleaseCommand { get; set; }
}


public partial class MainViewModel : ViewModelBase
{
    public InputHandler _InputHandler { get; } = new InputHandler();

    private static readonly Dictionary<Key, int> KeyToNote = new()
    {
        // Numbers
        { Key.NumPad0, 0 }, { Key.NumPad1, 1 }, { Key.NumPad2, 2 }, { Key.NumPad3, 3 }, { Key.NumPad4, 4 },
        { Key.NumPad5, 5 }, { Key.NumPad6, 6 }, { Key.NumPad7, 7 }, { Key.NumPad8, 8 }, { Key.NumPad9, 9 },
        
        // Letters
        { Key.D1, 0 }, { Key.D2, 1 }, { Key.D3, 2 }, { Key.D4, 3 }, { Key.D5, 4 },
        { Key.D6, 5 }, { Key.D7, 6 }, { Key.D8, 7 }, { Key.D9, 8 }, { Key.D0, 9 },

        { Key.Q, 10 }, { Key.W, 11 }, { Key.E, 12 }, { Key.R, 13 }, { Key.T, 14 },
        { Key.Y, 15 }, { Key.U, 16 }, { Key.I, 17 }, { Key.O, 18 }, { Key.P, 19 },

        { Key.A, 20 }, { Key.S, 21 }, { Key.D, 22 }, { Key.F, 23 }, { Key.G, 24 },
        { Key.H, 25 }, { Key.J, 26 }, { Key.K, 27 }, { Key.L, 28 },
        
        { Key.Z, 29 }, { Key.X, 30 }, { Key.C, 31 }, { Key.V, 32 }, { Key.B, 33 },
        { Key.N, 34 }, { Key.M, 35 },
    };
    private static readonly Scale _WesternScale = new Scale(rootFreq: 220, numKeys: 13);
    
    private readonly WasapiPlayer _WasapiPlayer = new WasapiPlayerBuilder()
        .WithLowLatency()
        .Build();
    
    private readonly PrimaryAudioBuffer _AudioBuffer = new PrimaryAudioBuffer();
    
    public ObservableCollection<KeyboardTemplateVM> KeyboardKeys { get; } = new();
    
    public MainViewModel()
    {
        _WasapiPlayer.Init(_AudioBuffer);
        _WasapiPlayer.Play();

        for (int i = 0; i < _WesternScale.numberOfKeys; i++)
        {
            KeyboardKeys.Add(new KeyboardTemplateVM
            {
                Label = $"Note{i}",
                Frequency = _WesternScale._keyFrequencies[i],
                PressCommand = new RelayCommand<double>(PlayNoteBUTTON),
                ReleaseCommand = new RelayCommand<double>(StopNoteBUTTON)
            });
        }
    }
    
    public static void PlayNoteBIND(KeyEventArgs e)
    {
        if (KeyToNote.TryGetValue(e.Key, out int noteIndex))
        {
            if (noteIndex < _WesternScale.numberOfKeys)
            {
                PlayNoteBUTTON(_WesternScale._keyFrequencies[noteIndex]);
            }
        }
    }

    public static void StopNoteBIND(KeyEventArgs e)
    {
        if (KeyToNote.TryGetValue(e.Key, out int noteIndex))
        {
            if (noteIndex < _WesternScale.numberOfKeys)
            {
                StopNoteBUTTON(_WesternScale._keyFrequencies[noteIndex]);
            }
        }
    }

    private static void PlayNoteBUTTON(double freq)
    {
        PrimaryAudioBuffer.ActivateTone(freq);
    }
    
    private static void StopNoteBUTTON(double freq)
    {
        PrimaryAudioBuffer.DeactiveTone(freq);
    }
}