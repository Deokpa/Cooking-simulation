using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public MeshFilter mesh_filter;
    public MeshFilter plane;

    public BoxCollider collider;
    public GameObject debug;
    public bool cutting = false;

    private Mesh mesh;

    private Vector3 cutting_normal_origin;

    void Start()
    {
        Mesh plane_mesh = this.plane.sharedMesh;
        cutting_normal_origin = new Vector3(0, 0, -1);

        mesh = mesh_filter.sharedMesh;
    }

    // Update is called once per frame

    void Update()
    {
        if(!cutting && collider.bounds.Contains(plane.transform.position))
        {
            Matrix4x4 rot_mat = Matrix4x4.Rotate(plane.transform.localRotation);
            Vector3 cutting_normal = rot_mat.MultiplyVector(cutting_normal_origin);

            List<Vector3> a_side_verts = new List<Vector3>();
            List<BoneWeight> a_side_boneWeights = new List<BoneWeight>();
            List<Vector3> a_side_normals = new List<Vector3>();
            List<Vector2> a_side_uvs = new List<Vector2>();
            List<int> a_side_tris = new List<int>();

            List<Vector3> b_side_verts = new List<Vector3>();
            List<BoneWeight> b_side_boneWeights = new List<BoneWeight>();
            List<Vector3> b_side_normals = new List<Vector3>();
            List<Vector2> b_side_uvs = new List<Vector2>();
            List<int> b_side_tris = new List<int>();

            for (int i = 0; i < mesh.triangles.Length; i += 3)
            {
                int idx0 = mesh.triangles[i];
                int idx1 = mesh.triangles[i + 1];
                int idx2 = mesh.triangles[i + 2];

                Vector3 verts0 = transform.TransformPoint(mesh.vertices[idx0]);
                Vector3 verts1 = transform.TransformPoint(mesh.vertices[idx1]);
                Vector3 verts2 = transform.TransformPoint(mesh.vertices[idx2]);

                float dot0 = Vector3.Dot(cutting_normal, verts0 - plane.transform.position);
                float dot1 = Vector3.Dot(cutting_normal, verts1 - plane.transform.position);
                float dot2 = Vector3.Dot(cutting_normal, verts2 - plane.transform.position);

                gameObject.SetActive(false);
                if (dot0 > 0 && dot1 > 0 && dot2 > 0)
                {
                    a_side_verts.Add(mesh.vertices[idx0]);
                    a_side_verts.Add(mesh.vertices[idx1]);
                    a_side_verts.Add(mesh.vertices[idx2]);

                    a_side_normals.Add(mesh.normals[idx0]);
                    a_side_normals.Add(mesh.normals[idx1]);
                    a_side_normals.Add(mesh.normals[idx2]);

                    a_side_uvs.Add(mesh.uv[idx0]);
                    a_side_uvs.Add(mesh.uv[idx1]);
                    a_side_uvs.Add(mesh.uv[idx2]);

                    a_side_tris.Add(a_side_tris.Count);
                    a_side_tris.Add(a_side_tris.Count);
                    a_side_tris.Add(a_side_tris.Count);

                    //a_side_boneWeights.Add(mesh.boneWeights[idx0]);
                    //a_side_boneWeights.Add(mesh.boneWeights[idx1]);
                    //a_side_boneWeights.Add(mesh.boneWeights[idx2]);
                }
                if (dot0 <= 0 && dot1 <= 0 && dot2 <= 0)
                {
                    b_side_verts.Add(mesh.vertices[idx0]);
                    b_side_verts.Add(mesh.vertices[idx1]);
                    b_side_verts.Add(mesh.vertices[idx2]);

                    b_side_normals.Add(mesh.normals[idx0]);
                    b_side_normals.Add(mesh.normals[idx1]);
                    b_side_normals.Add(mesh.normals[idx2]);

                    b_side_uvs.Add(mesh.uv[idx0]);
                    b_side_uvs.Add(mesh.uv[idx1]);
                    b_side_uvs.Add(mesh.uv[idx2]);

                    b_side_tris.Add(b_side_tris.Count);
                    b_side_tris.Add(b_side_tris.Count);
                    b_side_tris.Add(b_side_tris.Count);

                    //b_side_boneWeights.Add(mesh.boneWeights[idx0]);
                    //b_side_boneWeights.Add(mesh.boneWeights[idx1]);
                    //b_side_boneWeights.Add(mesh.boneWeights[idx2]);
                }

                /*
                GameObject aObject = new GameObject(name + "_A", typeof(MeshFilter), typeof(MeshRenderer));

                aObject.GetComponent<MeshFilter>().sharedMesh = a_mesh;
                aObject.transform.position = gameObject.transform.position;
                aObject.transform.rotation = gameObject.transform.rotation;

                gameObject.SetActive(false);*/
            }
            Mesh a_mesh = new Mesh();
            Mesh b_mesh = new Mesh();

            a_mesh.SetVertices(a_side_verts);
            a_mesh.SetNormals(a_side_normals);
            a_mesh.SetUVs(0, a_side_uvs);

            b_mesh.SetVertices(b_side_verts);
            b_mesh.SetNormals(b_side_normals);
            b_mesh.SetUVs(0, b_side_uvs);

            //a_mesh.boneWeights = a_side_boneWeights.ToArray();
            //b_mesh.boneWeights = b_side_boneWeights.ToArray();

            a_mesh.SetTriangles(a_side_tris.ToArray(), 0);
            b_mesh.SetTriangles(b_side_tris.ToArray(), 0);

            gameObject.SetActive(false);

            GameObject a = Instantiate(this.gameObject, this.transform);
            a.GetComponent<MeshFilter>().sharedMesh = a_mesh;

            GameObject b = Instantiate(this.gameObject, this.transform);
            b.GetComponent<MeshFilter>().sharedMesh = b_mesh;
            Debug.Log(cutting_normal);
        }
    }
}
