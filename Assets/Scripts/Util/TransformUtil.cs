using UnityEngine;

namespace Util{
    public class TransformUtil : MonoBehaviour{

        public static Quaternion GetLockedYRandomRotation(){
            return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        }
    }
}