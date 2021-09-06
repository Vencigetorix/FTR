using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3[] posSun;
    public Color[] color;
    public float temp;
    public float hora;
    public int clima; //Clima [0:4] 0 : Despejado 1 : Tormenta 
    public float speedTime; 
    public int day;
    public Light sun;
    void Start()
    {
        day = 0;
        clima = 0;
        temp = 19.0f;
        hora = 10.0f;
        speedTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Light and time
        //sun.gameObject.transform.Rotate(Time.deltaTime*speedTime, 0, 0, Space.World);
        
        hora += Time.deltaTime*speedTime;
        if(hora >= 24){
            hora = 0;
            day ++;
        }
    }

    //¿Cómo podemos controlar la temperatura copn respecto a la hora y al clima?
    
}
