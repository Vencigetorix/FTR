using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    Rigidbody rb;
    public Hungry hungry;
    public FireController fc;
    public Environment env;
    public bool isInf = false, isRec = false, isBleed = false;
    public float temp, speedTemp = 1f, HeatSource = 0f;
    public float levelInf = 0f, speedInf = 1.0f, speedRec, speedBleeding = 0f, levelRad = 0f;
    public float ropa = 100f, blood = 100f, salud = 100f; // de 0 a 100
    public int[] body; //0 - Cabeza, 1 - Pecho/abdomen, 2 - Brazos, 3 - Piernas.
    //0 - Sano, 1 - Daño muscular, 2 - Fractura

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Bleeding
        speedBleeding = (isBleed)?Mathf.Clamp(speedBleeding + 0.05f*Time.deltaTime, 0f, 3f):Mathf.Clamp(speedBleeding - 0.5f*Time.deltaTime, 0f, 3f);
        blood -= speedBleeding*Time.deltaTime;
        //Temperatura
        HeatSource = fc.temp;
        float coeTemp = ((Temp()*5 + Ropa() + HungryN()*1.5f))/7.5f;
        coeTemp-=0.2f;
        coeTemp*=speedTemp;
        temp = Mathf.Clamp(temp + coeTemp*Time.deltaTime, 34f, 40f);
        if(temp < 35f){
            Debug.Log("Baja temperatura");
        }else if(temp > 39f){
            Debug.Log("Alta temperatura");
        }
        //Infección
        levelInf = (isInf)?levelInf += Time.deltaTime*(speedInf):levelInf -= Time.deltaTime*(speedRec);
        //Muerte por sangre
        if(blood <= 0f) hungry.Die();
    }

    void OnTriggerEnter(Collider col){
        if(col.CompareTag("Damage"))
            Debug.Log("Recibe daño");//Hay que describir las distinas formas de recibir daño
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
        return (env.temp + HeatSource - 37f)/60f;
    }

    float HungryN(){
        return (hungry.Carbs/100f - 0.5f)*2f;
    }


}
