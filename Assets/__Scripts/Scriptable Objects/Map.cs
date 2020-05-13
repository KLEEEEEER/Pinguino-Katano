using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Maps/New map")]
public class Map : ScriptableObject
{
    [Scene]
    [SerializeField] private string name = string.Empty;
}
