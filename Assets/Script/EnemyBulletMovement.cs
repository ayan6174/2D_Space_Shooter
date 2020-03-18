using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 2.0f;
    private ShipMovement _ship;

    // Start is called before the first frame update
    void Start()
    {
        _ship = GameObject.Find("Ship").GetComponent <ShipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _bulletSpeed);
        Destroy(gameObject, 3); // to destroy the bullets after 3 sec
    }

    private void OnTriggerEnter2D (Collider2D other) 
    {
        if (other.tag == "Player") 
        {
            _ship.Damage();
            Destroy(this.gameObject);
        }
    }
}
