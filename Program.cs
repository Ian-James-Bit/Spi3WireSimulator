int main()
{
    ThreeWireSPI bus1=new ThreeWireSPI();//makes the spi the master and slave connect to
    Master driver1=new Master(bus1);//makes driver that is master that connect to bus1
    Slave sensor1=new Slave(bus1);//makes sensor that is slave that connects to bus1
    //have master write a byte(acting as directions) to slave then slave write back byte(acting as measurement) to master
    return 0; 
}
main();