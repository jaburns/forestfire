using UnityEngine;
using System.Collections.Generic;

public class ForestGenerator : MonoBehaviour
{
    public GameObject TreePrefab;
    public Vector2 SpawnRange;
    public int SpawnCount;

    List<TreeController> _trees;

    void Awake()
    {
        _trees = new List<TreeController>();

        for (int i = 0; i < SpawnCount; ++i) {
            _trees.Add(spawnTree(new Vector2 {
                x = (.5f - Random.value) * SpawnRange.x,
                y = (.5f - Random.value) * SpawnRange.y
            }));
        }

        settleTrees(_trees);
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

    void FixedUpdate()
    {
        settleTrees(_trees);
    }

    static void settleTrees(List<TreeController> allTrees)
    {
        var trees = allTrees; //new List<TreeController>(allTrees);

        foreach (var tree0 in trees) {
            // Find the closest other tree to tree0.
            TreeController closestTree = null;
            float closestTreeDist = float.MaxValue;

                Debug.Log(trees.Count);
            foreach (var tree1 in trees) {
                if (tree0 == tree1) continue;
                var d2 = (tree0.transform.position - tree1.transform.position).sqrMagnitude;
                if (d2 < closestTreeDist) {
                    closestTreeDist = d2;
                    closestTree = tree1;
                }
            }

            if (closestTree == null) continue;

            clumpTrees(tree0, closestTree);
        }
    }

    static void clumpTrees(TreeController a, TreeController b)
    {
        var shift = (a.transform.position - b.transform.position) * 0.1f;
        Debug.Log(shift);
        a.transform.position += shift;
        b.transform.position -= shift;
    }
}
