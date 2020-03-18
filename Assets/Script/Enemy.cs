using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemySpeed = 3.0f;

    [SerializeField] private GameObject _laser;
    [SerializeField] private bool _canFire = true;
    [SerializeField] private float _fireRate = 3.0f;
    private float _timer;

    // get the player ship component and run Damage();
    private ShipMovement _playerShip;
    
    // get the Ui manager component and run AddScore();
    private UiManager _uiManager;

    // get the animator component of the enemy
    private Animator _enemyDeathAnimator;

    // to get the game object
    private SpriteRenderer _enemySprite;

    // to get the audio source
    private AudioSource _destroyAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        //_enemySpeed = Random.Range(4, 8);
        _playerShip = GameObject.Find("Ship").GetComponent<ShipMovement>();
        if (_playerShip == null) 
        {
            Debug.LogError("Player ship is not found");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (_uiManager == null) 
        {
            Debug.LogError("Canvas is not found");
        }

        _enemyDeathAnimator = GetComponent<Animator>();
        if (_enemyDeathAnimator == null) 
        {
            Debug.LogError("Animator is not found");
        }

        _enemySprite = GetComponent<SpriteRenderer>();
        if (_enemySprite == null) 
        {
            Debug.LogError("SpriteRenderer is not found");
        }
        
        _destroyAudioClip = GetComponent <AudioSource>();
        
        if (_playerShip == null) 
        {
            Debug.Log("Ship GameObject can not be found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        Fire();
    }
    
    private void EnemyMovement () 
    {
        // it will come down
        transform.Translate (Vector3.down * Time.deltaTime * _enemySpeed);

        // if the enemy reached to bottom of the screen the respawn it to the top of the screen
        if (transform.position.y < -15) 
        {
            transform.position = new Vector3 (Random.Range(-7f, 7f), 7, 0);
        }
    }

    // if collided with the bullet or the player shipo then get destroyed
    private void OnTriggerEnter2D(Collider2D other)  
    {
        switch (other.tag) 
        {
            case ("Bullet") :
                _uiManager.AddScore();
                Destroy(other.gameObject);
                StartCoroutine (EmenyDeathSequence());
                break;
            case ("Player") :
                _playerShip.Damage();
                StartCoroutine (EmenyDeathSequence());
                break;
            default :
                break;
        }
    }

    // to organize the death animation
    IEnumerator EmenyDeathSequence () 
    {
        GetComponent<BoxCollider2D>().enabled = false;
        _destroyAudioClip.Play();
        _enemyDeathAnimator.SetTrigger("OnEnemyDestroy");
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    // shooting features for the enemy
    private void Fire() 
    {
        // if _canfire is true then fire the laser
        if (_canFire == true) 
        {
            Vector3 laserPos = new Vector3 (transform.position.x, transform.position.y - 1.2f, transform.position.z);
            Instantiate (_laser, laserPos, Quaternion.identity);
            //Debug.Break();
            _fireRate = Random.Range(3.0f, 5.0f);
            _canFire = false;
            _timer += Time.deltaTime;
            // if time.time is greater than _fireRate then the _canFire will be true
            if (_timer > _fireRate) 
            {
                _canFire = true;
            }
        }
    }
}