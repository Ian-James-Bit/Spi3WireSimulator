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
        //spliting data into array of booleans to transfer over data line
        byte[] byteArray=new byte[] {package};
        BitArray bitArray=new BitArray (byteArray);
        bool[] data=new bool[bitArray.Count];
        bitArray.CopyTo(data, 0);
        //make a loop to send byte, (first time around drive bit[i]), slave sample it, set serial clock to oppisite, drive next bit[i] 
        for (int i=0;i<8;i++)
        {
            bus.dataWire=data[i];
            bus.serialClockChange=true;
            Thread.Sleep(1000);
            bus.serialClockChange=false;

        }
    }

    //sampling, first bit starts as driven, sample it then serial clock rises, next bit is driven then serial clock falls, now again
    public void ReadData()
    {
        //loop
    }

};