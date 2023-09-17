using UniRx;

namespace Game.Level.Player
{
    public class PlayerInitialization : BaseLifecycle
    {
        public struct Dependency
        {
            public PlayerView PlayerView;
            public ReactiveCommand onFail;
        }

        public PlayerInitialization(Dependency dependency)
        {
            AddToDisposable(new PlayerGameplay(new PlayerGameplay.Dependency//вот тут есть пример, как из обычного класса взаимодействовать с юнити контекстом
            {//во первых мы пробрасываем все необходимые компоненты в зависимости
                force = dependency.PlayerView.Force,
                player = dependency.PlayerView.PlayerCollider,
                playerRigidbody = dependency.PlayerView.PlayerRigidbody,
                onFail = dependency.onFail
            }));
        }
    }
}