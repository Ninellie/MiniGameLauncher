using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private MiniGameData data;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private bool isContentLoaded;
    [SerializeField] private Button playButton;
    [SerializeField] private Image playButtonImage;
    [SerializeField] private GameObject loadButton;
    [SerializeField] private GameObject unloadButton;

    private AsyncOperationHandle<SceneInstance> _handle;

    private void Awake()
    {
        loadButton.SetActive(true);
        unloadButton.SetActive(false);
        playButton.interactable = false;
        playButtonImage.fillAmount = 0;
        title = GetComponentInChildren<TextMeshProUGUI>();
        SetTitle();
    }

    private void SetTitle()
    {
        title.text = data.Title;
    }

    // Вызывается на кнопку Play
    public void Play()
    {
        if (!isContentLoaded) return;
        if (!_handle.IsDone) return;
        _handle.Result.ActivateAsync();
    }
    
    // Вызывается на кнопку Load 
    public void Load()
    {
        if (isContentLoaded) return;
        StartCoroutine(LoadAsync());
    }

    // Вызывается на кнопку Unload 
    public void Unload()
    {
        if (!isContentLoaded) return;
        StartCoroutine(UnloadAsync());
    }

    private IEnumerator UnloadAsync()
    {
        playButton.interactable = false;
        unloadButton.SetActive(false);
        
        var unloadHandle = Addressables.UnloadSceneAsync(_handle.Result);
        yield return unloadHandle;
        isContentLoaded = false;
        
        loadButton.SetActive(true);
    }

    private IEnumerator LoadAsync()
    {
        loadButton.SetActive(false);
        playButtonImage.fillAmount = 0;
        
        _handle = Addressables.LoadSceneAsync(data.GameScene, LoadSceneMode.Single, false);
        yield return _handle;
        
        playButtonImage.fillAmount = _handle.PercentComplete;
        
        if (_handle.Status != AsyncOperationStatus.Succeeded) yield break;
        isContentLoaded = true;
        
        unloadButton.SetActive(true);
        playButton.interactable = true;
    }
}