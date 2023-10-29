using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

//[RequireComponent(typeof(SkinnedMeshRenderer))]
public class Cutable : MonoBehaviour
{
    public SkinnedMeshRenderer skinned_renderer;
    Mesh mesh;

    [SerializeField] public GameObject cut_obj;
    void Start()
    {
        mesh = skinned_renderer.sharedMesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cut(Vector3 pos, Vector3 normal)
    {
        Vector3 local_pos = transform.InverseTransformPoint(pos);
        Vector3 local_normal = transform.InverseTransformDirection(normal);

        List<Vector3> verts_a = new();
        List<Vector3> normals_a = new();
        List<Vector2> uvs_a = new();

        List<Vector3> verts_b = new();
        List<Vector3> normals_b = new();
        List<Vector2> uvs_b = new();

        List<Vector3> cutting_surface_v = new();
        List<Vector3> cutting_surface_nrm = new();
        List<Vector2> cutting_surface_uv = new();
        List<BoneWeight> cutting_surface_weight = new();

        List<BoneWeight> weights_a = new();
        List<BoneWeight> weights_b = new();

        List<int> tris_a = new();
        List<int> tris_b = new();

        Vector3[] verts = mesh.vertices;
        Vector3[] normals = mesh.normals;
        Vector2[] uvs = mesh.uv;
        BoneWeight[] weights = mesh.boneWeights;
        int[] tris = mesh.triangles;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Vector3 vertex1 = verts[tris[i]];
            Vector3 vertex2 = verts[tris[i + 1]];
            Vector3 vertex3 = verts[tris[i + 2]];

            Vector3 v1 = (vertex1 - local_pos);
            Vector3 v2 = (vertex2 - local_pos);
            Vector3 v3 = (vertex3 - local_pos);

            float dot1 = Vector3.Dot(v1, local_normal);
            float dot2 = Vector3.Dot(v2, local_normal);
            float dot3 = Vector3.Dot(v3, local_normal);

            if (dot1 >= 0 && dot2 >= 0 && dot3 >= 0) // 모든 점이 앞
            {
                verts_a.Add(vertex1);
                verts_a.Add(vertex2);
                verts_a.Add(vertex3);

                normals_a.Add(normals[tris[i]]);
                normals_a.Add(normals[tris[i + 1]]);
                normals_a.Add(normals[tris[i + 2]]);

                uvs_a.Add(uvs[tris[i]]);
                uvs_a.Add(uvs[tris[i + 1]]);
                uvs_a.Add(uvs[tris[i + 2]]);

                weights_a.Add(weights[tris[i]]);
                weights_a.Add(weights[tris[i + 1]]);
                weights_a.Add(weights[tris[i + 2]]);

                tris_a.Add(tris_a.Count);
                tris_a.Add(tris_a.Count);
                tris_a.Add(tris_a.Count);
            }
            else if(dot1 < 0 && dot2 < 0 && dot3 < 0) // 모든 점이 뒤
            {
                verts_b.Add(vertex1);
                verts_b.Add(vertex2);
                verts_b.Add(vertex3);

                normals_b.Add(normals[tris[i]]);
                normals_b.Add(normals[tris[i + 1]]);
                normals_b.Add(normals[tris[i + 2]]);

                uvs_b.Add(uvs[tris[i]]);
                uvs_b.Add(uvs[tris[i + 1]]);
                uvs_b.Add(uvs[tris[i + 2]]);

                weights_b.Add(weights[tris[i]]);
                weights_b.Add(weights[tris[i + 1]]);
                weights_b.Add(weights[tris[i + 2]]);

                tris_b.Add(tris_b.Count);
                tris_b.Add(tris_b.Count);
                tris_b.Add(tris_b.Count);
            }
            else
            {
                Vector3 alone, v2_tmp, v3_tmp;
                Vector3 alone_nrm, v2_nrm, v3_nrm;
                Vector3 alone_uv, v2_uv, v3_uv;
                BoneWeight alone_weight, v2_weight, v3_weight;
                if (dot2 * dot3 >= 0) // dot1은 혼자 있음
                {
                    alone = vertex1;
                    v2_tmp = vertex2;
                    v3_tmp = vertex3;

                    alone_nrm = normals[tris[i]];
                    v2_nrm = normals[tris[i + 1]];
                    v3_nrm = normals[tris[i + 2]];

                    alone_uv = uvs[tris[i]];
                    v2_uv = uvs[tris[i + 1]];
                    v3_uv = uvs[tris[i + 2]];

                    alone_weight = weights[tris[i]];
                    v2_weight = weights[tris[i + 1]];
                    v3_weight = weights[tris[i + 2]];
                }
                else if (dot1 * dot3 >= 0) // dot2는 혼자 있음
                {
                    alone = vertex2;
                    v2_tmp = vertex3;
                    v3_tmp = vertex1;

                    alone_nrm = normals[tris[i + 1]];
                    v2_nrm = normals[tris[i + 2]];
                    v3_nrm = normals[tris[i]];

                    alone_uv = uvs[tris[i + 1]];
                    v2_uv = uvs[tris[i + 2]];
                    v3_uv = uvs[tris[i]];

                    alone_weight = weights[tris[i + 1]];
                    v2_weight = weights[tris[i + 2]];
                    v3_weight = weights[tris[i]];
                }
                else if (dot1 * dot2 >= 0) // dot3은 혼자 있음
                {
                    alone = vertex3;
                    v2_tmp = vertex1;
                    v3_tmp = vertex2;

                    alone_nrm = normals[tris[i + 2]];
                    v2_nrm = normals[tris[i]];
                    v3_nrm = normals[tris[i + 1]];

                    alone_uv = uvs[tris[i + 2]];
                    v2_uv = uvs[tris[i]];
                    v3_uv = uvs[tris[i + 1]];

                    alone_weight = weights[tris[i + 2]];
                    v2_weight = weights[tris[i]];
                    v3_weight = weights[tris[i + 1]];
                }
                else
                {
                    continue;
                }

                Vector3 ov1 = alone - local_pos;
                Vector3 ov2 = v2_tmp - local_pos;
                Vector3 ov3 = v3_tmp - local_pos;
                float ov1_proj = Mathf.Abs(Vector3.Dot(ov1, local_normal));
                float ov2_proj = Mathf.Abs(Vector3.Dot(ov2, local_normal));
                float ov3_proj = Mathf.Abs(Vector3.Dot(ov3, local_normal));

                Vector3 v1v2 = v2_tmp - alone;
                Vector3 v1v3 = v3_tmp - alone;

                float p1_ratio = ov1_proj / (ov1_proj + ov2_proj);
                float p2_ratio = ov1_proj / (ov1_proj + ov3_proj);

                Vector3 p1 = alone + v1v2 * p1_ratio;
                Vector3 p2 = alone + v1v3 * p2_ratio;

                cutting_surface_v.Add(p1);
                cutting_surface_v.Add(p2);

                Vector3 p1_normal = Vector3.Lerp(alone_nrm, v2_nrm, p1_ratio);
                Vector3 p2_normal = Vector3.Lerp(alone_nrm, v3_nrm, p2_ratio);

                cutting_surface_nrm.Add(p1_normal);
                cutting_surface_nrm.Add(p2_normal);

                Vector2 p1_uv = Vector3.Lerp(alone_uv, v2_uv, p1_ratio);
                Vector2 p2_uv = Vector3.Lerp(alone_uv, v3_uv, p2_ratio);

                cutting_surface_uv.Add(p1_uv);
                cutting_surface_uv.Add(p2_uv);

                BoneWeight p1_weight = new()
                {
                    boneIndex0 = alone_weight.boneIndex0,
                    boneIndex1 = v2_weight.boneIndex0,
                    boneIndex2 = alone_weight.boneIndex1,
                    boneIndex3 = v2_weight.boneIndex1,
                    weight0 = Mathf.Lerp(alone_weight.weight0, v2_weight.weight0, p1_ratio),
                    weight1 = Mathf.Lerp(alone_weight.weight1, v2_weight.weight1, p1_ratio),
                    weight2 = Mathf.Lerp(alone_weight.weight2, v2_weight.weight2, p1_ratio),
                    weight3 = Mathf.Lerp(alone_weight.weight3, v2_weight.weight3, p1_ratio),
                };
                BoneWeight p2_weight = new()
                {
                    boneIndex0 = alone_weight.boneIndex0,
                    boneIndex1 = v3_weight.boneIndex0,
                    boneIndex2 = alone_weight.boneIndex1,
                    boneIndex3 = v3_weight.boneIndex1,
                    weight0 = Mathf.Lerp(alone_weight.weight0, v3_weight.weight0, p2_ratio),
                    weight1 = Mathf.Lerp(alone_weight.weight1, v3_weight.weight1, p2_ratio),
                    weight2 = Mathf.Lerp(alone_weight.weight2, v3_weight.weight2, p2_ratio),
                    weight3 = Mathf.Lerp(alone_weight.weight3, v3_weight.weight3, p2_ratio),
                };
                cutting_surface_weight.Add(p1_weight);
                cutting_surface_weight.Add(p2_weight);

                var verts_list1 = verts_a;
                var normals_list1 = normals_a;
                var uvs_list1 = uvs_a;
                var weights_list1 = weights_a;
                var tris_list1 = tris_a;

                var verts_list2 = verts_b;
                var normals_list2 = normals_b;
                var uvs_list2 = uvs_b;
                var weights_list2 = weights_b;
                var tris_list2 = tris_b;

                if(Vector3.Dot(ov1, local_normal) >= 0)
                {
                    verts_list1 = verts_b;
                    normals_list1 = normals_b;
                    uvs_list1 = uvs_b;
                    weights_list1 = weights_b;
                    tris_list1 = tris_b;

                    verts_list2 = verts_a;
                    normals_list2 = normals_a;
                    uvs_list2 = uvs_a;
                    weights_list2 = weights_a;
                    tris_list2 = tris_a;
                }

                //dot1 p1 p2 삼각형//
                verts_list2.Add(alone);
                verts_list2.Add(p1);
                verts_list2.Add(p2);

                normals_list2.Add(alone_nrm);
                normals_list2.Add(p1_normal);
                normals_list2.Add(p2_normal);

                uvs_list2.Add(alone_uv);
                uvs_list2.Add(p1_uv);
                uvs_list2.Add(p2_uv);

                weights_list2.Add(alone_weight);
                weights_list2.Add(p1_weight);
                weights_list2.Add(p2_weight);

                tris_list2.Add(tris_list2.Count);
                tris_list2.Add(tris_list2.Count);
                tris_list2.Add(tris_list2.Count);
                //-------------------//
                //dot2 dot3 p1 삼각형//
                verts_list1.Add(v2_tmp);
                verts_list1.Add(v3_tmp);
                verts_list1.Add(p1);

                normals_list1.Add(v2_nrm);
                normals_list1.Add(v3_nrm);
                normals_list1.Add(p1_normal);

                uvs_list1.Add(v2_uv);
                uvs_list1.Add(v3_uv);
                uvs_list1.Add(p1_uv);

                weights_list1.Add(v2_weight);
                weights_list1.Add(v3_weight);
                weights_list1.Add(p1_weight);

                tris_list1.Add(tris_list1.Count);
                tris_list1.Add(tris_list1.Count);
                tris_list1.Add(tris_list1.Count);
                //-------------------//
                //p1 dot3 p2 삼각형//
                verts_list1.Add(p1);
                verts_list1.Add(v3_tmp);
                verts_list1.Add(p2);

                normals_list1.Add(p1_normal);
                normals_list1.Add(v3_nrm);
                normals_list1.Add(p2_normal);

                uvs_list1.Add(p1_uv);
                uvs_list1.Add(v3_uv);
                uvs_list1.Add(p2_uv);

                weights_list1.Add(p1_weight);
                weights_list1.Add(v3_weight);
                weights_list1.Add(p2_weight);

                tris_list1.Add(tris_list1.Count);
                tris_list1.Add(tris_list1.Count);
                tris_list1.Add(tris_list1.Count);
                //-------------------//

            }
        }
        //---------------------------------절단면 채우기---------------------------------//
        
        Vector3 avg = Vector3.zero;
        for(int i = 0; i < cutting_surface_v.Count; ++i)
        {
            avg += cutting_surface_v[i];
        }
        avg /= cutting_surface_v.Count;

        //절단며 버텍스 시계열 정렬//
        SortedSet<Vector3> set = new(new Vector3Comparer(avg, local_normal));
        for (int i = 0; i < cutting_surface_v.Count; ++i)
            set.Add(cutting_surface_v[i]);

        cutting_surface_v = new List<Vector3>(set);
        //----------------------------//
        for (int i = 0; i < cutting_surface_v.Count; ++i)
        {
            Vector3 a, b, c;
            if (i + 1 < cutting_surface_v.Count)
            {
                a = cutting_surface_v[i];
                b = cutting_surface_v[i + 1];
                c = avg;
            }
            else
            {
                a = cutting_surface_v[i];
                b = cutting_surface_v[0];
                c = avg;
            }
            verts_a.Add(a);
            verts_a.Add(b);
            verts_a.Add(c);

            normals_a.Add(Vector3.Lerp(cutting_surface_nrm[i], -local_normal, 0.5f));
            normals_a.Add(Vector3.Lerp(cutting_surface_nrm[i + 1], -local_normal, 0.5f));
            normals_a.Add(-local_normal);

            uvs_a.Add(cutting_surface_uv[i]);
            uvs_a.Add(cutting_surface_uv[i + 1]);
            uvs_a.Add(Vector2.Lerp(cutting_surface_uv[i], cutting_surface_uv[i + 1], 0.5f));

            weights_a.Add(cutting_surface_weight[i]);
            weights_a.Add(cutting_surface_weight[i + 1]);
            weights_a.Add(cutting_surface_weight[i + 1]);

            tris_a.Add(tris_a.Count);
            tris_a.Add(tris_a.Count);
            tris_a.Add(tris_a.Count);

            verts_b.Add(c);
            verts_b.Add(b);
            verts_b.Add(a);

            normals_b.Add(local_normal);
            normals_b.Add(Vector3.Lerp(cutting_surface_nrm[i + 1], local_normal, 0.5f));
            normals_b.Add(Vector3.Lerp(cutting_surface_nrm[i], local_normal, 0.5f));

            uvs_b.Add(Vector2.Lerp(cutting_surface_uv[i], cutting_surface_uv[i + 1], 0.5f));
            uvs_b.Add(cutting_surface_uv[i + 1]);
            uvs_b.Add(cutting_surface_uv[i]);

            weights_b.Add(cutting_surface_weight[i + 1]);
            weights_b.Add(cutting_surface_weight[i + 1]);
            weights_b.Add(cutting_surface_weight[i]);

            tris_b.Add(tris_b.Count);
            tris_b.Add(tris_b.Count);
            tris_b.Add(tris_b.Count);
        }
        //----------------------------------------------------------------------------------//
        Debug.Log(verts_a.Count);
        Debug.Log(weights_a.Count);

        Debug.Log(verts_b.Count);
        Debug.Log(weights_b.Count);
        Mesh new_mesh_a = new()
        {
            vertices = verts_a.ToArray(),
            normals = normals_a.ToArray(),
            uv = uvs_a.ToArray(),
            boneWeights = weights_a.ToArray(),
            bindposes = mesh.bindposes,
            subMeshCount = mesh.subMeshCount,
            triangles = tris_a.ToArray()
        };
        skinned_renderer.sharedMesh = new_mesh_a;


        GameObject other = Instantiate(gameObject);
        Mesh new_mesh_b = new()
        {
            vertices = verts_b.ToArray(),
            normals = normals_b.ToArray(),
            uv = uvs_b.ToArray(),
            boneWeights = weights_b.ToArray(),
            bindposes = mesh.bindposes,
            subMeshCount = mesh.subMeshCount,
            triangles = tris_b.ToArray()
        };
        other.GetComponent<Cutable>().skinned_renderer.sharedMesh = new_mesh_b;
    }
}
public class Vector3Comparer : IComparer<Vector3>
{
    Vector3 center;
    Vector3 normal;
    Vector3 up;
    Vector3 right;
    public Vector3Comparer(Vector3 center, Vector3 normal)
    {
        this.center = center;
        this.normal = normal;
        this.right = Vector3.Cross(this.normal, Vector3.up).normalized;
        this.up = Vector3.Cross(this.right, this.normal);

        //Debug.LogFormat("center: {0}, {1}, {2}", this.center.x, this.center.y, this.center.z);
        //Debug.LogFormat("normal: {0}, {1}, {2}", this.normal.x, this.normal.y, this.normal.z);
        //Debug.LogFormat("up: {0},{1},{2}", this.up.x, this.up.y, this.up.z);
        //Debug.LogFormat("right: {0},{1},{2}", this.right.x, this.right.y, this.right.z);
    }
    public int Compare(Vector3 x, Vector3 y)
    {
        if ((x - y).magnitude < 0.0001f)
            return 0;
        Vector3 v1 = (x - center).normalized;
        Vector3 v2 = (y - center).normalized;

        float cos_a = Vector3.Dot(v1, this.up);
        float cos_b = Vector3.Dot(v2, this.up);

        Vector3 cross_a = Vector3.Cross(v1, this.up);
        Vector3 cross_b = Vector3.Cross(v2, this.up);

        float dot_a = Vector3.Dot(cross_a, normal);
        float dot_b = Vector3.Dot(cross_b, normal);
        if(cross_a.magnitude < 0.0001f && cross_b.magnitude > 0.0f)
        {
            if (dot_b > 0)
            {
                if (cos_a < cos_b)
                    return 1;
                else
                    return -1;
            }
            else
            {
                if (cos_a < cos_b)
                    return -1;
                else
                    return 1;
            }
        }
        if(cross_a.magnitude > 0.0f && cross_b.magnitude < 0.0001f)
        {
            if (dot_a > 0)
            {
                if (cos_a < cos_b)
                    return 1;
                else
                    return -1;
            }
            else
            {
                if (cos_a < cos_b)
                    return -1;
                else
                    return 1;
            }
        }
        if(cross_a.magnitude < 0.0001f && cross_b.magnitude < 0.0001f)
        {
            if (cos_a > cos_b)
                return -1;
            else
                return 1;
        }

        if (dot_a > 0.0f && dot_b > 0.0f)
        {
            if (cos_a > cos_b)
                return -1;
            else
                return 1;
        }
        else if(dot_a > 0.0f && dot_b < 0.0f)
        {
            return -1;
        }
        else if(dot_a < 0.0f && dot_b > 0.0f)
        {
            return 1;
        }
        else
        {
            if (cos_a < cos_b)
                return -1;
            else
                return 1;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Cutable))]
public class CutableTest : Editor
{
    SerializedProperty cutObjProperty;
    Transform pos;

    private void OnEnable()
    {
        cutObjProperty = serializedObject.FindProperty("cut_obj");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        GUILayout.Space(10.0f);
        GUILayout.Label("테스트용");
        EditorGUILayout.PropertyField(cutObjProperty, new GUIContent("Cut Object"), true);

        Cutable cuttable = (Cutable)target;

        if (GUILayout.Button("테스트"))
        {
            if (cuttable.cut_obj != null)
            {
                pos = cuttable.cut_obj.transform;
                cuttable.Cut(pos.position, -pos.forward);
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
