using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace DriverGame.Game
{
    [RequireComponent(typeof(CollectableManager))]
    public class GameManager : MonoBehaviour
    {
        [Header("Game")]
        [SerializeField] private Countdown countdown;
        [SerializeField] private BestTimeSetter bestTimeIndicator;
        [SerializeField, Tooltip("Delay before the countdown starts")] private float startDelay;
        [SerializeField, Tooltip("Time limit of the countdown")] private float timeLimit = 10.0f;
        [SerializeField] private UnityEvent onComplete;
        [SerializeField] private UnityEvent onFail;
        
        private CollectableManager _collectableManager;
        private Vehicle _playerVehicle;

        private bool _isGameOver;

        private void Awake()
        {
            _playerVehicle = GameObject.FindWithTag("Player").GetComponent<Vehicle>();

            _collectableManager = GetComponent<CollectableManager>();
            var bestTime = PlayerPrefs.GetFloat("BestTime", timeLimit);
            bestTimeIndicator.SetBestTime(bestTime);
            
            _collectableManager.onAllCollected += OnAllCollected;
            countdown.OnTimeOut += OnTimeOut;
        }

        private IEnumerator Start()
        {
            countdown.SetTimeLimit(timeLimit);

            yield return new WaitForSeconds(startDelay);

            countdown.StartCountdown();

            _isGameOver = false;
        }

        private void OnAllCollected() => EndGame(won: true);
        
        private void OnTimeOut() => EndGame(won: false);
        
        private void EndGame(bool won)
        {
            if (_isGameOver) return;
            _isGameOver = true;
            
            countdown.StopTimer();
            
            _playerVehicle.CanMove = false;
            _playerVehicle.Rigidbody.drag = 2.0f;

            if(won)
            {
                SaveTime();
                onComplete?.Invoke(); 
            }
            else
            {
                onFail?.Invoke();
            }
        }

        private void SaveTime()
        {
            var time = countdown.TimeLeft;
            var bestTime = PlayerPrefs.GetFloat("BestTime", timeLimit);
            
            if (time < bestTime)
            {
                bestTimeIndicator.SetBestTime(time);
                PlayerPrefs.SetFloat("BestTime", time);
            }
            PlayerPrefs.Save();
        }
    }
}