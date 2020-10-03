using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ComandPrompt : MonoBehaviour
{
    private string code;
    int linesCount;
    CommandSystem commandSystem;
    public TextMeshProUGUI errorText;
    private bool isCodeFine = true;

    private void Start() {
        commandSystem = transform.GetComponent<CommandSystem>();
    }

    public bool processCode() {

        for(int i=1; i<= linesCount; i++) {//processing line by line
            string codeLine = ReadLine(code, i);

            switch(codeLine) {
                case "moveForward();": {
                    Debug.Log("MoveForward function");
                    commandSystem.commands.Add(0);
                    break;
                }
                case "rotateRight();": {
                    Debug.Log("RotateRight function");
                    commandSystem.commands.Add(1);
                    break;
                }
                case "rotateLeft();": {
                    Debug.Log("RotateLeft function");
                    commandSystem.commands.Add(2);
                    break;
                }
                default: {
                    Debug.Log("Uknown function");
                    errorText.text = "I don't understand line " + i;
                    return false;
                    break;
                }
            }
        }
        return true;
    }

    public void updateCode(string newCode) {
        code = newCode;
        linesCount = code.Split('\n').Length;
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
