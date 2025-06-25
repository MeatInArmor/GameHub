
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "SoliderKitInfoSO", menuName = "SOFolder/SoliderKitInfoSO")]

public class SoliderKitInfoSO : ScriptableObject
{
    [SerializeField] private string SoliderType;
    [SerializeField] private int soliderKitIndex;
    [SerializeField] private int uniformQuantity;
    [Tooltip(" 1 - shirt\n 2 - pants, pajama_bot\n 3 - jacket, pajama_top\n 4 - boots\n 5 - helmet\n 6 - gloves\n 7 - mainWeapon\n 6-13 - equipment")]
    [SerializeField] private int[] uniformQueue;
    [SerializeField] private Vector3[] uniformChanging;
    [SerializeField] private int equipmentQuantity;
    [Tooltip(" 1 - shirt\n 2 - pants, pajama_bot\n 3 - jacket, pajama_top\n 4 - boots\n 5 - helmet\n 6 - gloves\n 7 - mainWeapon\n 6-13 - equipment")]
    [SerializeField] private int[] indexesClose;

    [SerializeField] private AudioClip soldierMusic;
    [SerializeField] private string soldierColor;
    [SerializeField] private int openingIndex;
    [SerializeField] private Sprite background;
    [SerializeField] private Sprite emblem;
    [SerializeField] private Sprite[] mainCloth;
    [SerializeField] private Sprite[] subCloth;
    [SerializeField] private Sprite[] pajama;
    [SerializeField] private Sprite winPose;









    public int SoliderKitIndex => soliderKitIndex;
    public int UniformQuantity => uniformQuantity;
    public int[] UniformQueue => uniformQueue;
    public int OpeningTextIndex => openingIndex;

    public Vector3[] UniformChanging => uniformChanging;
    public int EquipmentQuantity => equipmentQuantity;
    public int[] IndexesClose => indexesClose;
    public AudioClip SoldierMusic => soldierMusic;
    public string SoldierColor => soldierColor;
    public Sprite Emblem => emblem;
    public Sprite Background => background;
    public Sprite[] MainCloth => mainCloth;
    public Sprite[] SubCloth => subCloth;
    public Sprite[] Pajama => pajama;
    public Sprite WinPose => winPose;



}
