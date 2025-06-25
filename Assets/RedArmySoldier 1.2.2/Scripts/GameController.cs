using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public int numOfSoldierKits;
    public const int uniformItems = 9;
    public const int equipmentItems = 6;
    public const int percentOfOtherKtsItems = 40;

    [SerializeField] public CanvasController canvasController;

    [SerializeField] public bool endGame = false;
    [SerializeField] public bool secondStage = false;
    [SerializeField] public bool closeInventory = false;
    [SerializeField] public int nextSoldier;

    [SerializeField] public AudioController audioSource;
    [SerializeField] public AudioClip clip;

    [SerializeField] public int currentSoliderKitIndex;
    [SerializeField] public int itemsLeft;

    [SerializeField] public TextSO gameTexts;
    [SerializeField] public SoliderKitInfoSO[] soldiersData;
    [SerializeField] public SoliderKitInfoSO currentSoliderKit;
    [SerializeField] public List<ItemsSO> equipmentData = new List<ItemsSO>();
    [SerializeField] public List<ItemsSO> selectedEquipmentData = new List<ItemsSO>();

    [SerializeField] public Camera _camera;
    [SerializeField] public bool fullScreen = true;

    [SerializeField] public CanvasController[] canvaces;
    [SerializeField] public Scrollbar[] musicScrolls;
    [SerializeField] public Scrollbar[] soundScrolls;




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleFullscreen();
        }
    }

    void ToggleFullscreen()
    {
        if (!fullScreen)
        {
            fullScreen = true;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            fullScreen = false;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.Windowed);
        }
    }
    private void CheckScreen()
    {
        int index = 0;
        //Debug.Log(Screen.currentResolution.width + " " + Screen.currentResolution.height);
        if (Screen.currentResolution.width >= Screen.currentResolution.height)
        {
            index = 0;
        }
        else index = 1;
        SwitchCanvas(index);
    }

    private void SwitchCanvas(int index)
    {
        for(int i = 0; i < canvaces.Length; i++)
        {
            canvaces[i].gameObject.SetActive(false);
        }
        canvaces[index].gameObject.SetActive(true);
        canvasController = canvaces[index];
        audioSource.VolumeScrollBar[0] = musicScrolls[index];
        audioSource.VolumeScrollBar[1] = soundScrolls[index];
    }
    public int GetConst(string type)
    {
        switch (type)
        {
            case "kitNum":
                return numOfSoldierKits;
            case "uniform":
                return uniformItems + 1;
            case "equipment":
                return equipmentItems + uniformItems + 1;
            case "percent":
                return percentOfOtherKtsItems;
            default: return 0;
        }
    }
    public void SetSoldierKitIndex(int num)
    {
        currentSoliderKitIndex = num;
    }
    private void Awake()
    {
        CheckScreen();

        numOfSoldierKits = soldiersData.Length;
        canvasController.SetStartScreens();
    }
    private void Start()
    {

        fullScreen = true;
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
    }
    public void GameStart(int index)
    {
        secondStage = false;
        nextSoldier = index;

        canvasController.GameStart();
        closeInventory = false;

    }

    public void Reload(int kitNumber)
    {
        currentSoliderKitIndex = kitNumber;
        currentSoliderKit = soldiersData[currentSoliderKitIndex];
        for (int i = 0; i < soldiersData.Length; i++)
            if (soldiersData[i].SoliderKitIndex == currentSoliderKitIndex)
            {
                currentSoliderKit = soldiersData[i];
                break;
            }

        itemsLeft = currentSoliderKit.UniformQuantity;
        selectedEquipmentData.Clear();

        canvasController.Reload();

        audioSource.setClip(soldiersData[currentSoliderKitIndex].SoldierMusic, 0);
        ResetInventory();

    }

    public void ResetInventory()
    {
        int uni = 0;
        int eq = 0;
        Shuffle(equipmentData);
        for (int i = 0; i < equipmentData.Count; i++)
        {
            for (int j = 0; j < equipmentData[i].KitsIndex.Length; j++)
            {
                if (equipmentData[i].KitsIndex[j] == currentSoliderKitIndex)
                {
                    selectedEquipmentData.Add(equipmentData[i]);
                    if (equipmentData[i].ItemType == "uniform")
                        uni++;
                    if (equipmentData[i].ItemType == "equipment")
                        eq++;
                    j = 100;
                    break;
                }
            }

        }
        for (int i = 0; i < equipmentData.Count; i++)
        {
            for (int j = 0; j < equipmentData[i].KitsIndex.Length; j++)
            {
                if (equipmentData[i].KitsIndex[j] != currentSoliderKitIndex && equipmentData[i].KitsIndex.Length == 1)
                {
                    if (equipmentData[i].ItemType == "uniform" && uni < 12 || equipmentData[i].ItemType == "equipment")
                    {
                        selectedEquipmentData.Add(equipmentData[i]);
                        if (equipmentData[i].ItemType == "uniform")
                            uni++;
                        if (equipmentData[i].ItemType == "equipment")
                            eq++;
                        break;
                    }
                }
            }
        }

        Shuffle(selectedEquipmentData);
        canvasController.inventory.ResetContent(false, selectedEquipmentData);

    }

    public void Shuffle(List<ItemsSO> items)
    {
        System.Random r = new System.Random();
        int n = items.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = r.Next(0, i + 1);
            ItemsSO temp = items[i];
            items[i] = items[j];
            items[j] = temp;
        }
    }
    public void CheckFull()
    {
        if (!secondStage)
        {
            if (itemsLeft == 0)
            {
                secondStage = true;
                canvasController.EndClothAnim();
                closeInventory = true;
            }
        }
        else
            if (itemsLeft == 0)
        {

            canvasController.EndEquipmentAnim();
            secondStage = false;
            closeInventory = true;
        }
    }


    public void WrongPanelActivate(int num)
    {
        canvasController.ActivateWrong(num);
    }
    public void TextArmyNext(int index)
    {
        canvasController.TextArmyNext(index);
    }
    public void ItemsLeftTextChange()
    {
        canvasController.ItemsLeftTextChange();
    }
    public void ItemsLefValueChange(string equipType)
    {
        itemsLeft--;
        canvasController.ItemsLefValueChange(equipType);
        CheckFull();

    }
    public void InventoryNameTextChange()
    {
        canvasController.InventoryNameTextChange(Convert.ToInt32(secondStage));
    } 

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Again()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
