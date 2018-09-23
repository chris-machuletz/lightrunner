using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public Animator transitionAnim;
    //public string sceneName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // if (Input.GetKeyDown(KeyCode.Space))
       // {
            StartCoroutine(LoadScene());
            //SceneManager.LoadScene(sceneName);
       // }
	}

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2.8f);
        SceneManager.LoadScene(1);
    }
}
