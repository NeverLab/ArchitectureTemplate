using System;
using Game.Level.Player;
using TMPro;
using UniRx;
using UnityEngine;

namespace Game.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private float _tutorTime;
        [SerializeField] private TMP_Text _tutorText;
        [Serializable]
        public class PlayerComponentsProvider
        {
        }

        [SerializeField] private PlayerView _viewProvider;
        public PlayerView PlayerComponents => _viewProvider;
        public void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(_tutorTime)).Subscribe(_ => _tutorText.gameObject.SetActive(false))
                .AddTo(this);
        }
    }
}