using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NaverMapAPI : MonoBehaviour
{
    public RawImage mapRawImage;
    public Texture2D mapTexture { get; set; }
    [Header("맵 정보 입력")]
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



    private Vector2 userPosition;
    private Vector2 mainBuildingPosition;
    private Vector2 welfareCenterPosition;
    private Vector2 dormitoryPosition;
    private Vector2 engineeringBuildingAPosition;
    private Vector2 engineeringBuildingBPosition;
    private Vector2 tennisCourtPosition;
    private Vector2 playgroundPosition;

    private MapProcessor _mapProcessor;
    private void Start()
    {
        _mapProcessor = GetComponent<MapProcessor>();
        mapWidth = mapRectTransform.sizeDelta.x.ToString();
        mapHeight = mapRectTransform.sizeDelta.y.ToString();

        mapRawImage = GetComponent<RawImage>();

        mainBuilding.onClick.AddListener(() => OnPlaceButtonClick("mainBuilding"));
        welfareCenter.onClick.AddListener(() => OnPlaceButtonClick("welfareCenter"));
        dormitory.onClick.AddListener(() => OnPlaceButtonClick("dormitory"));
        engineeringBuildingA.onClick.AddListener(() => OnPlaceButtonClick("engineeringBuildingA"));
        engineeringBuildingB.onClick.AddListener(() => OnPlaceButtonClick("engineeringBuildingB"));
        tennisCourt.onClick.AddListener(() => OnPlaceButtonClick("tennisCourt"));
        playground.onClick.AddListener(() => OnPlaceButtonClick("playground"));

        StartCoroutine(RequestLocationPermission());
        StartCoroutine(MapLoader());
        StartCoroutine(UpdateUserLocation());
        SetPlaceLocationMarker();
    }
    
    //사용자 위치 정보 권한 획득
    private IEnumerator RequestLocationPermission()
    {
        if (!Input.location.isEnabledByUser)
        {
            // 위치 서비스가 꺼져 있거나 권한이 없는 경우 권한을 요청
            Debug.Log("Requesting location permission...");

            Permission.RequestUserPermission(Permission.FineLocation);

            // 권한 요청에 대한 사용자 응답을 기다림
            while (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                yield return null;
            }

            // 권한이 승인된 경우 위치 서비스 시작
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
            // 이미 위치 서비스 권한이 있는 경우
            Debug.Log("Location services are already enabled. Permission granted.");
        }
    }


    //사용자 위치 파악
    IEnumerator UpdateUserLocation()
    {
        while (true)
        {
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("사용자가 위치 서비스를 활성화하지 않았습니다.");
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
                Debug.Log("위치 서비스 초기화 대기 시간이 초과되었습니다.");
                yield break;
            }

            userLatitude = Input.location.lastData.latitude.ToString();
            userLongitude = Input.location.lastData.longitude.ToString();

            SetUserLocationMarker();
            Input.location.Stop();
            yield return new WaitForSeconds(0.5f);  // 0.5초마다 사용자 위치 업데이트
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
            mapTexture = DownloadHandlerTexture.GetContent(request);
            mapRawImage.texture = mapTexture;
            
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



        // 각 장소의 위치에 버튼을 설정
        mainBuildingPosition = SetLocationMarker(mainBuilding, mainBuildinglati, mainBuildinglong);
        welfareCenterPosition = SetLocationMarker(welfareCenter, welfareCenterlati, welfareCenterlong);
        dormitoryPosition = SetLocationMarker(dormitory, dormitorylati, dormitorylong);
        engineeringBuildingAPosition = SetLocationMarker(engineeringBuildingA, engineeringBuildingAlati, engineeringBuildingAlong);
        engineeringBuildingBPosition = SetLocationMarker(engineeringBuildingB, engineeringBuildingBlati, engineeringBuildingBlong);
        tennisCourtPosition = SetLocationMarker(tennisCourt, tennisCourtlati, tennisCourtlong);
        playgroundPosition = SetLocationMarker(playground, playgroundlati, playgroundlong);
    }
    private Vector2 SetLocationMarker(Button locationButton, float latitude, float longitude)
    {
        
        // 위치를 float로 변환
        Vector2 normalizedPos = Clamping(latitude, longitude);

        locationButton.GetComponent<RectTransform>().anchorMin = normalizedPos;
        locationButton.GetComponent<RectTransform>().anchorMax = normalizedPos;

        return normalizedPos;
    }
    private Vector2 SetLocatioDoor(float latitude, float longitude)
    {

        // 위치를 float로 변환
        Vector2 normalizedPos = Clamping(latitude, longitude);
        return normalizedPos;
    }

    private void SetUserLocationMarker()
    {
        // 사용자의 위치가 유효한 경우에만 처리
        if (!string.IsNullOrEmpty(userLatitude) && !string.IsNullOrEmpty(userLongitude))
        {
            // 사용자 위치를 float로 변환
            float userLat = float.Parse(userLatitude);
            float userLong = float.Parse(userLongitude);


            Vector2 normalizedUserPos = Clamping(userLat, userLong);
            userPosition = normalizedUserPos;
            // userLocationMarker 위치 설정
            userLocationMarker.rectTransform.anchorMin = userPosition;
            userLocationMarker.rectTransform.anchorMax = userPosition;

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

    public void OnPlaceButtonClick(string place)
    {
        float doorLati = 0f;
        float doorLong = 0f;

        switch (place)
        {
            case "mainBuilding":
                doorLati = 36.520056f;
                doorLong = 127.172804f;
                break;
            case "welfareCenter":
                doorLati = 36.520518f;
                doorLong = 127.173606f;
                break;
            case "dormitory":
                doorLati = 36.521198f;
                doorLong = 127.173368f;
                break;
            case "engineeringBuildingA":
                doorLati = 36.520557f;
                doorLong = 127.172414f;
                break;
            case "engineeringBuildingB":
                doorLati = 36.520902f;
                doorLong = 127.171743f;
                break;
            case "tennisCourt":
                doorLati = 36.520162f;
                doorLong = 127.173739f;
                break;
            case "playground":
                doorLati = 36.519942f;
                doorLong = 127.173809f;
                break;


            default:
                return;
        }
        //doorLati = 36.521198f;
        //doorLong = 127.173368f;

        // 사용자 위치와 선택한 장소의 위치를 사용하여 경로 초기화
        _mapProcessor.Init(userPosition, SetLocatioDoor(doorLati, doorLong));
    }

}