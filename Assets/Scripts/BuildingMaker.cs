using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class BuildingMaker : InfrastructureBehavior
{
    

    public Material building;


    IEnumerator Start()
    {
        while (!map.IsReady)
        {
            yield return null;
        }

        foreach (var way in map.ways.FindAll( (w)=> { return w.IsBuilding && w.NodeIDs.Count > 1; }))
        {
            GameObject go = new GameObject();
            Vector3 localOrigin = GetCenter(way);
            go.transform.position = localOrigin - map.bounds.Center;

            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();

            mr.material = building;

            List<Vector3> vectors = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> indicies = new List<int>();

            for (int i = 1; i < way.NodeIDs.Count; i++)
            {

                OsmNode p1 = map.nodes[way.NodeIDs[i - 1]];
                OsmNode p2 = map.nodes[way.NodeIDs[i]];

                Vector3 v1 = p1 - localOrigin;
                Vector3 v2 = p2 - localOrigin;
                Vector3 v3 = v1 + new Vector3(0, way.Height, 0);
                Vector3 v4 = v2 + new Vector3(0, way.Height, 0);

                vectors.Add(v1);
                vectors.Add(v2);
                vectors.Add(v3);
                vectors.Add(v4);

                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);
                normals.Add(-Vector3.forward);

                int idx1, idx2, idx3, idx4;
                idx4 = vectors.Count - 1;
                idx3 = vectors.Count - 2;
                idx2 = vectors.Count - 3;
                idx1 = vectors.Count - 4;

                indicies.Add(idx1);
                indicies.Add(idx3);
                indicies.Add(idx2);

                indicies.Add(idx3);
                indicies.Add(idx4);
                indicies.Add(idx2);

                indicies.Add(idx2);
                indicies.Add(idx3);
                indicies.Add(idx1);

                indicies.Add(idx2);
                indicies.Add(idx4);
                indicies.Add(idx3);
            }

            mf.mesh.vertices = vectors.ToArray();
            mf.mesh.normals = normals.ToArray();
            mf.mesh.triangles = indicies.ToArray();

            yield return null;
        }
    }

    
}
