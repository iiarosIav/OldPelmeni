using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ItemType { Default, Food, Weapon, Instrument, BuildingBlock }
public class ItemScriptableObject : ScriptableObject
{

    public string itemName;
    public int maximumAmount;
    public GameObject itemPrefab;
    public Sprite icon;
    public ItemType itemType;
    public string itemDescription;
    public bool isConsumeable;
    public string inHandName;
    public bool canBreak;

    [Header("Consumable Characteristics")]
    public float changeHealth;
    public float changeHunger;
    public float changeThirst;


}
