using UnityEngine;
using UnityEngine.UI;

public enum PlayerSide
{
    X,
    O
}

public class GameManager : MonoBehaviour
{
    [Range(3, 10)]
    public int gridNum = 3;
    public GameObject itemPrefab;

    [Header("UI Infos")]
    public Text infoText_X;
    public Text infoText_O;
    public Text infoText_Center;

    [Header("Sprites")]
    public Sprite x_sprite;
    public Sprite o_sprite;
    public Sprite square_sprite;

    [Header("Players")]
    public BasePlayer player_1;
    public BasePlayer player_2;

    private BasePlayer currentPlayer;
    private Board gameData;

    private Square[] squares;

    GameManager() { }

    void Start()
    {
        InitSquares();
        InitPlayers();
        Play();
    }

    private void InitSquares()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GetComponent<GridLayoutGroup>().constraintCount = gridNum;

        squares = new Square[gridNum * gridNum];
        for (int i = 0; i < gridNum * gridNum; i++)
        {
            GameObject item = Instantiate(itemPrefab, transform);
            item.name = "Item " + i;
            var square = item.GetComponent<Square>();
            if (square)
            {
                square.manager = this;
                squares[i] = square;
            }
        }
        return;
    }

    private void InitPlayers()
    {
        player_1.Init(this);
        player_2.Init(this);
    }

    public void Restart()
    {
        Play();
    }

    void Play()
    {
        gameData = new Board(gridNum, squares);
        if (Random.Range(0, 2) == 0)
        {
            player_1.StartRound(PlayerSide.X);
            player_2.StartRound(PlayerSide.O);
            infoText_X.text = player_1.playerName;
            infoText_O.text = player_2.playerName;
            currentPlayer = player_1;
        }
        else
        {
            player_2.StartRound(PlayerSide.X);
            player_1.StartRound(PlayerSide.O);
            infoText_X.text = player_2.playerName;
            infoText_O.text = player_1.playerName;
            currentPlayer = player_2;
        }
        Log("Game Starts!");
        currentPlayer.Turn();
    }

    BasePlayer GetPlayer(string key)
    {
        if (player_1.side.ToString() == key)
        {
            return player_1;
        }
        else
        {
            return player_2;
        }
    }

    void SwitchPlayer()
    {
        currentPlayer = currentPlayer == player_1 ? player_2 : player_1;
        currentPlayer.Turn();
        Log("Player " + currentPlayer.playerName + " turn");
    }

    public Sprite GetCurrentSprite()
    {
        if (currentPlayer.side == PlayerSide.X)
        {
            return x_sprite;
        }
        else
        {
            return o_sprite;
        }
    }

    public void MarkCallback()
    {
        string result = gameData.HasResult();
        if (result == null)
        {
            SwitchPlayer();
            return;
        }
        if (result == "Tie")
        {
            Log("Tie!");
            return;
        }
        Log(GetPlayer(result).playerName + " wins!");
    }

    public bool IsGameOver()
    {
        return gameData.gameOver;
    }

    public Board GetBoardData()
    {
        return gameData;
    }

    public BasePlayer GetCurrentPlayer()
    {
        return currentPlayer;
    }

    private void Log(string message)
    {
        infoText_Center.text = message;
        Debug.Log(message);
    }
}
