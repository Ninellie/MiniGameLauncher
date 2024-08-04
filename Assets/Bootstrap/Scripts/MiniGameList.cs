using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create MiniGamesList", fileName = "MiniGamesList", order = 0)]
public class MiniGameList : ScriptableObject
{
    [SerializeField] private List<MiniGameData> list;

    public List<MiniGameData> List => list;
}