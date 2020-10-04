using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float moveSpeed;
    public float rotateSpeed;
    public Vector3 movePoint;
    public float tileSize = 2f;

    public LayerMask stopMovement;
    public LayerMask Function;

    private int targetRotation = 0;
    private float rotateDirection = 0;

    private CommandSystem commandSystem;

    private bool moveDone = true;
    private bool rotateDone = true;

    private int[] rotationArray = { 0, 90, 180, 270 };

    private void Start() {
        commandSystem = GetComponent<CommandSystem>();
        movePoint = transform.position;
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.deltaTime);
        transform.Rotate(0, rotateDirection * rotateSpeed * Time.deltaTime, 0);


        if(Mathf.Abs(rotationArray[targetRotation] - transform.rotation.eulerAngles.y) < 1f && moveDone && !rotateDone) {
            //Debug.LogWarning("rotate done");
            rotateDone = true;
            rotateDirection = 0;
            commandSystem.finishCommand();
        }


        if(Vector3.Distance(transform.position, movePoint) <= 0.05f && rotateDone && !moveDone) {
            //Debug.LogWarning("move done");
            moveDone = true;
            commandSystem.finishCommand();
        }
    }

    public void MoveForward() {

        if(Physics.OverlapSphere(movePoint + transform.forward * tileSize, 0.2f, stopMovement).Length > 0 ||
            Physics.OverlapSphere(movePoint + transform.forward * tileSize, 0.2f, Function).Length > 0) {
            //Debug.LogWarning("Hitted sth");
            commandSystem.finishCommand();
        } else {
            //Debug.Log("Moving Forward");
            moveDone = false;
            movePoint += transform.forward * tileSize;
        }
        
    }



    public void Rotate(int direction) {
        if(direction < 0) {
            rotateDirection = -1;
            targetRotation--;
            if(targetRotation < 0) targetRotation = 3;
        } else { 
            rotateDirection = 1;
            targetRotation++;
            if(targetRotation > 3) targetRotation = 0;
        }
        //Debug.Log("Rotating");
        rotateDone = false;
    }
}
