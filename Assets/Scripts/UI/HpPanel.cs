using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpPanel : MonoBehaviour
{
    List<Image> images;
    TextMeshProUGUI text;

    private void Start()
    {
        images = new List<Image>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        Transform lowerRoot = transform.GetChild(1);
        for (int i = 0; i < lowerRoot.childCount; i++)
        {
            images.Add(lowerRoot.GetChild(i).GetComponent<Image>());
        }

        //UpdateHp(GameManager.instance.heath);
    }
   
    public void UpdateHp(int hp)
    {
        text.text = hp+"/2";
        Color c = images[0].color;
        for (int i = 0; i < images.Count; i++)
        {            
            images[i].color = new Color(c.r, c.g, c.b, 0);
        }

        if (hp == 2)
        {
            images[0].color = new Color(c.r, c.g, c.b, 1);
            images[1].color = new Color(c.r, c.g, c.b, 1);
        }else if (hp == 1)
        {
            images[1].color = new Color(c.r, c.g, c.b, 1);
        }
        else
        {
            for (int i = 0; i < images.Count; i++)
            {
                images[i].color = new Color(c.r, c.g, c.b, 0);
            }
        }
        
    }
}
