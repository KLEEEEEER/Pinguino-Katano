using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Map", menuName = "Maps/New map")]
public class Map : ScriptableObject
{
    [SerializeField] private Object scene;
    [SerializeField] private Image thumbnail;
    [SerializeField] private string description;
}
