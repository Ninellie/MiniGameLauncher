using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameProvider
{
    public Texture2D GetPreviewImage(string path)
    {
        return new Texture2D(100, 100);
    }

    public Scene GetScene(string path)
    {
        // поискать в скачанных
        // если на нашло, загрузить
        return new Scene();
    }
    
    private bool LocalContentAvailabilityCheck()
    {
        // поискать в локальных файлах игру
        
        return true;
    }
}