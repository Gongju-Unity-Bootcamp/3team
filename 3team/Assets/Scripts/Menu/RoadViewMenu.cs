using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.EventSystems;

public class RoadViewMenu : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Button upArrow;
    [SerializeField] private Button downArrow;
    [SerializeField] private Button rightArrow;
    [SerializeField] private Button leftArrow;

    private string[][] roadList;
    private readonly int verticalCount;
    private readonly int horizontalCount;

    //입구에서 시작한다는 가정으로 설정함.
    int userY = 1;
    int userX = 6;


    //데이터테이블에서 가져와서 초기화
    //public RoadViewMenu(int v, int h)
    //{
    //    verticalCount = v;
    //    horizontalCOunt = h;
    //}
    void Awake()
    {
        image = transform.Find("Road").GetComponent<Image>();
        upArrow = transform.Find("Up").GetComponent<Button>();
        downArrow = transform.Find("Down").GetComponent<Button>();
        leftArrow = transform.Find("Left").GetComponent<Button>();
        rightArrow = transform.Find("Right").GetComponent<Button>();
        SetButton();
        SetRoadImage();

    }
    private void Start()
    {
        Debug.Log(roadList[1][6]);
        RoadMove(userY, userX);
        SetArrow();
    }

    void SetButton()
    {
        Button[] arrowButtons = new Button[] { upArrow, downArrow, leftArrow, rightArrow };

        foreach (var button in arrowButtons)
        {
            button.OnClickAsObservable().Subscribe(_ => Arrowclick());
        }
    }

    private const string INP = "987654321";
    void SetRoadImage()
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
        var index = go.name switch
        {
            "Up"    => (1, 0),
            "Down"  => (-1, 0),
            "Left"  => (0, -1),
            "Right" => (0, 1)
        };
        userY += index.Item1;
        userX += index.Item2;
        RoadMove(userY, userX);
        SetArrow();

    }

    void RoadMove(int y, int x)
    {

        image.sprite = Manager.Resources.LoadSprite(roadList[y][x]);
    }

    void SetArrow()
    {
        if (roadList[userY + 1][userX] == INP)
        {
            upArrow.gameObject.SetActive(false);
        }
        else
        {
            upArrow.gameObject.SetActive(true);
        }
        if (roadList[userY - 1][userX] == INP)
        {
            downArrow.gameObject.SetActive(false);
        }
        else
        {
            downArrow.gameObject.SetActive(true);
        }
        if (roadList[userY][userX + 1] == INP)
        {
            rightArrow.gameObject.SetActive(false);
        }
        else
        {
            rightArrow.gameObject.SetActive(true);
        }

        if (roadList[userY][userX -1] == INP)
        {
            leftArrow.gameObject.SetActive(false);
        }
        else
        {
            leftArrow.gameObject.SetActive(true);
        }


    }
}
