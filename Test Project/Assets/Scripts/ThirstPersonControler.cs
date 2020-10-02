using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThirstPersonControler : MonoBehaviour
{
    public GameObject PauseUi, CameraTarget;
    private Rigidbody Rb;
    Vector3 Move;
    public Text DeathsText, TimeText, BestTime, PauseTimeText;
    public float Speed = 20, RotationSpeed = 0.01f;
    float Timer, MouseX;
    bool JumpPressed = true, InColoder = true;
    private void Start()
    {
        BestTime.text = PlayerPrefs.GetFloat("Time").ToString();
        PauseUi.SetActive(false);
        BestTime.text = PlayerPrefs.GetFloat("Time").ToString();
        DeathsText.text = PlayerPrefs.GetInt("Deaths").ToString();
        Rb = this.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        Timer += Time.deltaTime;
        TimeText.text = Timer.ToString("F2") + "s";
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        MouseX = MouseX + Input.GetAxis("Mouse X") * RotationSpeed;
        this.GetComponent<Transform>().rotation = Quaternion.Euler(0, MouseX * 0.1f, 0);
        Move = transform.right * x + transform.forward * z;
        Rb.velocity = new Vector3(Move.x * Speed,0, Move.z * Speed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpPressed == true)
            {
                if (InColoder == true)
                {
                    Rb.AddForce(transform.up * 200 * Speed);
                    JumpPressed = false;
                    InColoder = false;
                }
            }
        }
        else
        {
            JumpPressed = true;
            Rb.AddForce(transform.up * -15 * Speed);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        InColoder = true;
        if(collision.gameObject.name == "EndGame")
        {
            EndGame();
        }
        if (collision.gameObject.name == "Win")
        {
            WinGame();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        InColoder = false;
    }
    private void EndGame()
    {
        PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths") + 1);
        SceneManager.LoadScene(0);
    }
    private void WinGame()
    {
        PauseUi.SetActive(true);
        if(Timer < PlayerPrefs.GetFloat("Time"))
        {
            PlayerPrefs.SetFloat("Time", Timer);
        }
        if (PlayerPrefs.GetFloat("Time") == 0f)
        {
            PlayerPrefs.SetFloat("Time", Timer);
        }
        PauseTimeText.text = "Your time: " + Timer.ToString("F2") + "s";
        BestTime.text = PlayerPrefs.GetFloat("Time").ToString();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
