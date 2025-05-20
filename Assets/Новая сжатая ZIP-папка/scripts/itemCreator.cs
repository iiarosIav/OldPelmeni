using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Items/New Item")]
public class itemCreator : ItemScriptableObject
{

    private void Start()
    {
        itemType = ItemType.Food;
    }
}
