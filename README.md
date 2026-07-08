Basic and Extra Information:
    Basic:
        What is This?
            This project represents a Three Wire SPI being used between a slave and a master
            to communicate with each other, with the master sending read or write commands to
            the slave, and the slave executing write commands or sending data back if read
            command. SPI class is represent hardware, only wires, not really any code in 
            there except for event triggers because necesarry. SPI has three wires, one 
            for the serial clock, one for sending data, and then a chip select wire. Only 
            the master controls the clock and chip select. Both can send data through the
            data wire, and they send/recieve the data based of clock ticks.
    Extra Information:
        General:
            Whoever is sending data across the line send on falling edge, and whoever is 
            reading the data, reads on rising edge. Master ticks clock up then down, and thats
            one full tick. Data line doesnt float if not being used, its just left at high or
            low.
        Function Specific:
            Master:
                WriteData:
                    Changes dataline to true or false while ticking clock so slave can read in 
                    the data. Checks if it sent a read or write command. If it was a read 
                    command, it calls read data.
                ReadData:
                    Ticks the clock another 8 times so slave can send the data, and reads it in
                    and then converts it to a byte.
            Slave
                OnClockChanged:
                    Activates everytime the clock is changed, if bit count hasnt reached 8, it
                    knows its recieving the data, after it reaches 8, it checks if it was a 
                    read or write command, if it was a write command, it calls the command and
                    does the corresponding action, if it was a read command it calls up function
                    to load up a package then since the clock will still be ticking from the 
                    master, it will know that its not recieving anything anymore because of 
                    bit count, so it will start sending data through the data line where the 
                    master reads it.