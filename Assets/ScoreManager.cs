using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public GameObject scoreSlider;
    public GameObject timeSlider;
    Scrollbar scoreBar;
    Scrollbar timeBar;
    public int maxScore = 5;
    public float roundTime = 120;

    int currentScore = 0;

	// Use this for initialization
	void Start () {
        scoreBar = scoreSlider.GetComponent<Scrollbar>();
        timeBar = timeSlider.GetComponent<Scrollbar>();
	}
	
	// Update is called once per frame
	void Update () {
        timeBar.size = (float)Time.realtimeSinceStartup / roundTime;
	}

    void Message_BaseFound()
    {
        currentScore += 1;
        scoreBar.size = (float)currentScore / maxScore;
    }

}
