using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float timeToDead, temp, maxDistance, tempPerDis;
    float speed;

    void Start()
    {
        speed = (temp/timeToDead)/2;
    }

    void Update()
    {
        timeToDead-=Time.deltaTime;
        temp -= speed*Time.deltaTime;
        tempPerDis = temp/maxDistance;
        //Debug.Log(timeToDead);
    }
}
