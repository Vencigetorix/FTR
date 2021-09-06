using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hungry : MonoBehaviour
{
    public PlayerData pd;
    public float Protein, Carbs, Fats, Hunger, Hydration;
    public float MinProtein, MinCarbs, MinFats, MinHydration, MinHunger;
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
            Protein -= ProteinDecreaseRate * Time.deltaTime * coeP;
            float coeCDR = 1f - Mathf.Clamp(1f - pd.temp/36.5f, 0f, 0.5f) + 0.5f;
            if(pd.isInf) coeCDR += 1f;
            Carbs = Mathf.Clamp(Carbs - CarbsDecreaseRate * Time.deltaTime * coeCDR, 0f, 100f);
            Carbs += 0f;
            float coeFats = 1f - Mathf.Clamp(100 - pd.levelRad, 0f, 0.5f) + 0.5f;
            Fats -= FatsDecreaseRate * Time.deltaTime * coeFats;
            Hydration -= HydrationDecreaseRate * Time.deltaTime;
            Hunger = (Protein + Carbs + Fats)/3;
        }



        if (Hunger <= MinHunger)
            Die();
        if (Hydration <= MinHydration)
            Die();
    }

    public void Die()
    {
        Dead = true;
        //Debug.Log("Player is Dead");
        //return;
    }
}
