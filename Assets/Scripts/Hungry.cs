using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hungry : MonoBehaviour
{
    public PlayerData pd;
    public float Protein, Carbs, Fats, Hunger, Hydration;
    //[HideInInspector]
    public float ProteinDecreaseRate, CarbsDecreaseRate, FatsDecreaseRate, HydrationDecreaseRate;
    public bool Dead;

   
    void Update()
    {
        if (!Dead)
        {
            //Proteinas
            float coeP = 0;
            for (int i = 0; i < pd.body.Length; i++)//Busca partes heridas
                if(pd.body[i] >= 0){
                    coeP = 1f;
                    break;
                }
            float coePB = 1f - pd.blood/100f;
            float PDR = (1f + coeP + coePB)/3f;
            Protein = Mathf.Clamp(Protein - ProteinDecreaseRate * Time.deltaTime * PDR, 0f, 100f);
            //Carbohidratos
            float coeC = 0, coeCI;
            if (pd.temp < 35f)
                coeC = (35f - pd.temp);
            coeCI = pd.levelInf/100f;
            float CDR = (1f + coeC + coeCI)/3;
            Carbs = Mathf.Clamp(Carbs - CarbsDecreaseRate * Time.deltaTime * CDR, 0f, 100f);
            //Grasas
            float coeF = pd.levelRad/100f;
            float FDR = (1 + coeF)/2f;
            Fats = Mathf.Clamp(Fats - FatsDecreaseRate * Time.deltaTime * FDR, 0f, 100f);
            //Agua
            Hydration = Mathf.Clamp(Hydration - HydrationDecreaseRate * Time.deltaTime, 0f, 100f);
            //Comida general
            Hunger = (Protein + Carbs + Fats)/3;
        }



        /*if (Hunger <= MinHunger)
            Die();
        if (Hydration <= MinHydration)
            Die();*/
    }

    public void Die()
    {
        Dead = true;
        //Debug.Log("Player is Dead");
    }
}
