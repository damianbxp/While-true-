using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableCommends : MonoBehaviour
{
    public GameObject[] commandsPrefabs;

    float nextCommandXPosition = -550;

    private void Start() {
        for(int i = 0; i < commandsPrefabs.Length; i++) {
            GameObject command = Instantiate(commandsPrefabs[i], Vector3.zero, Quaternion.identity);
            command.transform.SetParent(gameObject.transform.GetChild(0).transform);
            command.transform.localScale = Vector3.one;
            command.transform.localPosition = new Vector3(nextCommandXPosition, 40, -40);
            nextCommandXPosition += command.GetComponent<RectTransform>().rect.width;
        }
    }
}
