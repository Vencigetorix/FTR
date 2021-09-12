using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float temp;
    Rigidbody rb;
    public Hungry hungry;
    public Environment env;
    public float idealTemp, lowTemp = 34.0f, highTemp = 40.0f, speedTemp = 1f, HeatSource = 0f;
    public bool isInf = false, isRec = false, isBleed = false;
    public float levelInf = 0f, speedInf = 1.0f, speedRec, speedBleeding = 0f, levelRad = 0f;
    public float ropa = 100f; // de 0 a 100
    public float blood = 100f;
    public float salud = 100f;
    public float maxDistance = 5f;
    public int[] body; //0 - Cabeza, 1 - Pecho/abdomen, 2 - Brazos, 3 - Piernas.
    //0 - Sano, 1 - Daño muscular, 2 - Fractura

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //temp = 36.5f;
        //body = new int[4];
    }

    // Update is called once per frame
    void Update()
    {
        //Bleeding
        if(isBleed) speedBleeding = Mathf.Clamp(speedBleeding + 0.05f*Time.deltaTime, 0f, 3f);
        else speedBleeding = Mathf.Clamp(speedBleeding - 0.5f*Time.deltaTime, 0f, 3f);
        blood -= speedBleeding*Time.deltaTime;
        //Temperatura
        //temp = temp + Time.deltaTime*(env.temp - temp)*speedTemp*Ropa()*Mathf.Clamp((100f - hungry.Carbs)/100f, 0.1f, 1f); //Falta la relación con el inventario y sus variables de alimento.
        //Debug.Log(Mathf.Clamp((100f - hungry.Carbs)/100f, 0.1f, 1f));
        float coeTemp = ((Temp()*5 + Ropa() + HungryN()*1.5f))/7.5f;
        coeTemp-=0.2f;
        coeTemp*=speedTemp;
        Debug.Log(coeTemp*100f);
        temp = temp + coeTemp*Time.deltaTime;
        //Infección
        if(isInf){
            levelInf = levelInf += Time.deltaTime*(speedInf);
        }else if(isRec){
            levelInf = levelInf -= Time.deltaTime*(speedRec);
        }
        //Muerte por sangre
        if(blood <= 0f){
            hungry.Die();
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Damage"))
            Debug.Log("Recibe daño");//Hay que describir las distinas formas de recibir daño
    }

    void OnTriggerStay(Collider col){
        if(col.CompareTag("HeatSource")){
            HeatSource = 79;
            //temp = Mathf.Clamp(temp + Time.deltaTime*(highTemp - temp)*((maxDistance - Vector3.Distance(col.gameObject.transform.position, this.gameObject.transform.position))/maxDistance)*(speedTemp*1.5f)*(2 - Ropa()),0f,45f);
            //Debug.Log(Time.deltaTime*(highTemp - temp)*((maxDistance - Vector3.Distance(col.gameObject.transform.position, this.gameObject.transform.position))/maxDistance)*(speedTemp*1.5f)*(2 - (2f-ropa)/2f));
        }
    }

    void OnTriggerExit(Collider col){
        if(col.CompareTag("HeatSource")){
            HeatSource = 0f;
        }
    }

    void OnCollisionEnter(Collision col){
        Debug.Log(col.relativeVelocity.magnitude);
        if(col.relativeVelocity.magnitude > 5){
            Debug.Log("Daño por caída");
        }
    }

    float Ropa(){
        return (ropa/100f - 0.5f)*2f;
    }

    float Temp(){
        return (env.temp + HeatSource - 37)/60;
    }

    float HungryN(){
        return (hungry.Carbs/100f - 0.5f)*2f;
    }


}
