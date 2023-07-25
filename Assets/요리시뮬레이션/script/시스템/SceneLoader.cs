using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] scenes;
    void Start()
    {
        foreach (var scene in scenes)
        {
            bool load = true;
            for(int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene s = SceneManager.GetSceneAt(i);
                if (s.name == scene)
                {
                    load = false;
                    break;
                }
            }
            if(load)
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
