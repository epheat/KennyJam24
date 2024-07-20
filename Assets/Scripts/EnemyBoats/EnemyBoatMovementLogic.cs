using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBoatMovementLogic : MonoBehaviour {
    public abstract Quaternion GetNextTurnRotation();
}
