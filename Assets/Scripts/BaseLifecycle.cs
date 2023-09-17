using System;
using System.Collections.Generic;

public class BaseLifecycle : IDisposable
{
    private readonly HashSet<IDisposable> _disposables = new HashSet<IDisposable>();
    public void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
        OnDispose();
    }
    protected virtual void OnDispose(){}

    protected void RemoveFromDisposable<T>(T disposable) where T : IDisposable
    {
        if (!_disposables.Contains(disposable)) return;
        _disposables.Remove(disposable);
    }
    protected T AddToDisposable<T>(T disposable) where T : IDisposable
    {
        if (_disposables.Contains(disposable)) throw new ArgumentException(disposable + ": already added");
        _disposables.Add(disposable);
        return disposable;
    }
}