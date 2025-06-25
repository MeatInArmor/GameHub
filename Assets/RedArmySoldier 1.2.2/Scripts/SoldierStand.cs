using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SoldierStand : MonoBehaviour
{
    [SerializeField] public GameController gameController;
    [SerializeField] public CanvasController canvasController;


    [SerializeField] public TextSO gameTexts;
    [SerializeField] public SoliderKitInfoSO currentSoldier;

    [SerializeField] public Sprite emptyPicture;
    [SerializeField] public Image[] soldier;                          // 0 - body, 1 - shirt, 2 - pants, 3 - jacket, 4 - boots, 5 - helmet, 6 - gloves, 7 - mainWeapon
    [SerializeField] public Image winPose;
    [SerializeField] public bool[] isItemEquipped;
    [SerializeField] public int itemsEquipped;

    [SerializeField] public int currentItemIndex;

    [SerializeField] public GameObject backpack;

    [SerializeField] public GraphicRaycaster raycaster;
    [SerializeField] public EventSystem eventSystem;


    private void Awake()
    {
        itemsEquipped = 0;
        isItemEquipped[0] = true;
    }
    public int CheckEquipped(int[] kitIndex, string equipType, int queue, AudioClip clip)
    {
        bool correctKitIndex = false;
        bool equipped = false;
        for (int i = 0; i < kitIndex.Length; i++)
            if (kitIndex[i] == gameController.currentSoliderKitIndex)
            {
                correctKitIndex = true;
                if (equipType != "equipment")
                {
                    bool good = true;
                    if (queue != currentSoldier.UniformQueue[itemsEquipped])
                    {
                        good = false;
                    }

                    if (good)
                    {
                        itemsEquipped++;
                        equipped = true;
                        PutOn(queue, equipType, clip);
                    }
                }
                else
                {
                    itemsEquipped++;
                    PutOn(queue, equipType, clip);
                    equipped = true;

                }
            }
        if (equipped)
        {
            gameController.ItemsLefValueChange(equipType);
            return 0;
        }
        else if (!correctKitIndex)
        {
            gameController.WrongPanelActivate(1);
            return 1;
        }
        else
        {
            gameController.WrongPanelActivate(2);
            return 2;
        }

    }
    public void PutOn(int queue, string equipType, AudioClip clip)
    {

        if (equipType == null)
        {
            soldier[queue].sprite = emptyPicture;
        }
        else if (equipType == "basic")
        {
            soldier[queue].sprite = gameController.currentSoliderKit.Pajama[queue];
        }
        else
        {
            // 0 - body, 1 - shirt, 2 - pants, 3 - jacket, 4 - boots, 5 - helmet, 6 - gloves, 7 - mainWeapon

            soldier[queue].sprite = gameController.currentSoliderKit.MainCloth[queue];


            if (equipType == "uniform")
            {
                int x = (int)currentSoldier.UniformChanging[queue].x;
                int y = (int)currentSoldier.UniformChanging[queue].y;
                int z = (int)currentSoldier.UniformChanging[queue].z;

                if (x != 0)
                    soldier[x].sprite = gameController.currentSoliderKit.SubCloth[x];
                if (y != 0)
                    soldier[y].sprite = gameController.currentSoliderKit.SubCloth[y];
                if (z != 0)
                    soldier[z].sprite = gameController.currentSoliderKit.SubCloth[z];
            }
            if (equipType == "uniform" || equipType == "equipment")
            {
                gameController.audioSource.setClip(clip, 1);
            }
        }


    }
    public void Undress(int[] closedIndexes)
    {
        itemsEquipped = 0;
        for (int i = 1; i < soldier.Length; i++)
        {
            PutOn(i, null, null);
        }

        PutOn(0, "basic", null);
        PutOn(2, "basic", null);
        PutOn(3, "basic", null);

        for (int i = 1; i < isItemEquipped.Length; i++)
        {
            isItemEquipped[i] = false;
            for (int j = 0; j < closedIndexes.Length; j++)
                if (closedIndexes[j] == i)
                {
                    isItemEquipped[i] = true;
                    break;
                }
        }
    }



    public bool IsPointerOverUI()
    {
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        return results.Count > 0;
    }
    public void SetSoldier(SoliderKitInfoSO newSoldier)
    {
        currentSoldier = newSoldier;
    }
    public void WinPose()
    {
        for (int i = 0; i <= 9; i++)
        {
            PutOn(i, null, null);
        }
    }
    public void SetWinPose()
    {

        winPose.sprite = gameController.currentSoliderKit.WinPose;
    }

}
