using UnityEngine;

static public class SceneSwitcher
{
    static public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    static public void Gameplay()
    {
        Application.LoadLevel("Gameplay");
    }

    static public void WaterWin()
    {
        Application.LoadLevel("WaterWin");
    }

    static public void FireWin()
    {
        Application.LoadLevel("FireWin");
    }
}
