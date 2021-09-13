using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public List<Fire> fires = new List<Fire>();
    public float temp;
    public Transform Player;

    void Update(){
        float max = 0;
        for (int i = 0; i < fires.Count; i++){
            Fire f = fires[i];
            if(f.timeToDead <= 0){
                fires.Remove(f);
                Destroy(f.gameObject);
                i--;
            }else{
                float disBetPla = Vector3.Distance(Player.position, f.transform.position);
                if(max < Mathf.Clamp(f.maxDistance - disBetPla, 0, f.maxDistance)*f.tempPerDis)
                    max = Mathf.Clamp(f.maxDistance - disBetPla, 0, f.maxDistance)*f.tempPerDis;
            }
        }
        temp = max;
    }

    public void AddFire(Fire f){
        fires.Add(f);
    }
}
