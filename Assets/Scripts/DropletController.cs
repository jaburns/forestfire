using System.Collections;
using UnityEngine;

public class DropletController : MonoBehaviour
{
    public float Lifetime = 2f;

    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.localScale *= Random.Range(0.8f, 1.2f);
        StartCoroutine(dieAfterLifetime());
    }

    public void Initialize(Vector2 dir)
    {
        _rb.velocity = 15 * dir;
    }

    IEnumerator dieAfterLifetime()
    {
        yield return new WaitForSeconds(Lifetime);
        die();
    }

    void die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.SendMessage(Messages.Splash, SendMessageOptions.DontRequireReceiver);
        die();
    }
}
