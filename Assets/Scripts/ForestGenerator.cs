using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public GameObject TreePrefab;
    public Vector2 SpawnRange;
    public int SpawnCount;

    void Awake()
    {
        for (int i = 0; i < SpawnCount; ++i) {
            spawnTree(new Vector2 {
                x = (.5f - Random.value) * SpawnRange.x,
                y = (.5f - Random.value) * SpawnRange.y
            });
        }
    }

    TreeController spawnTree(Vector2 pos)
    {
        var treeObj = Instantiate(TreePrefab, pos.AsVector3(), Quaternion.identity) as GameObject;
        return treeObj.GetComponent<TreeController>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, SpawnRange.AsVector3());
    }
}
