using System;
using System.Reflection;
using Game.GameStart;
using Game.Level;
using UniRx;
using UnityEngine;

namespace Game
{
    public class GameInitialization : BaseLifecycle
    {
        public enum GameState : byte
        {
            Start,
            Level
        }
        public struct Dependency
        {//Используем внедрение зависимостей. То есть вместо того, чтобы самостоятельно их получать - нам их предоставляет класс в иерархии выше
            public ViewProvider viewProvider;
        }
        //классы с названием инициализация создают и инитят вью и логику.
        // под инитом мы подразумеваем как предоставление зависимостей для вью так и связывание методов у вью и логики
        //также на их базе можно сделать стейт машину как в примере ниже
        //получившаяся группа классов имеет время жизни, например эта - живет все время жизни приложения
        
        public GameInitialization(Dependency dependency)
        {
            IDisposable initialization = null;
            //Мы используем реактивные поля, на их изменение можно подписаться, важно не забывать уничтожать подписки вовремя
            //поэтому через специальный метод из базового класса мы складываем все в сет, чтобы разом в нужный момент все почистить через вызов Dispose
            //временем жизни управляет родительский объект , то есть объект, который создал текущий, он и задиспоузит класс в нужное время
            var state = AddToDisposable(new ReactiveProperty<GameState>());
            state.Subscribe(val =>
            {
                initialization?.Dispose();
                //У нас есть два состояния - старт игры и сама игра. Переключение происходит тут. Можно не пересоздавать
                // а закешировать эти классы, чтобы избежать лишних аллокаций. Выбор на стороне реализующего
                switch (val)
                {
                    case GameState.Start:
                        initialization = AddToDisposable(new GameStartInitialization(new GameStartInitialization.Dependency
                        {
                            gameState = state,
                            viewContent = new PrefabContent<GameStartView>(dependency.viewProvider.GameStartViewPrefab),
                            root = dependency.viewProvider.UiRoot.transform
                        }));
                        break;
                    case GameState.Level:
                        initialization =
                            AddToDisposable(new LevelInitialization(new LevelInitialization.Dependency
                            {
                                state = state,
                                viewContent = new PrefabContent<LevelView>(dependency.viewProvider.LevelViewPrefab) 
                            }));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(val), val, null);
                }
            });
            //даже тут есть какая то минимальная логика в классе Game. важно чтобы бизнес логика была разделена с логикой инита, поэтому выносим
            AddToDisposable(new Game(state));
            GetType().GetConstructor(null).Invoke(null);
            //Activator.CreateInstance(GetType());
        }
    }
}