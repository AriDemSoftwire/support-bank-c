using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Transactions;
using CsvHelper;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// reading second file and parsing it into Transaction objects immediately
//using (var reader = new StreamReader("C:/Users/MSIG/Desktop/work/Softwire/C#/support-bank/DodgyTransactions2015.csv"))
//using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//{ try
//    {
//        var records = csv.GetRecords<Transaction>();
//        List<Transaction> transactions2 = records.ToList();
//    }
//    catch (ArgumentException)
//    {
//        Console.WriteLine("Data type invalid");
//    }

using (StreamReader file = File.OpenText(@"C:/Users/MSIG/Desktop/work/Softwire/C#/support-bank/Transactions2013.json"))
using (JsonTextReader reader = new JsonTextReader(file))
{
    JArray file2 = (JArray)JToken.ReadFrom(reader);
}

// reading the first file and parsing it manually
string[] lines = File.ReadAllLines("C:/Users/MSIG/Desktop/work/Softwire/C#/support-bank/Transactions2014.csv");
    List<Transaction> transactions = new List<Transaction>();
    List<Account> accounts = new List<Account>();
    List<string> names = new List<string>();

    //getting a transactions array
    for (int i = 1; i < lines.Length; i++)
    {
        string[] transactionData = lines[i].Split(',');

        double amount = double.Parse(transactionData[4], System.Globalization.CultureInfo.InvariantCulture);

        Transaction myTrans = new Transaction(transactionData[0],
            transactionData[1],
            transactionData[2],
            transactionData[3],
            amount);

        transactions.Add(myTrans);
    }

    // getting an array of names
    foreach (Transaction trans in transactions)
    {
        if (!names.Contains(trans.From))
        {
            names.Add(trans.From);
        }

        if (!names.Contains(trans.To))
        {
            names.Add(trans.To);
        }
    }

    // getting an array of accounts
    foreach (string name in names)
    {
        double accBalance = 0;
        List<Transaction> transFrom = new List<Transaction>();
        List<Transaction> transTo = new List<Transaction>();

        foreach (Transaction trans in transactions)
        {
            if (trans.From == name)
            {
                accBalance = accBalance + trans.Amount;
                transFrom.Add(trans);
            }

            if (trans.To == name)
            {
                accBalance = accBalance - trans.Amount;
                transTo.Add(trans);
            }
        }
        accounts.Add(new Account(name, accBalance, transFrom, transTo));
    }

    // reading user input
    string command = "";
    command = Console.ReadLine();
    if (command == "List All") {
        listAll(accounts);
    }
    foreach (Account acc in accounts)
    {
        string input = command.Substring(5);

        if (input == acc.name)
        {
            listTransactions(accounts, input);
            break;
        }
    }

    // functions
    static void listAll(List<Account> accounts)
    {
        foreach (Account acc in accounts)
        {
            Console.WriteLine($"Account name: {acc.name}");
            Console.WriteLine($"Account balance: {Math.Round(acc.balance, 2)}");
            Console.WriteLine("");
        }
    }

    static void listTransactions(List<Account> accounts, string name)
    {

        foreach (Account acc in accounts)
        {
            if (acc.name == name)
            {
                Console.WriteLine($"Name: {acc.name}");
                Console.WriteLine("");
                foreach (Transaction trans in acc.from)
                {
                    Console.WriteLine(trans.Date);
                    Console.WriteLine(trans.From);
                    Console.WriteLine(trans.To);
                    Console.WriteLine(trans.Narrative);
                    Console.WriteLine(trans.Amount);
                    Console.WriteLine("");
                }

                foreach (Transaction trans in acc.to)
                {
                    Console.WriteLine(trans.Date);
                    Console.WriteLine(trans.From);
                    Console.WriteLine(trans.To);
                    Console.WriteLine(trans.Narrative);
                    Console.WriteLine(trans.Amount);
                    Console.WriteLine("");
                }
            }
        }
    }

//}

// classes
public class Transaction
{
    public string Date;
    public string From;
    public string To;
    public string Narrative;
    public double Amount;
    public Transaction(string Date, string From, string To, string Narrative, double Amount)
    {
        this.Date = Date;
        this.From = From;
        this.To = To;
        this.Narrative = Narrative;
        this.Amount = Amount;
    }
}

public class Account
{
    public string name;
    public double balance;
    public List<Transaction> to;
    public List<Transaction> from;
    
    public Account (string name, double balance, List<Transaction> to, List<Transaction> from)
    {
        this.name = name;
        this.balance = balance;
        this.to = to;
        this.from = from;
    }
}

