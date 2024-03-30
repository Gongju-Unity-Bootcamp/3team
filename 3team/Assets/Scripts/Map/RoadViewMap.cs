using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // ��ư�� ����ϱ� ���� �߰��ؾ� �� using ��

public class RoadViewMap : UI
{
    private List<Button> buttons = new List<Button>(); // ��ư�� ������ ����Ʈ
    private RoadViewMenu _RoadViewmenu;
    // Start is called before the first frame update
    void Start()
    {
        FindButtons(transform); // ������ �� ��ư�� ã�� �Լ� ȣ��
        AddButtonClickEvents(); // ��ư�� Ŭ�� �̺�Ʈ�� �߰��ϴ� �Լ� ȣ��
        _RoadViewmenu = transform.parent.GetComponent<RoadViewMenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ���� ������Ʈ�� �ڽĵ��� ��ȸ�ϸ鼭 ��ư ������Ʈ�� ã�� �Լ�
    void FindButtons(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Button button = child.GetComponent<Button>(); // �ڽ��� ��ư ������Ʈ ��������
            if (button != null)
            {
                buttons.Add(button); // ��ư�� �ִٸ� ����Ʈ�� �߰�
            }
        }
    }

    // ��ư�� Ŭ�� �̺�Ʈ�� �߰��ϴ� �Լ�
    void AddButtonClickEvents()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i; // Ŭ������ ����Ͽ� ���� ��ư�� �ε����� ����

            buttons[i].onClick.AddListener(() => ButtonClickHandler(buttonIndex));
        }
    }

    // ��ư Ŭ�� �� ����� �޼���
    void ButtonClickHandler(int index)
    {
        #region ��ư��
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
