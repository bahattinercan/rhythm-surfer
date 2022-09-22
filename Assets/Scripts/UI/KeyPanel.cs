using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanel : MonoBehaviour
{
    List<Image> images;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        images = new List<Image>();
        for (int i = 0; i < transform.childCount; i++)
        {
            images.Add(transform.GetChild(i).GetComponent<Image>());
        }
        UpdateKeys(GameManager.instance.keys);
    }

    public void UpdateKeys(int keys)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].color = new Color(1, 1, 1, 0);
        }

        for (int i = 0; i < keys; i++)
        {
            images[i].color = new Color(1, 1, 1, 1);
        }
    }
}
