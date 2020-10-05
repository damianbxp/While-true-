using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameConsole : MonoBehaviour {

    public TextMeshProUGUI consoleText;
    public TMP_InputField inputField;
    public CommandSystem commandSystem;
    public LevelManager levelManager;

    string[] startText = {
        "Infinite loop detected\n",
        "Initializing self-repair", ".", ".", ".",
        "\tFAILED\n",
        "Manual fix required\n",
        "type 'help' for additional information"
    };
    bool startTextLoading = true;
    int consoleLoadLine = 0;
    float lastTime;
    public float minLogTime = 0.5f;
    public float maxLogTime = 1f;
    float delayTime = 0.5f;

    public bool levelFinished = false;

    private void Start() {
        lastTime = Time.time;
    }

    private void Update() {
        if(startTextLoading) consoleStartText();
    }

    public void processCommand(string command) {
        startTextLoading = false;
        switch(command) {
            case "exit": {
                Application.Quit();
                break;
            }
            case "help": {
                consoleText.text = "exit\t\t\texit program\n" +
                "run\t\t\trun your code\n" +
                "stop\t\t\tstops running program\n" +
                "reload\t\treloads level\n" +
                "menu\t\t\tloads main menu\n" +
                "commands\t\tdisplays available commands\n" +
                "help\t\t\tdisplays this help";
                inputField.text = "";
                break;
            }
            case "commands": {
                consoleText.text = "moveForward();\t\tmove forward one tile\n" +
                    "turnLeft();\t\t\tturn left\n" +
                    "turnRight();\t\t\tturn right\n" +
                    "store();\t\t\tstore function in buffor\n" +
                    "exec();\t\t\trun stored function";
                inputField.text = "";
                break;
            }
            case "run": {
                consoleText.text = "Running Program...";
                inputField.text = "";
                commandSystem.launchProgram();
                break;
            }
            case "stop": {
                consoleText.text = "Stoping Program...";
                inputField.text = "";
                commandSystem.stopProgram();
                break;
            }
            case "reload": {
                levelManager.reloadLevel();
                break;
            }
            case "menu": {
                levelManager.LoadLevel(0);
                break;
            }
            case "next": {
                if(levelFinished) {
                    levelManager.LoadNextLevel();
                } else {
                    consoleText.text = "Command execution failed";
                    inputField.text = "";
                }
                break;
            }
            default: {
                consoleText.text = "Unknown Command: '" + command + "'"+ "\ntype 'help' for help";
                inputField.text = "";
                break;
            }
        }
    }

    void consoleStartText() {
        if(Time.time - lastTime > delayTime) {
            if(consoleLoadLine > startText.Length - 1) {
                startTextLoading = false;
            } else {
                consoleText.text += startText[consoleLoadLine];
                consoleLoadLine++;
                lastTime = Time.time;
                delayTime = Random.Range(minLogTime, maxLogTime);
            }


        }
    }
}
