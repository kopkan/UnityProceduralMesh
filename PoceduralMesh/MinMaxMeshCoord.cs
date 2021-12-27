using UnityEngine;

namespace Wcom.ProceduralMesh
{
    [System.Serializable]
    class MinMaxMeshCoord
    {
        public Shared.MinMaxValue x, y, z;

        public MinMaxMeshCoord(in Mesh mesh)
        {
            _calc(mesh);
        }

        private void _calc(in Mesh mesh)
        {
            x = new Shared.MinMaxValue();
            y = new Shared.MinMaxValue();
            z = new Shared.MinMaxValue();
            for (int i = 0; i < mesh.vertices.Length; i++)
            {
                x.newContender(mesh.vertices[i].x);
                y.newContender(mesh.vertices[i].y);
                z.newContender(mesh.vertices[i].z);
            }
        }

        public Vector3 sideToSharedCoord(in Vector3 side)
        {

            float[] sArr = new float[] { side.x, side.y, side.z };
            Shared.MinMaxValue[] mmvArr = new Shared.MinMaxValue[] { x, y, z };

            float[] tmpRes = new float[3];

            for (int q = 0; q < 3; q++)
            {
                tmpRes[q] = MeshBase.badValue;
                if (sArr[q] == 1)
                {
                    tmpRes[q] = mmvArr[q].max;
                }
                if (sArr[q] == -1)
                {
                    tmpRes[q] = mmvArr[q].min;
                }
            }

            return new Vector3(tmpRes[0], tmpRes[1], tmpRes[2]);
        }
    }
}
