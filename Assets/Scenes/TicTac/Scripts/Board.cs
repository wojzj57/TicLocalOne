using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board
{
    public int gridNum;
    public bool gameOver = false;

    public Square[,] data;

    public Board(int num, Square[] squares)
    {
        gridNum = num;
        data = new Square[num, num];
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                var sq = squares[i * num + j];
                sq.Reset();
                data[i, j] = squares[i * num + j];
            }
        }
    }

    public void SetValue(int row, int col, string value)
    {
        data[row, col].Value = value;
    }

    public string GetValue(int row, int col)
    {
        return data[row, col].Value;
    }

    public string HasResult()
    {
        string result = CheckWin(gridNum, data);
        if (result != null)
        {
            gameOver = true;
        }
        return result;
    }



    public static string CheckWin(int gridNum, Square[,] data)
    {
        string flag = null;
        for (int i = 0; i < gridNum; i++)
        {
            if (!data[i, 0].isMarked)
            {
                continue;
            }
            flag = data[i, 0].Value;
            for (int j = 1; j < gridNum; j++)
            {
                if (flag != data[i, j].Value || !data[i, j].isMarked)
                {
                    flag = null;
                    break;
                }
            }
            if (flag != null)
            {
                return flag;
            }
        }

        for (int i = 0; i < gridNum; i++)
        {
            if (!data[0, i].isMarked)
            {
                continue;
            }
            flag = data[0, i].Value;
            for (int j = 0; j < gridNum; j++)
            {
                if (flag != data[j, i].Value || !data[j, i].isMarked)
                {
                    flag = null;
                    break;
                }
            }
            if (flag != null)
            {
                return flag;
            }
        }

        if (data[0, 0].isMarked)
        {
            flag = data[0, 0].Value;
            for (int i = 1; i < gridNum; i++)
            {
                if (flag != data[i, i].Value || !data[i, i].isMarked)
                {
                    flag = null;
                    break;
                }
            }
            if (flag != null)
            {
                return flag;
            }
        }

        if (data[0, gridNum - 1].isMarked)
        {
            flag = data[0, gridNum - 1].Value;
            for (int i = 1; i < gridNum; i++)
            {
                if (flag != data[i, gridNum - 1 - i].Value || !data[i, gridNum - 1 - i].isMarked)
                {
                    flag = null;
                    break;
                }
            }
            if (flag != null)
            {
                return flag;
            }
        }

        for (int i = 0; i < gridNum; i++)
        {
            for (int j = 0; j < gridNum; j++)
            {
                if (!data[i, j].isMarked)
                {
                    return null;
                }
            }
        }

        return "Tie";
    }
}
