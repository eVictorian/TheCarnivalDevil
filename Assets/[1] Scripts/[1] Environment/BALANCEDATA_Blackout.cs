using System;
using UnityEngine;
using NaughtyAttributes;

//REPRESENTS AN ACTION/EVENT BEING UNDERTAKEN//
[CreateAssetMenu(fileName = "[BalanceData][Blackout] New", menuName = "Balance Data/++BLACKOUT")]
public class BALANCEDATA_Blackout : ScriptableObject
{
    [Label("Whiteout Duration")] public float RULE_WhiteoutDuration;
    [Label("Blackout Duration")] public float RULE_BlackoutDuration;
}