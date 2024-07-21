using System;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Vector2 target;
    private float moveSpeed;
    private float maxY;
    private float distanceToTargetToDestroyBomb = 1f;

    private AnimationCurve _axisCorrectionAnimationCurve;
    private AnimationCurve _animationCurve;

    private Vector3 trajectoryStartPoint;
    private float totalDistance;
    private float timeToTarget;
    private float elapsedTime;

    private void Start()
    {
        trajectoryStartPoint = transform.position;
        totalDistance = Vector3.Distance(trajectoryStartPoint, target);
        timeToTarget = totalDistance / moveSpeed;
        elapsedTime = 0f;
    }

    private void Update()
    {
        if (target != null)
        {
            elapsedTime += Time.deltaTime;
            UpdateBombPosition();
        }
    }

    public void Init(Vector2 target, float moveSpeed, float maxY)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        float xDistanceTarget = target.x - transform.position.x;
        this.maxY = Mathf.Abs(xDistanceTarget * maxY);
    }

    public void InitAnimationCurves(AnimationCurve animationCurve, AnimationCurve axisCorrectionAnimationCurve)
    {
        this._animationCurve = animationCurve;
        this._axisCorrectionAnimationCurve = axisCorrectionAnimationCurve;
    }

    private void UpdateBombPosition()
    {
        float progress = elapsedTime / timeToTarget;
        Vector3 newPosition = Vector3.Lerp(trajectoryStartPoint, target, progress);

        // Apply parabolic height
        float height = maxY * (1 - (progress * 2 - 1) * (progress * 2 - 1));
        newPosition.y += height;

        // Ensure newPosition is valid
        if (!float.IsNaN(newPosition.x) && !float.IsNaN(newPosition.y) && !float.IsNaN(newPosition.z))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.LogError($"Invalid position calculated: newPosition = {newPosition}");
        }
    }
}
