using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalSpawner : MonoBehaviour
{
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
        StartCoroutine(musicFinishCO(audioSource.GetComponent<MusicController>().startDelay));
    }

    IEnumerator musicFinishCO(float startDelay)
    {
        yield return new WaitForSeconds(startDelay+.1f);
        while (true)
        {
            if (audioSource.isPlaying == false && GameManager.instance.eGameState==EGameState.play)
            {
                yield return new WaitForSeconds(.1f);
                Vector3 playerPos = GameManager.instance.player.transform.position;
                Vector3 spawnPos = new Vector3(0,0, playerPos.z + GameManager.instance.spawnDistance);
                GameObject go = Instantiate(GameManager.instance.cristalPrefabSO.prefab, spawnPos,  Quaternion.identity);
                break;
            }
            yield return null;
        }
    }
}
