using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public Vector3 basePos;
    Transform playerTransform;

    private void Start()
    {
        basePos = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(basePos.x, basePos.y, playerTransform.position.z + basePos.z);
    }
}
