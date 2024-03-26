using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
using System;

public class RoadViewMenu : MonoBehaviour
{
    private GameObject buttons;
    private GameObject roadImages;

    private Image imageCentor;
    private Image imageUp;
    private Image imageDown;
    private Image imageLeft;
    private Image imageRight; 
    
    private Button upArrow;
    private Button downArrow;
    private Button rightArrow;
    private Button leftArrow;

    private const string INP = "987654321";

    private string[][] roadList;
    private readonly int verticalCount;
    private readonly int horizontalCount;

    //입구에서 시작한다는 가정으로 설정함.
    int userY = 1;
    int userX = 6;

    void Awake()
    {
        SetComponent();
        SetButton();
        SetRoadArray();
    }

    /// <summary>
    /// 범위를 확장시킬 경우 초기화 메서드
    /// </summary>
    public void Init()
    {
        SetRoadArray();
    }

    void SetComponent()
    {
        buttons = transform.Find("Buttons").gameObject;
        roadImages = transform.Find("RoadImages").gameObject;

        imageCentor = roadImages.transform.Find("RoadCentor").GetComponent<Image>();
        imageUp = roadImages.transform.Find("RoadUp").GetComponent<Image>();
        imageDown = roadImages.transform.Find("RoadDown").GetComponent<Image>();
        imageLeft = roadImages.transform.Find("RoadLeft").GetComponent<Image>();
        imageRight = roadImages.transform.Find("RoadRight").GetComponent<Image>();

        upArrow = buttons.transform.Find("Up").GetComponent<Button>();
        downArrow = buttons.transform.Find("Down").GetComponent<Button>();
        leftArrow = buttons.transform.Find("Left").GetComponent<Button>();
        rightArrow = buttons.transform.Find("Right").GetComponent<Button>();
    }
    void SetButton()
    {
        Button[] arrowButtons = new Button[] { upArrow, downArrow, leftArrow, rightArrow };

        foreach (var button in arrowButtons)
        {
            button.OnClickAsObservable().Subscribe(_ => Arrowclick());
        }
    }

    /// <summary>
    /// 지역을 확장시킬경우 클래스로 배열을 관리하고 매개변수로 클래스를 전달.
    /// ex: public class RoadViewArray
    ///     {
    ///         public int[][] RoadView { get; set;}
    ///     }
    ///  => void SetRoadImage(RoadViewArray roadview)
    /// </summary>
    void SetRoadArray()
    {
        roadList = new string[10][];
        roadList[9] = new string[] { INP, INP,  INP,  INP,  INP,  INP,  INP,  INP };
        roadList[8] = new string[] { INP, "16", "15", "14", "13", "11", "10", INP };
        roadList[7] = new string[] { INP, "16", "15", "14", "13", "12", "8",  INP };
        roadList[6] = new string[] { INP, "17", INP,  INP,  INP,  INP,  "7",  INP };
        roadList[5] = new string[] { INP, "29", "19", "20", "21", "22", "5",  INP };
        roadList[4] = new string[] { INP, "28", INP,  INP,  INP,  INP,  "4",  INP };
        roadList[3] = new string[] { INP, "27", "26", "25", "24", "23", "3",  INP };
        roadList[2] = new string[] { INP, INP,  INP,  INP,  INP,  INP,  "1",  INP };
        roadList[1] = new string[] { INP, INP,  INP,  INP,  INP,  INP,  "0",  INP };
        roadList[0] = new string[] { INP, INP,  INP,  INP,  INP,  INP,  INP,  INP };
    }

    void Arrowclick()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        var index = TupleIndex(go);
        userY += index.Item1;
        userX += index.Item2;
        RoadMove(go);
        ArrowBSP();

    }

    //void RoadMove(int y, int x)
    void RoadMove(GameObject go)
    {
        (int vertical, int horizontal) = TupleIndex(go);
        var image = ImagePosition(go);

        image.Item1.transform.position = new Vector2(0, vertical);
        image.Item2.transform.position = new Vector2(horizontal, 0);

        image.Item1.name = GetName(image.Item1.transform.position);
        image.Item2.name = GetName(image.Item2.transform.position);

        SetImage();
    }

    void SetImage()
    {
        imageUp.sprite = Manager.Resources.LoadSprite(roadList[userY + 1][userX]);
        imageDown.sprite = Manager.Resources.LoadSprite(roadList[userY - 1][userX]);
        imageLeft.sprite = Manager.Resources.LoadSprite(roadList[userY][userX - 1]);
        imageRight.sprite = Manager.Resources.LoadSprite(roadList[userY][userX + 1]);
    }

    string GetName(Vector2 vector)
    {
        string name = "";
        switch (vector)
        {
            case var v when v == new Vector2(1, 0):
                name = "RoadUp";
                break;
            case var v when v == new Vector2(-1, 0):
                name = "RoadDown";
                break;
            case var v when v == new Vector2(0, 1):
                name = "RoadRight";
                break;
            case var v when v == new Vector2(0, -1):
                name = "RoadLeft";
                break;
            default:
                break;
        };

        return name;
    }
    void ArrowBSP()
    {
        upArrow.gameObject.SetActive(IsArrowInBound(upArrow.gameObject));
        downArrow.gameObject.SetActive(IsArrowInBound(downArrow.gameObject));
        leftArrow.gameObject.SetActive(IsArrowInBound(leftArrow.gameObject));
        rightArrow.gameObject.SetActive(IsArrowInBound(rightArrow.gameObject));

    }
    bool IsArrowInBound(GameObject arrow)
    {
        bool isINP = true;
        var array = TupleIndex(arrow);
        
        if (roadList[array.Item1][array.Item2] == INP)
        {
            isINP = false;
        }

        return isINP;
    }

    (int, int) TupleIndex(GameObject go)
    {
        var index = go.name switch
        {
            "Up" => (1, 0),
            "Down" => (-1, 0),
            "Left" => (0, -1),
            "Right" => (0, 1),
            _ => throw new ArgumentException("Unexpected value: " + go.name)
        };

        return (index.Item1, index.Item2);
    }

    (GameObject, GameObject) ImagePosition(GameObject go)
    {
        (GameObject vertical, GameObject horizontal) = go.name switch
        {
            "Up" => (imageCentor.gameObject, imageUp.gameObject),
            "Down" => (imageCentor.gameObject, imageDown.gameObject),
            "Left" => (imageCentor.gameObject, imageLeft.gameObject),
            "Right" => (imageCentor.gameObject, imageRight.gameObject),
            _ => throw new ArgumentException("Unexpected value: " + go.name)
        };

        return (vertical, horizontal);
    }
}
