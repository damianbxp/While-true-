﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI menuText;
    public TextMeshProUGUI consoleText;
    public LevelManager levelManager;
    public TMP_InputField inputField;



    int menuLine = 0;
    string[] menuLines = {
        "Rebooting",
        ".",
        ".",
        ".",
        "\nInitializing",
        "\n\t HVA-8K",
        "\n\t HUF-6D",
        "\n\t TAH-8B",
        "\n\t AJHE-K51",
        "\n\t UWM-17K",
        "\nDetecting Leaks",
        "\nRemoving Cache",
        "\nWarning - Infinite Loop Detected",
        "\nAdjusting Vision",
        ".", ".",".",
        "\n\tRed Chanel",
        " OK",
        "\n\tBlue Chanel",
        " OK",
        "\n\tGreen Chanel",
        " OK",
        "\nRebooting Completed",
        "\nType 'help' for help"
    };

    bool startTextLoading = true;

    float lastTime;
    public float minLogTime = 0.5f;
    public float maxLogTime = 1f;
    float delayTime = 0.5f;

    private void Start() {
        lastTime = Time.time;
        inputField.Select();
        inputField.ActivateInputField();

        Cursor.lockState = CursorLockMode.Confined;

        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/
    }

    private void Update() {
        if(startTextLoading) startText();
        if(EventSystem.current.currentSelectedGameObject != inputField.gameObject) EventSystem.current.SetSelectedGameObject(inputField.gameObject);
    }

    void startText() {
        if(Time.time - lastTime > delayTime) {
            if(menuLine > menuLines.Length - 1) {
                startTextLoading = false;
            } else {
                menuText.text += menuLines[menuLine];
                menuLine++;
                lastTime = Time.time;
                delayTime = Random.Range(minLogTime, maxLogTime);
            }
            

        }
    }

    public void processConsoleInput(string command) {
        startTextLoading = false;
        switch(command) {
            case "help": {
                menuText.text = "About game:\n" +
                    "Someone placed infinite loop in robot operating system.\nYour job is to fix that.\nYou have to get 'break;' function to break loop.\n" +
                    "First you need to get to function, then grab it and finaly execute\n" +
                    "\n on left you will have program to modify (remember that you are in infinite loop)" +
                    "\n lines you wrote are limited (shown on left bottom)" +
                    "\n\non right you will have console to run robot" +
                    "\n\n robot can't go through objects (he will dismiss command)" +
                "\nexit\t\t\t\texit program\n" +
                "start\t\t\t\tstart game\n" +
                "load <level>\t\t\tloads level specified in <level>\n" +
                "credits\t\t\tdisplays credits\n" +
                "help\t\t\t\tdisplays this help";
                consoleText.text = "";
                break;
            }
            case "start": {
                // loading level 1
                Debug.Log("Loading level 1");
                levelManager.LoadLevel(1);
                break;
            }
            case "credits": {
                menuText.text = "Game created for Ludum Dare 47 by Damian Bartuś" +
                    "\n\n\nAudio source - Freesound:\n" +
                    "Sci-fi Laboratory Ambience by Iwan Gabovitch under CC-BY 3.0 License with sounds by wolick, swiftoid, Diboz\n" +
                    "squashy555\n" +
                    "Autistic Lucario\n";
                consoleText.text = "";
                break;
            }
            case "exit": {
                Application.Quit();
                break;
            }
            default: {
                if(command.Length > 3) {
                    if(command.Substring(0, 4) == "load") {
                        if(command.Length > 4) {
                            string iStr = "" + command[5];
                            int levelNumber = 0;
                            if(int.TryParse(iStr, out levelNumber)) {
                                if(levelNumber > 0 && levelNumber < levelManager.scenes.Length) {
                                    //loading level
                                    Debug.Log("Loading level: " + levelNumber);
                                    levelManager.LoadLevel(levelNumber);
                                } else {
                                    menuText.text = "Level not Found";
                                }

                            } else {
                                menuText.text = "Not valid argument";
                            }
                        } else {
                            menuText.text = "Argument not found";
                        }
                    } else {
                    }
                } else {
                    menuText.text = "Unknown Command: '" + command + "'" + "\ntype 'help' for help";
                }
                break;
            }
        }
    }
}
