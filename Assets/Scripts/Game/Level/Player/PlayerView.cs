using UnityEngine;

namespace Game.Level.Player
{
    public class PlayerView : MonoBehaviour, IView<PlayerView.Dependency>
    {
        [SerializeField] private BoxCollider _player;
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private float _force;
        public BoxCollider PlayerCollider => _player;
        public Rigidbody PlayerRigidbody => _playerRigidbody;
        public float Force => _force;
        public struct Dependency
        {
            public uint scale;
        }

        public void Init(Dependency dependency)
        {
            throw new System.NotImplementedException();
        }
    }
}