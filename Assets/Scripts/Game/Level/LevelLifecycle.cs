using UniRx;

namespace Game.Level
{
    public class LevelLifecycle : BaseLifecycle
    {
        public struct Dependency
        {
            public ReactiveProperty<GameInitialization.GameState> gameState;
        }

        private readonly ReactiveProperty<GameInitialization.GameState> _gameState;
        public LevelLifecycle(Dependency dependency)
        {
            _gameState = dependency.gameState;
        }

        public void LevelEnd()
        {
            _gameState.Value = GameInitialization.GameState.Start;
        }
    }
}