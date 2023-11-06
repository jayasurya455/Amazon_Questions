using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;


class Result
{
    //1 Q in amazon
    public static List<int> minimalHeaviestSetA(List<int> arr)
    {
        int maxSum = 0;
        int currentSum = 0;
        int maxNum = 0;
        int secMaxNum = 0;

        foreach (int num in arr)
        {
            currentSum = maxNum + num;

            if (num > maxNum)
            {
                secMaxNum = maxNum;
                maxNum = num;
            }
            else if (num > secMaxNum)
            {
                secMaxNum = num;
            }

            if (currentSum > maxSum)
            {
                maxSum = currentSum;
            }
            else if (currentSum < 0)
            {
                currentSum = 0;
            }
        }

        return new List<int> { secMaxNum, maxNum};
    }
    //2 Q in AMazon

    public static List<int> numberOfItems(string s, List<int> startIndices, List<int> endIndices) 
    {
        List<int> sets = new List<int>();
        foreach (var item in startIndices.Select((x, i) => new {i, x})) 
        {
            sets.Add(countStartsInCompartments(s.Substring(item.x - 1, endIndices[item.i])));
        }
        return sets;
    }

    public static int countStartsInCompartments(string s) {
        int count = 0;
        List<int> sets = new List<int>();
        bool started = false;
        foreach (var item in s) 
        {
            if (item.ToString() == "|" && started == false)
            {
                started = true;
                count = 0;
            }
            else if (item.ToString() == "*")
            {
                count++;
            }
            else if (started == true && item.ToString() == "|")
            {
                sets.Add(count);
                count = 0;
            }
        }
        return sets.Sum();
    }

    //3 Q in Amazon
    public static List<string> processLogs(List<string> logs, int threshold)
    {
        try 
        {
            Dictionary<string, int> countDictionary = new Dictionary<string, int>();

            // Count the occurrences of each string
            foreach (string log in logs)
            {
                string[] logParts = log.Split(' ');
                foreach (string part in logParts)
                {
                    if (countDictionary.ContainsKey(part))
                        countDictionary[part]++;
                    else
                        countDictionary[part] = 1;
                }
            }

            // Filter the strings based on the threshold
            List<string> result = countDictionary
                .Where(kv => kv.Value >= threshold)
                .Select(kv => kv.Key)
                .ToList();

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return logs;
        }
    }
}

class Solution
{
    public static void Main(string[] args)
    {

        List<int> arr = new List<int>();

        arr = new List<int> { 4,2,5,1,6 };
        string s = "*|*|*|";
        List<int> startingIndices = new List<int> { 1 };
        List<int> endingIndices = new List<int> { 6 };

        List<string> logs = new List<string> { "11 12 14", "11 12 23", "11 12 14", "11 13 54" };
        int threshold = 2;

        List<int> result = Result.minimalHeaviestSetA(arr);
        List<int> result1 = Result.numberOfItems(s, startingIndices, endingIndices);
        List<string> result2 = Result.processLogs(logs, threshold);
        foreach (var item in result2) {
            Console.WriteLine(item);
        }
    }
}

