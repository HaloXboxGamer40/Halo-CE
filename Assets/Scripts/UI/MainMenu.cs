using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject campaignMenu;

    public void Quit() {

        Application.Quit();

    }

    public void Campaign() {

        mainMenu.SetActive(false);
        campaignMenu.SetActive(true);

    }

    public void Level1() {

        SceneManager.LoadScene("Pillar of Autumn");

    }

}
