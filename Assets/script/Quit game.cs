using UnityEngine;
using UnityEditor; // Chỉ cần nếu bạn muốn hỗ trợ Editor mode

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
#if UNITY_EDITOR
        // Dừng trò chơi khi đang chạy trong Unity Editor
        EditorApplication.isPlaying = false;
#else
        // Thoát trò chơi khi chạy trên nền tảng build
        Application.Quit();
#endif
    }
}