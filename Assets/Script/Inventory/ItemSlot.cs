using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemSlot : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public int quantity = 1;
    public Sprite itemIcon;
    public bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public void AddItem(ItemDetails itemDetails)
    {
        itemName = itemDetails.ItemName;
        quantity = itemDetails.Quantity;
        itemIcon = itemDetails.ItemIcon;
        isFull = true;


        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemIcon;

    }
}
