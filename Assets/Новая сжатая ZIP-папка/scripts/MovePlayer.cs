using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer spqr;

    float horizontal;
    float vertical;
    private float moveInput;
    public bool flipOne = true;
    Vector2 vcTwo;
    public static int collectedAmount = 0;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDaley;
    Rigidbody2D rb;
    public int health;
    private Camera mainCamera;
    public float reachDistance = 3f;


    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    [SerializeField] private Transform player;
    RaycastHit2D hit;
        


    public float runSpeed = 20.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spqr = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        float ShootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if ((ShootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDaley)
        {
            Shoot(ShootHor, shootVert);
            lastFire = Time.time;
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, reachDistance );


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hit.collider.gameObject.GetComponent<Item>() != null)
            {
                AddItem(hit.collider.gameObject.GetComponent<Item>().item, hit.collider.gameObject.GetComponent<Item>().amount);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector3
        (
         (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
         (y < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
         0
        );
    }


    public void RemoveItemFromSlot(int slotId)
    {
        InventorySlot slot = slots[slotId];

        slot.item = null;
        slot.isEmpty = true;
        slot.amount = 0;
        slot.iconGO.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        slot.iconGO.GetComponent<Image>().sprite = null;
        slot.itemAmountText.text = "";
    }


    public void AddItemToSlot(ItemScriptableObject _item, int _amount, int slotId)
    {
        InventorySlot slot = slots[slotId];
        slot.item = _item;
        slot.isEmpty = false;
        slot.SetIcon(_item.icon);

        if (_amount <= _item.maximumAmount)
        {
            slot.amount = _amount;
            if (slot.item.maximumAmount != 1)
            {
                slot.itemAmountText.text = slot.amount.ToString();
            }
        }
        else
        {
            slot.amount = _item.maximumAmount;
            _amount -= _item.maximumAmount;
            if (slot.item.maximumAmount != 1)
            {
                slot.itemAmountText.text = slot.amount.ToString();
            }
        }
    }




    private void FixedUpdate()
    {
        body.linearVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if (flipOne == false && horizontal > 0)
        {
            Flip();
        }
        else if (flipOne == true && horizontal < 0)
        {
            Flip();
        }

    }

    void Flip()
    {
        spqr.flipX = true;
        flipOne = !flipOne;
        Vector3 vcOne = transform.localScale;
        vcOne.x *= -1;
        transform.localScale = vcOne;
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
    }

    public void AddItem(ItemScriptableObject _item, int _amount)
    {

        int amount = _amount;
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                if (slot.amount + amount <= _item.maximumAmount)
                {
                    slot.amount += amount;
                    slot.itemAmountText.text = slot.amount.ToString();
                    return;
                }
                else
                {
                    amount -= _item.maximumAmount - slot.amount;
                    slot.amount = _item.maximumAmount;
                    slot.itemAmountText.text = slot.amount.ToString();
                }
                continue;
            }
        }

        bool allFull = true;
        foreach (InventorySlot inventorySlot in slots)
        {
            if (inventorySlot.isEmpty)
            {
                allFull = false;
                break;
            }
        }
        if (allFull)
        {
            GameObject itemObject = Instantiate(_item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
            itemObject.GetComponent<Item>().amount = _amount;
        }

        foreach (InventorySlot slot in slots)
        {
            if (amount <= 0)
                return;
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);

                if (amount <= _item.maximumAmount)
                {
                    slot.amount = amount;
                    if (slot.item.maximumAmount != 1)
                    {
                        slot.itemAmountText.text = slot.amount.ToString();
                    }
                    break;
                }
                else
                {
                    slot.amount = _item.maximumAmount;
                    amount -= _item.maximumAmount;
                    if (slot.item.maximumAmount != 1)
                    {
                        slot.itemAmountText.text = slot.amount.ToString();
                    }
                }

                allFull = true;
                foreach (InventorySlot inventorySlot in slots)
                {
                    if (inventorySlot.isEmpty)
                    {
                        allFull = false;
                        break;
                    }
                }
                if (allFull)
                {
                    GameObject itemObject = Instantiate(_item.itemPrefab, player.position + Vector3.up + player.forward, Quaternion.identity);
                    itemObject.GetComponent<Item>().amount = amount;
                    Debug.Log("Throw out");
                    return;
                }
            }
        }
    }
}
