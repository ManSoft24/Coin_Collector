using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public string itemDescription;
    public int quantity = 1;
    public Sprite itemIcon;
    public bool isFull;

    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int MaxStackSize = 99;

    //selected item
    [SerializeField] public GameObject selectedPanel;
    public bool isItemSelected;
    public InventoryManager inventoryManager;

    public void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }
    
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }

    public void LeftClick()
    {
        Debug.Log("Left click, ditekan");
        inventoryManager.DisselectAllSlot();

        selectedPanel.SetActive(true);
        isItemSelected = true;
    }

    public void RightClick()
    {
        Debug.Log("Right click, ditekan");
        
    }
}
