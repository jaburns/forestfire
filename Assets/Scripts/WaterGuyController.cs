using UnityEngine;

public class WaterGuyController : MonoBehaviour
{
    public int PlayerIndex;
    public float speed = 0.1f;

    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + Inputs.GetStick(PlayerIndex) * speed);
    }
}
