using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextSO", menuName = "SOFolder/TextSO")]

public class TextSO : ScriptableObject
{
    [SerializeField] private string _equipRedArmySolider_MainText;
    [SerializeField] private string[] _itemsLeft;
    [SerializeField] private string[] _soliderType_Basic;
    [SerializeField] private string[] _soliderType_ForTextInsert;
    [SerializeField] private string[] _inventoryName;
    [SerializeField] private string[] _readyToBattle;
    [SerializeField] private string[] _wrongMessage;
    [SerializeField] private string[] _openingSoldierText;



    public string EquipRedArmySolider_MainText => _equipRedArmySolider_MainText;
    public string[] ItemsLeft => _itemsLeft;
    public string[] SoliderType_Basic =>_soliderType_Basic;
    public string[] SoliderType_ForTextInsert => _soliderType_ForTextInsert;
    public string[] InventoryName => _inventoryName;
    public string[] ReadyToBattle => _readyToBattle;
    public string[] WrongMessage => _wrongMessage;
    public string[] OpeningSoldierText => _openingSoldierText;

}
