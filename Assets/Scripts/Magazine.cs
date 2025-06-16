using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Magazine",menuName = "ScriptableObject/Magazine")]
public class Magazine : ScriptableObject
{
    [Range(1, 90)] public int bulletNum;
    public GameObject emptyPrefab;
    public GameObject prefab;
    public magazineType magazineType;
}
public enum magazineType { Pistol,Rifle,Sniper}
