using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public GameObject faled;
    public GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Skip()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void LoadDefoldLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void Falied()
    {
        faled.SetActive(true);
        win.SetActive(false);
    }
    public void Win()
    {
        win.SetActive(true);
        faled.SetActive(false);
    }
}
