using Leon;
using UnityEngine;

public class PlayerPosition : SceneSingleton<PlayerPosition>
{
    public Vector3 GetWorldPosition => transform.position;
}