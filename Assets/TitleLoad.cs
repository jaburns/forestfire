using UnityEngine;
using System.Collections;

public class TitleLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Inputs.GetButton(0, Inputs.Button.Start))
        {
            Debug.Log("Loading SCene");
            SceneSwitcher.Gameplay();
        }
	}
}
