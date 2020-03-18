using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    [SerializeField] private int _speed = 3;
    [SerializeField] private int _powerUpID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -7) 
        {
            DestroyPowerUp();
        }
    }

    public void DestroyPowerUp() 
    {
        Destroy(this.gameObject);
    }

    // when collided trigger with player then
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player") 
        {
            switch (_powerUpID)
            {
                case 1:
                    // allow the player to triple shot
                    GameObject.Find("ShipCannon").GetComponent<ShipGun>().EnableTripleShot();
                    DestroyPowerUp();
                    break;
                case 2:
                    // allow the player to boost the speed
                    Debug.Log("Speed boost is activated");
                    other.GetComponent<ShipMovement>().EnableSpeedBoost();
                    DestroyPowerUp();
                    break;
                case 3:
                    // allow the to power up the shield
                    Debug.Log("Shield is activated");
                    other.GetComponent<ShipMovement>().EnableShield ();
                    DestroyPowerUp();
                    break;
                default:
                    break;
            }
        }
    }
}
