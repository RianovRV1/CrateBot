﻿using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FollowPath : MonoBehaviour 
{
	public enum FollowType
	{
		MoveTowards,
		Lerp
	}
	public FollowType Type = FollowType.MoveTowards;
	public PathDefiniton Path;
	public float Speed = 1;
	public float MaxDistanceToGoal = .1f;
    public bool _moving;
	private IEnumerator<Transform> _currentPoint;

	public void Start()
	{
		if (Path == null) 
		{
			return;
		}

		_currentPoint = Path.GetPathEnumerator ();
		_currentPoint.MoveNext();

		if (_currentPoint.Current == null)
						return;

		transform.position = _currentPoint.Current.position;
	}
	public void Update()
	{
		if (_currentPoint == null || _currentPoint.Current == null)
			return;
        if (!_moving)
        {
            transform.position = _currentPoint.Current.position;
            var raycast = Physics2D.Raycast(transform.position, new Vector2(0, 1), 2, 1 << LayerMask.NameToLayer("Player"));
            if (raycast)
                _moving = true;
            return;
        }
		if (Type == FollowType.MoveTowards)
						transform.position = Vector3.MoveTowards (transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		else if (Type == FollowType.Lerp)
						transform.position = Vector3.Lerp (transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);

		var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
		if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
						_currentPoint.MoveNext();
	 }
}

