public class ThreeWireSPI{
    public bool chipSelect1=true; //represents cs wire

    public bool dataWire=false;//represents data wire

    //Mode 0: CPOL = 0, CPHA = 0 (Clock idles low, samples on rising edge)
    //can either be down(0==false) or up(1==true), down+up=1tick, the clock idles at logic Low (0==false). The first edge is Rising, and the second is Falling.
    public bool serialClock=false;//represents serial clock wire

    //event stuff
    public event Action<bool>? ChipSelectOn;
    public event Action<bool>? ClockChanged;

    //called when changing cs or clock to trigger event for slave
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