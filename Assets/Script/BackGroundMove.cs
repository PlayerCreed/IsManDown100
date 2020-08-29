using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//背景滚动
public class BackGroundMove : MonoBehaviour
{
    Material material;

    Vector2 materialPos;

    public Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        materialPos += speed * Time.deltaTime;
        material.mainTextureOffset = materialPos;
    }
}
