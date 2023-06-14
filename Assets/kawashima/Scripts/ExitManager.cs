using UnityEngine;

public class ExitManager : MonoBehaviour
{
    //シングルトン(プログラム開始から終了まで一つのオブジェクトだけを存在させようっていう構想)
    //ようはゲープロの「ge->???」のgeの部分　これはゲームエンジンのポインタで開始から終了まで一つだけで動いている
    public static ExitManager _singleton;

    [Header("Exitウィンドウ")]
    [SerializeField] GameObject _exitWindow;
    [Header("デバッグ確認用")]
    [SerializeField] bool _menuFlag = false;
    [SerializeField] bool _fullSCFlag = true;

    void OnEnable()
    {
        //シングルトンのオブジェクトとして生成してゲームが閉じるまで存在させ続ける
        if (_singleton == null)
        {
            _singleton = this;
            DontDestroyOnLoad(gameObject);
            _exitWindow.SetActive(false);
        }
        else
            Destroy(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FuncCanvasChange();
        }
    }

    public void FuncGoExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void FuncCanvasChange()
    {
        _menuFlag = !_menuFlag;
        _exitWindow.SetActive(_menuFlag);
        Time.timeScale = _menuFlag ? 0.0f : 1.0f;
    }

    public void FuncScreenChange()
    {
        _fullSCFlag = !_fullSCFlag;
        //二行か一行で引数だけ変えられないものか
        if (_fullSCFlag)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
        }
    }
}