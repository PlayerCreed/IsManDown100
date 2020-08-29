using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrap : MonoBehaviour
{

    public float windPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (transform.rotation == Quaternion.Euler(0, 0, 0))
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(windPower, 0));
            else
                collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(-windPower, 0));
        }
    }
}
