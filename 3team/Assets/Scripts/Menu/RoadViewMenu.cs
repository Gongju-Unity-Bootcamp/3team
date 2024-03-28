using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;
using System;
using UniRx.Triggers;

public class RoadViewMenu : MonoBehaviour
{
    private GameObject buttons;

    private Image image;
    
    private Button upArrow;
    private Button downArrow;
    private Button rightArrow;
    private Button leftArrow;

    private const string INP = "987654321";

    private string[][] roadList;
    private readonly int verticalCount;
    private readonly int horizontalCount;

    //입구에서 시작한다는 가정으로 설정함.
    public int userY { get; set; } = 1;
    public int userX { get; set; } = 6;

    void Awake()
    {

        Init();
        SetButton();
        SetRoadArray();
        SetImage();
        ArrowBSP();
        
    }

    private void Start()
    {
        transform.Find("RoadImage").gameObject.SetActive(false);
    }
    /// <summary>
    /// 범위를 확장시킬 경우 초기화 메서드
    /// </summary>
    public void Init()
    {
        buttons = transform.Find("Buttons").gameObject;
        upArrow = buttons.transform.Find("Up").GetComponent<Button>();   
        downArrow = buttons.transform.Find("Down").GetComponent<Button>();   
        leftArrow = buttons.transform.Find("Left").GetComponent<Button>();   
        rightArrow = buttons.transform.Find("Right").GetComponent<Button>();   
        image = transform.Find("RoadImage").GetComponent<Image>();
    }

    public void SetButton()
    {
        Button[] arrowButtons = new Button[4] { upArrow, downArrow, leftArrow, rightArrow };

        foreach (var button in arrowButtons)
        {
            button.OnPointerClickAsObservable().Subscribe(_ => Arrowclick(_)).AddTo(button.GetComponent<Component>());
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

    public void Arrowclick(PointerEventData eventData)
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        var index = TupleIndex(go);
        userY += index.Item1;
        userX += index.Item2;
        SetImage();
        ArrowBSP();

    }


    public void SetImage()
    {
        Debug.Log(roadList[userX][userY]);
        image.sprite = Manager.Resources.LoadSprite(roadList[userY][userX]);
        Debug.Log(userX);
        Debug.Log(userY);
    }
    public void SetImageToRoadViewMap(int x,int y)
    {
        userX = x;
        userY = y;
        SetImage();
    }
    public void ArrowBSP()
    {
        upArrow.gameObject.SetActive(IsArrowInBound(upArrow.gameObject));
        downArrow.gameObject.SetActive(IsArrowInBound(downArrow.gameObject));
        leftArrow.gameObject.SetActive(IsArrowInBound(leftArrow.gameObject));
        rightArrow.gameObject.SetActive(IsArrowInBound(rightArrow.gameObject));

    }
    public bool IsArrowInBound(GameObject arrow)
    {
        bool isINP = true;
        var array = TupleIndex(arrow);
        var userPo = (userY + array.Item1,  userX + array.Item2); 

        if (roadList[userPo.Item1][userPo.Item2] == INP)
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
}
