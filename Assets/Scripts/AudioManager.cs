using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
//        //Set screen size for Standalone
//#if UNITY_STANDALONE
//        Screen.SetResolution(564, 960, true);
//        Screen.fullScreen = true;
//#endif

        int numberOfAudioManagers = FindObjectsOfType<AudioManager>().Length;
        if (numberOfAudioManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
