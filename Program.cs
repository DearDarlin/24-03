using System;
using System.IO;
using System.Text.Json.Serialization;

[Serializable]
public class Journal
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public DateTime PublicationDate { get; set; }
    public int Pages { get; set; }
    public Journal()
    {

    }

    public Journal(string title, string publisher, DateTime publicationDate, int pages)
    {
        Title = title;
        Publisher = publisher;
        PublicationDate = publicationDate;
        Pages = pages;
    }

    public override string ToString()
    {
        return $"Title: {Title}, Publisher: {Publisher}, Publication Date: {PublicationDate.ToShortDateString()}, Pages: {Pages}";
    }

    public string toFileString()
    {
        return $"{Title},{Publisher},{PublicationDate.ToShortDateString()},{Pages}";
    }

    public static Journal fromFileString(string fileString)
    {
        var parts = fileString.Split(',');
        if (parts.Length != 4)
            throw new FormatException("Invalid file string format");
        return new Journal
        {
            Title = parts[0],
            Publisher = parts[1],
            PublicationDate = DateTime.Parse(parts[2]),
            Pages = int.Parse(parts[3])
        };
    }

}

class Program
{
    static void Main()
    {
        Console.Write("Enter journal title: ");
        string title = Console.ReadLine();

        Console.Write("Enter journal publisher: ");
        string publisher = Console.ReadLine();

        Console.Write("Enter publication date (yyyy-mm-dd): ");
        DateTime publicationDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter number of pages: ");
        int pages = int.Parse(Console.ReadLine());

        Journal journal = new Journal(title, publisher, publicationDate, pages);
        Console.WriteLine("Journal created:");
        Console.WriteLine(journal.ToString());
        string filePath = "journal.txt";
        File.WriteAllText(filePath, journal.toFileString());
        Console.WriteLine($"Journal saved to {filePath}");
        string fileContent = File.ReadAllText(filePath);
        Journal loadedJournal = Journal.fromFileString(fileContent);
        Console.WriteLine("Journal loaded from file:");
        Console.WriteLine(loadedJournal.ToString());
       
    }
}