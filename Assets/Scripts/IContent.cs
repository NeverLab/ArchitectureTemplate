using System;

public interface IContent<out T>
{
    T Get();
    void GetAsync(Action<T> onComplete);
}