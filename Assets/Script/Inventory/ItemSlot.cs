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

    public ItemDetails ItemDetails => itemDetails;
    private ItemDetails itemDetails;


    public void Start()
    {
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    public void AddItem(ItemDetails item)
    {
        itemDetails = item;
        itemName = item.ItemName;
        itemIcon = item.ItemIcon;
        AddQuantity(item.Quantity);

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
        if (IsEmpty || inventoryManager == null)
        {
            return;
        }
        inventoryManager.SelectSlot(this);
    }

    public void RightClick()
    {
        Debug.Log("Right click, ditekan");
        
    }
    public bool IsEmpty => quantity <= 0;
}
    