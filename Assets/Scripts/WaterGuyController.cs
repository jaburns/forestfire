using UnityEngine;

public class WaterGuyController : MonoBehaviour
{
    public int PlayerIndex;
    public float SlerpSpeed = 0.2f;
    public float WaterForce = 1.0f;
    public float Friction = 1.0f;
    public float WalkForce = 1.0f;
    public float maxVelocity = 15;

    public GameObject DropletPrefab;


    Rigidbody2D _rb;
    float _frictionScale = 1f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    DropletController CreateDroplet(Vector2 pos, Vector2 dir)
    {
        var go = Instantiate(DropletPrefab, pos.AsVector3(), Quaternion.identity) as GameObject;
        var droplet = go.GetComponent<DropletController>();
        droplet.Initialize(dir);
        return droplet;
    }

    void FixedUpdate()
    {
        var stick = Inputs.GetStick(PlayerIndex);

        _rb.angularVelocity = 0;

        var faceVec = new Vector2 {
            x = Mathf.Cos(_rb.rotation * Mathf.Deg2Rad),
            y = Mathf.Sin(_rb.rotation * Mathf.Deg2Rad)
        };

        var walkForce = WalkForce * faceVec;

        if (stick.sqrMagnitude > 0.25f) {
            var newDegs = Mathf.Rad2Deg * Mathf.Atan2(stick.y, stick.x);
            var slerped = Quaternion.Slerp(Quaternion.Euler(0, 0, _rb.rotation), Quaternion.Euler(0, 0, newDegs), SlerpSpeed).eulerAngles.z;
            _rb.MoveRotation(slerped);
        } else {
            walkForce = Vector2.zero;
        }

        var frictionForce = Vector2.zero;

        if (Inputs.GetButton(PlayerIndex, Inputs.Button.Fire1)) {
            walkForce = Vector2.zero;
            var waterForce = -faceVec * WaterForce;
            _rb.AddForce(waterForce);

            for (int i = 0; i < 8; ++i) {
                CreateDroplet(_rb.position + faceVec, faceVec);
            }

            frictionForce = -_rb.velocity * Friction;
            frictionForce *= Mathf.Abs(frictionForce.normalized.Cross(waterForce.normalized));
        } else {
            _rb.velocity *= 0.9f;
        }

        _rb.AddForce(frictionForce + walkForce);

        if (_rb.velocity.magnitude > maxVelocity) {
            _rb.velocity = _rb.velocity.normalized * maxVelocity;
        }
    }

    void Message_SetFire() {
        _frictionScale = 0;
    }
}
