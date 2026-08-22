using System;
using NAudio.Wave;
using Tone = SynthInstrumentModellerMVVM.Models.Tone;

namespace SynthInstrumentModellerMVVM.ViewModels;

public class PrimaryAudioBuffer() : WaveProvider32(AudioSampleRate, 1)
{
    private const int MaxPolyphony = 12;
    private static Tone[] Tones = new Tone[MaxPolyphony];
    
    
    private const int AudioSampleRate = 48000;

    static PrimaryAudioBuffer() // Construct the allocated array of tones.
    {
        for (int i = 0; i < MaxPolyphony; i++)
        {
            Tones[i] = new Tone();
        }
    }
    
    public static void ActivateTone(double freq)
    {
        foreach (var tone in Tones)
        {
            if (!tone.IsActive)
            {
                tone.Frequency = freq;
                tone.Phase = 0.0;
                tone.IsActive = true;
                break;
            }
        }
        Console.WriteLine("Activated Tone");
    }
    
    public static void DeactiveTone(double freq)
    {
        foreach (var tone in Tones)
        {
            if (tone.IsActive && tone.Frequency.Equals(freq))
            {
                tone.IsActive = false;
            }
        }
        
        Console.WriteLine("Deactivated Tone");
    }
    
    public override int Read(Span<float> buffer)
    {
        buffer.Clear();
        
        foreach (var tone in Tones)
        {
            if (tone.IsActive) 
                bufferGenerateNewWave(buffer, tone);
            
        }

        return buffer.Length;
    }
    
    public void bufferGenerateNewWave(Span<float> buffer, Tone tone)
    {   
        double phase = tone.Phase;
        
        
        for (int i = 0; i < buffer.Length; i++)
        {
            buffer[i] += (float) Math.Sin(phase * 2 * Math.PI) + 0.5f * (float)  Math.Sin(phase * 4 * Math.PI) + 0.25f * (float) Math.Sin(phase * 8 * Math.PI); // Sine Waveform.
            
            phase += tone.Frequency / AudioSampleRate;
            
            if (phase > 1) phase -= 1; // Clamp to avoid going above 1.
        }
        
        tone.Phase = phase;

    }
}