using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FieldOfView : MonoBehaviour
{
    public float ViewRadius;
    [Range(0,360)]
    public float ViewAngle;

    public LayerMask obstacleMask;

    [HideInInspector]

    public float RayCount;
    public int EdgeResolveIterations;
    public float EdgeDitanceThreshold;

    public MeshFilter ViewMeshFilter;
    Mesh ViewMesh;

    private void Start()
    {
        ViewMesh = new Mesh();
        ViewMesh.name = "View Mesh";
        ViewMeshFilter.mesh = ViewMesh;
    }

    void LateUpdate()
    {
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        int StepCount = Mathf.RoundToInt(ViewAngle * RayCount);
        float StepAngleSize = ViewAngle / StepCount;
        List<Vector3> ViewPoints = new List<Vector3>();
        ViewCastInfo OldViewCast = new ViewCastInfo();

        for (int i = 0; i <= StepCount; i++)
        {
            float Angle = transform.eulerAngles.y - ViewAngle / 2 + StepAngleSize * i;
            ViewCastInfo NewViewCast = ViewCast(Angle);

            if (i > 0)
            {
                bool EdgeDistanceThresholdExceeded = Mathf.Abs(OldViewCast.dst - NewViewCast.dst) > EdgeDitanceThreshold;
                if (OldViewCast.hit != NewViewCast.hit || (OldViewCast.hit && NewViewCast.hit && EdgeDistanceThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(OldViewCast, NewViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        ViewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        ViewPoints.Add(edge.pointB);
                    }
                }
            }
            ViewPoints.Add(NewViewCast.point);
            OldViewCast = NewViewCast;
        }

        int VertexCount = ViewPoints.Count + 1;
        Vector3[] vertices = new Vector3[VertexCount];
        int[] triangles = new int[(VertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i =0; i < VertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(ViewPoints[i]);

            if (i < VertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        ViewMesh.Clear();

        ViewMesh.vertices = vertices;
        ViewMesh.triangles = triangles;
        ViewMesh.RecalculateNormals();
    }

    #region inutile
    EdgeInfo FindEdge(ViewCastInfo MinViewCast, ViewCastInfo MaxViewCast)
    {
        float MinAngle = MinViewCast.angle;
        float MaxAngle = MaxViewCast.angle;
        Vector3 MinPoint = Vector3.zero;
        Vector3 MaxPoint = Vector3.zero;

        for (int i = 0; i < EdgeResolveIterations; i++)
        {
            float angle = (MinAngle + MaxAngle) / 2;
            ViewCastInfo NewViewCast = ViewCast(angle);

            bool EdgeDistanceThresholdExceeded = Mathf.Abs(MinViewCast.dst - NewViewCast.dst) > EdgeDitanceThreshold;
            if (NewViewCast.hit == MinViewCast.hit && !EdgeDistanceThresholdExceeded)
            {
                MinAngle = angle;
                MinPoint = NewViewCast.point;
            }
            else
            {
                MaxAngle = angle;
                MaxPoint = NewViewCast.point;
            }
        }

        return new EdgeInfo(MinPoint, MaxPoint);
    }

    ViewCastInfo ViewCast(float GlobalAngle)
    {
        Vector3 dir = DirFromAngle(GlobalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast (transform.position, dir, out hit, ViewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, GlobalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * ViewRadius, ViewRadius, GlobalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
    #endregion
}
