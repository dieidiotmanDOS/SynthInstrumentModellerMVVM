namespace SynthInstrumentModellerMVVM.Models;

public class Tone()
{
    public double Frequency {get; set;} = 0;

    // public double[] Harmonics {get; set;}

    public double Phase { get; set; } = 0;
    
    public bool IsActive { get; set; } = false;
    
    
}