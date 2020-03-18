using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    [SerializeField] private bool _isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        _isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true) 
        {
            // to load scene again
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
    }

    public void IsGameOver () 
    {
        _isGameOver = true;
    }
}
