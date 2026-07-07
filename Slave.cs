using System.Runtime.CompilerServices;
public class Slave
{
    private bool[] data=new bool[8];
    
     //for sensor readings
    private int tempReading=Random.Shared.Next(10, 100);
    private int moistureReading=Random.Shared.Next(0, 5);

    //private byte data,data read from the master without read or write first bit. the rest is there and is either adress of what data master wants or the command for what master wants slave to do, eg. reset or calibrate
    private byte finalData;
    private int bitCount;//so slave knows which bit reading since event function is called each time clock half tick
    private int sendBackBitCount;
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
        if (isChipSelectHigh == false)//pulled low
        {
            bitCount=0;
            sendBackBitCount=7;
        }
    }

    private void OnClockChanged(bool isClockHigh)
    {
        // The hardware always "sees" the clock, but its brain only acts 
        // if Chip Select is currently active pulled low (false)
        if (bus.chipSelect1 == false)
        {
            if(bitCount<8)
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
                        Console.WriteLine(finalData);
                        //call read(0/false) or write(1/true) depending on what first bit is
                        if (data[0]==false)//change this
                        {
                            Console.WriteLine("1");
                            finalData=readingTo();//data request
                        }
                        else
                        {
                            writtenTo();//command
                        }
                    }
                }
            }
            else
            {    
                if(bus.serialClockChange==false)
                {
                    bus.dataWire = (finalData & (1 << sendBackBitCount)) != 0;
                    sendBackBitCount--;
                }
            }
        }
    }
    
    //were being written to, look at command and do corresponding action
    public void writtenTo()
    {
        if (finalData==0x70)//command == 112 then
        {
            Console.WriteLine("Reseting important hardware.");//no hardware but just pretend
        }
        else if (finalData==0x71)
        {
            Console.WriteLine("Calibrating important hardware.");//no hardware but just pretend
        }
    }

    //slave is reading command from master, such as calibration or reset
    public byte readingTo()
    {
        Console.WriteLine("reading called");
        byte package=new byte();
        if (finalData==0x70)//use temp sensor data
        {
            Console.WriteLine("temp reading is :"+tempReading);
            package = (byte)tempReading;  
        }
        else if(finalData==0x71)//use humidity sensor data
        {
            package = (byte)moistureReading; 
        }
        return package;
    }

    
    
}