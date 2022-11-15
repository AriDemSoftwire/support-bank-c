﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Transactions;

string[] lines = File.ReadAllLines("C:/Users/MSIG/Desktop/work/Softwire/C#/support-bank/Transactions2014.csv");
List<Transaction> transactions = new List<Transaction>();
List<Account> accounts = new List<Account>();
List<string> names = new List<string>();

for (int i = 1; i < lines.Length; i++)
{
    string[] transactionData = lines[i].Split(',');
   
    double amount = double.Parse(transactionData[4], System.Globalization.CultureInfo.InvariantCulture);

    Transaction myTrans = new Transaction(transactionData[0], transactionData[1],
        transactionData[2], transactionData[3], amount);

   transactions.Add(myTrans);
}

// getting an array of names
foreach (Transaction trans in transactions)
{
    if (!names.Contains(trans.from))
    {
        names.Add(trans.from);
    }

    if (!names.Contains(trans.to))
    {
        names.Add(trans.to);
    }
}

foreach (string name in names)
{
    double accBalance = 0;
    List<Transaction> transFrom = new List<Transaction>();
    List<Transaction> transTo = new List<Transaction>();

    foreach (Transaction trans in transactions)
    {
        if (trans.from == name)
        {
            accBalance = accBalance + trans.amount;
            transFrom.Add(trans);
        }

        if (trans.to == name)
        {
            accBalance = accBalance - trans.amount;
            transTo.Add(trans);
        }
    }
    accounts.Add(new Account(name, accBalance, transFrom, transTo));
}

    Console.WriteLine(accounts[2].name);
    Console.WriteLine(Math.Round(accounts[2].balance, 2));
    foreach (Transaction trans in accounts[2].from)
{
    Console.WriteLine(trans.date);
    Console.WriteLine(trans.from);
    Console.WriteLine(trans.to);
    Console.WriteLine(trans.narrative);
    Console.WriteLine(trans.amount);
    Console.WriteLine("");
}


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

