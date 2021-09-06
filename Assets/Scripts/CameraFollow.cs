using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public GameObject cam, camAux;
    public float smoothTime = 0.5f;
    public float smoothTime2 = 0.5f;
    public float invAxis = 1f;
    float smoothSpeed = 0.0f; 
    float smoothSpeedX = 0.0f; 
    float smoothSpeedY = 0.0f; 
    float smoothSpeedZ = 0.0f; 
    public float maxDRC = 3f;
    public float speed = 50f, altC = 0f, altCAux = 0f, maxAltC = 5f, minAltC = 0;
    Vector3 posCam, posCamAux;


    void Start(){
        posCam = camAux.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        altC = Mathf.Clamp(altC + invAxis*speed*0.1f*Time.deltaTime*Input.GetAxis("rstickv"), minAltC, maxAltC);
        this.gameObject.transform.Rotate(0, Time.deltaTime*speed*Input.GetAxis("rstickh"), 0, Space.World);
        if(Input.GetKey(KeyCode.UpArrow))
            altC = Mathf.Clamp(altC + invAxis*speed*0.07f*Time.deltaTime, minAltC, maxAltC);
        if(Input.GetKey(KeyCode.DownArrow))
            altC = Mathf.Clamp(altC - invAxis*speed*0.07f*Time.deltaTime, minAltC, maxAltC);
        if(Input.GetKey(KeyCode.LeftArrow))
            this.gameObject.transform.Rotate(0, -Time.deltaTime*speed, 0, Space.World);
        if(Input.GetKey(KeyCode.RightArrow))
            this.gameObject.transform.Rotate(0, Time.deltaTime*speed, 0, Space.World);
    }

    void FixedUpdate(){
        this.transform.position = player.transform.position;
        if(altCAux <= 10f){//Si la altura es poca, se hace el suavizado horizontal de la cámara
            posCam.x = Mathf.SmoothDamp(posCam.x, camAux.transform.position.x, ref smoothSpeedX, smoothTime2);
            posCam.z = Mathf.SmoothDamp(posCam.z, camAux.transform.position.z, ref smoothSpeedZ, smoothTime2);    
            posCam.y = Mathf.SmoothDamp(posCam.y, camAux.transform.position.y, ref smoothSpeedY, smoothTime2);
        }else{//En otro caso, la cámara no tiene suavizado horizontal
            posCam.x = camAux.transform.position.x;
            posCam.z = camAux.transform.position.z;
            posCam.y = camAux.transform.position.y;
        }
        //Suavizado vertical
        altCAux = Mathf.SmoothDamp(altCAux, altC, ref smoothSpeed, smoothTime);
        posCamAux = posCam + new Vector3(0f, altCAux,0f);
        //Seguimiento de la cámara
        cam.transform.position = posCamAux;
        cam.transform.LookAt(player.transform.position + Vector3.up*0.4f, Vector3.up);
        //Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward)* maxDRC, Color.yellow);
        //RaycastHit hit;
        /*if(Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, maxDRC)){
            Debug.Log("Colisión");
            Debug.Log(hit.collider.tag);
            if(hit.collider.CompareTag("Terrain")){
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(255f,255f,255f,0f);
            }
        }*/
        //transform.TransformDirection(Vector3.forward)
    }
}
