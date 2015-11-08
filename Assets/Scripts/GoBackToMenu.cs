using UnityEngine;
using System.Collections;

public class GoBackToMenu : MonoBehaviour
{
    public float WaitTime = 5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(WaitTime);
        SceneSwitcher.MainMenu();
    }
}
