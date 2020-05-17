using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Maps/New map")]
public class Map : ScriptableObject
{
    [SerializeField] private string name = string.Empty;
}
