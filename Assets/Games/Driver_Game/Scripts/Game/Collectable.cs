using System;
using UnityEngine;
using UnityEngine.Events;

namespace DriverGame.Game
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private GameObject particlesPrefab = null;

        public UnityAction OnCollect { get; set; } = null;

        private Transform _transform;
        
        private void Awake()
        {
            _transform = transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (particlesPrefab != null)
            {
                Instantiate(particlesPrefab, _transform.position, _transform.rotation);
            }

            OnCollect?.Invoke();

            gameObject.SetActive(false);
        }
    }
}