using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    public abstract void Init(GameManager gameManager);
    public abstract void StartRound(PlayerSide side);
    public abstract void Turn();
    public static string playerType;

    [HideInInspector]
    public PlayerSide side;
    public string playerName;
}
