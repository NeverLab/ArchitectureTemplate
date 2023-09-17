using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Game.Level
{
    public class PlayerGameplay : BaseLifecycle
    {
        public struct Dependency
        {
            public BoxCollider player;
            public Rigidbody playerRigidbody;
            public ReactiveCommand onFail;
            public float force;
        }

        public PlayerGameplay(Dependency dependency)
        {
            //тут можно посмотреть как в обычном классе можно манипулировать юнити контекстом с помощью unirx. показываю как пример, при такой реализации вью можно вообще держать как провайдеры компонентов
            AddToDisposable(Observable.EveryUpdate().Subscribe(_ =>
            {
                if (Input.GetMouseButton(0))
                {
                    var pos = Input.mousePosition;
                    if (Camera.main != null)
                    {
                        var ray = Camera.main.ScreenPointToRay(pos);
                        if(Physics.Raycast(ray))
                        {
                            dependency.playerRigidbody.AddForce(Vector3.up * dependency.force, ForceMode.Impulse);
                        }
                    }
                }
            }));
            dependency.player.OnTriggerEnterAsObservable().TakeUntilDestroy(dependency.player).Subscribe(_ =>
            {
                dependency.onFail.Execute();
            });
        }
    }
}