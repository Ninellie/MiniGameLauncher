using System;
using UnityEngine;
using UnityEngine.Events;

namespace DriverGame.Game
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI text;

        public UnityAction OnTimeOut { get; set; }
        public float TimeLeft { get; private set; }

        private bool _isRunning;
        
        public void SetTimeLimit(float timeLimit)
        {
            TimeLeft = timeLimit;

            SetText(timeLimit);
        }

        public void StartCountdown()
        {
            _isRunning = true;
        }

        public void StopTimer()
        {
            _isRunning = false;
        }

        private void Update()
        {
            if (!_isRunning) return;
            
            TimeLeft -= Time.deltaTime;
            TimeLeft = Mathf.Max(0.0f, TimeLeft);

            SetText(TimeLeft);

            if (!(TimeLeft <= 0.0f)) return;
            
            _isRunning = false;

            OnTimeOut?.Invoke();
        }

        private void SetText(float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);

            text.text = timeSpan.ToString("mm\\:ss\\:ff");
        }
    }
}