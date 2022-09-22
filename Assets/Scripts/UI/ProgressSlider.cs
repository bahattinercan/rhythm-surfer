using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    Slider slider;
    private void Start()
    {
        slider=GetComponent<Slider>();
    }

    public void UpdateSlider(int score)
    {
        slider.value = score;
    }


}
