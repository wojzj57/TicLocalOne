using UnityEngine;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    private static object get;

    [HideInInspector]
    public GameManager manager;

    public bool isMarked = false;
    private string value;

    public string Value
    {
        get { return value; }
        set
        {
            this.value = value;
            if (value != null)
            {
                isMarked = true;
            }
            else
            {
                isMarked = false;
            }
        }
    }

    public void Draw()
    {
        if (value == "X")
        {
            GetComponent<Image>().sprite = manager.x_sprite;
        }
        else if (value == "O")
        {
            GetComponent<Image>().sprite = manager.o_sprite;
        }
    }

    public void Mark()
    {
        if (manager)
        {
            Value = manager.GetCurrentPlayer().side.ToString();
            Draw();
            manager.MarkCallback();
        }
    }

    public void OnClick()
    {
        if (isMarked || manager.IsGameOver())
        {
            return;
        }
        Mark();
    }

    public void Reset()
    {
        Value = null;
        GetComponent<Image>().sprite = manager.square_sprite;
    }
}
