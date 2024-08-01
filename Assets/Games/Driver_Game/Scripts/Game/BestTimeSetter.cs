using System;
using UnityEngine;

namespace DriverGame.Game
{
    public class BestTimeSetter : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI text;

        public void SetBestTime(float time)
        {
            var timeSpan = TimeSpan.FromSeconds(time);
            var timeText = timeSpan.ToString("mm\\:ss\\:ff");
            text.SetText($"BEST TIME \n {timeText}");
        }
    }
}