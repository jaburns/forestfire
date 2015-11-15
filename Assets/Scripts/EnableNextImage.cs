using UnityEngine;
using System.Collections;

public class EnableNextImage : MonoBehaviour {
    public GameObject nextImage;
    public float delay;
	// Use this for initialization
	void Start () {
        Invoke("NextImage", delay);
	}

    void NextImage()
    {
        nextImage.SetActive(true);
    }
}
