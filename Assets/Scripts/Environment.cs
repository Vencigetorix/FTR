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
    public int clima; //Clima [0:5] 0 : Despejado 1 : Nublado 2 : nieve 3 : Tormenta 4 : Tormentafuerte  5 : radioactivo
    public float speedTime; 
    public int day;
    public Light sun;
    public float visibility;
    void Start()
    {
        day = 0;
        clima = 0;
        temp = 14.0f;
        hora = 10.0f;
        speedTime = 1.0f;
    }
// -2  8 18
// -6  4 14
// -10 0 10
// -14 -4 6
// -18 -8 2
// -22 -12 -2

    // Update is called once per frame
    void Update()
    {
        hora += Time.deltaTime*speedTime;
        if(hora >= 24){
            hora = 0;
            day ++;
            clima = Random.Range(0,6);
        }
        if(hora >= 7 && hora <= 19){
            temp = temp + 1.5f; //day
        }else{  //Night 
            if (hora != 0){
                temp = temp - 1.0f;  
            }
            else{
                switch (clima){
                    case 0:
                        temp = 8.0f;
                        break;
                    case 1:
                        temp = 4.0f;
                        break;
                    case 2:
                        temp = 0.0f;
                        break;
                    case 3:
                        temp = -4.0f;
                        break;
                    case 4:
                        temp = -8.0f;
                        break;
                    case 5:
                        temp = -12.0f;
                        break;
                }
            }            
        }           
    }
    //¿Cómo podemos controlar la temperatura copn respecto a la hora y al clima?
    
}

