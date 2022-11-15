using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Transactions;

string[] lines = File.ReadAllLines("C:/Users/MSIG/Desktop/work/Softwire/C#/support-bank/Transactions2014.csv");
List<Transaction> transactions = new List<Transaction>();

for (int i = 1; i < lines.Length; i++)
{
    string[] transactionData = lines[i].Split(',');
   
    double amount = double.Parse(transactionData[4], System.Globalization.CultureInfo.InvariantCulture);

    Transaction myTrans = new Transaction(transactionData[0], transactionData[1],
        transactionData[2], transactionData[3], amount);

   transactions.Add(myTrans);
}

Dictionary<string, double> listAll = new Dictionary<string, double>();

foreach (Transaction trans in transactions)
{
    try
    {
        listAll.Add(trans.from, trans.amount);
    }
    catch (ArgumentException)
    {
        continue;
    }

    try
    {
        listAll.Add(trans.to, trans.amount);
    }
    catch (ArgumentException)
    {
        continue;
    }
}

foreach ( KeyValuePair<string, double> pair in listAll)
{
    Console.WriteLine(pair.Key);
    Console.WriteLine(pair.Value); 
}

//foreach (Transaction trans in transactions)
//{
//    Console.WriteLine(trans.date);
//    Console.WriteLine(trans.from);
//    Console.WriteLine(trans.to);
//    Console.WriteLine(trans.narrative);
//    Console.WriteLine(trans.amount);
//    Console.WriteLine("");
//}


public class Transaction
{
    public string date;
    public string from;
    public string to;
    public string narrative;
    public double amount;
    public Transaction(string date, string from, string to, string narrative, double amount)
    {
        this.date = date;
        this.from = from;
        this.to = to;
        this.narrative = narrative;
        this.amount = amount;
    }
}

public class Account
{
    public string name;
    public double balance;
    
    public Account (string name, double balance)
    {
        this.name = name;
        this.balance = balance;
    }
}

