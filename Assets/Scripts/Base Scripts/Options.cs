using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{
    public int targetFPS = 60;
    private void Awake()
    {
        Application.targetFrameRate = targetFPS;
    }
}
