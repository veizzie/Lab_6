using System;
using System.Text;

public delegate void Handler(int number);

class Sportsman
{
    public int Number { get; }
    private bool hitTarget;
    public bool HitTarget { get { return hitTarget; } }

    public Handler Event1;
    public Handler Event2;

    public Sportsman(int number)
    {
        Number = number;
        Random random = new Random();
        hitTarget = random.Next(2) == 1;
    }

    public void GenerateEvent()
    {
        if (hitTarget)
            Event1?.Invoke(Number);
        else
            Event2?.Invoke(Number);
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Спортсмен {Number}: {(hitTarget ? "Влучив" : "Не влучив")}");
    }
}

class Judge
{
    private int totalSportsmen;
    private int hitsCount;

    public Handler Event3;
    public Handler Event4;

    public Judge(int totalSportsmen)
    {
        this.totalSportsmen = totalSportsmen;
    }

    public void HandleEvent1(int number)
    {
        hitsCount++;
    }

    public void HandleEvent2(int number)
    {
    }

    public void GenerateEvents()
    {
        if (hitsCount > totalSportsmen / 2)
        {
            Event3?.Invoke(0);
        }
        else
        {
            Event4?.Invoke(0);
        }
    }

    public void DisplayResult()
    {
        if (hitsCount > totalSportsmen / 2)
        {
            Console.WriteLine("Молодці");
        }
        else
        {
            Console.WriteLine("Потрібно більше тренуватися");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        int totalSportsmen = 10; // Змініть кількість спортсменів за потреби
        Judge judge = new Judge(totalSportsmen);

        Sportsman[] sportsmen = new Sportsman[totalSportsmen];
        for (int i = 0; i < totalSportsmen; i++)
        {
            sportsmen[i] = new Sportsman(i + 1);
            sportsmen[i].Event1 = judge.HandleEvent1;
            sportsmen[i].Event2 = judge.HandleEvent2;
        }

        foreach (var sportsman in sportsmen)
        {
            sportsman.GenerateEvent();
            sportsman.DisplayInfo();
        }

        judge.GenerateEvents();
        judge.DisplayResult();
    }
}
