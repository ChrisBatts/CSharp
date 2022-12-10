// 
// COP 4365 – Fall 2022 
// 
// Homework #4: Traffic Study 
// 
// Description: Program to monitor incoming cars at the intersection
// 
// File name: hw2_2
// 
// By: Christopher Batts 
// 
// 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace hw2_2
{
    public partial class Form1 : Form
    {

        int secs = 0;
        int emerTime = 0;
        int counter = 0;
        int sequence = 1;
        int numN = 0;
        int numS = 0;
        int numE = 0;
        int numW = 0;
        int nAvg = 0;
        int sAvg = 0;
        int eAvg = 0;
        int wAvg = 0;
        int maxLine = 0;

        public Form1()
        {
            InitializeComponent();

        }

        //Creating traffic light objects

        public trafficLight north = new trafficLight("North", "Green", true, "South");
        public trafficLight south = new trafficLight("South", "Red", false, "East");
        public trafficLight east = new trafficLight("East", "Red", false, "West");
        public trafficLight west = new trafficLight("West", "Red", false, "North");
        Boolean emergency = false;

        List<car> cars = new List<car>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (secs == 0)
            {
                Console.WriteLine("Current Time    North light    South light    East Light     West light");
                Console.WriteLine("------------    -----------    -----------    ----------     ----------");

                //Creating car list

                foreach (string line in System.IO.File.ReadLines(@"HW #4 Data.txt"))
                {
                    cars.Add(new car(line[0], int.Parse(line.Remove(0, 1))));
                    counter++;
                }
            }

            if (secs == 240)
            {
                for (int i = 0; i < counter - 1; i++)
                {
                    if (cars[i].passed == true)
                    {
                        if (cars[i].direction == 'N')
                        {
                            numN++;
                            nAvg = nAvg + cars[i].waitTime;
                        }
                        if (cars[i].direction == 'S')
                        {
                            numS++;

                            sAvg = sAvg + cars[i].waitTime;
                        }
                        if (cars[i].direction == 'E')
                        {
                            numE++;

                            eAvg = eAvg + cars[i].waitTime;
                        }
                        if (cars[i].direction == 'W')
                        {
                            numW++;

                            wAvg = wAvg + cars[i].waitTime;
                        }
                    }

                }
                nAvg = nAvg / numN;
                sAvg = sAvg / numS;
                eAvg = eAvg / numE;
                wAvg = wAvg / numW;
                Console.WriteLine("**************************************************************************************************************");
                Console.WriteLine("**************************************************************************************************************");
                Console.WriteLine("**************************************************************************************************************");
                Console.WriteLine(" ");
                Console.WriteLine("Amount of cars in North direction: " + north.numCars + ", Average wait time for cars in North direction: " + nAvg);
                Console.WriteLine("Amount of cars in South direction: " + south.numCars + ", Average wait time for cars in South direction: " + sAvg);
                Console.WriteLine("Amount of cars in East direction: " + east.numCars + ", Average wait time for cars in East direction: " + eAvg);
                Console.WriteLine("Amount of cars in West direction: " + west.numCars + ", Average wait time for cars in West direction: " + wAvg);
                Console.WriteLine("Largest amount of cars in line at one time: " + maxLine);
                Console.WriteLine(" ");
                Console.WriteLine("**************************************************************************************************************");
                Console.WriteLine("**************************************************************************************************************");
                Console.WriteLine("**************************************************************************************************************");
            }

            north.runLight(emergency, north, south, east, west, secs);
            south.runLight(emergency, north, south, east, west, secs);
            east.runLight(emergency, north, south, east, west, secs);
            west.runLight(emergency, north, south, east, west, secs);

            north.newLight(north, south, east, west);
            south.newLight(north, south, east, west);
            east.newLight(north, south, east, west);
            west.newLight(north, south, east, west);



            for (int i = 0; i < counter - 1; i++)
            {
                cars[i].passed = cars[i].passLight(north, south, east, west, secs);
                if (cars[i].passed && cars[i].done)
                {
                    if (north.numLine > maxLine)
                    {
                        maxLine = north.numLine;

                    }
                    if (south.numLine > maxLine)
                    {
                        maxLine = south.numLine;
                    }
                    if (east.numLine > maxLine)
                    {
                        maxLine = east.numLine;
                    }
                    if (west.numLine > maxLine)
                    {
                        maxLine = west.numLine;
                    }
                    if (cars[i].direction == 'N')
                    {
                        north.numLine = 0;
                    }
                    if (cars[i].direction == 'S')
                    {
                        south.numLine = 0;
                    }
                    if (cars[i].direction == 'E')
                    {
                        east.numLine = 0;
                    }
                    if (cars[i].direction == 'W')
                    {
                        west.numLine = 0;
                    }
                    cars[i].done = false;
                    cars[i].sequence = sequence;
                    sequence++;
                    cars[i].printCar(secs);
                }
            }
            secs++;
            //Optional GUI stuff
            if (north.config == "Green")
            {
                northLightYel.Visible = false;
                northLightRed.Visible = false;
                northLight.Visible = true;
            }

            if (north.config == "Yellow")
            {
                northLightYel.Visible = true;
                northLightRed.Visible = false;
                northLight.Visible = false;
            }

            if (north.config == "Red")
            {
                northLightYel.Visible = false;
                northLightRed.Visible = true;
                northLight.Visible = false;
            }

            if (south.config == "Green")
            {
                southLightYel.Visible = false;
                southLight.Visible = false;
                southLightGreen.Visible = true;
            }
            if (south.config == "Yellow")
            {
                southLightYel.Visible = true;
                southLight.Visible = false;
                southLightGreen.Visible = false;
            }
            if (south.config == "Red")
            {
                southLightYel.Visible = false;
                southLight.Visible = true;
                southLightGreen.Visible = false;
            }
            //
            if (east.config == "Green")
            {
                eastLightYel.Visible = false;
                eastLight.Visible = false;
                eastLightGreen.Visible = true;
            }
            if (east.config == "Yellow")
            {
                eastLightYel.Visible = true;
                eastLight.Visible = false;
                eastLightGreen.Visible = false;
            }
            if (east.config == "Red")
            {
                eastLightYel.Visible = false;
                eastLight.Visible = true;
                eastLightGreen.Visible = false;
            }
            //
            if (west.config == "Green")
            {
                westLightYel.Visible = false;
                westLight.Visible = false;
                westLightGreen.Visible = true;
            }
            if (west.config == "Yellow")
            {
                westLightYel.Visible = true;
                westLight.Visible = false;
                westLightGreen.Visible = false;
            }
            if (west.config == "Red")
            {
                westLightYel.Visible = false;
                westLight.Visible = true;
                westLightGreen.Visible = false;
            }

            //End optional GUI stuff

            timeLab.Text = secs.ToString();

        }


        private void timeLab_Click(object sender, EventArgs e)
        {

        }

        private void emergency1_Click(object sender, EventArgs e)
        {


            emergency = true;
            north.emergency2 = true;
            north.runLight(emergency, north, south, east, west, secs);
            north.newLight(north, south, east, west);
            south.runLight(emergency, north, south, east, west, secs);
            south.newLight(north, south, east, west);
            east.newLight(north, south, east, west);
            east.newLight(north, south, east, west);
            west.runLight(emergency, north, south, east, west, secs);
            west.newLight(north, south, east, west);
            Console.Write(secs);
            Console.WriteLine("     An emergency vehicle is approaching from the North.");
            Console.Write(secs);
            north.printLights(north, south, east, west);
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {


            emerTime++;
            if (emerTime == 10)
            {
                Console.Write(secs);
                Console.WriteLine("     Emergency vehicle has passed.");
                emerTime = 0;
                emergency = false;
                timer2.Stop();
            }
        }

        private void emergency2_Click(object sender, EventArgs e)
        {


            emergency = true;
            south.emergency2 = true;
            north.runLight(emergency, north, south, east, west, secs);
            north.newLight(north, south, east, west);
            south.runLight(emergency, north, south, east, west, secs);
            south.newLight(north, south, east, west); ;
            east.runLight(emergency, north, south, east, west, secs);
            east.newLight(north, south, east, west);
            west.runLight(emergency, north, south, east, west, secs);
            west.newLight(north, south, east, west);
            Console.Write(secs);
            Console.WriteLine("     An emergency vehicle is approaching from the south.");
            north.printLights(north, south, east, west);
            timer2.Start();

        }

        private void emergency3_Click(object sender, EventArgs e)
        {

            emergency = true;
            east.emergency2 = true;
            north.runLight(emergency, north, south, east, west, secs);
            north.newLight(north, south, east, west);
            south.runLight(emergency, north, south, east, west, secs);
            south.newLight(north, south, east, west);
            east.runLight(emergency, north, south, east, west, secs);
            east.newLight(north, south, east, west);
            west.runLight(emergency, north, south, east, west, secs);
            west.newLight(north, south, east, west); ;
            Console.Write(secs);
            Console.WriteLine("     An emergency vehicle is approaching from the East.");
            Console.Write(secs);
            north.printLights(north, south, east, west);
            timer2.Start();
        }

        private void emergency4_Click(object sender, EventArgs e)
        {
            emergency = true;
            west.emergency2 = true;

            north.newLight(north, south, east, west);
            north.runLight(emergency, north, south, east, west, secs);


            south.newLight(north, south, east, west);
            south.runLight(emergency, north, south, east, west, secs);

            east.newLight(north, south, east, west);
            east.runLight(emergency, north, south, east, west, secs);


            west.newLight(north, south, east, west);
            west.runLight(emergency, north, south, east, west, secs);

            Console.Write(secs);
            Console.WriteLine("     An emergency vehicle is approaching from the West.");
            north.printLights(north, south, east, west);
            Console.Write(secs);
            timer2.Start();
        }
    }
    //Traffic light class
    public class trafficLight
    {
        public String name;
        public String config;
        public Boolean on = false;
        public String next;
        public Boolean priority = false;
        public Boolean emergency2 = false;
        public int secs = 0;
        public int emerSecs = 0;
        public int numLine;
        public int numCars = 0;
        public trafficLight(string N, String C, bool on, String next1)
        {
            this.name = N;
            this.config = C;
            this.on = on;
            this.next = next1;
        }
        //Method name: newLight
        //Description: This method is responsible for turning lights on/off and keeping the right rotation
        public void newLight(trafficLight light, trafficLight light2, trafficLight light3, trafficLight light4)
        {

            if ((this.on == true && this.secs >= 13) || (this.name == "North" && light2.name == "South" && this.secs == 6) || (this.name == "North" && light2.name == "South" && this.secs == 13))
            {

                if (this.name == "North" && light2.name == "South" && this.secs == 6)
                {
                    light2.on = true;

                }
                else if (this.name == "North" && light2.name == "South" && this.secs == 13)
                {
                    this.secs = 0;
                    this.on = false;
                }
                else if (this.name == "North" && light2.name == "South" && this.secs == 14)
                {
                    this.on = false;
                    this.secs = 0;
                    light2.on = true;
                    light2.secs = 0;
                }
                else
                {
                    this.on = false;
                    if (this.next == "East")
                    {
                        light3.on = true;
                        this.secs = 0;
                    }
                    if (this.next == "West")
                    {
                        light4.on = true;
                        this.secs = 0;
                    }
                    if (this.next == "North")
                    {
                        light.on = true;
                        this.secs = 0;
                    }


                }

            }


        }
        //Method name: RunLights
        //Description: This method runs the lights and changes their colors
        public void runLight(Boolean emergency1, trafficLight north, trafficLight south, trafficLight east, trafficLight west, int secs)
        {
            if (emergency1 == true && this.emergency2 == true)
            {
                if (this.on == false)
                {
                    this.on = true;
                }
                if (emerSecs == 0)
                {

                    this.config = "Green";

                }
                if (emerSecs == 7)
                {
                    this.config = "Yellow";

                }
                if (emerSecs >= 10)
                {
                    this.config = "Red";
                    this.emergency2 = false;
                    this.secs = 14;

                    emerSecs = -1;

                }
                emerSecs++;



            }
            else if (emergency1 == true && emergency2 == false)
            {
                if (this.on == true)
                {
                    if (secs < 9)
                    {
                        secs = 0;
                    }
                    if (secs > 9)
                    {
                        secs = 13;
                    }
                    this.on = false;
                    this.config = "Red";

                }

            }
            if (this.on == true && emergency1 == false && emergency2 == false)
            {
                if (this.secs == 0)
                {
                    this.config = "Green";
                    Console.Write(secs);
                    this.printLights(north, south, east, west);
                }
                if (this.secs == 9)
                {
                    this.config = "Yellow";
                    Console.Write(secs);
                    this.printLights(north, south, east, west);
                }
                if (this.secs >= 12)
                {
                    this.config = "Red";
                    Console.Write(secs);
                    this.printLights(north, south, east, west);

                }
                this.secs++;

            }


        }
        // 
        // Method Name: printLights 
        // Description: prints out the configuration of each traffic light
        public void printLights(trafficLight t, trafficLight c, trafficLight b, trafficLight r)
        {
            Console.WriteLine("                  " + t.config + "           " + c.config + "            " + b.config + "           " + r.config);
        }


    }

    //Car class
    public class car
    {
        public char direction;
        public int arrTime;
        public int sequence;
        public int exTime;
        public int waitTime;
        public Boolean passed;
        public Boolean inLine;
        public Boolean done;

        public car()
        {
            direction = 'x';
            arrTime = 0;
            sequence = 0;
            exTime = 0;
            waitTime = 0;
            passed = false;
            inLine = false;
            done = false;
        }

        public car(char d, int t)
        {
            direction = d;
            arrTime = t;
        }
        public void printCar(int secs)
        {
            Console.WriteLine("             Car " + sequence + " arrived at " + arrTime + ", from the " + direction + ", exited at " + exTime + ", and waited for " + waitTime);
        }

        public Boolean passLight(trafficLight n, trafficLight s, trafficLight e, trafficLight w, int secs)
        {
            if (this.passed)
            {
                return true;
            }
            if (this.direction == 'N' && this.arrTime <= secs && this.inLine == false && n.config != "Green" && this.waitTime == 0)
            {
                n.numLine++;
                this.inLine = true;
                return false;
            }
            if (this.direction == 'S' && this.arrTime <= secs && this.inLine == false && s.config != "Green" && this.waitTime == 0)
            {
                s.numLine++;
                this.inLine = true;
                return false;
            }
            if (this.direction == 'E' && this.arrTime <= secs && this.inLine == false && e.config != "Green" && this.waitTime == 0)
            {
                e.numLine++;
                this.inLine = true;
                return false;
            }
            if (this.direction == 'W' && this.arrTime <= secs && this.inLine == false && w.config != "Green" && this.waitTime == 0)
            {
                w.numLine++;
                this.inLine = true;
                return false;
            }


            else if (this.direction == 'N' && this.arrTime <= secs && n.config == "Green" && this.waitTime == 0)
            {
                this.exTime = secs;
                this.waitTime = this.exTime - this.arrTime;
                this.done = true;
                n.numCars++;
                return true;
            }
            else if (this.direction == 'S' && this.arrTime <= secs && s.config == "Green" && this.waitTime == 0)
            {
                this.exTime = secs;
                this.waitTime = this.exTime - this.arrTime;
                this.done = true;
                s.numCars++;
                return true;
            }
            else if (this.direction == 'E' && this.arrTime <= secs && e.config == "Green" && this.waitTime == 0)
            {
                this.exTime = secs;
                this.waitTime = this.exTime - this.arrTime;
                this.done = true;
                e.numCars++;
                return true;
            }
            else if (this.direction == 'W' && this.arrTime <= secs && w.config == "Green" && this.waitTime == 0)
            {
                this.exTime = secs;
                this.waitTime = this.exTime - this.arrTime;
                this.done = true;
                w.numCars++;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void isLine(trafficLight n, trafficLight s, trafficLight e, trafficLight w, int secs)
        {
            if (this.direction == 'N' && this.arrTime >= secs && n.config != "Green")
            {
                n.numLine++;
            }
            if (this.direction == 'S' && this.arrTime >= secs && n.config != "Green")
            {
                s.numLine++;
            }
            if (this.direction == 'N' && this.arrTime >= secs && n.config != "Green")
            {
                e.numLine++;
            }
            if (this.direction == 'N' && this.arrTime >= secs && n.config != "Green")
            {
                w.numLine++;
            }
        }


    }
}


