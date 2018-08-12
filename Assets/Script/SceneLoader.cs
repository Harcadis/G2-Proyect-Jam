using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip startSound;

    public void LoadScene(int level)
    {
        audioSource.PlayOneShot(startSound);
        Application.LoadLevel(level);
    }
}
