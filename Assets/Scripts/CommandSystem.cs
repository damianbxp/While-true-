using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandSystem : MonoBehaviour
{
    public ComandPrompt cmd;

    private Movement movement;
    private Functions functions;
    public List<int> commands = new List<int>();
    private bool commandDone = false;
    private bool programDone = true;
    private int currentCommand = 0;


    private void Start() {
        movement = GetComponent<Movement>();
        functions = GetComponent<Functions>();

    }

    private void Update() {
        if(commandDone) {
            commandDone = false;
            if(!programDone) runNextCommand();
        }
    }

    void runNextCommand() {
        pickCommand(commands[currentCommand]);
    }

    void pickCommand(int commandId) {
        //Debug.Log("Executing comand: " + commandId);

        switch(commandId) {
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
            case 3: {//store buffor
                if(functions.storeBuffor()) {
                    finishCommand();
                } else {
                    cmd.printToConsole("Nothing to pick up", Color.yellow);
                    finishCommand();
                }
                break;
            }
            case 4: {//execute bufor
                int buffor = functions.executeBuffor();
                if(buffor == -1) {
                    cmd.printToConsole("Buffor is empty", Color.yellow);
                    finishCommand();
                } else {
                    pickCommand(buffor);
                }
                break;
            }
            case 5: { // break
                stopProgram();
                LevelCompleted();
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
            Debug.LogWarning("Loop Finished");
        }
    }

    public void launchProgram() {
        if(cmd.processCode()) {
            if(commands.Count > 0) {
                Debug.LogWarning("Program Launched");
                programDone = false;
                commandDone = true;
            } else {
                Debug.LogWarning("No functions to execute");
            }
        }
    }

    public void stopProgram() {
        programDone = true;
        commands.Clear();
        Debug.LogWarning("Program Done");
    }
    
    void LevelCompleted() {
    }
}
