using UnityEngine;

public class FireGuyController : MonoBehaviour
{
    public int PlayerIndex;
    public float SlerpSpeed = 0.2f;
    public float Friction = 1.0f;
    public float WalkForce = 1.0f;

    public GameObject ExplosionPrefab;

    Rigidbody2D _rb;
    bool _holdingFire;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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

        if (Inputs.GetButton(PlayerIndex, Inputs.Button.Fire2)) {
            if (!_holdingFire) {
                fireBomb(faceVec);
            }
            _holdingFire = true;
        } else {
            _holdingFire = false;
        }

        var frictionForce = -_rb.velocity * Friction;
        _rb.AddForce(frictionForce + walkForce);
    }

    void fireBomb(Vector2 faceVec)
    {
        Instantiate(ExplosionPrefab, transform.position+faceVec.AsVector3(), Quaternion.identity);
    }
}

