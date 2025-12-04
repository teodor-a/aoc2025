using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;


public class MyProgram {

    static void DayOne()
    {
        Console.WriteLine("Begin day 1.");

        // Read the input file:
        string fileName = "inputs/input_day1";
        var lines = File.ReadLines(fileName);

        // Initialise start position as per instructions:
        int currentPos = 50;

        // Initialise counter for number of times we land on zero:
        int zeroCounter = 0; 

        // Go through each movement:
        foreach (var line in lines) {

            // Isolate the two pieces of information:
            char direction = char.Parse(line.Substring(0, 1));
            int distance = Int32.Parse(line.Substring(1));
            
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
        Console.WriteLine("We landed on zero " + zeroCounter + " times.");
    }

    static void DayTwo_part1()
    // This gives the right answer for part one.
    // While clever solution, it is not expandable at all and the reveal of part2 was a disaster for it.
    {
        Console.WriteLine("Begin day 2.");
        
        // Read the input file:
        string fileName = "inputs/input_day2";
        var lines = File.ReadAllText(fileName).Split(","); // This time everything is on one line
        List<long> invalidIDs = new List<long>();
        
        // Read each "line" 
        foreach (var line in lines)
        {
            // Get the IDs:
            var thisLine = line.Split("-");
            string firstID = thisLine[0];
            string lastID = thisLine[1];

            // Also as ints so we can do number stuff:
            long startID_0 = Int64.Parse(firstID);
            long stopID_0 = Int64.Parse(lastID);
            
            //Console.WriteLine("Start at " + firstID + ", end at " + lastID);
            
            // If not an even number of digits in starting number;
            // make it to the closest higher even-digit number:
            if (firstID.Length % 2 != 0)
            {
                long startID = (long)Math.Pow(10, firstID.Length);
                firstID = startID.ToString();
            }

            // Get the first half of the start ID digits: 
            string firstHalfofFirstID = firstID.Substring(0, firstID.Length/2);
            long firstHalfofFirstID_numeric = Int64.Parse(firstHalfofFirstID);

            // Create the lowest potential invalid ID:
            long potentialInvalidID = Int64.Parse(string.Concat(Enumerable.Repeat(firstHalfofFirstID, 2)));
            //potentialInvalidID = Math.Max(potentialInvalidID, startID_0);
            while (potentialInvalidID < startID_0)
            {
                // Create the next invalid ID:
                firstHalfofFirstID_numeric += 1;
                firstHalfofFirstID = firstHalfofFirstID_numeric.ToString();
                potentialInvalidID = Int64.Parse(string.Concat(Enumerable.Repeat(firstHalfofFirstID, 2)));
            }

            // Keep finding more invalid ids
            // by incrementing the first half by one,
            // duplicating, and checking if it is within range:
            while (potentialInvalidID >= startID_0 & potentialInvalidID <= stopID_0)
            {
                //System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Found invalid ID: " + potentialInvalidID);
                // Record it:
                invalidIDs.Add(potentialInvalidID);
                // Create the next invalid ID:
                firstHalfofFirstID_numeric += 1;
                firstHalfofFirstID = firstHalfofFirstID_numeric.ToString();
                potentialInvalidID = Int64.Parse(string.Concat(Enumerable.Repeat(firstHalfofFirstID, 2)));
            }
            //System.Threading.Thread.Sleep(2000);
        }

        // Sum up all the invalid ids. Cannot use .sum() on array because it contains long??
        long sum = 0;
        foreach (var invid in invalidIDs) {
            sum += invid;
        }

        Console.WriteLine("The sum of all the invalid IDs is: " + sum);
    }

    static void DayTwo_part2() {
        // Rewrite parts of part 1 because a whole new approach is needed. This part can only be bruteforced.'
        
        // Read the input file:
        string fileName = "inputs/input_day2";
        var lines = File.ReadAllText(fileName).Split(","); // This time everything is on one line
        List<long> invalidIDs = new List<long>();
        
        // Read each "line" 
        foreach (var line in lines)
        {
            // Get the IDs:
            var thisLine = line.Split("-");
            string firstID = thisLine[0];
            string lastID = thisLine[1];

            // Also as ints so we can do number stuff:
            long startID_0 = Int64.Parse(firstID);
            long stopID_0 = Int64.Parse(lastID);
            
            //Console.WriteLine("Start at " + firstID + ", end at " + lastID);
            
            long currentID = startID_0;
            while (currentID <= stopID_0)
            {/*
                if (currentID.ToString().Length % 2 != 0)
                {
                    char[] characters = currentID.ToString().ToCharArray().Distinct();
                    if (characters.Length > 1) 
                    {
                        invalidIDs.Add(currentID);
                    }
                } else {*/
                    if (is_ID_invalid(currentID.ToString())) 
                    {
                        invalidIDs.Add(currentID);
                    }
                currentID += 1;

                }
                //currentID += 1;
            }
        long sum = 0;
        foreach (var invid in invalidIDs) {
            sum += invid;
        }
        Console.WriteLine("The sum of all the invalid IDs is: " + sum);

    }
    

    static bool is_ID_invalid(string ID)
    {
        for(int parts = 2; parts <= ID.Length; parts++)
        {
            if (ID.Length % parts != 0)
            {
                continue;
            }
            int splitPos = ID.Length/parts;
            string onePart = ID.Substring(0, splitPos);
            string allParts = string.Concat(Enumerable.Repeat(onePart, parts));
            if (allParts == ID)
            {
                return true;
            }
        }
        return false;
    }

    static public void Main()
    {
        DayOne();
        DayTwo_part1();
        DayTwo_part2();
    }

}