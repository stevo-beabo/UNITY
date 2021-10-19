using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_Keyboard : MonoBehaviour
{
    private GameObject _objectActive;
    private AudioSource _audioSourceControl;

    // Start is called before the first frame update
    void Start()
    {
        _objectActive = GameObject.Find("Keyboard");
        _audioSourceControl = GetComponent<AudioSource>();
        _audioSourceControl.Play();
        _audioSourceControl.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Level == 0 && !_objectActive)
        {
                _audioSourceControl.mute = false; 
        }
    }
}
