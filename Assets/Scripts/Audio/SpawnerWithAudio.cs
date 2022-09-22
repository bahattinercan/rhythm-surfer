using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWithAudio : MonoBehaviour
{
    private bool canSpawn = true;
    private float beforeRotationZ = 0;
    private float rotationAngle = 30;
   
    public GameObject spawnedGO, obsPrefab;
    public Material goodMat, badMat;
    private Transform playerPos;
    private AudioPeer audioPeer;

    private void Start()
    {
        audioPeer = GetComponent<AudioPeer>();
        playerPos = GameObject.FindGameObjectWithTag(Tags.playerMovement).transform;
        obsPrefab = GameManager.instance.obsPrefabSO.prefab;
    }

    private void Update()
    {
        SpawnCube();        
    }

    void SpawnCube()
    {
        if (audioPeer.highestBand > .65f && audioPeer.highestBand2 > .45f && canSpawn)
        {
            canSpawn = false;
            spawnedGO = Instantiate(obsPrefab, new Vector3(0, 0, playerPos.position.z + GameManager.instance.spawnDistance), Quaternion.identity);
            /*
            int chance = Random.Range(0, 100);
            if (chance < 2)
            {
                // chance
                spawnedGO = Instantiate(GameManager.instance.chanceObsP, new Vector3(0, 0, playerPos.position.z + spawnDistance), Quaternion.identity);
            }
            else if (chance < 8 && chance >= 2)
            {
                // minus plus
                spawnedGO = Instantiate(GameManager.instance.plusMinusObsP, new Vector3(0, 0, playerPos.position.z + spawnDistance), Quaternion.identity);
            }
            else if (chance < 12 && chance >= 8)
            {
                // multi divide
                spawnedGO = Instantiate(GameManager.instance.multiDivideObsP, new Vector3(0, 0, playerPos.position.z + spawnDistance), Quaternion.identity);
            }
            else if (chance < 100 && chance >= 12)
            {
                // normal
                spawnedGO = Instantiate(GameManager.instance.normalObsP, new Vector3(0, 0, playerPos.position.z + spawnDistance), Quaternion.identity);
            }
            */

            spawnedGO.transform.SetParent(transform);
            if (Random.Range(0, 100) < 50 ? true : false)
            {
                spawnedGO.transform.eulerAngles = new Vector3(0, 0, beforeRotationZ + Random.Range(rotationAngle/2, rotationAngle));
            }
               
            else
            {
                spawnedGO.transform.eulerAngles = new Vector3(0, 0, beforeRotationZ - Random.Range(rotationAngle/2, rotationAngle));
            }
            beforeRotationZ = spawnedGO.transform.eulerAngles.z;
            List<Transform> goods = new List<Transform>();
            List<Transform> bads = new List<Transform>();
            List<Transform> goodMeshs = new List<Transform>();
            List<Transform> badMeshs = new List<Transform>();
            foreach (Transform child in spawnedGO.transform)
            {
                if (child.name == "good")
                {
                    goods.Add(child);                   
                }
                else if(child.name=="bad")
                {
                    bads.Add(child);
                }                
            }
            foreach (Transform item in goods)
            {
                item.GetComponent<Obstacle>().color = GameManager.instance.spawnColorType;
            }
            foreach (Transform item in bads)
            {
                item.GetComponent<Obstacle>().color = GameManager.instance.antiColorType;
            }

            foreach (Transform child in spawnedGO.transform)
            {
                if (child.name == "goodMesh")
                {
                    goodMeshs.Add(child);
                }
                else if (child.name == "badMesh")
                {
                    badMeshs.Add(child);
                }
            }
            foreach (Transform item in goodMeshs)
            {
                item.GetChild(0).GetComponent<MeshRenderer>().material = goodMat;
            }
            foreach (Transform item in badMeshs)
            {
                item.GetChild(0).GetComponent<MeshRenderer>().material = badMat;
            }
            StartCoroutine(SpawnDelayCo());
        }
    }

    IEnumerator SpawnDelayCo()
    {
        yield return new WaitForSeconds(GameManager.instance.spawnDelay);
        canSpawn = true;
    }
}
