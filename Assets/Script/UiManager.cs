using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private int _playerScore = 0;

    // for lives 
    [SerializeField] private Image _lifeImage;
    [SerializeField] private Sprite [] _lifeSprite;

    // game over
    [SerializeField] private Text _gameOverText;

    // restart text
    [SerializeField] private Text _restartText;

    // to reload the grab the game manager component
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverText.text = "";
        _restartText.text = "";
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Player score :: " + _playerScore.ToString();
    }

    public void AddScore() 
    {
        _playerScore += 10;
    }
    
    public void UpdateLives (int currentLifeIndex) 
    {
        // display image sprite
        // give it a new one based on the current life index
        _lifeImage.sprite = _lifeSprite [currentLifeIndex];
        if (currentLifeIndex == 0) 
        {
            DeathSequence ();
        }
    }

    // when lives are zero then
    private void DeathSequence () 
    {
        StartCoroutine(GameOverTextFlicker());
         _restartText.text = "Press R to Restart";
        _gameManager.IsGameOver();
    }

    // game over text flicker
    IEnumerator  GameOverTextFlicker ()
    {
        while (true) 
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
