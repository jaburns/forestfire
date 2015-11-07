using UnityEngine;
using System.Collections;

public class WaterGuyController : MonoBehaviour
{
    Rigidbody2D _rb;
    public string Player = "P1";
    string Horizontal = "Horizontal";
    string Vertical = "Vertical";
    string Fire1 = "Fire1";
    string Fire2 = "Fire2";
    string StartButton = "Start";
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        Horizontal += Player;
        Vertical += Player;
        Fire1 += Player;
        Fire2 += Player;
        // Start is common to both players
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis(Horizontal);
        float v = Input.GetAxis(Vertical);

        transform.position = transform.position += new Vector3(v, h, 0);
    }
}
