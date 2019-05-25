using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
	public AudioClip Explosion;
	public AudioSource Source;
    // Start is called before the first frame update
    void Start()
    {
    	Source.clip = Explosion;
    	Source.Play();
		Destroy(gameObject, 2.0f);
    }
}
