using System;
using System.Collections;
public class Master
{
    private ThreeWireSPI bus; //point to same SPI that master and slave use
    
    //private byte dataIn;//stores data master recieves sent from slave

    //contructor
    public Master(ThreeWireSPI bus)
    {
        this.bus = bus;
    }

    //used by the master to select slave, choice represents which slave if there were mulitple
    public void ActivateChipSelect(int choice)
    {
        if (choice == 1)
        {
            bus.chipSelect1On=false;//active low
        }
    }

    //driving, drive first bit, get sampled by slave on rising edge, set serial clock to true(half tick), drive next bit, set serial clokc to false(half tick), now again
    public void WriteData(byte package){
        //make a loop to send byte, (first time around drive bit[i]), slave sample it, set serial clock to oppisite, drive next bit[i] 
        for (int i=7;i>=0;i--)
        {
            //doing bitwise math to send data
            bus.dataWire = (package & (1 << i)) != 0;
            bus.serialClockChange=true;
            Thread.Sleep(200);
            bus.serialClockChange=false;
        }
        if ((package & 0x80) != 0)//just a write function so nothing to read and turn chip select off
        {
            bus.chipSelect1=false;
        }
        else//a read function, so read what slave sent back 
        {
            Console.WriteLine("sucess: "+ReadData());
        }
        //return cs to high if done, if we wrote, or if we read leave it on turn around and call readData
    }

    //sampling, first bit starts as driven, sample it then serial clock rises, next bit is driven then serial clock falls, now again
    public byte ReadData()
    {
        bool[] data=new bool[8];
        byte finalData=new byte();
        //read the data in
        for(int i=0;i<8;i++){
            //run the clock so the slave can send
            bus.serialClockChange=true;
            data[i]=bus.dataWire;
            Thread.Sleep(200);
            bus.serialClockChange=false;
        }
        //convert into byte
        for (int i = 0; i < data.Length && i < 8; i++)
        {
            if (data[i])
            {
            // i=0 shifts 7 times (MSB), i=7 shifts 0 times (LSB)
            finalData |= (byte)(1 << (7 - i));
            }
        }
        return finalData;
    }

};