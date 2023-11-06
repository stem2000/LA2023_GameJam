using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] Transform _rotatedBody;
    [SerializeField] float _rotationSpeed = 60f;

    void Update()
    {
        _rotatedBody.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }
}
