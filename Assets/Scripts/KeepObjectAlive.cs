using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeepObjectAlive : MonoBehaviour
{
    public static KeepObjectAlive S;
    private void Awake()
    {
        // Ensure GameManager is a singleton
        if (S == null)
        {
            S = this;
            DontDestroyOnLoad(this.gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(this.gameObject); // Destroy any duplicate GameManager instances
        }
    }
}

    
