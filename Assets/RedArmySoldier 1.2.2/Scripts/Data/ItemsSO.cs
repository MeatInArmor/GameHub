using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EquipmentSO", menuName = "SOFolder/EquipmentSO")]

public class ItemsSO : ScriptableObject
{
    [SerializeField] private string name_;
    [SerializeField] private int[] kitsIndex;
    [Tooltip("pajama, uniform, equipment")]
    [SerializeField] private string itemType;
    [SerializeField] private Sprite sprite;
    [Tooltip(" 1 - shirt\n 2 - pants, pajama_bot\n 3 - jacket, pajama_top\n 4 - boots\n 5 - helmet\n 6 - gloves\n 7 - mainWeapon\n 7-13 - equipment")]
    [SerializeField] private int queue;
    [SerializeField] private AudioClip[] sounds;

    public string Name => name_;
    public int[] KitsIndex => kitsIndex;
    public string ItemType => itemType;
    public Sprite Sprite => sprite;
    public int Queue => queue;
    public AudioClip[] Sounds => sounds;
}
