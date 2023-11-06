using System;
using UnityEngine;

public interface ICollectable
{
    public event Action<int> OnCollectableTaken;
    public Vector3 GetPosition();
    public void Collect();
}