using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMoover : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRendererPoints;
    [SerializeField] private BoxController _boxController;  
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _finishPoint;

    private Vector3 [] _wayPoints;

    private bool _isMoving;
    
    public void Move()
    {
        GetPoints();
        StartCoroutine(Moving());
    }

    private void GetPoints()//получаем массив координат для движения
    {
        _wayPoints = new Vector3[_lineRendererPoints.positionCount];
        _lineRendererPoints.GetPositions(_wayPoints);
    }

    private IEnumerator Moving()
    {
        for (int currentNextPoint = 0; currentNextPoint < _wayPoints.Length-1; currentNextPoint++)
        {

            while (transform.position!=_wayPoints[currentNextPoint])
            {
                transform.position = Vector3.MoveTowards(transform.position, _wayPoints[currentNextPoint], _moveSpeed*Time.deltaTime);
                yield return null;
            }
            
        }

        if (transform.position==_wayPoints[^1])
        {
            _boxController.DropDown(_finishPoint.position);
        }
        
    }

}
