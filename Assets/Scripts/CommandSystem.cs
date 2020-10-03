using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSystem : MonoBehaviour
{
    private Movement movement;
    //public string[] commands;
    public List<int> commands = new List<int>();
    private bool commandDone = false;
    private bool programDone = true;
    private int currentCommand = 0;

    private void Start() {
        movement = GetComponent<Movement>();
    }

    private void Update() {
        if(commandDone) {
            commandDone = false;
            if(!programDone) runNextCommand();
        }
    }

    void runNextCommand() {
        //Debug.Log("Executing comand: " + currentCommand);
        switch(commands[currentCommand]) {
            case 0: {//forward
                movement.MoveForward();
                break;
            }
            case 1: {//right
                movement.Rotate(1);
                break;
            }
            case 2: {//left
                movement.Rotate(-1);
                break;
            }
            default: {
                Debug.LogError("Command Not Found");
                break;
            }
        }
    }

    public void finishCommand() {
        commandDone = true;
        if(currentCommand < commands.Count - 1) currentCommand++;
        else {
            currentCommand = 0;
        }
    }

    public void launchProgram() {
        programDone = false;
        commandDone = true;
    }

    public void stopProgram() {
        programDone = true;
        commands.Clear();
        Debug.LogWarning("Program Done");
    }
}
