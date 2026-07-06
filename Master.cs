public class Master
{
    private ThreeWireSPI bus; //point to same SPI that master and slave use

    private byte dataIn;//stores data master recieves sent from slave

    //contructor
    public Master(ThreeWireSPI bus)
    {
        //this.bus = bus;
    }

    //used by the master to select slave, choice represents which slave if there were mulitple
    public void activateChipSelect(int choice)
    {
        //if choice == 1 then make chipselect1 active and wake up that corresponding slave
    }

    //driving, drive first bit, get sampled by slave on rising edge, set serial clock to true(half tick), drive next bit, set serial clokc to false(half tick), now again
    public void writeData(byte package){
        //split package up into list of bits or booleans(true or false/0 or 1)
        //make a loop to send byte, (first time around drive bit[i]), slave sample it, set serial clock to oppisite, drive next bit[i] 
    }

    //sampling, first bit starts as driven, sample it then serial clock rises, next bit is driven then serial clock falls, now again
    public void readData()
    {
        //loop
    }

};