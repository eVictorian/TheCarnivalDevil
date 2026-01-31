using System;
using UnityEngine;
using NaughtyAttributes;

//REPRESENTS AN ACTION/EVENT BEING UNDERTAKEN//
[CreateAssetMenu(fileName = "[BalanceData][Guest][Roaming] New", menuName = "Balance Data/++GUESTROAMING")]
public class BALANCEDATA_GUEST_Roaming : ScriptableObject
{
    [Label("Random Wait Time Range"), MinMaxSlider(5,60)] public Vector2 RULE_WaitTimeRange;
}