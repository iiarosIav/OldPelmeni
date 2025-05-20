using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quickSlotInventory : MonoBehaviour
{
    public Transform quickslotParent;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;
    public Transform itemContainer;
    public InventorySlot activeSlot = null;
    public Indicators indicators;

    void Update()
    {
        float mw = Input.GetAxis("Mouse ScrollWheel");
        // Используем колесико мышки
        if (mw > 0.1)
        {
            // Берем предыдущий слот и меняем его картинку на обычную

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // Здесь добавляем что случится когда мы УБЕРАЕМ ВЫДЕЛЕНИЕ со слота (Выключить нужный нам предмет, поменять аниматор ...)

            // Если крутим колесиком мышки вперед и наше число currentQuickslotID равно последнему слоту, то выбираем наш первый слот (первый слот считается нулевым)
            if (currentQuickslotID >= quickslotParent.childCount - 1)
            {
                currentQuickslotID = 0;
            }
            else
            {
                // Прибавляем к числу currentQuickslotID единичку
                currentQuickslotID++;
            }
            // Берем предыдущий слот и меняем его картинку на "выбранную"

            SelectSlot();
            // Здесь добавляем что случится когда мы ВЫДЕЛЯЕМ слот (Включить нужный нам предмет, поменять аниматор ...)

        }
        if (mw < -0.1)
        {
            // Берем предыдущий слот и меняем его картинку на обычную

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // Здесь добавляем что случится когда мы УБЕРАЕМ ВЫДЕЛЕНИЕ со слота (Выключить нужный нам предмет, поменять аниматор ...)


            // Если крутим колесиком мышки назад и наше число currentQuickslotID равно 0, то выбираем наш последний слот
            if (currentQuickslotID <= 0)
            {
                currentQuickslotID = quickslotParent.childCount - 1;
            }
            else
            {
                // Уменьшаем число currentQuickslotID на 1
                currentQuickslotID--;
            }
            // Берем предыдущий слот и меняем его картинку на "выбранную"

            SelectSlot();
            // Здесь добавляем что случится когда мы ВЫДЕЛЯЕМ слот (Включить нужный нам предмет, поменять аниматор ...)

        }
        // Используем цифры
        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            // если мы нажимаем на клавиши 1 по 5 то...
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                // проверяем если наш выбранный слот равен слоту который у нас уже выбран, то
                if (currentQuickslotID == i)
                {
                    // Ставим картинку "selected" на слот если он "not selected" или наоборот
                    if (quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite == notSelectedSprite)
                    {
                        SelectSlot();
                    }
                    else
                    {
                        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
                        activeSlot = null;

                    }
                }
                // Иначе мы убираем свечение с предыдущего слота и светим слот который мы выбираем
                else
                {
                    quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;

                    currentQuickslotID = i;

                    SelectSlot();
                }
            }
        }
    }

    private void SelectSlot()
    {
        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;
        activeSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
    }

    private void RemoveConsumableItem()
    {
        if (quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount <= 1)
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponentInChildren<DragAndDrop>()
                .NullifySlotData();
        }
        else
        {
            quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().amount--;
            quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>().itemAmountText
                .text = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>()
                .amount.ToString();
        }
    }

    private void ChangeCharacteristics()
    {
        InventorySlot itemSlot = quickslotParent.GetChild(currentQuickslotID).GetComponent<InventorySlot>();
        if (itemSlot.item.changeHunger == 0 && itemSlot.item.changeThirst == 0 && itemSlot.item.changeHealth == 0)
            return;

        indicators.ChangeHealthAmount(itemSlot.item.changeHealth);  

        RemoveConsumableItem();
    }
}
