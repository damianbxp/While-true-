using Cinemachine;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private bool _freeLookActive;

    private void Start() {
        CinemachineCore.GetInputAxis = GetInputAxis;
    }

    private void Update() {
        _freeLookActive = Input.GetKey(KeyCode.LeftAlt); // 0 = left mouse btn or 1 = right
    }

    private float GetInputAxis(string axisName) {
        return !_freeLookActive ? 0 : Input.GetAxis(axisName == "Mouse Y" ? "Mouse Y" : "Mouse X");
    }
}
