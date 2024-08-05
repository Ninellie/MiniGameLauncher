using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class MiniGameData
{
    [SerializeField] private string title;
    //[SerializeField] private string key;
    [SerializeField] private AssetReference gameScene;

    public string Title => title;
    
    //public string Key => key;
    
    public AssetReference GameScene => gameScene;
}