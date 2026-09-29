using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestroyDis : MonoBehaviour
{
    static DonDestroyDis instance;

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(instance);
    }
}
