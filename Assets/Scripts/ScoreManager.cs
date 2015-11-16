using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Image[] baseIcons;
    public GameObject endScene;
    public Text timerText;
    public int maxScore = 5;
    public float roundTime = 120;
    float startTime;
    public Color fadedBaseColor = new Color(1, 1, 1, .3f);
    public Color foundBaseColor = Color.green;

    int currentScore = 0;

    // Use this for initialization
    void Start () {
        foreach (Image i in baseIcons)
        {
            i.color = fadedBaseColor;
        }
     
        startTime = (float)Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update () {
        float t = roundTime - ((float)Time.realtimeSinceStartup - startTime);

        timerText.text = t.ToString("0");
        
        if (t <= 0) {
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
        baseIcons[currentScore].color = foundBaseColor;
        currentScore += 1;
        
        if (currentScore >= maxScore) {
            endScene.SetActive(true);
            Invoke("FireWin", 1.5f);
        }

        
    }

}
