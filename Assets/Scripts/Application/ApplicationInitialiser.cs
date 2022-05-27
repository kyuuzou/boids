using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationInitialiser : MonoBehaviour {

    [SerializeField]
    private bool runInBackground = true;
    
    [SerializeField]
    private int targetFrameRate = 60;
    
    private void Start() {
        Application.runInBackground = this.runInBackground;
        Application.targetFrameRate = this.targetFrameRate;
    }
}
