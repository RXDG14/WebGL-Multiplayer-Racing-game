using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    Rigidbody rb;

    Vector3 savedPos;
    Vector3 movement;
    Vector3 joystickMovement;


    //public FixedJoystick fixedJoystick;
    public PhotonView view;

    //[SerializeField] AudioSource spawnSound;
    //[SerializeField] AudioSource hitSound;
    //[SerializeField] AudioSource powerupSound;
    //[SerializeField] ParticleSystem hitEffect;
    //[SerializeField] ParticleSystem spawnEffect;
    //[SerializeField] GameObject speedBoostParticles;
    //[SerializeField] GameObject speedBoostIndicator;
    //[SerializeField] GameObject powerupIndicator;
    //[SerializeField] GameObject protectionStarIndicator;

    [SerializeField] float speed = 100f;
    [SerializeField] float boostSpeed = 1000f;
    [SerializeField] float boostSpeedTimer = 5f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float powerupTimer = 5f;
    [SerializeField] float powerupStrength = 5f;

    [SerializeField] bool isGrounded = false;
    [SerializeField] bool hasPowerup = false;
    [SerializeField] bool hasSavedOnce = false;
    [SerializeField] bool hasProtectionStar = false;
    
    void Awake()
    {
        if (view.IsMine)
        {
            rb = GetComponent<Rigidbody>();
        }
        //speedBoostParticles.SetActive(false);
    }

    void Update()
    {
        if (view.IsMine)
        {
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            //joystickMovement = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton2)) && isGrounded)
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (movement.magnitude != 0)// || joystickMovement.magnitude != 0)
            {
                moveCharacter(movement);
                //moveCharacter(joystickMovement);
            }
            else
            {
                stopChar();
            }

            if (transform.position.y < -15 && hasSavedOnce)
            {
                transform.position = savedPos;
                ResetAbilities();
            }
            else if (transform.position.y < -15 && !hasSavedOnce)
            {
                transform.position = new Vector3(0, 0, 0);
                ResetAbilities();
            }
        }
    }

    void moveCharacter(Vector3 direction)
    {
        if (view.IsMine)
        {
            rb.AddForce(direction.normalized * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    void stopChar()
    {
        if (view.IsMine)
        {
            if (isGrounded)
            {
                rb.AddForce(-rb.velocity * speed / 3.5f * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
        }
    }

    public void Jump()
    {
        if (view.IsMine)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (view.IsMine)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }
            if (other.gameObject.tag == "RedEnemy")
            {
                if (!hasProtectionStar)
                {
                    transform.position = savedPos;
                    ResetAbilities();
                }
            }
            if (other.gameObject.tag == "Enemy" && hasPowerup)
            {
                Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
                enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
                //hitSound.Play();
                //Instantiate(hitEffect, transform.position, Quaternion.identity);
                hasPowerup = false;
                //powerupIndicator.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (view.IsMine)
        {
            if (other.gameObject.tag == "Powerup")
            {
                hasPowerup = true;
                //powerupSound.Play();
                Destroy(other.gameObject);
                //powerupIndicator.SetActive(true);
                StartCoroutine(PowerupTime());
            }
            if (other.gameObject.tag == "SpeedBoost")
            {
                speed = boostSpeed;
                //powerupSound.Play();
                Destroy(other.gameObject);
                //speedBoostParticles.SetActive(true);
                //speedBoostIndicator.SetActive(true);
                StartCoroutine(SpeedBoostTime());
            }
            if (other.gameObject.tag == "ProtectionStar")
            {
                hasProtectionStar = true;
                //powerupSound.Play();
                //protectionStarIndicator.SetActive(true);
                Destroy(other.gameObject);
                StartCoroutine(ProtectionStar());
            }
            if (other.gameObject.tag == "RedEnemyOnPlane")
            {
                if (!hasProtectionStar)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    transform.position = savedPos;
                    ResetAbilities();
                }
            }
            if (other.gameObject.tag == "SavePoint")
            {
                savedPos = other.transform.position;
                hasSavedOnce = true;
            }
        }
    }

    IEnumerator PowerupTime()
    {
        yield return new WaitForSeconds(powerupTimer);
        hasPowerup = false;
        //powerupIndicator.SetActive(false);
    }

    IEnumerator SpeedBoostTime()
    {
        yield return new WaitForSeconds(boostSpeedTimer);
        //speedBoostParticles.SetActive(false);
        //speedBoostIndicator.SetActive(false);
        speed = 10f;
    }

    IEnumerator ProtectionStar()
    {
        yield return new WaitForSeconds(5f);
        hasProtectionStar = false;
        //protectionStarIndicator.SetActive(false);
    }

    void ResetAbilities()
    {
        //spawnSound.Play();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //Instantiate(spawnEffect, transform.position, Quaternion.identity);
        hasPowerup = false;
        //powerupIndicator.SetActive(false);
        hasProtectionStar = false;
        //protectionStarIndicator.SetActive(false);
        //speedBoostParticles.SetActive(false);
        //speedBoostIndicator.SetActive(false);
        speed = 10f;
    }

    void OnCollisionStay(Collision other)
    {
        if (view.IsMine)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = true;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(view.IsMine)
        {
            if (other.gameObject.tag == "Ground")
            {
                isGrounded = false;
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (view.IsMine)
        {
            transform.position = savedPos;
            ResetAbilities();
        }
    }
}