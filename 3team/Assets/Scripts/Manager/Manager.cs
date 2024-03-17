using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    public static SoundManager Sound { get; private set; }

    public static ResourcesManager Resources { get; private set; }

    public static DataManager Data { get; private set; }

    public static UIManager UI { get; private set; }

    public static VidioManager Vidio { get; private set; }


    private void Awake()
    {
        if(Instance == null) 
        { 
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        Init();
    }

    void Init()
    {
        GameObject go;

        go = new GameObject(nameof(DataManager));
        go.transform.parent = transform;
        Data = go.AddComponent<DataManager>();

        go = new GameObject(nameof(Resources));
        go.transform.parent = transform;
        Resources = go.AddComponent<ResourcesManager>();


        go = new GameObject(nameof(SoundManager));
        go.transform.parent = transform;
        Sound = go.AddComponent<SoundManager>();

        go = new GameObject(nameof(UIManager));
        go.transform.parent = transform;
        UI = go.AddComponent<UIManager>();

        go = new GameObject(nameof(VidioManager));
        go.transform.parent = transform;
        Vidio = go.AddComponent<VidioManager>();


        //Data.Init();
        Resources.Init();
        Sound.Init();
        UI.Init();
        Vidio.Init();
    }

}

//��巹����� ������Ʈ Ȯ��
//���̽� ���Ϸ� �ҷ��� ������ csv���Ͽ� ����(�̹��� ���ϵ�)
//������ġ��ü���� ����ϴ� ���α׷�?

