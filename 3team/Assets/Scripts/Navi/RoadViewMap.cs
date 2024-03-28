using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // 버튼을 사용하기 위해 추가해야 할 using 문

public class RoadViewMap : UI
{
    private List<Button> buttons = new List<Button>(); // 버튼을 저장할 리스트
    private RoadViewMenu _RoadViewmenu;
    // Start is called before the first frame update
    void Start()
    {
        FindButtons(transform); // 시작할 때 버튼을 찾는 함수 호출
        AddButtonClickEvents(); // 버튼에 클릭 이벤트를 추가하는 함수 호출
        _RoadViewmenu = transform.parent.GetComponent<RoadViewMenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 게임 오브젝트의 자식들을 순회하면서 버튼 컴포넌트를 찾는 함수
    void FindButtons(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Button button = child.GetComponent<Button>(); // 자식의 버튼 컴포넌트 가져오기
            if (button != null)
            {
                buttons.Add(button); // 버튼이 있다면 리스트에 추가
            }
        }
    }

    // 버튼에 클릭 이벤트를 추가하는 함수
    void AddButtonClickEvents()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i; // 클로저를 사용하여 현재 버튼의 인덱스를 보존

            buttons[i].onClick.AddListener(() => ButtonClickHandler(buttonIndex));
        }
    }

    // 버튼 클릭 시 실행될 메서드
    void ButtonClickHandler(int index)
    {
        #region 버튼들
        switch (index)
        {
            case 0:
                _RoadViewmenu.SetImageToRoadViewMap(6, 1);
                break;
            case 1:
                _RoadViewmenu.SetImageToRoadViewMap(6, 2);
                break;
            case 2:
                _RoadViewmenu.SetImageToRoadViewMap(6, 3);
                break;
            case 3:
                _RoadViewmenu.SetImageToRoadViewMap(6, 4);
                break;
            case 4:
                _RoadViewmenu.SetImageToRoadViewMap(6, 5);
                break;
            case 5:
                _RoadViewmenu.SetImageToRoadViewMap(6, 6);
                break;
            case 6:
                _RoadViewmenu.SetImageToRoadViewMap(6, 7);
                break;
            case 7:
                _RoadViewmenu.SetImageToRoadViewMap(5, 7);
                break;
            case 8:
                _RoadViewmenu.SetImageToRoadViewMap(4, 7);
                break;
            case 9:
                _RoadViewmenu.SetImageToRoadViewMap(3, 7);
                break;
            case 10:
                _RoadViewmenu.SetImageToRoadViewMap(2, 7);
                break;
            case 11:
                _RoadViewmenu.SetImageToRoadViewMap(1, 7);
                break;
            case 12:
                _RoadViewmenu.SetImageToRoadViewMap(1, 6);
                break;
            case 13:
                _RoadViewmenu.SetImageToRoadViewMap(1, 5);
                break;
            case 14:
                _RoadViewmenu.SetImageToRoadViewMap(2, 5);
                break;
            case 15:
                _RoadViewmenu.SetImageToRoadViewMap(3, 5);
                break;
            case 16:
                _RoadViewmenu.SetImageToRoadViewMap(4, 5);
                break;
            case 17:
                _RoadViewmenu.SetImageToRoadViewMap(5, 5);
                break;
            case 18:
                _RoadViewmenu.SetImageToRoadViewMap(1, 4);
                break;
            case 19:
                _RoadViewmenu.SetImageToRoadViewMap(1, 3);
                break;
            case 20:
                _RoadViewmenu.SetImageToRoadViewMap(2, 3);
                break;
            case 21:
                _RoadViewmenu.SetImageToRoadViewMap(3, 3);
                break;
            case 22:
                _RoadViewmenu.SetImageToRoadViewMap(4, 3);
                break;
            case 23:
                _RoadViewmenu.SetImageToRoadViewMap(5, 3);
                break;

        }
        #endregion
        base.ForwardPage(_RoadViewmenu.roadView);
        
    }
}
