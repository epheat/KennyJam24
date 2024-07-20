using UnityEngine;
using Util;

public class RandomMovement : EnemyBoatMovementLogic {
    public override Quaternion GetNextTurnRotation(){
        return TransformUtil.GetLockedYRandomRotation();
    }
}
