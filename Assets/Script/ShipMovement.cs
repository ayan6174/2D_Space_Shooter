using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private bool _speedBoostEnable = false;
    [SerializeField] private bool _shieldEnable = false;
    [SerializeField] private int _life = 3;
    [SerializeField] private bool _shieldVisual = false;
    [SerializeField] private GameObject _shieldVisulizer;
    [SerializeField] private GameObject _damageFireRight;
    [SerializeField] private Animator _damageFireRightAnimation;
    [SerializeField] private GameObject _damageFireLeft;
    [SerializeField] private Animator _damageFireLeftAnimator;
    [SerializeField] private AudioSource _audioClip;
    [SerializeField] private AudioClip _destroyAudioClip;
    [SerializeField] private AudioClip _powerUpAudioClip;
    [SerializeField] private AudioClip _damageAudioClip;
    
    private SpawnManager sm; // to stop the enemy spawning
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3 (0, -3, 0);

        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(sm == null) 
        {
            Debug.Log("SpawnManager is not found");
        }

        _shipGun = GameObject.Find("ShipCannon");
        if(_shipGun == null) 
        {
            Debug.Log("Ship gun is not found in ship");
        }
        
        _damageFireRight.SetActive(false);
        _damageFireRightAnimation = _damageFireRight.GetComponent <Animator>();
 
        _damageFireLeft.SetActive(false);
        _damageFireLeftAnimator = _damageFireLeft.GetComponent <Animator>();
        
        // for visualzing the shield
        _shieldVisulizer.SetActive(false);

        _audioClip = GetComponent <AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // to bound the max and min range of the player in x and y axes
        float xPos = Mathf.Clamp(transform.position.x, -8f, 8f);
        float yPos = Mathf.Clamp (transform.position.y, -4.0f, 4.0f);
        transform.position = new Vector3 (xPos, yPos, 0);
        
        // player monement
        PlayerMovement();
    }

    // to move the player
    void PlayerMovement () 
    {
        float horizontalInput = Input.GetAxis ("Horizontal");
        transform.Translate (Vector3.right * horizontalInput * _speed * Time.deltaTime);

        float verticalInput = Input.GetAxis ("Vertical");
        transform.Translate (Vector3.up * verticalInput * _speed * Time.deltaTime);
    }

    // life and damage system
    public void Damage () 
    {
        if (_shieldEnable == false) 
        {
        _life--;
        _audioClip.clip = _damageAudioClip;
        _audioClip.Play();
        GameObject.Find("Canvas").GetComponent<UiManager>().UpdateLives(_life);
        Debug.Log("Player life remains :: " + _life);
            if (_life == 0) 
            {
                sm.StopSpawning();
                _audioClip.clip = _destroyAudioClip;
                _audioClip.Play();
                Destroy(this.gameObject);
            }
            else if (_life == 2) 
            {
                _damageFireRight.SetActive(true);
                _damageFireRightAnimation.SetTrigger("DamageFire");
            }
            else if (_life == 1) 
            {
                _damageFireLeft.SetActive(true);
                _damageFireLeftAnimator.SetTrigger("DamageFire");
            }
        }
    }

    private GameObject _shipGun;
    private void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.tag == "PowerUp") 
        {
            Debug.Log("Collided with powerup");
            _audioClip.clip = _powerUpAudioClip;
            _audioClip.Play();
            // if true then turn _tripleShot into true in 'ShipGun' script
            _shipGun.GetComponent<ShipGun>().EnableTripleShot();
        }
    }

    // when gets hit with the speedBoost the speed is increased by 8 units
    public void EnableSpeedBoost () 
    {
        _audioClip.clip = _powerUpAudioClip;
        _audioClip.Play();
        _speedBoostEnable = true;
        _speed += 8.0f;
        StartCoroutine(DisableSpeedBoost());
    }
    // to disable the speed boost power up
    IEnumerator DisableSpeedBoost()
    {
        while (_speedBoostEnable == true) 
        {
            yield return new WaitForSeconds(10.0f);
            _speedBoostEnable = false;
            _speed -= 8.0f;
        }
    }

    // to enable the shield when gets hit with the shield power up
    public void EnableShield () 
    {
        _audioClip.clip = _powerUpAudioClip;
        _audioClip.Play();
        _shieldEnable = true;
        _shieldVisulizer.SetActive(true);
        // take no damage
        StartCoroutine(DisableShieldPowerUp());
    }
    
    // to disable the shield power up
    IEnumerator DisableShieldPowerUp()
    {
        while (_shieldEnable == true) 
        {
            yield return new WaitForSeconds (10);
            _shieldEnable = false;
            _shieldVisulizer.SetActive(false);
        }
    }
}