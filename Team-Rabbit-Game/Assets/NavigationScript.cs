using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void getHomeScreen()
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void getNextScreen()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void getPauseScreen()
    {
        SceneManager.LoadScene("pauseScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
