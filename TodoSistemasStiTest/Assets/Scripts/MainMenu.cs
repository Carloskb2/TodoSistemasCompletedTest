using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button StartGameButton;
    [SerializeField] Button QuitGameButton;
    [SerializeField] Button SettingsGameButton;
    [SerializeField] GameObject settingsBox;

    [SerializeField] InputField nameInputField;
    [SerializeField] InputField passwordInputField;
    [SerializeField] GameObject pointersGameObject;



    public void StartGame()
    {
        if (nameInputField.text.Contains("Carlos") && passwordInputField.text.Contains("Test"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
            pointersGameObject.SetActive(true);
    }

    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }

    public void Settings()
    {
        settingsBox.gameObject.SetActive(true);
    }
}
