using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class Program : MonoBehaviour
{
    public GameObject endSemicolon;
    public Transform player;
    //public string[] commands;

    private GameObject[] commandsPrefabs;

    private List<GameObject> addedCommends = new List<GameObject>();

    private void Start() {
        commandsPrefabs = transform.parent.GetChild(1).GetComponent<AvailableCommends>().commandsPrefabs;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) addCommand(0);
        if(Input.GetKeyDown(KeyCode.W)) addCommand(1);
        if(Input.GetKeyDown(KeyCode.E)) addCommand(2);
    }

    public void addCommand(int i) {
        GameObject command = Instantiate(commandsPrefabs[i], Vector3.zero, Quaternion.identity);

        command.transform.SetParent(gameObject.transform.GetChild(0).transform);
        command.transform.localScale = Vector3.one;
        command.transform.localPosition = new Vector3(-150, 380 - 60 * i, -40);
        addedCommends.Add(command);
        updateProgram();
        
    }

    public void updateProgram() {
        for(int i = 0; i < addedCommends.Count; i++) {
            addedCommends[i].transform.localPosition = new Vector3(-150, 380 - 60 * i, -40);
        }
        endSemicolon.transform.localPosition = new Vector3(-150, 380 - 60 * addedCommends.Count, -40);
    }

    public void changeOrder(int oldId ,int newId) {
        if(addedCommends.Count > 1) {
            GameObject temp = addedCommends[oldId];
            addedCommends.RemoveAt(oldId);
            addedCommends.Insert(newId, temp);

            //Debug.Log("Placed item at " + newId);

        }
        fixCommandSlot();

        updateProgram();
    }

    public void deleteCommand(int index) {
        if(addedCommends.Count == 1) {
            Destroy(addedCommends[0]);
            addedCommends.Clear();
        } else if(addedCommends.Count > 1) {
            addedCommends.RemoveAt(index);
            Destroy(addedCommends[index]);
        }
        fixCommandSlot();
    }

    private void fixCommandSlot() {
        if(addedCommends.Count > 0) {
            foreach(GameObject command in addedCommends) {
                command.GetComponent<DragDrop>().commandSlot = addedCommends.IndexOf(command);
            }
        }
    }

    public void launchProgram() {

        foreach(GameObject command in addedCommends) {
            player.GetComponent<CommandSystem>().commands.Add(command.transform.GetComponent<DragDrop>().commandTypeId);
        }
        player.GetComponent<CommandSystem>().launchProgram();
    }
}
