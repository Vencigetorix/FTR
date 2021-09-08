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
            float coeP = 0f;
            for (int i = 0; i < pd.body.Length; i++)
                coeP += pd.body[i];
            coeP = Mathf.Clamp(coeP, 0f, 0.5f) + 0.5f;
            float coePB = 2f - pd.blood/100f;
            Protein = Mathf.Clamp(Protein - ProteinDecreaseRate * Time.deltaTime * coeP * coePB, 0f, 100f);

            //float coeCDR = 1f - Mathf.Clamp(1f - pd.temp/36.5f, 0f, 0.5f) + 0.5f;
            float coeCDR = Mathf.Clamp(1f - pd.temp/36.5f, 0f, 0.5f) + 0.5f;
            if(pd.isInf) coeCDR += 1f;
            //Debug.Log(coeCDR);
            Carbs = Mathf.Clamp(Carbs - CarbsDecreaseRate * Time.deltaTime * coeCDR, 0f, 100f);
            Carbs += 0f;
            float coeFats = 1f - Mathf.Clamp(100 - pd.levelRad, 0f, 0.5f) + 0.5f;
            Fats = Mathf.Clamp(Fats - FatsDecreaseRate * Time.deltaTime * coeFats, 0f, 100f);
            Hydration = Mathf.Clamp(Hydration - HydrationDecreaseRate * Time.deltaTime, 0f, 100f);
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
        //return;
    }
}
