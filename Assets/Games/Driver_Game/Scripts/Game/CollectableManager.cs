using UnityEngine;
using UnityEngine.Events;

namespace DriverGame.Game
{
    public class CollectableManager : MonoBehaviour
    {
        public UnityAction onAllCollected;

        private Collectable[] _collectables;
        private int _amount = 0;
        private int _collected = 0;

        private void Awake()
        {
            _collectables = FindObjectsOfType<Collectable>();

            if (_collectables == null) return;
            
            _amount = _collectables.Length;

            foreach (var collectable in _collectables)
            {
                collectable.OnCollect += OnCollect;
            }
        }

        private void OnCollect()
        {
            _collected++;

            if (_collected != _amount) return;
            
            onAllCollected?.Invoke();
        }
    }
}