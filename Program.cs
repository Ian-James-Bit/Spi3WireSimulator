int main()
{
    ThreeWireSPI bus1=new ThreeWireSPI();//makes the spi the master and slave connect to
    Master driver1=new Master(bus1);//makes driver that is master that connect to bus1
    Slave sensor1=new Slave(bus1);//makes sensor that is slave that connects to bus1
    //have master write a byte(acting as directions) to slave then slave write back byte(acting as measurement) to master

    //testing:
    //byte resetCommand=0b1111_0000; //240 but 112 bc first bit is adress;write
    //byte calibrationCommand=0b1111_0001; //241 but 113 bc first bit is adress;write
    byte readTemp=0b0111_0000; // 112 bc first bit is adress;read
    //byte readMoisture=0b1111_0001; //113 bc first bit is adress;read
    driver1.ActivateChipSelect(1);
    driver1.WriteData(readTemp);
    return 0; 
}
main();