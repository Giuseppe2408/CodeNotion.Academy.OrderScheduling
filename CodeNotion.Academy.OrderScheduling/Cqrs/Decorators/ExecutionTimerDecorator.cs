using System.Diagnostics;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Cqrs.Decorators;

public class ExecutionTimerDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly Timer _timer;

    public ExecutionTimerDecorator(Timer timer)
    {
        _timer = timer;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _timer.StartTimer();

        var result = next.Invoke();

        _timer.EndTimer();

        return result;
    }
}

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