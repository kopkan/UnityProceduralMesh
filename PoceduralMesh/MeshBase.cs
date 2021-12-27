using UnityEngine;

namespace Wcom.ProceduralMesh
{
    class MeshBase
    {
        public const int vertexInSide = 2 * 3;
        public const int vertexInTriangle = 1 * 3;

        public const float badValue = -float.MaxValue;
        public static readonly Vector3 badPoint = new Vector3(badValue, badValue, badValue);


        public static Vector3 getPolygonSharedCoord(in Mesh mesh, int sideId, int vertexInPolygon)
        {
            var vertices = mesh.vertices;
            var triangles = mesh.triangles;


            int vId = triangles[sideId * vertexInPolygon];

            Vector3 shared = vertices[vId];
            for (int q = 1; q < vertexInPolygon; q++)
            {
                int id = sideId * vertexInPolygon + q;
                int i = triangles[id];

                if (shared.x != vertices[i].x)
                {
                    shared.x = ProceduralMesh.MeshBase.badValue;
                }
                if (shared.y != vertices[i].y)
                {
                    shared.y = ProceduralMesh.MeshBase.badValue;
                }
                if (shared.z != vertices[i].z)
                {
                    shared.z = ProceduralMesh.MeshBase.badValue;
                }
            }
            return shared;
        }

        public static Vector3 getTriangleSharedCoord(in Mesh mesh, int sideId)
        {
            return getPolygonSharedCoord(mesh, sideId, MeshBase.vertexInTriangle);
        }

        public static Vector3 getSideSharedCoord(in Mesh mesh, int sideId)
        {
            return getPolygonSharedCoord(mesh, sideId, MeshBase.vertexInSide);
        }



        /*
         * NOT USED IN CURRENT TASK, ONLY FOR TMP EXAMPLE, DONT USING THIS METHOD IN CURRENT VIEW
         */
        Mesh RemoveRepeatedVertex(Vector3[] vertices, int[] triangles)
        {
            int repeat = 0;
            Vector3 removedVertece = new Vector3(badValue, badValue, badValue);

            for (int i = 0; i < vertices.Length; i++)
            {
                var curent = vertices[i];
                if (curent == removedVertece)
                {
                    continue;
                }

                for (int q = i + 1; q < vertices.Length; q++)
                {
                    if (curent == vertices[q])
                    {
                        vertices[q] = removedVertece;
                        for (int t = 0; t < triangles.Length; t++)
                        {
                            if (triangles[t] == q)
                            {
                                triangles[t] = i;
                            }
                        }
                        repeat++;
                    }
                }
            }

            ////////////////Resize vertex Array
            for (int i = vertices.Length - 1; i >= 0; i--)
            {
                if (vertices[i] == removedVertece)
                {
                    for (int t = 0; t < triangles.Length; t++)
                    {
                        if (triangles[t] > i)
                        {
                            triangles[t] = triangles[t] - 1;
                        }
                    }
                }
            }

            var newVert = new Vector3[vertices.Length - repeat];
            int vId = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i] != removedVertece)
                {
                    newVert[vId] = vertices[i];
                    vId++;
                }
            }

            var mesh = new Mesh();
            mesh.vertices = newVert;
            mesh.triangles = triangles;
            return mesh;
        }
    }
}
