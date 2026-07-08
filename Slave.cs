public class Slave
{    
     //for sensor readings
    private readonly int tempReading=Random.Shared.Next(10, 100);
    private readonly int moistureReading=Random.Shared.Next(0, 5);

    private bool[] data=new bool[8];//to take in read or write command sent from master
    private byte finalData;//stores actual command without r/w bit(msb)
    private int bitCount;//so slave knows which bit its reading.
                        //also used to know if your supposed to be sending something back when the clock keeps going
    private int sendBackBitCount;//to know which bit slave is on when sending back
    private ThreeWireSPI bus; //point to same SPI that master and slave use

    //contructor, "subscribes" two functions to events
    public Slave(ThreeWireSPI bus)
    {
        this.bus = bus;

        this.bus.ChipSelectOn += OnChipSelectOn;
        this.bus.ClockChanged += OnClockChanged;
    }

    //on cs change this is called.resets bit counts if activated bc starting new communication
    private void OnChipSelectOn(bool isChipSelectHigh)
    {
        if (isChipSelectHigh == false)//pulled low
        {
            bitCount=0;
            sendBackBitCount=7;
        }
    }

    //on clock change this is called. Reads in command from master, and if "read" command, send back data
    private void OnClockChanged(bool isClockHigh)
    {
        // The hardware always "sees" the clock, but its brain only acts if Chip Select is currently active pulled low (false)
        if (bus.chipSelect1 == false)
        {
            if(bitCount<8)//check if reading or sending on clock tick
            {
                //if clock is high read in a bit
                if (bus.serialClock==true)
                {
                    data[bitCount]=bus.dataWire;
                    bitCount++;
                    if (bitCount==8)
                    {
                        //change bool arr into byte
                        for (int i = 1; i < data.Length && i < 8; i++)
                        {
                            if (data[i])
                            {
                                finalData |= (byte)(1 << (7-i));
                            }
                        }
                        //call read(0/false) or write(1/true) depending on what first bit is
                        if (data[0]==false)
                        {
                            finalData=readingTo();//data request
                        }
                        else
                        {
                            writtenTo();//command
                        }
                    }
                }
            }
            else//send data back
            {    
                if(bus.serialClockChange==false)
                {
                    bus.dataWire = (finalData & (1 << sendBackBitCount)) != 0;
                    sendBackBitCount--;
                }
            }
        }
    }
    
    //were being written to, look at command and do corresponding action, no hardware but just pretend
    public void writtenTo()
    {
        if (finalData==0x70)
        {
            Console.WriteLine("SLAVE: Reseting important hardware.");
        }
        else if (finalData==0x71)
        {
            Console.WriteLine("SLAVE: Calibrating important hardware.");
        }
    }

    //loads up the data requested from master
    public byte readingTo()
    {
        byte package=new byte();
        if (finalData==0x70)//use temp sensor data
        {
            package = (byte)tempReading;  
        }
        else if(finalData==0x71)//use humidity sensor data
        {
            package = (byte)moistureReading; 
        }
        Console.WriteLine("SLAVE: Data Sending: "+package);
        return package;
    }
}