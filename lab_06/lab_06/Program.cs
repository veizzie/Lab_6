using System;
using System.Text;

class Sportsman
{
    public int Number { get; }
    private bool hitTarget;
    public bool HitTarget { get { return hitTarget; } }

    public event EventHandler Event1;
    public event EventHandler Event2;

    public Sportsman(int number)
    {
        Number = number;
        Random random = new Random();
        hitTarget = random.Next(2) == 1;
    }

    public void GenerateEvent()
    {
        if (hitTarget)
            Event1?.Invoke(this, EventArgs.Empty);
        else
            Event2?.Invoke(this, EventArgs.Empty);
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

    public event EventHandler Event3;
    public event EventHandler Event4;

    public Judge(int totalSportsmen)
    {
        this.totalSportsmen = totalSportsmen;
    }

    public void HandleEvent1(object sender, EventArgs e)
    {
        hitsCount++;
    }

    public void HandleEvent2(object sender, EventArgs e)
    {
    }

    public void GenerateEvents()
    {
        if (hitsCount > totalSportsmen / 2)
        {
            Event3?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Event4?.Invoke(this, EventArgs.Empty);
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
            sportsmen[i].Event1 += judge.HandleEvent1;
            sportsmen[i].Event2 += judge.HandleEvent2;
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
