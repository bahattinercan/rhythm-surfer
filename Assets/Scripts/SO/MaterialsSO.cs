using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MaterialsSO")]
public class MaterialsSO : ScriptableObject
{
    public Material blue,purple,red,yellow;

    public Material GetMaterial(EColor eColor)
    {
        switch (eColor)
        {
            case EColor.blue:
                return blue;
            case EColor.purple:
                return purple;
            case EColor.red:
                return red;
            case EColor.yellow:
                return yellow;
            default:
                Debug.LogWarning("b�yle renk yok");
                return blue;
        }

    }
}
