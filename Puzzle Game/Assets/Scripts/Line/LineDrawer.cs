using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineDrawer : MonoBehaviour
{
    #region Variables

    [Header("Components")]

    [SerializeField] private GameObject linePrefab;
	[SerializeField] private Transform lineParent;
	[SerializeField] private RigidbodyType2D lineRigidBodyType = RigidbodyType2D.Kinematic;
	[SerializeField] private LayerMask dontDrawOver;
	[SerializeField] private UIHandler pointsCount;
	[SerializeField] private GameManager manager;

	private GameObject pencilContainer;

	[HideInInspector]
	public NewLine currentLine;
	public string parentName;

    #endregion

    #region Builtin Methods

    private void Start()
	{
		manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		if (manager.selectedCarPrefab == null)
        {
			pencilContainer = Instantiate(manager.defaultPencilPrefab);
			pencilContainer.SetActive(false);
		}
		else
        {
			pencilContainer = Instantiate(manager.selectedPencilPrefab);
			pencilContainer.SetActive(false);
		}
		if (lineParent == null)
		{
			lineParent = GameObject.Find(parentName).transform;
		}
		pointsCount.usedPoints = 0;
	}

    private void Update()
    {
		if(pointsCount.canDraw)
        {
			if (Input.GetMouseButtonDown(0))
			{
				CreateNewLine();
			}
			if (Input.GetMouseButtonUp(0))
			{
				ReleaseCurrentLine();
			}
			if (currentLine != null)
			{
				currentLine.AddPoint(DontOverwrite());
				if(currentLine != null)
                {
					if(currentLine.points.Count > 2)
                    {
						pointsCount.pointsRemaining = pointsCount.maxPoints - currentLine.points.Count - pointsCount.usedPoints;
					}
					if (pointsCount.pointsRemaining < 1)
					{
						ReleaseCurrentLine();
						pointsCount.canDraw = false;
					}
				}
				if ((currentLine != null) && currentLine.ReachedPointsLimit())
				{
					ReleaseCurrentLine();
				}
			}
		}
	}

    #endregion

    #region Custom Methods

    private Vector2 DontOverwrite()
    {
		if(Physics2D.CircleCast(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.1f , Vector2.zero, 1f, dontDrawOver))
        {
			ReleaseCurrentLine();
			return Vector3.zero;
		}
        else
        {
			pencilContainer.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pencilContainer.transform.position = new Vector3(pencilContainer.transform.position.x, pencilContainer.transform.position.y, 0f);
			return Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
    }

	private void CreateNewLine()
	{
		currentLine = (Instantiate(linePrefab, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<NewLine>();
		currentLine.name = "Line";
		currentLine.transform.SetParent(lineParent);
		currentLine.SetRigidBodyType(lineRigidBodyType);
		pointsCount.usedPoints = pointsCount.maxPoints - pointsCount.pointsRemaining;
		pencilContainer.SetActive(true);
	}

	public void ReleaseCurrentLine()
	{
		EnableLine();
		if(currentLine != null)
        {
			if (currentLine.points.Count <= 2)
			{
				Destroy(currentLine.gameObject);
			}
		}
		pencilContainer.SetActive(false);
		currentLine = null;
	}

	private void EnableLine()
	{
		if(currentLine != null)
        {
			currentLine.EnableCollider();
			currentLine.SimulateRigidBody();
		}
	}

    #endregion
}
