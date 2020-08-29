using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryCtr : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "TarpMod")
        {
            Destroy(collision.gameObject);
            GameCtr.instance.score += 1;
        }         
    }
}
