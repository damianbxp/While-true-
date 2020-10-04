using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameConsole : MonoBehaviour {

    public TextMeshProUGUI consoleText;
    public TMP_InputField inputField;
    public CommandSystem commandSystem;
    public LevelManager levelManager;

    public void processCommand(string command) {

        switch(command) {
            case "exit": {
                Application.Quit();
                break;
            }
            case "help": {
                consoleText.text = "exit\t\t\texit program\n" +
                "run\t\t\trun your code\n" +
                "stop\t\t\tstops running program\n" +
                "reload\t\t\treloads level\n" +
                "menu\t\t\tloads main menu\n" +
                "help\t\t\tdisplays this help";
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
            default: {
                consoleText.text = "Unknown Command: '" + command + "'";
                inputField.text = "";
                break;
            }
        }
    }

}
