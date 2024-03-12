using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NaverMapAPI : MonoBehaviour
{
    public RawImage mapRawImage;

    [Header("�� ���� �Է�")]
    public string strBaseURL = "";
    public string latitude = "";
    public string longitude = "";
    public string clientId = "";
    public string clientSecret = "";
    public string level = "18";

    private string mapWidth = "";
    private string mapHeight = "";

    private string userLatitude = "";
    private string userLongitude = "";

    public Image userLocationMarker;
    public RectTransform mapRectTransform;

    private string minMaplati = "36.518502";
    private string maxMaplati = "36.522464";
    private string minMaplong = "127.171687";
    private string maxMaplong = "127.174377";


    public Button mainBuilding;
    public Button welfareCenter;
    public Button dormitory;
    public Button engineeringBuildingA;
    public Button engineeringBuildingB;
    public Button tennisCourt;
    public Button playground;
    private void Start()
    {
        mapWidth = mapRectTransform.sizeDelta.x.ToString();
        mapHeight = mapRectTransform.sizeDelta.y.ToString();
        Debug.Log(mapWidth);
        mapRawImage = GetComponent<RawImage>();
        StartCoroutine(RequestLocationPermission());
        StartCoroutine(MapLoader());
        StartCoroutine(UpdateUserLocation());
        SetPlaceLocationMarker();
    }


    //����� ��ġ ���� ���� ȹ��
    private IEnumerator RequestLocationPermission()
    {
        if (!Input.location.isEnabledByUser)
        {
            // ��ġ ���񽺰� ���� �ְų� ������ ���� ��� ������ ��û
            Debug.Log("Requesting location permission...");

            Permission.RequestUserPermission(Permission.FineLocation);

            // ���� ��û�� ���� ����� ������ ��ٸ�
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }

            // ������ ���ε� ��� ��ġ ���� ����
            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Input.location.Start();
            }
            else
            {
                Debug.Log("Location permission denied by the user.");
            }
        }
        else
        {
            // �̹� ��ġ ���� ������ �ִ� ���
            Debug.Log("Location services are already enabled. Permission granted.");
        }
    }


    //����� ��ġ �ľ�
    IEnumerator UpdateUserLocation()
    {
        while (true)
        {
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("����ڰ� ��ġ ���񽺸� Ȱ��ȭ���� �ʾҽ��ϴ�.");
                yield return null;
            }

            Input.location.Start();

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait <= 0)
            {
                Debug.Log("��ġ ���� �ʱ�ȭ ��� �ð��� �ʰ��Ǿ����ϴ�.");
                yield break;
            }

            userLatitude = Input.location.lastData.latitude.ToString();
            userLongitude = Input.location.lastData.longitude.ToString();

            SetUserLocationMarker();

            Input.location.Stop();
            yield return new WaitForSeconds(0.5f);  // 2.5�ʸ��� ����� ��ġ ������Ʈ
        }
    }

    IEnumerator MapLoader()
    {
        string str = $"{strBaseURL}?center={longitude},{latitude}&level={level}&w={mapWidth}&h={mapHeight}";

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(str);

        request.SetRequestHeader("X-NCP-APIGW-API-KEY-ID", clientId);
        request.SetRequestHeader("X-NCP-APIGW-API-KEY", clientSecret);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            mapRawImage.texture = DownloadHandlerTexture.GetContent(request);

        }
    }

    private void SetPlaceLocationMarker()
    {
        float mainBuildinglati = 36.520280f;
        float mainBuildinglong = 127.172651f;

        float welfareCenterlati = 36.520418f;
        float welfareCenterlong = 127.173239f;

        float dormitorylati = 36.521394f;
        float dormitorylong = 127.172874f;

        float engineeringBuildingAlati = 36.520748f;
        float engineeringBuildingAlong = 127.172416f;

        float engineeringBuildingBlati = 36.521127f;
        float engineeringBuildingBlong = 127.171946f;

        float tennisCourtlati = 36.520211f;
        float tennisCourtlong = 127.174051f;

        float playgroundlati = 36.519721f;
        float playgroundlong = 127.172933f;



    // �� ����� ��ġ�� ��ư�� ����
        SetLocationMarker(mainBuilding, mainBuildinglati, mainBuildinglong);
        SetLocationMarker(welfareCenter, welfareCenterlati, welfareCenterlong);
        SetLocationMarker(dormitory, dormitorylati, dormitorylong);
        SetLocationMarker(engineeringBuildingA, engineeringBuildingAlati, engineeringBuildingAlong);
        SetLocationMarker(engineeringBuildingB, engineeringBuildingBlati, engineeringBuildingBlong);
        SetLocationMarker(tennisCourt, tennisCourtlati, tennisCourtlong);
        SetLocationMarker(playground, playgroundlati, playgroundlong);
    }
    private void SetLocationMarker(Button locationButton, float latitude, float longitude)
    {
        // ��ġ�� float�� ��ȯ
        Vector2 normalizedPos = Clamping(latitude, longitude);

        // ��ư ��ġ ����
        locationButton.GetComponent<RectTransform>().anchorMin = normalizedPos;
        locationButton.GetComponent<RectTransform>().anchorMax = normalizedPos;
    }
    private void SetUserLocationMarker()
    {
        // ������� ��ġ�� ��ȿ�� ��쿡�� ó��
        if (!string.IsNullOrEmpty(userLatitude) && !string.IsNullOrEmpty(userLongitude))
        {
            // ����� ��ġ�� float�� ��ȯ
            float userLat = float.Parse(userLatitude);
            float userLong = float.Parse(userLongitude);


            Vector2 normalizedUserPos = Clamping(userLat, userLong);

            // userLocationMarker ��ġ ����
            userLocationMarker.rectTransform.anchorMin = normalizedUserPos;
            userLocationMarker.rectTransform.anchorMax = normalizedUserPos;


        }
    }

    private Vector2 Clamping(float latitude, float longitude)
    {
        float clampedLat = Mathf.Clamp(latitude, float.Parse(minMaplati), float.Parse(maxMaplati));
        float clampedLong = Mathf.Clamp(longitude, float.Parse(minMaplong), float.Parse(maxMaplong));

        Vector2 Pos = new Vector2((clampedLong - float.Parse(minMaplong)) / (float.Parse(maxMaplong) - float.Parse(minMaplong)),
                                 (clampedLat - float.Parse(minMaplati)) / (float.Parse(maxMaplati) - float.Parse(minMaplati)));

        return Pos;
    }
}