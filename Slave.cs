public class Slave
{
    private byte data;//data read from the master that contains read or write in first bit and the rest is either adress of what data master wants or the command for what master wants slave to do, eg. reset or calibrate
    private int bitCount;//so slave knows which bit reading 
    private ThreeWireSPI bus; //point to same SPI that master and slave use

    //contructor
    public Slave(ThreeWireSPI bus)
    {
        this.bus = bus;

        this.bus.ChipSelectOn += OnChipSelectOn;
        this.bus.ClockChanged += OnClockChanged;
    }

    //when cs1==false, clock will also start, which activates these two functions, cs func resets data , on clock changes starts reading bits...
    //into byte data then determines if read or write, 
    private void OnChipSelectOn(bool isChipSelectHigh)
    {
        if (isChipSelectHigh == false)
        {
            //need to reset bit count
        }
    }

    private void OnClockChanged(bool isClockHigh)
    {
        // The hardware always "sees" the clock, but its brain only acts 
        // if Chip Select is currently active (false)
        if (bus.chipSelect1 == false)
        {
            //if clock is high read in a bit
        }
    }
    
    //slave is writting data from adress recieved to the master
    public void write()
    {
        
    }

    //slave is reading command from master, such as calibration or reset
    public void read()
    {
        
    }

    
    
}