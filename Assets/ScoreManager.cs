using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public GameObject scoreSlider;
    public GameObject timeSlider;
    public GameObject endScene;
    Scrollbar scoreBar;
    Scrollbar timeBar;
    public int maxScore = 5;
    public float roundTime = 120;
    float startTime;

    int currentScore = 0;

    // Use this for initialization
    void Start () {
        scoreBar = scoreSlider.GetComponent<Scrollbar>();
        timeBar = timeSlider.GetComponent<Scrollbar>();
        startTime = (float)Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update () {
        var t = ((float)Time.realtimeSinceStartup - startTime) / roundTime;
        timeBar.size = t;
        if (t >= 1) {
            endScene.SetActive(true);
            Invoke("WaterWin", 1.5f);
        }
    }

    void WaterWin()
    {
        SceneSwitcher.WaterWin();
    }

    void FireWin()
    {
        SceneSwitcher.FireWin();
    }

    void Message_BaseFound()
    {
        currentScore += 1;
        scoreBar.size = (float)currentScore / maxScore;

        if (currentScore >= maxScore) {
            endScene.SetActive(true);
            Invoke("FireWin", 1.5f);
        }
    }

}
