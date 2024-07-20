using UnityEngine;

[CreateAssetMenu(fileName = "EnemyBoat", menuName = "EnemyBoat/newBoat", order = 1)]
public class EnemyBoat : ScriptableObject{
    public string boatName;
    public int health;
    public int damage;
    public float speed;
    public float turnSpeed;
    public float fireRate;
    public float range;
    public AudioClip shootSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public GameObject prefab;
    public float spawnWeight;
}
