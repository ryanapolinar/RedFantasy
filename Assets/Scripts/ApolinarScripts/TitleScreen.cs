using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    public int option = 0;
    bool optionConfirmed = false;

    public GameObject StartPointer, QuitPointer;

    AudioSource[] sounds;

    public Text StartText, QuitText;

	// Use this for initialization
	void Start () {
        option = 0;
        optionConfirmed = false;

        sounds = GetComponents<AudioSource>();

        ColorSelectedOption();
    }
	
	// Update is called once per frame
	void Update () {
        TitleMove();
        OptionConfirm();

		if (optionConfirmed)
        {
            OptionSegue();
        }
    }

    public void TitleMove()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            PlayAudio(sounds[0]);
            if (option <= 0)
                option = 1;
            else
                option--;
            ColorSelectedOption();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            PlayAudio(sounds[0]);
            if (option >= 1)
                option = 0;
            else
                option++;
            ColorSelectedOption();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    public void OptionConfirm()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAudio(sounds[1]);
            optionConfirmed = true;
        }
    }

    public void ColorSelectedOption()
    {
        StartText.color = new Color(155 / 255f, 155 / 255f, 155 / 255f);
        QuitText.color = new Color(155 / 255f, 155 / 255f, 155 / 255f);
        StartPointer.SetActive(false);
        QuitPointer.SetActive(false);
        switch (option)
        {
            case 0:
                StartPointer.SetActive(true);
                StartText.color = Color.black;
                break;
            case 1:
                QuitPointer.SetActive(true);
                QuitText.color = Color.black;
                break;
        }
    }

    public void OptionSegue()
    {
        switch (option)
        {
            case 0:
                SceneManager.LoadScene("ApolinarGame");
                break;
            case 1:
                Application.Quit();
                break;
        }
        optionConfirmed = false;
    }

    void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
