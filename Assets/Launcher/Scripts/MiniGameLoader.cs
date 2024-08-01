using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public static class MiniGameLoader
{
    public static Scene GetMenu()
    {
        return SceneManager.GetSceneByBuildIndex(0);
    }
    
    public static Scene GetGameSelectionMenu()
    {
        return SceneManager.GetSceneByBuildIndex(1);
    }

    public static async Task<Scene> GetGame(string title)
    {
        // The task is complete. Be sure to check the Status is successful before storing the Result.
        var handle = Addressables.LoadSceneAsync(title);
        return await Task.FromResult(handle.Result.Scene);
    }
}