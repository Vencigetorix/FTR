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
            //Coeficientes
            float coeP = 0; // Coeficiente por daño
            float coePB = 1f - pd.blood/100f; // Coeficiente por sangre
            float coeC = 0; // Coeficiente de calor
            float coeF = pd.levelRad/100f; //Coeficiente por radiación
            float coeCI = pd.levelInf/100f; //Coeficiente por infección
            for (int i = 0; i < pd.body.Length; i++)//Busca partes heridas
                if(pd.body[i] >= 0){
                    coeP = 1f;
                    break;
                }
            if (pd.temp < 35f) 
                coeC = (35f - pd.temp);
            //Proteinas
            float PDR = (1f + coeP + coePB)/3f; //Protein Decrease Rate
            Protein = Mathf.Clamp(Protein - ProteinDecreaseRate * Time.deltaTime * PDR, 0f, 100f);
            //Carbohidratos
            float CDR = (1f + coeC + coeCI)/3; //Carbs Decrease Rate
            Carbs = Mathf.Clamp(Carbs - CarbsDecreaseRate * Time.deltaTime * CDR, 0f, 100f);
            //Grasas
            float FDR = (1 + coeF)/2f; // Fats Decrease Rate
            Fats = Mathf.Clamp(Fats - FatsDecreaseRate * Time.deltaTime * FDR, 0f, 100f);
            //Agua 
            float HDR = (coeC*1 + coeCI*1 + coePB*1 + coeP*1 + coeF*1 + coe*1)/6f; //Hyd Decrease Rate
            Hydration = Mathf.Clamp(Hydration - HydrationDecreaseRate * Time.deltaTime * HDR, 0f, 100f);
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
