using Game;
using UnityEngine;

public class EnterPoint : MonoBehaviour
{
    [SerializeField] private ViewProvider _viewProvider;
    //Входная точка, в этом месте начинается инит всего. Вьюхи тянутся через провайдеры
    void Start()
    {
        new GameInitialization(new GameInitialization.Dependency
        {
            viewProvider = _viewProvider
        });
    }
}
