using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    private Movement movement;
    public LayerMask functionMask;

    private int functionBuffor = -1;

    private void Start() {
        movement = transform.GetComponent<Movement>();    
    }

    public bool storeBuffor() {
        if(Physics.OverlapSphere(transform.position + transform.forward * movement.tileSize, 0.2f, functionMask).Length > 0) {
            Collider[] colision = Physics.OverlapSphere(transform.position + transform.forward * movement.tileSize, 0.2f, functionMask);
            functionBuffor = colision[0].transform.GetComponent<PickableFunction>().commandTypeId;
            return true;
        }
        return false;
    }

    public int executeBuffor() {
        return functionBuffor;
    }
}
