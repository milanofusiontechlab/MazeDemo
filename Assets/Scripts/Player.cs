using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; 
    public float rotationSpeed = 200f; 
    public DynamicJoystick dynamicJoystick;
    public ParticleSystem psg;
    public ParticleSystem psy;




    void Update()
    {
        if (GameManager.Instance.GamePlay)
        {
            MovePlayer();
            RotatePlayer();
        }
    }
  


    void MovePlayer()
    {

        float horizontalInput = dynamicJoystick.Horizontal;
        float verticalInput = dynamicJoystick.Vertical;

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    void RotatePlayer()
    {
        float horizontalInput = dynamicJoystick.Horizontal;
        float verticalInput = dynamicJoystick.Vertical;

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            Vector3 targetDirection = new Vector3(horizontalInput, 0f, verticalInput);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GreenS"))
        {
            psg.Play();
            GameManager.Instance.Score += 1;
            GameManager.Instance.audiosource.PlayOneShot(GameManager.Instance.Greenscolliedaudio);
        }
        else if (collision.gameObject.CompareTag("RedS"))
        {
            Handheld.Vibrate();
            GameManager.Instance.Score -= 1;
            GameManager.Instance.audiosource.PlayOneShot(GameManager.Instance.Redscolliedaudio);
        }
        else if (collision.gameObject.CompareTag("BOX"))
        {
            psy.Play();
            GameManager.Instance.Score += 3;
            GameManager.Instance.audiosource.PlayOneShot(GameManager.Instance.Redscolliedaudio);
        }
        Destroy(collision.gameObject);
      
        if (PlayerPrefs.GetInt("Score") < GameManager.Instance.Score)
        {
            PlayerPrefs.SetInt("Score", (int)GameManager.Instance.Score);
        }
        
        GameManager.Instance.ScoreText.text = GameManager.Instance.Score.ToString() + "/50";
    }
}
