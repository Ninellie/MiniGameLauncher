using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

[Serializable]
public class MiniGameData
{
    [SerializeField] private string title;
    [SerializeField] private AssetReference gameScene;

    public string Title => title;
    public AssetReference GameScene => gameScene;
}