using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour
{
    public EColor color;
    private int value = 2;
    public EObstacle eObstacle;

    // Start is called before the first frame update
    void Start()
    {
        value = GameManager.instance.obsNormalValue;
        // random value
        switch (eObstacle)
        {
            case EObstacle.normal:
                value=Random.Range(value - 1, value + 1);
                break;
            case EObstacle.chance:
                value = Random.Range(value - 10, value + 10);
                break;
            case EObstacle.multiplier:
                value = Random.Range(value, value + 1);
                break;
            case EObstacle.divide:
                value = Random.Range(value , value + 1);
                break;
            case EObstacle.minus:
                value = Random.Range(value - 5, value + 10);
                break;
            case EObstacle.plus:
                value = Random.Range(value - 5, value + 5);
                break;
            default:
                break;
        }
        /*
        TextMesh text;
        // set the value text
        string yazi = "";
        switch (eObstacle)
        {
            case EObstacle.normal:
                break;
            case EObstacle.chance:
                yazi = "???";
                break;
            case EObstacle.multiplier:
                yazi = "x" + value;
                break;
            case EObstacle.divide:
                yazi = "/" + value;
                break;
            case EObstacle.minus:
                yazi = "-" + value;
                break;
            case EObstacle.plus:
                yazi = "+" + value;
                break;
            default:
                break;
        }
        if(eObstacle!=EObstacle.normal)
            text.text = yazi;
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.player))
        {
            PlayerColor pColor = other.GetComponent<PlayerColor>();
            if (color == pColor.color)
            {
                switch (eObstacle)
                {
                    case EObstacle.normal:
                        GameManager.instance.score += value;
                        break;
                    case EObstacle.chance:
                        if (Random.Range(0, 2) == 0 ? true : false)
                        {
                            GameManager.instance.score += value;
                        }
                        else
                        {
                            GameManager.instance.score -= value;
                        }
                        
                        break;
                    case EObstacle.multiplier:
                        GameManager.instance.score *= value;
                        break;
                    case EObstacle.divide:
                        GameManager.instance.score /= value;
                        break;
                    case EObstacle.minus:
                        GameManager.instance.score -= value;
                        break;
                    case EObstacle.plus:
                        GameManager.instance.score += value;
                        break;
                    default:
                        break;
                }
                if (GameManager.instance.score < 0)
                    GameManager.instance.score = 0;
                GameManager.instance.UpdateScore();
                GameManager.instance.PlayCollectParticle();
                transform.parent.Find("goodMesh").gameObject.SetActive(false);
                gameObject.SetActive(false);

            }
            else
            {
                GameManager.instance.Fail();
            }
        }
    }
}
