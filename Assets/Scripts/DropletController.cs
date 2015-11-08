using System.Collections;
using UnityEngine;

public class DropletController : MonoBehaviour
{
    public float Lifetime = 2f;
    public Material DropletLarge;
    public Material DropletMedium;
    public Material DropletSmall;

    public float MediumRatio = 0.6f;
    public float SmallRatio = 0.3f;
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        float rand = Random.value;
        if (rand < SmallRatio)
        {
            transform.localScale *= Random.Range(0.1f, 0.2f);
        }
        else if (rand < MediumRatio)
        {
            transform.localScale *= Random.Range(0.3f, 0.6f);
        }
        else
        {
            transform.localScale *= Random.Range(0.8f, 1.2f);
        }

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
