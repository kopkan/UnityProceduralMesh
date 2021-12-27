using UnityEngine;
using System.Collections;

namespace Wcom.ProceduralMesh
{
    class MeshEdit
    {
        private Mesh _originalMesh;
        private int[] _triangles;
        MinMaxMeshCoord _meshMinMaxInfo;

        public MeshEdit(in Mesh mesh)
        {
            _originalMesh = mesh;
            _triangles = _originalMesh.triangles;
            _meshMinMaxInfo = new MinMaxMeshCoord(in mesh);
        }


        public void cutSides(Vector3[] sides)
        {
            var sidesInSC = new Vector3[sides.Length];
            for (int i = 0; i < sides.Length; i++)
            {
                sidesInSC[i] = _meshMinMaxInfo.sideToSharedCoord(sides[i]);
            }


            ArrayList arr = new ArrayList();
            for (int i = 0; i < _triangles.Length / MeshBase.vertexInTriangle; i++)
            {
                Vector3 shared = MeshBase.getTriangleSharedCoord(_originalMesh, i);

                for (int q = 0; q < sidesInSC.Length; q++)
                {
                    if (shared == sidesInSC[q])
                    {
                        Debug.Log("Side id ID=" + i + " shared=" + shared);
                        arr.Add(i);
                    }
                }
            }
            arr.Sort();

            for (int i = arr.Count - 1; i >= 0; i--)
            {
                Debug.Log("del arr[i]=" + arr[i] + " i=" + i);
                _removeTriangle((int)arr[i]);
            }
        }

        public Mesh getResultMesh()
        {

            var newMesh = _originalMesh;
            newMesh.triangles = _triangles;
            return newMesh;
        }

        void _removeTriangle(int sideId)
        {
            int[] sli;
            Shared.ArrayHelp.Cut(_triangles, out sli, sideId * MeshBase.vertexInTriangle, MeshBase.vertexInTriangle);
            _triangles = (int[])sli;
        }
    }
}
