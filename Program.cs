int main()
{
    ThreeWireSPI bus1=new ThreeWireSPI();//makes the spi the master and slave connect to
    Master driver1=new Master(bus1);//makes driver that is master that connect to bus1
    Slave sensor1=new Slave(bus1);//makes sensor that is slave that connects to bus1

    //Different tests:
    //byte resetCommand=0b1111_0000; //write command example1
    //byte calibrationCommand=0b1111_0001; //write command example2
    byte readTemp=0b0111_0000; //read command example1
    //byte readMoisture=0b0111_0001; //read command example2

    driver1.ChangeChipSelectState(1);//pull low to activate
    driver1.WriteData(readTemp);
    driver1.ChangeChipSelectState(1);//return to high to deactivate

    return 0;
}
main();