using System ;
using System.Collections.Generic;
using Avalonia.Remote.Protocol.Input;

namespace SynthInstrumentModellerMVVM.Models;

public class Scale
{
    public int numberOfKeys {get; private set;}
    public double[] _keyFrequencies {get; private set; }
    
    public Scale(double rootFreq, int numKeys)
    {
        numberOfKeys = numKeys;
        
        _keyFrequencies = new double[numKeys];
        
        for (int i = 0; i < numKeys; i++)
        {
            _keyFrequencies[i] = rootFreq * Math.Pow(2.0,  i / (double) numKeys);
        }
    }
}