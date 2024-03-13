using System;
using UnityEngine;

public class AiPlayer : BasePlayer
{
    public static new string playerType = "AI";
    public GameManager gameManager;

    private static int MaxScore = 999999;

    private Board Board
    {
        get { return gameManager.GetBoardData(); }
    }

    public override void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public override void StartRound(PlayerSide side)
    {
        this.side = side;
    }

    public override void Turn()
    {
        Debug.Log("AI turn");
        Invoke("Move", 0.5f);
    }

    private void Move()
    {
        int bestScore = -MaxScore;
        int bestI = 0;
        int bestJ = 0;

        var currentBoard = Board;
        int gridNum = currentBoard.gridNum;

        Square[,] boards = currentBoard.data;
        for (int i = 0; i < gridNum; ++i)
        {
            for (int j = 0; j < gridNum; ++j)
            {
                if (!boards[i, j].isMarked)
                {
                    boards[i, j].Value = side.ToString();
                    int score = MiniMax(gridNum, boards, 0, false);
                    boards[i, j].Value = null;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestI = i;
                        bestJ = j;
                    }
                }
            }
        }

        boards[bestI, bestJ].Mark();
    }

    private int MiniMax(int gridNum, Square[,] boards, int depth, bool isMaximizing)
    {
        string result = Board.CheckWin(gridNum, boards);
        if (result != null)
        {
            return EvaluateResult(result);
        }

        if (isMaximizing)
        {
            int bestScore = -MaxScore;
            for (int i = 0; i < gridNum; ++i)
            {
                for (int j = 0; j < gridNum; ++j)
                {
                    if (!boards[i, j].isMarked)
                    {
                        boards[i, j].Value = side.ToString();
                        int score = MiniMax(gridNum, boards, depth + 1, false);
                        boards[i, j].Value = null;
                        bestScore = Math.Max(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = MaxScore;
            for (int i = 0; i < gridNum; ++i)
            {
                for (int j = 0; j < gridNum; ++j)
                {
                    if (!boards[i, j].isMarked)
                    {
                        boards[i, j].Value =
                            side == PlayerSide.X
                                ? PlayerSide.O.ToString()
                                : PlayerSide.X.ToString();
                        int score = MiniMax(gridNum, boards, depth + 1, true);
                        boards[i, j].Value = null;
                        bestScore = Math.Min(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }

    private int EvaluateResult(string result)
    {
        if (result == side.ToString())
        {
            return 10;
        }
        if (result == "Tie")
        {
            return 0;
        }
        return -10;
    }
}
