using System.Diagnostics;

namespace CodeNotion.Academy.OrderScheduling;

public class Timer
{
    private readonly Stopwatch _sw = new();
    
    public void StartTimer()
    {
        _sw.Start();
    }

    public void EndTimer()
    {
        _sw.Stop();
        Console.WriteLine("Tempo passato {0}", _sw.ElapsedMilliseconds);
    }
}