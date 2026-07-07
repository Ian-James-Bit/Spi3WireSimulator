public class Slave
{
    private bool[] data=new bool[8];
    //private byte data;//data read from the master that contains read or write in first bit and the rest is either adress of what data master wants or the command for what master wants slave to do, eg. reset or calibrate
    private int bitCount;//so slave knows which bit reading since event function is called each time clock half tick
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

    //resets bit count bc new communication
    private void OnChipSelectOn(bool isChipSelectHigh)
    {
        Console.WriteLine("Chip select called");
        if (isChipSelectHigh == false)//pulled low
        {
            data=new bool[8];
            bitCount=0;
        }
    }

    private void OnClockChanged(bool isClockHigh)
    {
        Console.WriteLine("on clock change called");
        // The hardware always "sees" the clock, but its brain only acts 
        // if Chip Select is currently active pulled low (false)
        if (bus.chipSelect1 == false)
        {
            //if clock is high read in a bit
            if (bus.serialClock==true)
            {
                data[bitCount]=bus.dataWire;
                //for testing
                Console.WriteLine(data[bitCount]);
                bitCount++;
                if (bitCount==8)
                {
                    Console.WriteLine("Now we call read or write everything has been succesful.");
                    //call read or write depending on what first bit is
                }
            }
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