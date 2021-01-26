using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateScreenSettings : MonoBehaviour
{
    public void OnRoundsChanged(float value)
    {
        PlayerPrefs.SetInt("Rounds", (int)value);
    }
}
