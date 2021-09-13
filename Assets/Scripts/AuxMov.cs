using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxMov : MonoBehaviour
{
    public float speed = 1, force, power = 1f;
    Vector3 scaled = Vector3.one * 0.3f;
    Vector3 unscaled = Vector3.one * 0.2377f;
    Vector3 inp;
    public GameObject cameraF;
    Rigidbody rb;
    public GameObject fogata;
    public FireController fc;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inp = new Vector3(0,0,0);
    }

    // Update is called once per frame

    void Update(){
        inp = new Vector3(-Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if(Input.GetKey(KeyCode.A)){
            inp.x = 1f;
            //this.gameObject.transform.position += Vector3.left*Time.deltaTime*speed;
        }
        if(Input.GetKey(KeyCode.D)){
            inp.x = -1f;
            //this.gameObject.transform.position += Vector3.right*Time.deltaTime*speed;
        }
        if(Input.GetKey(KeyCode.W)){
            inp.z = 1f;
            //this.gameObject.transform.position += Vector3.forward*Time.deltaTime*speed;
        }
        if(Input.GetKey(KeyCode.S)){
            inp.z = -1f;
            //this.gameObject.transform.position += Vector3.back*Time.deltaTime*speed;
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton0)){
            Debug.Log("A");
            rb.AddForce(Vector3.up*force, ForceMode.Impulse);
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton1)){
            Debug.Log("B");
        }
        if(Input.GetKey(KeyCode.JoystickButton1)){
            Debug.Log("B");
            power = 1.5f;
        }else{
            power = 1f;
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton2)){
            Debug.Log("X");
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton3)){
            Debug.Log("Y");
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton4)){
            Debug.Log("LB");
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton5)){
            Debug.Log("RB");
            GameObject fire = Instantiate(fogata, new Vector3(0, 0, 0) + this.gameObject.transform.position, Quaternion.identity);
            fc.AddFire(fire.GetComponent<Fire>());
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton6)){
            Debug.Log("Back");
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton7)){
            Debug.Log("Start");
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton8)){
            Debug.Log("Joystick Izquierdo");
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton9)){
            Debug.Log("Joystick Derecho");
        }
    }
    void FixedUpdate()
    {
        if(inp.magnitude > 0.1f){
            float angle = Mathf.Atan2(inp.z, inp.x)*Mathf.Rad2Deg + cameraF.transform.rotation.eulerAngles.y;
	        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.up);
            //rb.MovePosition(transform.position + new Vector3(0, 0, power*speed*Time.deltaTime*inp.normalized.magnitude));
            transform.Translate(new Vector3(0, 0, power*speed*Time.deltaTime*inp.normalized.magnitude));
        }
    }



}