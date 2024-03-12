using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{

    public int sceneNumber;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneNumber == 0)
        {
            StartSceneControls();
        }
        else if(sceneNumber == 1)
        {
            MainSceneControls();
        }
        else if(sceneNumber == 2)
        {
            EndSceneControls();
        }
    }

    void StartSceneControls()
    {
        if(Input.GetKeyDown(KeyCode.Return) )
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void MainSceneControls()
    {
        if(Input.GetKeyDown(KeyCode.Space) )
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    void EndSceneControls()
    {
        if(Input.GetKeyDown(KeyCode.R) )
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
