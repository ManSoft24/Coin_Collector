using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, GrabableItem
{
    private bool isInventoryOpen = false;

    InputAction inventoryAction;
    
    [SerializeField] private GameObject InventoryCanvas;


    // Add item to inventory
    [SerializeField] private int slotCount = 16;

    private ItemSlot[] itemSlot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        inventoryAction = InputSystem.actions.FindAction("Inventory");
        Time.timeScale = 1f;

        // initialize item slot
        CreateSlots();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryAction.triggered && InventoryCanvas != null && isInventoryOpen == false)
        {
            Debug.Log("Inventory is Active");
            Time.timeScale = 0f;
            InventoryCanvas.SetActive(true);
            isInventoryOpen = true;
        } else if(inventoryAction.triggered && InventoryCanvas != null && isInventoryOpen == true)
        {
            Debug.Log("Inventory is Inactive");
            Time.timeScale = 1f;
            InventoryCanvas.SetActive(false);
            isInventoryOpen = false;
        }


    }

    public void AddItem(ItemDetails item)
    {
        Debug.Log("Item added to inventory: " + item.ItemName + " Quantity: " + item.Quantity);
        Debug.Log(item.ItemDescription);

        if (itemSlot == null || itemSlot.Length == 0)
        {
            Debug.LogWarning("Inventory has no slots, '" + item.ItemName + "' was lost.");
            return;
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {  
            if (!itemSlot[i].isFull)
            {
                itemSlot[i].AddItem(item);
                return;
            }
        }

        Debug.LogWarning("Inventory is full, '" + item.ItemName + "' was lost.");

    }

    private void CreateSlots()
    {
        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");
        if (slotPrefab == null)
        {
            Debug.LogError("ItemSlot prefab was not found in a Resources folder.", this);
            itemSlot = new ItemSlot[0];
            return;
        }

        // Put the slots inside the GridLayoutGroup container (InventorySlots), if there is one
        Transform parent = InventoryCanvas.transform;
        GridLayoutGroup grid = InventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);
        if (grid != null)
        {
            parent = grid.transform;
        }

        itemSlot = new ItemSlot[slotCount];
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(slotPrefab, parent);
        }
    }

    
    
    
}
