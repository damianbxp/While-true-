using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour
{
    public float rotateSpeed;
    public float moveSpeed;
    public Vector3 moveOffset;
    private Transform mesh;
    private Vector3 originalPosition;
    private int moveSwich = 1;

    private void Start() {
        mesh = transform.GetChild(0).transform;
        originalPosition = mesh.position;
    }
    void Update()
    {
        mesh.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        mesh.position = Vector3.MoveTowards(mesh.position, originalPosition + moveSwich * moveOffset, moveSpeed * Time.deltaTime);
        if(mesh.position == originalPosition + moveSwich * moveOffset) {
            moveSwich *= -1;
        }
    }
}
