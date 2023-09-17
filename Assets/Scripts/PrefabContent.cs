using System;
using Object = UnityEngine.Object;

public class PrefabContent<T> : IContent<T> where T : Object
{
    private readonly T _prefab;
    private T _instance;

    public PrefabContent(T prefab)
    {
        _prefab = prefab;
    }
    public T Get()
    {
        if (_instance == null)
        {
            _instance = Object.Instantiate(_prefab);
        }

        return _instance;
    }

    public void GetAsync(Action<T> onComplete)
    {
        onComplete?.Invoke(Get());
    }
}