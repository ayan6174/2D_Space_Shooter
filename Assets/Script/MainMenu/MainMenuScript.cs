using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadGameScene() 
    {
        // load the game scene
        SceneManager.LoadScene(1);
    }

}
