using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCtr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ScenesCut()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("SampleScene");
        yield return new WaitForEndOfFrame();
        op.allowSceneActivation = true;
    }

    public void StartGame()
    {
        StartCoroutine(ScenesCut());
    }

    public void Quit()
    {
        Application.Quit();
    }
}
