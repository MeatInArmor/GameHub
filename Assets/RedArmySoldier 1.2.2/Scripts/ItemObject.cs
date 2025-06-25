using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemObject : MonoBehaviour
{
    public event System.Action doInventoryOpenClose;
    public event System.Action<ItemObject> checkItem;



    [SerializeField] public SoldierStand soliderStand;
    [SerializeField] public DragNDropPicture picture;

    [SerializeField] public string name_;
    [SerializeField] public int[] kitsIndex;
    [SerializeField] public string itemType;
    [SerializeField] public Image backgroundItemImage;
    [SerializeField] public Image dragableItemImage;
    [SerializeField] public int queue;
    [SerializeField] public GameObject closeingPanel;
    [SerializeField] public Text nameText;
    [SerializeField] public AudioClip[] sounds;
    [SerializeField] public bool dragged = false;

    [SerializeField] public Animator animator;

    protected int _animIDFade;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        _animIDFade = Animator.StringToHash("StartFade");
        soliderStand = GetComponentInParent<Inventory>().soliderStand;
        picture.doInventoryOpenClose += OpenCloseInventory;
    }
    private void OnDisable()
    {
        picture.doInventoryOpenClose -= OpenCloseInventory;

    }
    public void SetData(ItemsSO data)
    {
        name_ = data.Name;
        nameText.text = data.Name;
        kitsIndex = data.KitsIndex;
        itemType = data.ItemType;
        queue = data.Queue;
        backgroundItemImage.sprite = data.Sprite;
        dragableItemImage.sprite = data.Sprite;
        sounds = data.Sounds;
    }
    public void PictureDrop()
    {
        checkItem?.Invoke(this);
    }
    public void EndPictureDrop(int num)
    {
        if (num == 0 || num == 1)
            CloseSlot();        
    }

    public void CloseSlot()
    {
        dragged = true;
        closeingPanel.SetActive(true);
        closeingPanel.transform.parent = transform;
        closeingPanel.transform.SetAsLastSibling();
    }

    public void OpenCloseInventory()
    {
        doInventoryOpenClose?.Invoke();
    }
    public void StartFade()
    {
        animator.SetTrigger(_animIDFade);
    }
    
    public void MouseEnter()
    {
        if(!dragged)
            closeingPanel.SetActive(true);
    }
    public void MouseExit()
    {
        if (!dragged)
            closeingPanel.SetActive(false);
    }

}

