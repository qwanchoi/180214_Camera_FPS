using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {
    public float sensitivity = 100f;
    float rotationX;
    float rotationY;

    public GameObject shell;
    GameObject character;

    void Start () {
        character = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
        float h = Input.GetAxis ( "Mouse X" );
        float v = Input.GetAxis ( "Mouse Y" );
        print ( "mouseLook.cs >> " + h );
        rotationY += h * sensitivity * Time.deltaTime;
        rotationX += v * sensitivity * Time.deltaTime;

        if( rotationX > 85f ) rotationX = 85f;
        if( rotationX < -70f ) rotationX = -70f;

        transform.localRotation = Quaternion.AngleAxis ( -rotationX , Vector3.right );
        character.transform.localRotation = Quaternion.AngleAxis ( rotationY , Vector3.up );

        if ( Input.GetKeyDown ( "escape" ) ) {
            Cursor.lockState = CursorLockMode.None;
        }

        if ( Input.GetKey (KeyCode.Mouse0 ) ) {
            var myShell = Instantiate ( shell , transform.Find ( "shellPos" ).transform.position , transform.rotation );
            myShell.GetComponent<Rigidbody> ().AddForce ( transform.right * Random.Range(100f,300f));
            //myShell.GetComponent<Rigidbody> ().AddTorque ( Random.insideUnitSphere * 100f );
            //.AddTorque(Random.insideUnitySphere * force); (회전하는 힘)

            Destroy ( myShell , 50.0f );
        }
	}
}
