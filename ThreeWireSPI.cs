public class ThreeWireSPI{
    bool chipSelect1=true; //active low meaning if true not active, if false active

    bool dataWire=false;//dataLow 0(false), dataHigh 1(true) 0 or 1 data is bits, if nothing is being sent data wire just floats in one of these states

    //Mode 0: CPOL = 0, CPHA = 0 (Clock idles low, samples on rising edge)
    bool serialClock=false;//can either be down(0==false) or up(1==true), down+up=1tick, the clock idles at logic Low (0==false). The first edge is Rising, and the second is Falling.
}