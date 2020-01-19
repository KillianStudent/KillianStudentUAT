using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        Object.Destroy(gameObject, 3.0f);   // destroys itself after 3 seconds to not waste space
    }
}
