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
    }

    public void Initialize(Vector2 dir)
    {
        
        float rand = Random.value;
        if (rand < SmallRatio)
        {
            dir = dir.Rotate((Random.value - 0.5f) * 90);
            transform.localScale *= Random.Range(0.1f, 0.2f);
            transform.localPosition += new Vector3(0, Random.Range(-1.0f, 1));
            _rb.velocity = 05 * dir * Random.Range(0.95f, 1.05f);
            _rb.drag = Random.Range(0f, 1.0f);
            GetComponent<Renderer>().material = DropletSmall;
            Lifetime = Random.Range(Lifetime, Lifetime * 3);

        }
        else if (rand < MediumRatio + SmallRatio)
        {
            dir = dir.Rotate((Random.value - 0.5f) * 8);
            transform.localScale *= Random.Range(0.2f, 1.2f);
            transform.localPosition += new Vector3(0, Random.Range(-1.0f, 1));
            _rb.velocity = 40 * dir * Random.Range(0.95f, 1.05f);
            _rb.drag = Random.Range(0.1f, 0.3f);
            GetComponent<Renderer>().material = DropletMedium;
            Lifetime = Random.Range(Lifetime, Lifetime * 2);
        }
        else
        {
            dir = dir.Rotate((Random.value - 0.5f) * 5);
            transform.localScale *= Random.Range(1.2f, 1.8f);
            transform.localPosition += new Vector3(0, Random.Range(-1.0f, 1));
            _rb.velocity = 50 * dir * Random.Range(0.95f, 1.05f);
            _rb.drag = Random.Range(0f, 0.2f);
            GetComponent<Renderer>().material = DropletLarge;
            Lifetime = Random.Range(Lifetime, Lifetime * 1.5f);
        }

        StartCoroutine(dieAfterLifetime());

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
