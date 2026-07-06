using System.ComponentModel;
using System.Runtime.CompilerServices;
public class ThreeWireSPI{
    public bool chipSelect1=true; //active low meaning if true not active, if false(low) active

    public bool dataWire=false;//dataLow 0(false), dataHigh 1(true) 0 or 1 data is bits, if nothing is being sent data wire just floats in one of these states

    //Mode 0: CPOL = 0, CPHA = 0 (Clock idles low, samples on rising edge)
    public bool serialClock=false;//can either be down(0==false) or up(1==true), down+up=1tick, the clock idles at logic Low (0==false). The first edge is Rising, and the second is Falling.

    // The electrical wave events
    public event Action<bool>? ChipSelectOn;
    public event Action<bool>? ClockChanged;

    //simulating changing voltage of chip select wire, pulling low
    public bool chipSelect1On
    {
        get => chipSelect1;
        set
        {
            if (chipSelect1 != value)
            {
                chipSelect1 = value;
                ChipSelectOn?.Invoke(chipSelect1); // Broadcast CS change
            }
        }
    }

    public bool serialClockChange
    {
        get => serialClock;
        set
        {
            if (serialClock != value)
            {
                serialClock = value;
                ClockChanged?.Invoke(serialClock); // Broadcast Clock change
            }
        }
    }

}