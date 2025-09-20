using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class Article
{
    public string Title { get; set; }
    public int SymbolCount { get; set; }
    public string Preview { get; set; }

    public override string ToString()
    {
        return $"   Article: {Title}, Symbols: {SymbolCount}, Preview: {Preview}";
    }
}

public class Journal
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public DateTime PublicationDate { get; set; }
    public int Pages { get; set; }
    public List<Article> Articles { get; set; } = new List<Article>();

    public Journal() { }

    public Journal(string title, string publisher, DateTime publicationDate, int pages)
    {
        Title = title;
        Publisher = publisher;
        PublicationDate = publicationDate;
        Pages = pages;
    }

    public override string ToString()
    {
        string articlesInfo = Articles.Count == 0 ? "   No articles" : string.Join("\n", Articles);
        return $"Title: {Title}, Publisher: {Publisher}, " +
               $"Date: {PublicationDate.ToShortDateString()}, Pages: {Pages}\n{articlesInfo}";
    }
}

class Program
{
    static void Main()
    {
        Console.Write("How many journals do you want to enter? ");
        int journalCount = int.Parse(Console.ReadLine());

        Journal[] journals = new Journal[journalCount];

        for (int i = 0; i < journalCount; i++)
        {
            Console.WriteLine($"\nJournal {i + 1}:");

            Console.Write("Enter journal title: ");
            string title = Console.ReadLine();

            Console.Write("Enter journal publisher: ");
            string publisher = Console.ReadLine();

            Console.Write("Enter publication date (yyyy-mm-dd): ");
            DateTime publicationDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter number of pages: ");
            int pages = int.Parse(Console.ReadLine());

            journals[i] = new Journal(title, publisher, publicationDate, pages);

            Console.Write("How many articles in this journal? ");
            int articleCount = int.Parse(Console.ReadLine());

            for (int j = 0; j < articleCount; j++)
            {
                Console.WriteLine($" Article {j + 1}:");

                Console.Write("  Title: ");
                string artTitle = Console.ReadLine();

                Console.Write("  Symbol count: ");
                int symbolCount = int.Parse(Console.ReadLine());

                Console.Write("  Preview: ");
                string preview = Console.ReadLine();

                journals[i].Articles.Add(new Article
                {
                    Title = artTitle,
                    SymbolCount = symbolCount,
                    Preview = preview
                });
            }
        }

        string filePath = "journals.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var j in journals)
            {
                writer.WriteLine(j);
                writer.WriteLine(new string('-', 50));
            }
        }

        Console.WriteLine($"\nJournals saved to {filePath}");

        Console.WriteLine("\nLoaded journals from file:");
        string[] lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }
}
﻿
