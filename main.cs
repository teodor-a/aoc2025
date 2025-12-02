using System.IO;
using System;



public class MyProgram {

    static void DayOne()
    {
        // Read the input file:
        string fileName = "inputs/input_day1";
        var lines = File.ReadLines(fileName);

        // Initialise start position as per instructions:
        int currentPos = 50;

        // Initialise counter for number of times we land on zero:
        int zeroCounter = 0; 

        // Go through each movement:
        foreach (var line in lines) {

            Console.WriteLine("Current position: " + currentPos);

            // Isolate the two pieces of information:
            char direction = char.Parse(line.Substring(0, 1));
            int distance = Int32.Parse(line.Substring(1));

            Console.WriteLine("Direction: " + direction + ", Distance: " + distance);
            
            // Go up or down according to L or R:
            if (direction == 'L')
            {
                currentPos -= distance;
            } else if (direction == 'R')
            {
                currentPos += distance;
            } else
            {
                Console.WriteLine("Error! Strange direction: " + direction);
                System.Environment.Exit(1);
            }

            // Adjust position if it is above 99 or below 0:
            if (currentPos > 99 | currentPos < 0)
            {
                currentPos %= 100;
            }
            
            // Check if we are at zero:
            if (currentPos == 0)
            {
                zeroCounter += 1;
            }

        }
        Console.WriteLine("We landed on zero " + zeroCounter + " times.");    }

    static public void Main()
    {
        DayOne();
    }
}