using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public float startDelay; // 3f

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMusicCo());
    }

    IEnumerator StartMusicCo()
    {
        yield return new WaitForSeconds(startDelay);
        GetComponent<AudioSource>().Play();
    }
}
