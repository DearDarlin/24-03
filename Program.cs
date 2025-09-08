using System;
using System.IO;

[Serializable]
public class  Fraction
{
    public int Numerator { get; set; }
    public int Denominator { get; set; }
    public Fraction() { }
    public Fraction(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator == 0?1:denominator;
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }

    public string ToFileString()
    {
        return $"{Numerator},{Denominator}";
    }

    public static Fraction FromFileString(string fileString)
    {
        var parts = fileString.Split(',');
        if (parts.Length != 2)
            throw new FormatException("Invalid format");
        return new Fraction(int.Parse(parts[0]), int.Parse(parts[1]));
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of fractions:");
        int n = int.Parse(Console.ReadLine());
        Fraction[] fractions = new Fraction[n];
        for (int i =0; i<n; i++)
        {
            Console.WriteLine($"Fraction {i + 1}:");
            Console.Write("Numerator: ");
            int numerator = int.Parse(Console.ReadLine());
            Console.Write("Denominator: ");
            int denominator = int.Parse(Console.ReadLine());
            fractions[i] = new Fraction(numerator, denominator);

        }
        string filePath = "fractions.txt";
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var fraction in fractions)
            {
                writer.WriteLine(fraction.ToFileString());
            }
        }
        Console.WriteLine($"Fractions saved to {filePath}");


        Fraction[] loadedFractions;
        string[] lines = File.ReadAllLines(filePath);
        loadedFractions = new Fraction[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            loadedFractions[i] = Fraction.FromFileString(lines[i]);
        }
        Console.WriteLine("Loaded fractions:");
        foreach (var fraction in loadedFractions)
        {
            Console.WriteLine(fraction);
        }
    }
}