using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorObs : MonoBehaviour
{
    public EColor eColor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.player))
        {
            GameManager.instance.ChangePlayerColor(eColor);
            GameManager.instance.PlayCollectParticle();
            Destroy(transform.parent.gameObject);
        }
    }
}
