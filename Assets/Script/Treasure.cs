using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public void SetExcavated()
    {
        var selfSprite = GetComponent<SpriteRenderer>();
        selfSprite.sprite = Resources.Load<Sprite>("Material/Hole");
        selfSprite.color = new Color(255,255,255,1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().SetAbleDig(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().RemoveAbleDig();
        }
    }
}
