using Game.Level.Player;
using UniRx;
using UnityEngine;

namespace Game.Level
{
    public class LevelInitialization : BaseLifecycle
    {
        public struct Dependency
        {
            public ReactiveProperty<GameInitialization.GameState> state;
            public IContent<LevelView> viewContent;
        }

        private LevelView _view;
        public LevelInitialization(Dependency dependency)
        {
            //у классов из этой группы время жизни - пока играется уровень
            var onfail = AddToDisposable(new ReactiveCommand());
            _view = dependency.viewContent.Get();
            var levelLifecycle = AddToDisposable(new LevelLifecycle(new LevelLifecycle.Dependency//Классов логики может быть и несколько
            {//количество классов не регламентировано, но важно чтобы они составляли собой группу
                gameState = dependency.state
            }));
            AddToDisposable(onfail.Subscribe(_ =>
            {
                levelLifecycle.LevelEnd();//подписываем соответствующий метод логики на событие
            }));
            AddToDisposable(new PlayerInitialization(new PlayerInitialization.Dependency//для плеера у нас своя иерархия, дочерняя к уровню тк пдеер существует пока уровень создан
            {
                PlayerView = _view.PlayerComponents,
                onFail = onfail
            }));
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            Object.Destroy(_view.gameObject);
        }
    }
}