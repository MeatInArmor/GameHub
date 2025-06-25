using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] public GameController gameController;
    [SerializeField] public CanvasController canvasController;
    [SerializeField] public DragablePanel panel;

    [SerializeField] public SoldierStand soliderStand;
    [SerializeField] public GameObject wonPanel;
    [SerializeField] public ItemObject inventoryItemPrefab;


    [SerializeField] public TextSO gameTexts;
    [SerializeField] public TMP_Text iventoryName;
    [SerializeField] public GameObject content;
    [SerializeField] public List<ItemsSO> equipmentData = new List<ItemsSO>();

    [SerializeField] public List<ItemObject> items = new List<ItemObject>();

 


    public void ResetContent(bool inventoryType, List<ItemsSO> selectedEquipment)
    {

        OpenText();
        for (int i = 0; i < items.Count; i++)
        {
            items[i].checkItem -= CheckCurrentItem;
            items[i].doInventoryOpenClose -= OpenClose;

            Destroy(items[i].gameObject);
        }
        items.Clear();

        equipmentData = selectedEquipment;
       
        int minQueue = inventoryType == false ? 0 : gameController.GetConst("uniform");
        int maxQueue = inventoryType == false ? gameController.GetConst("uniform") : gameController.GetConst("equipment");

        for (int i = 0; i < selectedEquipment.Count; i++) 
        {
            if (selectedEquipment[i].Queue >= minQueue && selectedEquipment[i].Queue < maxQueue)
            {
                ItemObject item = Instantiate(inventoryItemPrefab, content.transform);
                item.checkItem += CheckCurrentItem;
                item.doInventoryOpenClose += OpenClose;
                item.SetData(selectedEquipment[i]);
                items.Add(item);
            }
        }
    }


    public void CheckCurrentItem(ItemObject droppedItem)
    {

        if (droppedItem != null)
        {
            System.Random r = new System.Random();

            int rnd = r.Next(0, droppedItem.sounds.Length - 1);
            int num = soliderStand.CheckEquipped(droppedItem.kitsIndex, droppedItem.itemType, droppedItem.queue, droppedItem.sounds[rnd]);
            droppedItem.EndPictureDrop(num);

        }
        StartCoroutine(WaitChecking());
    }
    public void CloseItemSlot(ItemObject closeingItem)
    {
        foreach(var item in items)
        {
            if (item.name_ == closeingItem.name_)
            {
                item.CloseSlot();
                break;
            }
        }
    }
    public void FadeInventory()
    {
        foreach (var item in items)
        {
            item.StartFade();
        }
    }

    public void OpenText()
    {
        Color textColor = iventoryName.color;
        textColor.a = 255;
        iventoryName.color = textColor;
    }
    public IEnumerator WaitChecking()
    {
        yield return new WaitForSeconds(1f);
        OpenClose();
        yield break;
    }
    public void OpenClose()
    {
        if(!gameController.closeInventory)
        if (panel != null)
            panel.OpenCloseButtonClick();
    }

}
