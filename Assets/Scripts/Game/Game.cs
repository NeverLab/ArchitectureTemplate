using UniRx;

namespace Game
{
    public class Game : BaseLifecycle

    {
        public Game(ReactiveProperty<GameInitialization.GameState> state)
        {
            state.Value = GameInitialization.GameState.Start;
        }
    }
}