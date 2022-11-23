using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Add this class to an object to make it follow a path
/// </summary>
public class PathFollow : MonoBehaviour 
{
	/// the type of follow behavior	
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}
	/// the follow behavior
	public FollowType Type = FollowType.MoveTowards;
	/// the path to follow
	public PathDefinition Path;
	/// the movement speed
	public float Speed = 1;
	/// the maximum distance to goal
	public float MaxDistanceToGoal = .1f;
	public Vector3 CurrentSpeed;
	
	private IEnumerator<Transform> _currentPoint;
	public BoxCollider2D _boxCollider { get; private set; }

	/// <summary>
	/// Initialization
	/// </summary>
	public void Start ()
	{
		// if the path is null we trigger an error and exit
		if(Path == null)
		{
			Debug.LogError("Path Cannot be null", gameObject);
			return;
		}
		
		if (transform.GetComponent<BoxCollider2D>()!=null)
			_boxCollider=transform.GetComponent<BoxCollider2D>();

		// storage
		_currentPoint = Path.GetPathEnumerator();
		_currentPoint.MoveNext();
		
		if(_currentPoint.Current == null)
			return;

		// initial positioning
		transform.position = _currentPoint.Current.position;
	}

	/// <summary>
	/// Every frame, we make the object follow its path
	/// </summary>
	public void Update ()
	{
		if(_currentPoint == null || _currentPoint.Current == null)
			return;
			
		Vector3 initialPosition=transform.position;
		
		if(Type == FollowType.MoveTowards)
			transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		else if(Type == FollowType.Lerp)
			transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		
		var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
		if(distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
			_currentPoint.MoveNext();
			
		Vector3 finalPosition=transform.position;
		CurrentSpeed=(finalPosition-initialPosition)/Time.deltaTime;
	}
}
