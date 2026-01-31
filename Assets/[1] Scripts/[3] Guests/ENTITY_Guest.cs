using UnityEngine;
using NaughtyAttributes;

public class ENTITY_Guest : ENTITY
{
    [SerializeField] private GameObject corpsePrefab;

    public void InstantiateCorpse(){ GameObject freshCorpse = Instantiate(corpsePrefab); freshCorpse.transform.position = transform.position;}
}