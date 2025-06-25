using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    [SerializeField] public GameController gc;

    //[SerializeField] public CanvasController // alternative;

    [SerializeField] public SoldierStand soldierStand;
    [SerializeField] public Inventory inventory;
    [SerializeField] public TMP_Text wonPanel;
    [SerializeField] public GameObject soldierBack;
    [SerializeField] public Image SoldierEmblem;
    [SerializeField] public Image[] galka;
    [SerializeField] public bool[] finished;
    //[SerializeField] public bool endGame = false;
    [SerializeField] public GameObject WonScreen;


    [SerializeField] public Image backgroundImage;
    [SerializeField] public Image darkImage;
    [SerializeField] public GameObject blockingPanel;

    [SerializeField] public GameObject screen;

    [SerializeField] public TMP_Text armyText;
    [SerializeField] public TMP_Text inventoryText;
    [SerializeField] public TMP_Text itemsLeftText;
    [SerializeField] public TMP_Text itemsLeftValue;

    [SerializeField] public GameObject wrongPanel;
    [SerializeField] public GameObject helpPanel;

    [SerializeField] public TMP_Text helpText;
    [SerializeField] public TMP_Text wrongText;

    [SerializeField] public string str;


    [SerializeField] public Animator animator;

    [SerializeField] public bool canClick = false;

    [SerializeField] private int fontSize;

    protected int _animIDEndUniform;
    protected int _animIDDarkness;
    protected int _animIDHelp;
    protected int _animIDWrong;
    protected int _animIDStart;
    protected int _animIDFinish;
    protected int _animIDEnd;
    protected int _animIDSecond;
    protected int _animIDAgain;


    private void Awake()
    {

        animator = GetComponent<Animator>();
        _animIDEndUniform = Animator.StringToHash("EndUniform");
        _animIDDarkness = Animator.StringToHash("Dark");
        _animIDHelp = Animator.StringToHash("Help");
        _animIDWrong = Animator.StringToHash("Wrong");
        _animIDStart = Animator.StringToHash("Start");
        _animIDFinish = Animator.StringToHash("Finish");
        _animIDEnd = Animator.StringToHash("End");
        _animIDSecond = Animator.StringToHash("Second");
        _animIDAgain = Animator.StringToHash("Again");



        str = gc.gameTexts.OpeningSoldierText[1];
        str = TextDecorator(str, null, null);

    }
    public void GameStart()
    {
        animator.SetTrigger(_animIDDarkness);


    }

    public void Reload()
    {
        animator.SetBool(_animIDSecond, false);

        soldierStand.currentSoldier = gc.currentSoliderKit;
        soldierBack.gameObject.SetActive(true);
        inventory.wonPanel.SetActive(false);
        soldierStand.winPose.gameObject.SetActive(false);
        soldierStand.SetWinPose();
        OpenText();
        TextArmyNext(0);
        backgroundImage.sprite = gc.currentSoliderKit.Background;
        SoldierEmblem.sprite = gc.currentSoliderKit.Emblem;

        armyText.text = TextDecorator(gc.gameTexts.EquipRedArmySolider_MainText, gc.gameTexts.SoliderType_ForTextInsert[gc.currentSoliderKitIndex], gc.currentSoliderKit.SoldierColor);
        inventoryText.text = TextDecorator(gc.gameTexts.InventoryName[0], gc.gameTexts.SoliderType_ForTextInsert[gc.currentSoliderKitIndex], null);
        itemsLeftText.text = TextDecorator(gc.gameTexts.ItemsLeft[0], null, null);
        StartCoroutine(TextFix(0)); 

        StartCoroutine(TextFix(0));

        soldierStand.Undress(gc.soldiersData[gc.currentSoliderKitIndex].IndexesClose);

    }

    public void SetStartScreens()
    {
        screen.SetActive(true);
        WonScreen.SetActive(false);
        blockingPanel.SetActive(false);
    }

    public void EndClothAnim()
    {
        animator.SetTrigger(_animIDEndUniform);
        inventory.FadeInventory();
    }
    public void EndEquipmentAnim()
    {
        animator.SetTrigger(_animIDFinish);
        inventory.FadeInventory();
    }  
    public void InventoryNameTextChange(int inventoryType)
    {
        inventoryText.text = TextDecorator(gc.gameTexts.InventoryName[inventoryType], gc.gameTexts.SoliderType_ForTextInsert[gc.currentSoliderKitIndex], null);

    }
    public void OpenText()
    {
        Color textColor = itemsLeftValue.color;
        textColor.a = 255;
        itemsLeftText.color = textColor;
        itemsLeftValue.color = textColor;
        armyText.color = textColor;
        textColor.a = 0;
        wonPanel.color = textColor;
    }

    public void TextArmyNext(int index)
    {
        wonPanel.text = TextDecorator(gc.gameTexts.ReadyToBattle[index], gc.gameTexts.SoliderType_Basic[gc.currentSoliderKitIndex], gc.currentSoliderKit.SoldierColor);
    }

    public string TextDecorator(string originalText, string insertText, string insertColor)
    {
        char[] temp = originalText.ToCharArray();
        StringBuilder sb = new StringBuilder();
        foreach (char c in temp)
        {

            switch (c)
            {
                case '~':
                    sb.Append(fontSize);
                    break;
                case '&':
                    sb.Append(insertText);
                    break;
                case '#':
                    sb.Append(gc.currentSoliderKit.SoldierColor);
                    break;
                case '$':
                    if(gc.currentSoliderKit.OpeningTextIndex == 0)
                        sb.Append(gc.gameTexts.OpeningSoldierText[0]);
                    else
                        sb.Append(str);
                    break;
                
                default:
                    sb.Append(c);
                    break;
            }

        }
        return sb.ToString();
    }

    public void InventoryTextChange(int index)
    {
        inventoryText.text = TextDecorator(gc.gameTexts.InventoryName[Convert.ToInt32(gc.secondStage)], gc.gameTexts.SoliderType_ForTextInsert[gc.currentSoliderKitIndex], null);

    }
    public void ItemsLeftTextChange()
    {
        itemsLeftText.text = gc.gameTexts.ItemsLeft[1];
    }

    public void ItemsLefValueChange(string equipType)
    {
        int num;
        if (int.TryParse(itemsLeftValue.text, out num))
            if (equipType == "uniform")
                itemsLeftValue.text = (gc.itemsLeft).ToString();
            else if (equipType == "equipment")
                itemsLeftValue.text = (gc.itemsLeft).ToString();
    }
    private IEnumerator TextFix(int type)
    {
        yield return null;
        if (type == 0)
            itemsLeftValue.text = TextDecorator(gc.currentSoliderKit.UniformQuantity.ToString(), null, null);
        else
            itemsLeftValue.text = TextDecorator(gc.currentSoliderKit.EquipmentQuantity.ToString(), null, null);
    }








    public void Anim_PartChange()
    {
        {
            if (!gc.secondStage)
            {
                gc.itemsLeft = gc.currentSoliderKit.UniformQuantity;

            }
            else
            {
                gc.itemsLeft = gc.currentSoliderKit.EquipmentQuantity;

            }
            gc.ItemsLeftTextChange();

            gc.InventoryNameTextChange();

        }
    }
    public void Anim_SecondPartStart()
    {
        {
            gc.itemsLeft = gc.currentSoliderKit.EquipmentQuantity;

            inventory.ResetContent(true, gc.selectedEquipmentData);


            gc.secondStage = true;
            gc.closeInventory = false;
        }
        animator.SetBool(_animIDSecond, true);
        StartCoroutine(TextFix(1));

    }
    public void CheckDark()
    {

            gc.Reload(gc.nextSoldier);

    }

    public void ActivateHelp()
    {
        if (!gc.endGame)
        {
            animator.SetTrigger(_animIDHelp);

        }
    }
    public void ActivateWrong(int num)
    {
        
        wrongText.text = TextDecorator(gc.gameTexts.WrongMessage[num - 1], gc.gameTexts.SoliderType_ForTextInsert[gc.currentSoliderKitIndex], gc.currentSoliderKit.SoldierColor);
        animator.SetTrigger(_animIDWrong);
    }
    public void Anim_CanClick()
    {
        canClick = true;
    }

    public void Anim_FinishStage()
    {
        TextArmyNext(1);
        soldierStand.WinPose();
        soldierStand.winPose.gameObject.SetActive(true);
        galka[gc.currentSoliderKitIndex].gameObject.SetActive(true);
        finished[gc.currentSoliderKitIndex] = true;
        {
            bool is_all = true;
            foreach (var f in finished)
                if (!f)
                    is_all = false;
            if (is_all && !gc.endGame)
            {
                if (!gc.endGame)
                {
                    animator.SetTrigger(_animIDEnd);
                }
                gc.endGame = true;
            }
        }
    
    }

    public void Again()
    {
        StartCoroutine(Fade());

    }
    private IEnumerator Fade()
    {
        float targetAlpha = 1f;
        float duration = 2f;
        Color color = darkImage.color;
        float startAlpha = color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            color.a = newAlpha;
            darkImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        darkImage.color = color;
        gc.Again();

    }
}


