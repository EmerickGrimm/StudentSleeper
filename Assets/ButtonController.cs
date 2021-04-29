using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ButtonController : MonoBehaviour
{

    public Button[] buttons;

    GameObject oldPlayer;
    GameObject NewPlayer;

    public Sprite PlayerImg;
    public Sprite DefaultImg;

    int[] LBorder = { 1, 7, 13, 19, 25, 31 };
    int[] RBorder = { 6, 12, 18, 24, 30, 36 };

    List<int> Borders = new List<int>();
    List<string> ActiveButtons = new List<string>();



    private void Start()
    {
        DetermBorders();

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnClick(button.transform.gameObject));
        }
    }

    private void OnClick(GameObject ButtonClicked)
    {
        MoveMade(ButtonClicked);
    }

    public void MoveMade(GameObject ButtonMoved)
    {

        if (!NewPlayer)
        {
            NewPlayer = ButtonMoved;
            DisableOld();
            EnableButtonsFor(NewPlayer);
            setSprite(true);
        }
        else
        {
            oldPlayer = NewPlayer;
            NewPlayer = ButtonMoved;
            DisableOld();
            EnableButtonsFor(NewPlayer);
            setSprite(false);

        }
    }

    void setSprite(bool isFirstMove)
    {
        Image OldImage;
        Image NewImage;

        if (isFirstMove)
        {
            NewImage = NewPlayer.GetComponent<Image>();
            NewImage.sprite = PlayerImg;
        }
        else
        {
            OldImage = oldPlayer.GetComponent<Image>();
            NewImage = NewPlayer.GetComponent<Image>();

            OldImage.sprite = DefaultImg;
            OldImage.type = NewImage.type;
            NewImage.sprite = PlayerImg;
        }

        return;
    }

    void EnableButtonsFor(GameObject CurrentPos)
    {
        int CurrentPosNum = int.Parse(CurrentPos.name);

        //Debug.LogError("Current pos" + CurrentPos.name);

        ActiveButtons.Add(CurrentPosNum.ToString());

        Enable(CurrentPosNum);

        if (((CurrentPosNum + 1) < 36))
        {
            if (isBorder(CurrentPosNum, 0) && !(isBorder((CurrentPosNum + 1), 1)))
            {
                Enable(CurrentPosNum + 1);
            } else if (isBorder(CurrentPosNum, 0) && (isBorder((CurrentPosNum + 1), 1)))
            {
                Debug.Log("Aboba 1");
            } else
            {
                Enable(CurrentPosNum + 1);
            }
        }
        if (((CurrentPosNum - 1) > 0))
        {
            if (isBorder(CurrentPosNum, 0) && !(isBorder((CurrentPosNum - 1), 2)))
            {
                Enable(CurrentPosNum - 1);
            }
            else if (isBorder(CurrentPosNum, 0) && (isBorder((CurrentPosNum - 1), 2)))
            {
                Debug.Log("Aboba");
            }
            else
            {
                Enable(CurrentPosNum - 1);
            }
        }

        if ((CurrentPosNum + 6) < 36)
        {
            Debug.LogError((CurrentPosNum + 6) + "is less than 30 | " + CurrentPosNum);
            Enable(CurrentPosNum + 6);
        }
        else if ((CurrentPosNum + 6) == 36)
        {
            Enable(CurrentPosNum + 6);
        }
        else
        {
            Debug.LogError((CurrentPosNum + 6) + "is greater than 30 | " + CurrentPosNum);
        }

        if ((CurrentPosNum - 6) > 0)
        {
            Debug.Log(CurrentPosNum);
            Debug.Log(CurrentPosNum - 6);
            Enable(CurrentPosNum - 6);
        }
    }

    //Названно по мудацки, но мне лень придумывать нормальные названия войдов.
    //EnableButtonsFor просчитывает и через Enable включает нужные елменты у нужного обьекта.
    void Enable(int num)
    {
        Debug.Log(num);
        Debug.Log(num.ToString());
        GameObject target = GameObject.Find(num.ToString());
        Debug.Log(target.name);
        target.GetComponent<Image>().enabled = true;
        target.GetComponent<Button>().enabled = true;
        Debug.LogWarning("Enabling " + target.name);
        ActiveButtons.Add(num.ToString());
    }

    void DisableOld()
    {
        int i = 0;
        GameObject target;
        if (ActiveButtons.Count > 0)
        {
            while (i != (ActiveButtons.Count))
            {
                target = GameObject.Find(ActiveButtons[i]);
                // Debug.LogWarning("Disabling" + ActiveButtons[i]);
                target.GetComponent<Image>().enabled = false;
                target.GetComponent<Button>().enabled = false;
                i++;
            }
            ActiveButtons.Clear();
        }

    }

    //Определяем границы чтобы не вывалится за них при активации кнопок
    void DetermBorders()
    {

        Borders.AddRange(LBorder);
        Borders.AddRange(RBorder);
        return;
    }

    bool isBorder(int num, int Area)
    {
        switch (Area)
        {
            case 0:
                {
                    if (Borders.Contains(num))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case 1:
                {
                    if (LBorder.Contains(num))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case 2:
                {
                    if (RBorder.Contains(num))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            default:
                {
                    return false;
                }
        }
    }
}
