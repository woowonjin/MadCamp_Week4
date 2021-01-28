using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtest : MonoBehaviour
{
    public GameObject bird;

    void Start()
    {
        bird = GameObject.Find("Bird");
    }

    public void BuyPet()
    {
        
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
        //SceneManager.MoveGameObjectToScene(bird, SceneManager.GetSceneByBuildIndex(2));
        GameObject instance = Instantiate(bird);
        instance.transform.position = new Vector2(-4.917f, 0.991f);
        DontDestroyOnLoad(instance);

    }
}
