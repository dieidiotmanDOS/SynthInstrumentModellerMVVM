using System;

namespace SynthInstrumentModellerMVVM.Models;

public class WaveGenerator
{
    private const int AudioSampleRate = 48000;
    private double currentPhase;

    private double _frequency {get; set;} = 440.0;   
    
    
}