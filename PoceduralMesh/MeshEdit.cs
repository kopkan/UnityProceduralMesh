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


            ArrayList foundTriangleIds = new ArrayList();
            for (int i = 0; i < _triangles.Length / MeshBase.vertexInTriangle; i++)
            {
                Vector3 sharedCoord = MeshBase.getTriangleSharedCoord(_originalMesh, i);

                for (int q = 0; q < sidesInSC.Length; q++)
                {
                    if (sharedCoord == sidesInSC[q])
                    {
                        foundTriangleIds.Add(i);
                    }
                }
            }
            foundTriangleIds.Sort();

            for (int i = foundTriangleIds.Count - 1; i >= 0; i--)
            {
                _removeTriangle((int)foundTriangleIds[i]);
            }
        }

        public Mesh getResultMesh()
        {
            var newMesh = Object.Instantiate(_originalMesh);
            newMesh.name = "MeshEdit";
            newMesh.triangles = _triangles;
            return newMesh;
        }

        void _removeTriangle(int sideId)
        {
            int[] newTriagles;
            Shared.ArrayHelp.Cut(_triangles, out newTriagles, sideId * MeshBase.vertexInTriangle, MeshBase.vertexInTriangle);
            _triangles = (int[])newTriagles;
        }
    }
}
