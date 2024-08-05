using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    [SerializeField] private MiniGameData data;
    [SerializeField] private TextMeshProUGUI title;
    //[SerializeField] private bool isContentLoaded;
    [SerializeField] private Button playButton;
    [SerializeField] private Image playButtonImage;
    [SerializeField] private GameObject loadButton;
    [SerializeField] private GameObject unloadButton;

    public MiniGameData Data
    {
        get => data;
        set => data = value;
    }

    private void Start()
    {
        title = GetComponentInChildren<TextMeshProUGUI>();
        
        // Я устал T_T
        playButton.interactable = true;
        loadButton.SetActive(false);
        unloadButton.SetActive(false);

        //StartCoroutine(CheckIfContentLoaded());
    }

    public void SetTitle()
    {
        title.text = data.Title.ToUpper();
    }

    // Вызывается на кнопку Play
    public void Play()
    {
        //if (!isContentLoaded) return;

        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        var handle = Addressables.LoadSceneAsync(data.GameScene);
        yield return handle;
        
        if (handle.Status == AsyncOperationStatus.Succeeded)
            yield return handle.Result.ActivateAsync();
    }
    
    /*
    // Вызывается на кнопку Load 
    public void Load()
    {
        if (isContentLoaded) return;
        StartCoroutine(LoadAsync());
    }

    // Вызывается на кнопку Unload 
    public void Unload()
    {
        StartCoroutine(UnloadAsync());
    }

    private IEnumerator UnloadAsync()
    {
        if (!isContentLoaded) yield break;
        
        Addressables.ClearDependencyCacheAsync(data.GameScene);
        
        isContentLoaded = false;
        
        UpdateButtons();
    }
    
    private IEnumerator LoadAsync()
    {
        loadButton.SetActive(false);
        playButtonImage.fillAmount = 0;
        
        var downloadSizeHandle = Addressables.GetDownloadSizeAsync(data.GameScene);
        yield return downloadSizeHandle;
        
        if (downloadSizeHandle.Result > 0)
        {
            var dependenciesHandle = Addressables.DownloadDependenciesAsync(data.GameScene);

            yield return dependenciesHandle;
            
            if (dependenciesHandle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning($"{dependenciesHandle.Status}");
                yield break;
            }
        }
        
        isContentLoaded = true;

        UpdateButtons();
    }

    private IEnumerator CheckIfContentLoaded()
    {
        var downloadSizeHandle = Addressables.GetDownloadSizeAsync(data.GameScene);
        yield return downloadSizeHandle;
        isContentLoaded = downloadSizeHandle.Result <= 0;
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        unloadButton.SetActive(isContentLoaded);
        loadButton.SetActive(!isContentLoaded);
        playButton.interactable = isContentLoaded;
    }
    */
}