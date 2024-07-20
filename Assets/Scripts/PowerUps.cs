using UnityEngine;

public enum PowerUpType {
    BoatSpeed,
    CannonRange,
    CannonCapacity,
    ReloadSpeed,
}

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp/newPowerUp", order = 1)]
public class PowerUp : ScriptableObject {
    public PowerUpType type;
    public float amount;
}
