using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = -30f;

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
    }
}
