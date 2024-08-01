using TMPro;
using UnityEngine;

public class ClickerGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string playerPrefsKey;
    [SerializeField] private int clickCount;

    private void Awake()
    {
        clickCount = PlayerPrefs.GetInt(playerPrefsKey, clickCount);
        UpdateText();
    }

    public void Increase()
    {
        clickCount++;
        UpdateText();
    }

    private void UpdateText()
    {
        PlayerPrefs.SetInt(playerPrefsKey, clickCount);
        PlayerPrefs.Save();
        clickCount = PlayerPrefs.GetInt(playerPrefsKey);
        text.SetText(clickCount.ToString());
    }
}
