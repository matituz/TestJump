﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
