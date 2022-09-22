using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanel : MonoBehaviour
{
    public List<Image> starList;
    public TextMeshProUGUI text;

    private void Start()
    {
        GameManager.instance.scorePanel = this;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void UpdateScore()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        foreach (Image item in starList)
        {
            item.color = new Color(1, 1, 1, 0);
        }
        int starNumber = GameManager.instance.GetScoreStar();
        for (int i = 0; i < starNumber; i++)
        {
            starList[i].color = new Color(1, 1, 1, 1);
        }
        text.text = "Score\n" + GameManager.instance.score;
    }
}