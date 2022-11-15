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
    //foreach (string transaction in transactionData)
    //{
    //    Console.WriteLine(transaction);
    //}
    //Console.WriteLine(transactionData[4]);
    double amount = double.Parse(transactionData[4], System.Globalization.CultureInfo.InvariantCulture);

    Transaction myTrans = new Transaction(transactionData[0], transactionData[1],
        transactionData[2], transactionData[3], amount);

    Console.WriteLine(myTrans.from);

    transactions.Add(myTrans);
   
}

  //  Console.WriteLine(transactions[4].date, transactions[4].from, transactions[4].to, transactions[4].narrative, transactions[4].amount);


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

