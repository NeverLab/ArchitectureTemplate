using UniRx;
using UnityEngine;

namespace Game.GameStart
{
    public class GameStartInitialization : BaseLifecycle
    {
        public struct Dependency
        {
            public ReactiveProperty<GameInitialization.GameState> gameState;
            public IContent<GameStartView> viewContent;
            public Transform root;
        }

        private readonly GameStartView _view;
        public GameStartInitialization(Dependency dependency)
        {
            //инит меню старта игры, время жизни - пока пользователь не нажмет начало игры
            //как только это произойдет - стейт машина в классе выше переключит стейт и задеспоузит текущий
            var onStartGame = AddToDisposable(new ReactiveCommand());
            _view = dependency.viewContent.Get();
            _view.transform.SetParent(dependency.root);
            _view.Init(new GameStartView.Dependency//инитим вью и передаем нужные зависимости
            {
                onStartGame = onStartGame
            });
            var logic = AddToDisposable(new GameStart(dependency.gameState));//далее инитим логику
            AddToDisposable(onStartGame.Subscribe(_ =>
            {
                logic.StartGame();//и подписываем соответствующие публичные методы логики на события. тоже инит по сути
            }));
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            Object.Destroy(_view.gameObject);//когда будет произведен диспоуз - подчищаем и вьюху
        }
    }
}