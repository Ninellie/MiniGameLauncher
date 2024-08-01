using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MiniGameIconsLoader : MonoBehaviour
{
    [SerializeField] private string path;
    
    private void Awake()
    {
        // Пытаемся загрузить с сервера.
        var miniGames = GetMiniGamePrefabs(path);
        
        
        // Если не получается
        
        // Используем PlayerPrefs
        
        
        
    }


    private async Task<MiniGameList> GetMiniGamePrefabs(string path)
    {
        var handle = Addressables.LoadAssetAsync<MiniGameList>(path);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        Debug.LogError("Failed to load MiniGameList from address: " + path);
        return null;
    }
}