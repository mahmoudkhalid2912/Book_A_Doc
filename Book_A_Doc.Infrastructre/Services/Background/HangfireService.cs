using Hangfire;
using System.Linq.Expressions;

namespace Book_A_Doc.Infrastructre.Services.Background;

public class HangfireService : IBackgroundService
{
    public void Enqueue(Expression<Func<Task>> methodCall)
    {
        BackgroundJob.Enqueue(methodCall);
    }

    public void Enqueue<T>(Expression<Func<T, Task>> methodCall)
    {
        BackgroundJob.Enqueue(methodCall);
    }
}
