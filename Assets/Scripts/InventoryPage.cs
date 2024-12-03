using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Inventory.UI;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private InventoryDescription itemDescription;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<InventoryItem> listOfItems = new List<InventoryItem>();

    
    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitialiseInventory(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDropped += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;

        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listOfItems.Count > itemIndex)
        {
            listOfItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleShowItemActions(InventoryItem inventoryItem)
    {
        
    }

    private void HandleEndDrag(InventoryItem inventoryItem)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(inventoryItem);
        OnStartDragging?.Invoke(index);
        
    }

    public void  CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(InventoryItem inventoryItem)
    {
        int index = listOfItems.IndexOf(inventoryItem);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();        
    }

    private void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach (InventoryItem item in listOfItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }
}
