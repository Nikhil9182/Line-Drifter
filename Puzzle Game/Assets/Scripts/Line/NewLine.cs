using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class NewLine : MonoBehaviour
{
    #region Variables

    [Header("Components")]

    [SerializeField] private Material lineMaterial;
    [SerializeField] private float distanceBTWPoints;
    [SerializeField,Range(0, 5000)] private float maxPoints = Mathf.Infinity;

    private Rigidbody2D rigidBody;
    private LineRenderer lineRenderer;
    private PolygonCollider2D polyCollider;
    private UIHandler pointsCounter;

    [HideInInspector]
    public List<Vector2> points; //points list of the line
    private List<Vector2> polygon2DPoints; //points list of polygonCollider2d
    private Vector2 tempVector;
    private Vector2 direction;
    private float angle;
    [HideInInspector]
    public float halfWidth;

    #endregion

    #region Builtin Methods

    private void Awake()
    {
        points = new List<Vector2>();
        polygon2DPoints = new List<Vector2>();
        rigidBody = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        polyCollider = GetComponent<PolygonCollider2D>();

        if(lineMaterial == null)
        {
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
        }
        lineRenderer.material = lineMaterial;
        halfWidth = lineRenderer.endWidth / 2.0f;

    }

    #endregion

    #region Custom Methods

    public void AddPoint(Vector3 point)
    {
        //If the given point already exists ,then skip it
        if (points.Contains(point) || point == Vector3.zero)
        {
            return;
        }

        if (points.Count > 1)
        {
            if (Vector2.Distance(point, points[points.Count - 1]) < distanceBTWPoints)
            {
                return;//skip the point
            }
        }
        point.z = 0f;
        //Add the point to the points list
        points.Add(point);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);

        //Add the point to the collider of the line
        AddPointToCollider(points.Count - 1);
    }

    public void EnableCollider()
    {
        polyCollider.enabled = true;
    }

    public void SetRigidBodyType(RigidbodyType2D type)
    {
        rigidBody.bodyType = type;
    }

    public bool ReachedPointsLimit()
    {
        return points.Count >= maxPoints;
    }

    public void SimulateRigidBody()
    {
        rigidBody.simulated = true;
    }

    public void AddPointToCollider(int index)
    {
        direction = points[index] - points[index + 1 < points.Count ? index + 1 : (index - 1 >= 0 ? index - 1 : index)];
        angle = Mathf.Atan2(direction.x, -direction.y);

        tempVector = points[index];
        tempVector.x = tempVector.x + halfWidth * Mathf.Cos(angle);
        tempVector.y = tempVector.y + halfWidth * Mathf.Sin(angle);
        polygon2DPoints.Insert(polygon2DPoints.Count, tempVector);

        tempVector = points[index];
        tempVector.x = tempVector.x - halfWidth * Mathf.Cos(angle);
        tempVector.y = tempVector.y - halfWidth * Mathf.Sin(angle);
        polygon2DPoints.Insert(0, tempVector);

        polyCollider.points = polygon2DPoints.ToArray();
    }

    #endregion
}
