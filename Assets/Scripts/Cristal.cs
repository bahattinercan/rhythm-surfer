using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cristal : MonoBehaviour
{
    private int value;
    private void Start()
    {
        value = GameManager.instance.cristalValue;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.player))
        {
            GameManager.instance.score += value;
            GameManager.instance.UpdateScore();
            GameManager.instance.PlayCollectParticle();
            GameManager.instance.PlayCollectSound();
            Destroy(gameObject);
        }
    }
}
