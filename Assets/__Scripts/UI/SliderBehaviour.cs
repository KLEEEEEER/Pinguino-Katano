using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderBehaviour : MonoBehaviour
{
    Text textComponent;
    private void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void OnSliderChanged(float value)
    {
        textComponent.text = value.ToString();
    }
}
