using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MiniGameLoader : MonoBehaviour
{
    [SerializeField] private AssetReference miniGameList;
    [SerializeField] private GameObject miniGamePreviewPrefab;
    [SerializeField] private Transform gamesPreviewAnchor;

    private void Start()
    {
        StartCoroutine(CreateGamesPreview());
    }

    private IEnumerator CreateGamesPreview()
    {
        var handler = Addressables.InitializeAsync();

        yield return handler;

        var miniGameListHandle = Addressables.LoadAssetAsync<MiniGameList>(miniGameList);
        
        yield return miniGameListHandle;

        if (miniGameListHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning($"MiniGameListHandleStatus = {miniGameListHandle.Status}");
            yield break;
        }
        
        foreach (var miniGameData in miniGameListHandle.Result.List)
        {
            var gamePreviewHandle = Instantiate(miniGamePreviewPrefab, gamesPreviewAnchor);

            if (gamePreviewHandle.TryGetComponent(out MiniGame gamePreview))
            {
                gamePreview.Data = miniGameData;
                gamePreview.SetTitle();
            }
            else
            {
                Debug.LogWarning("A MiniGame component does not attached to loaded mini game preview prefab");
            }
        }   
    }
}