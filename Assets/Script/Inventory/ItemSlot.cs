using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;


public class ItemSlot : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public int quantity = 1;
    public Sprite itemIcon;
    public bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int MaxStackSize = 99;

    public void AddItem(ItemDetails itemDetails)
    {
        itemName = itemDetails.ItemName;
        // quantity = itemDetails.Quantity;
        itemIcon = itemDetails.ItemIcon;
        AddQuantity(itemDetails.Quantity);
        // isFull = true;


        // quantityText.text = quantity.ToString();
        // quantityText.enabled = true;
        // itemImage.sprite = itemIcon;

    }

    public bool CanStackWith(string otherItemName)
    {
        return !isFull && itemName == otherItemName;
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
        isFull = quantity >= MaxStackSize;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemIcon;

    }
}
