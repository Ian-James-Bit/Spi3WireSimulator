public class Master
{
    private ThreeWireSPI bus; //point to same SPI that master and slave use
    
    //contructor
    public Master(ThreeWireSPI bus)
    {
        this.bus = bus;
    }

    //used by the master to select slave and de(activate) chip select, choice represents which slave if there were multiple
    public void ChangeChipSelectState(int choice)
    {
        if (choice == 1)
        {
            bus.chipSelect1On=!bus.chipSelect1;//active low
        }
    }

    //sends a "read" or "write" command to the slave, and if "read" calls readData function to process data and prints it
    public void WriteData(byte package){
        //sending package to slave
        for (int i=7;i>=0;i--)
        {
            bus.dataWire = (package & (1 << i)) != 0;
            bus.serialClockChange=true;
            bus.serialClockChange=false;
        }
        if ((package & 0x80) == 0)//if read command
        {
            Console.WriteLine("MASTER: Data Recieved: "+ReadData());
        }
    }

    //reading data in from slave and returning it as byte
    public byte ReadData()
    {
        bool[] data=new bool[8];
        byte finalData=new byte();
        for(int i=0;i<8;i++){
            bus.serialClockChange=true;
            data[i]=bus.dataWire;
            bus.serialClockChange=false;
        }
        //convert into byte
        for (int i = 0; i < data.Length && i < 8; i++)
        {
            if (data[i])
            {
            finalData |= (byte)(1 << (7 - i));
            }
        }
        return finalData;
    }

};