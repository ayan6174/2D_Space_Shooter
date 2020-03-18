using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    [SerializeField] private int _rotationSpeed;

    private Animator _explosionAnimation;

    private SpawnManager _spawnManager;

    private ShipMovement _playerShip;

    private AudioSource _destroyAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        _explosionAnimation = GameObject.Find("Explosion").GetComponent<Animator>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent <SpawnManager>();
        _playerShip = GameObject.Find("Ship").GetComponent <ShipMovement>();
        _destroyAudioClip = GetComponent <AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    // check for the laser collision 
    // instantiate the explosion at the position of the astroid
    // destroy the explosion in 3 sec form the scene

    private void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.tag == "Bullet") 
        {
            Destroy(other.gameObject);
            _explosionAnimation.SetTrigger("StartExplosion");
            _destroyAudioClip.Play();
            GetComponent <SpriteRenderer> ().enabled = false;
            GetComponent <CircleCollider2D> ().enabled = false;
            _spawnManager.StartEnemyWave();
            Destroy(this.gameObject, 3.0f);
        }
        else if (other.tag == "Player") 
        {
            _playerShip.Damage();
            _explosionAnimation.SetTrigger("StartExplosion");
            _destroyAudioClip.Play();
            GetComponent <SpriteRenderer> ().enabled = false;
            GetComponent <CircleCollider2D> ().enabled = false;
            _spawnManager.StartEnemyWave();
            Destroy(this.gameObject, 3.0f);
        }
    }

}
