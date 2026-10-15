using System;

namespace program;

class Program{
    static void Main(){
        Console.WriteLine("Привіт досліднику! Ця програма допоможе тобі вивести дані прогресу інших дослідників.\n");
        string filePath = "Progress.txt";
        int n = GetValidInt("Введіть кількість дослідників: ");
        var progress = new List<Researcher>();
        for (int i = 0; i < n; i++) {
            Console.WriteLine($"Дослідник {i + 1}:");
            string name = GetValidString("Введіть ім'я дослідника: ");
            int progressValue = GetValidInt("Введіть прогрес дослідника: ");
            progress.Add(new Researcher(name, progressValue));
        }
        File.WriteAllLines(filePath, progress.Select(p => $"{p.Name}: {p.ProgressValue}"));
        var top = File.ReadAllLines(filePath)
            .Select(line => line.Split(':'))
            .Select(parts => new Researcher(parts[0].Trim(), int.Parse(parts[1].Trim())))
            .OrderByDescending(p => p.ProgressValue)
            .Take(3)
            .ToList();
        Console.WriteLine("\nТоп 3 дослідники за прогресом:");
        foreach (var p in top) {
            Console.WriteLine($"{p.Name}: {p.ProgressValue}");
        }
    }
    static int GetValidInt(string message) {
        int value;
        while (true) {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out value)) {
                return value;
            }
            Console.WriteLine("Будь ласка, введіть дійсне ціле число.");
        }
    }
    static string GetValidString(string message) {
        string input;
        do {
            Console.Write(message);
            input = Console.ReadLine()?.Trim() ?? string.Empty;
        } while (string.IsNullOrEmpty(input));
        return input;
    }
}
class Researcher {
    public string Name { get; }
    public int ProgressValue { get; }
    public Researcher(string name, int progressValue) {
        Name = name;
        ProgressValue = progressValue;
    }
}
