using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameStart
{
    public class GameStartView : MonoBehaviour, IView<GameStartView.Dependency>
    {
        [SerializeField] private Button _startGameButton;
        public struct Dependency
        {
            public ReactiveCommand onStartGame;//обрати внимание что зависимости не от классов а от реактивных полей и коммманд
        }

        public void Init(Dependency dependency)//вью держим максимально тонкой, по сути обрабатываем нажатие и дергаем проброшенную команду
        {
            _startGameButton.OnClickAsObservable().TakeUntilDestroy(gameObject).Subscribe(_ =>
            {
                dependency.onStartGame.Execute();
            });//синтаксис от unirx 
        }
    }
}