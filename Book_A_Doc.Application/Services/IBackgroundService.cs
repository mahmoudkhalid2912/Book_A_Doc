using System.Linq.Expressions;

public interface IBackgroundService
{
    void Enqueue(Expression<Func<Task>> methodCall);

    void Enqueue<T>(Expression<Func<T, Task>> methodCall);
}
