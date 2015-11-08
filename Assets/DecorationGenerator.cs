using UnityEngine;
using System.Collections;

public class DecorationGenerator : MonoBehaviour {

    public GameObject ThingPrefab;
    public Vector2 SpawnRange;
    public int SpawnCount;
    public float SmallSize;
    public float LargeSize;

    void Awake()
    {
        for (int i = 0; i < SpawnCount; ++i)
        {
            spawnThing(new Vector2
            {
                x = (.5f - Random.value) * SpawnRange.x,
                y = (.5f - Random.value) * SpawnRange.y
            });
        }
    }

    void spawnThing(Vector2 pos)
    {
        var thingObj = Instantiate(ThingPrefab, pos.AsVector3(), Quaternion.identity) as GameObject;
        thingObj.transform.localScale *= Random.value * (LargeSize - SmallSize) + SmallSize;
        thingObj.transform.parent = transform;
        thingObj.transform.rotation = Quaternion.EulerAngles(0, 0, Random.value * 2000);
    }
}
