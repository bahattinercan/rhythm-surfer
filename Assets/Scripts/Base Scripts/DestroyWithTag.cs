using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithTag : MonoBehaviour
{
    public string tag;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag(tag))
        {
            Destroy(collision.gameObject);
        }
    }
}
