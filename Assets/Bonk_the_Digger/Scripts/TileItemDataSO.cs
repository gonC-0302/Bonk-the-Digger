using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileItemData", menuName = "Scriptable Objects/TileItemDataSO")]
public class TileItemDataSO : ScriptableObject
{
    public List<Item> itemsList = new List<Item>();

    [Serializable]
    public class Item
    {
        public TileType type;
        public Sprite spr;
    }
}
