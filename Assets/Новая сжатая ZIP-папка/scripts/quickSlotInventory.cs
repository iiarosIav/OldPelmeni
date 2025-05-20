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
        // ���������� �������� �����
        if (mw > 0.1)
        {
            // ����� ���������� ���� � ������ ��� �������� �� �������

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // ����� ��������� ��� �������� ����� �� ������� ��������� �� ����� (��������� ������ ��� �������, �������� �������� ...)

            // ���� ������ ��������� ����� ������ � ���� ����� currentQuickslotID ����� ���������� �����, �� �������� ��� ������ ���� (������ ���� ��������� �������)
            if (currentQuickslotID >= quickslotParent.childCount - 1)
            {
                currentQuickslotID = 0;
            }
            else
            {
                // ���������� � ����� currentQuickslotID ��������
                currentQuickslotID++;
            }
            // ����� ���������� ���� � ������ ��� �������� �� "���������"

            SelectSlot();
            // ����� ��������� ��� �������� ����� �� �������� ���� (�������� ������ ��� �������, �������� �������� ...)

        }
        if (mw < -0.1)
        {
            // ����� ���������� ���� � ������ ��� �������� �� �������

            quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;
            // ����� ��������� ��� �������� ����� �� ������� ��������� �� ����� (��������� ������ ��� �������, �������� �������� ...)


            // ���� ������ ��������� ����� ����� � ���� ����� currentQuickslotID ����� 0, �� �������� ��� ��������� ����
            if (currentQuickslotID <= 0)
            {
                currentQuickslotID = quickslotParent.childCount - 1;
            }
            else
            {
                // ��������� ����� currentQuickslotID �� 1
                currentQuickslotID--;
            }
            // ����� ���������� ���� � ������ ��� �������� �� "���������"

            SelectSlot();
            // ����� ��������� ��� �������� ����� �� �������� ���� (�������� ������ ��� �������, �������� �������� ...)

        }
        // ���������� �����
        for (int i = 0; i < quickslotParent.childCount; i++)
        {
            // ���� �� �������� �� ������� 1 �� 5 ��...
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                // ��������� ���� ��� ��������� ���� ����� ����� ������� � ��� ��� ������, ��
                if (currentQuickslotID == i)
                {
                    // ������ �������� "selected" �� ���� ���� �� "not selected" ��� ��������
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
                // ����� �� ������� �������� � ����������� ����� � ������ ���� ������� �� ��������
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
