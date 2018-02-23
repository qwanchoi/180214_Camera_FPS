using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public float moveSpeed = 8f;
    public ParticleSystem myFx;

    CharacterController con;
    Vector3 moveDirection = Vector3.zero;

    float jumpSpeed = 8f;
    float gravity = 30f;

    private ParticleSystem buff;
    private bool ctrlFlag = true;
    //private float slideSpeed = 5f;
    private Vector3 slidingV;


    void Start () {
        con = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(con.isGrounded) {
            float h = Input.GetAxisRaw ( "Horizontal" );
            float v = Input.GetAxisRaw ( "Vertical" );

            moveDirection = ( new Vector3 ( h , 0f , v ) );
            //transform.LookAt ( transform.position + moveDirection );

            moveDirection = transform.TransformDirection ( moveDirection );
            /*ㄴ로컬공간에서 월드공간으로 /direction/을 변환합니다.
                이 연산은 transform의 스케일이나 위치에 영향 받지 않습니다. 반환된 벡터는 /direction/처럼 같은 길이를 갖습니다.
             */
            if(!ctrlFlag) {
                moveDirection += slidingV;
            }

            moveDirection *= moveSpeed;
            if ( Input.GetButtonDown ( "Jump" ) && ctrlFlag)
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        con.Move ( moveDirection * Time.deltaTime );

        //if( buff != null) {
        //    buff.transform.position = new Vector3 ( transform.position.x , transform.position.y-1f, transform.position.z );
        //}
	}
    private void OnControllerColliderHit( ControllerColliderHit hit ) {

        if( hit.collider.gameObject.name == "step3" ) {
            
            if( buff == null) { // if(!isHealing) { ~ }
                //buff = Instantiate ( myFx , new Vector3 ( transform.position.x , transform.position.y-1f , transform.position.z ) , Quaternion.Euler(90f,0f,0f), transform );
                buff = Instantiate ( myFx , transform.Find( "FxPos" ).transform.position , Quaternion.Euler ( 90f , 0f , 0f ) , transform );
                Destroy ( buff.gameObject , 1.9f );
                // Invoke("함수명" , 1.9f); -> 1.9초 뒤에 함수 호출
            }

        }
        //print (hit.normal.z/ hit.normal.y); => 45도 => 1.0;
        float degreeLimit = GetComponent<CharacterController> ().slopeLimit;
        float curDegree = Vector3.Angle ( transform.up, hit.normal );
        slidingV = new Vector3 ( hit.normal.x , hit.normal.y-5f, hit.normal.z );

        //Debug.DrawRay ( transform.position , hit.normal , Color.magenta , 5f );

        //print ("Move.cs >> " + curDegree );
        if( curDegree > degreeLimit ) {
            ctrlFlag = false;
            moveDirection = slidingV;
        } else {
            ctrlFlag = true;
        }
    }


}
