using UnityEngine;

public class Player : BasePlayer
{
    public static new string playerType = "Player";

    public override void Init(GameManager gameManager) { }

    public override void StartRound(PlayerSide side)
    {
        this.side = side;
    }

    public override void Turn()
    {
        Debug.Log("Player turn");
    }
}
