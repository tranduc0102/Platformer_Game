using System;
using Unity.Mathematics;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private Transform target;

    [SerializeField] private float shootRate;
    [SerializeField] private float bombSpeed;
    [SerializeField] private float maxY;

    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private AnimationCurve _CorrectionAnimationCurve;
    private float shootTime;

    public void SpawnBomb()
    {
        Vector3 bounus = new Vector3(6f, 0f,0f);
        Vector2 targetPosition = target.position+bounus; // Cập nhật vị trí mục tiêu hiện tại
        Bomb bomb = Instantiate(_bombPrefab, transform.position, quaternion.identity).GetComponent<Bomb>();
        bomb.Init(targetPosition, bombSpeed, maxY);
        bomb.InitAnimationCurves(_animationCurve, _CorrectionAnimationCurve);
    }
}