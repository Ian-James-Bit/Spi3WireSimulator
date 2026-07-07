int main()
{
    ThreeWireSPI bus1=new ThreeWireSPI();//makes the spi the master and slave connect to
    Master driver1=new Master(bus1);//makes driver that is master that connect to bus1
    Slave sensor1=new Slave(bus1);//makes sensor that is slave that connects to bus1
    //have master write a byte(acting as directions) to slave then slave write back byte(acting as measurement) to master

    //testing:
    byte importantData=0b01101_0110; //214
    driver1.ActivateChipSelect(1);
    Console.WriteLine(bus1.chipSelect1);
    driver1.WriteData(importantData);
    return 0; 
}
main();