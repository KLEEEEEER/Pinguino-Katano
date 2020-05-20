using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Map", menuName = "Maps/New map")]
public class Map : ScriptableObject
{
    [SerializeField] private string scene;
}
