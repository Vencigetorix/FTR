using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGhost : MonoBehaviour
{
    private RaycastHit hit;

    
    void OnTriggerEnter(Collider col){
        MeshRenderer mr = col.gameObject.GetComponent<MeshRenderer>();
        mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 0.5f);
        Debug.Log(mr.material.color);
        //.gameObject.SetActive(false);
    }
    void OnTriggerExit(Collider col){
        MeshRenderer mr = col.gameObject.GetComponent<MeshRenderer>();
        mr.material.color = new Color(mr.material.color.r, mr.material.color.g, mr.material.color.b, 1f);
        //col.gameObject.GetComponent<Renderer>().material = mat[0];
    }
}
