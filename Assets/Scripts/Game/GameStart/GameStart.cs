using UniRx;

namespace Game.GameStart
{
    public class GameStart : BaseLifecycle
    {
        private readonly ReactiveProperty<GameInitialization.GameState> _gameState;
        public GameStart(ReactiveProperty<GameInitialization.GameState> gamestate)
        {
            _gameState = gamestate;
        }
        //в примере бизнес логики немного, но нужно обратить внимание на то, как она вынесена
        public void StartGame()
        {
            _gameState.Value = GameInitialization.GameState.Level;
        }
    }
}