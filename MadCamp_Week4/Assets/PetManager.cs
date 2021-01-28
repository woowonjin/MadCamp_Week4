using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    public static PetManager _Instance;
    
    private void Start()
    {
        if (_Instance == null)
        {
            DontDestroyOnLoad(this);
            _Instance = this;
        }
    }

    public void AddGameObject(GameObject child)
    {
        child.transform.parent = gameObject.transform;
    }


    public void SetActivePets()
    {

    }
}
