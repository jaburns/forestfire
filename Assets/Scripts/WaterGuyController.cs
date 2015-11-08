using UnityEngine;

public class WaterGuyController : MonoBehaviour
{
    public int PlayerIndex;
    public float speed = 0.1f;
    public float SlerpSpeed = 0.2f;
    public float WaterForce = 1.0f;

    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var stick = Inputs.GetStick(PlayerIndex);

        _rb.angularVelocity = 0;

        if (stick.sqrMagnitude > 0.25f) {
            var newDegs = Mathf.Rad2Deg * Mathf.Atan2(stick.y, stick.x);
            var slerped = Quaternion.Slerp(Quaternion.Euler(0, 0, _rb.rotation), Quaternion.Euler(0, 0, newDegs), SlerpSpeed).eulerAngles.z;
            _rb.MoveRotation(slerped);
        }

        if (Inputs.GetButton(PlayerIndex, Inputs.Button.Fire1)) {
            var faceVec = new Vector2 {
                x = Mathf.Cos(_rb.rotation * Mathf.Deg2Rad),
                y = Mathf.Sin(_rb.rotation * Mathf.Deg2Rad)
            };
            _rb.AddForce(WaterForce * -faceVec);
        }
    }
}
