using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    public GameObject bullet;
    private int bulletCount = 0;
    [SerializeField] int maxBullet = 10;
    [SerializeField] bool canFire = true;
    [SerializeField] float fireRate = 1.0f;
    private float nextFire = 0.0f;
    private float timer = 0.0f;
    [SerializeField] float waitTime = 5.0f;
    
    [SerializeField] private bool _tripleShot = false;
    [SerializeField] private float _tripleShotDuration = 10.0f;
    private float _tripleShotTimer = 0.0f;
    
    [SerializeField] private float _cannonOffsetY;
    [SerializeField] private float _cannon2OffsetX;
    [SerializeField] private float _cannon3OffsetX;

    [SerializeField] private AudioSource _laserAudio;

    void Start () 
    {
        _tripleShot = false;

        _laserAudio = GetComponent <AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MaingunFire();
        
        GunCoolDown();

        DisableTripleShot();
    }

    void MaingunFire () 
    {
        
        if ( Input.GetKey(KeyCode.Space) 
        && bulletCount < maxBullet  // if bullet count reaches to max then can not fire
        && Time.time > nextFire 
        && canFire == true ) 
        {
            Instantiate (bullet, transform.position, Quaternion.identity);
            // laser audio play
            _laserAudio.Play();

            if (_tripleShot == true) 
            {
                // for cannon2
                Vector3 newPos2= new Vector3(transform.position.x + _cannon2OffsetX, transform.position.y + _cannonOffsetY, transform.position.z);
                Instantiate(bullet, newPos2, Quaternion.identity);

                // for cannon3
                Vector3 newPos3= new Vector3(transform.position.x + _cannon3OffsetX, transform.position.y + _cannonOffsetY, transform.position.z);
                Instantiate(bullet, newPos3, Quaternion.identity);
            }

            nextFire = Time.time + fireRate; // control the fire rate
            bulletCount = bulletCount + 1; // count the max bullt number in order to implement the GunCoolDonw()
            
            // when the gun gets hot it can not fire
            if (bulletCount == maxBullet) 
            {
                canFire = false;
                Debug.Log("Weapon is gets hot");
            }
        }
    }

    void GunCoolDown () 
    {
        // wait for the gun to cool down
        if (canFire == false) 
        {
            timer += Time.deltaTime;
            if (timer > waitTime) 
            {
                canFire = true;
                bulletCount = 0;
                timer = 0.0f;
                Debug.Log("Weapon is cool now");
            }
        }
    }

    public void EnableTripleShot() 
    {
        _tripleShot = true;
    }

    private void DisableTripleShot () 
    {
        // add a timer for 10 sec (from inspector) 
        if (_tripleShot == true) 
        {
            _tripleShotTimer += Time.deltaTime;
            if (_tripleShotTimer > _tripleShotDuration) 
            {
                _tripleShot = false;
                _tripleShotTimer = 0.0f;
                Debug.Log("Triple shot is disabled.");
            }
        }
    }
}