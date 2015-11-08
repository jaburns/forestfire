using UnityEngine;

public class Inputs : MonoBehaviour
{
    // static Inputs s_instance;
    // static Inputs Instance { get {
    //     if (s_instance == null) {
    //         var go = new GameObject();
    //         go.name = "{SINGLETON} Inputs";
    //         s_instance = go.AddComponent<Inputs>();
    //     }
    //     return s_instance;
    // } }
    //  TODO debounce in Update

    static readonly string[] PLAYER_SUFFIX = { "P1", "P2" };

    public enum Button {
        Fire1,
        Fire2,
        Start
    }

    static public Vector2 GetStick(int player)
    {
        return new Vector2 {
            x = Input.GetAxis("Horizontal" + PLAYER_SUFFIX[player]),
            y = Input.GetAxis("Vertical" + PLAYER_SUFFIX[player]),
        };
    }

    static public bool GetButton(int player, Inputs.Button button)
    {
        switch(button) {
            case Inputs.Button.Fire1: return Input.GetButton("Fire1" + PLAYER_SUFFIX[player]);
            case Inputs.Button.Fire2: return Input.GetButton("Fire2" + PLAYER_SUFFIX[player]);
            case Inputs.Button.Start: return Input.GetButton("Start" + PLAYER_SUFFIX[player]);
        }
        return false;
    }
}
