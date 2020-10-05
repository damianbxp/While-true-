using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComandPrompt : MonoBehaviour
{
    private string code;
    int linesCount;
    public int maxLinesCount;
    CommandSystem commandSystem;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI linesCountText;
    public TMP_InputField inputField;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start() {
        commandSystem = transform.GetComponent<CommandSystem>();
        inputField.Select();
        inputField.ActivateInputField();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public bool processCode() {

        if(linesCount > maxLinesCount) {
            printToConsole("Not enought memory for that code", Color.red);
            return false;
        }

        for(int i=1; i<= linesCount; i++) {//processing line by line
            string codeLine = ReadLine(code, i);

            if(codeLine != null && codeLine.Length > 0) {

                switch(codeLine) {
                    case "moveForward();": {
                        Debug.Log("MoveForward function");
                        commandSystem.commands.Add(0);
                        break;
                    }
                    case "turnRight();": {
                        Debug.Log("RotateRight function");
                        commandSystem.commands.Add(1);
                        break;
                    }
                    case "turnLeft();": {
                        Debug.Log("RotateLeft function");
                        commandSystem.commands.Add(2);
                        break;
                    }
                    case "store();": {
                        Debug.Log("Store function");
                        commandSystem.commands.Add(3);
                        break;
                    }
                    case "exec();": {
                        Debug.Log("exec function");
                        commandSystem.commands.Add(4);
                        break;
                    }
                    default: {
                        Debug.Log("Uknown function");
                        printToConsole("I don't understand line " + i, Color.red);
                        commandSystem.commands.Clear();
                        return false;
                    }
                }
            }
        }
        printToConsole("Compile Succesfull", Color.green);
        return true;
    }

    public void updateCode(string newCode) {
        code = newCode;
        linesCount = code.Split('\n').Length;
        linesCountText.text = "Lines Count: " + linesCount + "/" + maxLinesCount;
        if(linesCount > maxLinesCount) linesCountText.color = Color.red;
        else linesCountText.color = Color.green;
    }

    public void printToConsole(string text, Color color) {
        if(color == Color.red) {
            audioSource.volume = 0.4f;
            audioSource.PlayOneShot(audioClip);
        }
        errorText.color = color;
        errorText.text = text;
    }


    private static string ReadLine(string text, int lineNumber) {
        var reader = new StringReader(text);

        string line;
        int currentLineNumber = 0;

        do {
            currentLineNumber += 1;
            line = reader.ReadLine();
        }
        while(line != null && currentLineNumber < lineNumber);

        return (currentLineNumber == lineNumber) ? line :
                                                   string.Empty;
    }
}
