using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] scenes;
    int currentScene;

    private void Start() {
        currentScene = Array.IndexOf(scenes, SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(int sceneId) {
        currentScene = sceneId;
        SceneManager.LoadScene(scenes[sceneId]);
    }

    public void LoadNextLevel() {
        if(currentScene < scenes.Length) {
            LoadLevel(currentScene);
        } else {
            LoadLevel(0);
        }

        
    }
    public void reloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
