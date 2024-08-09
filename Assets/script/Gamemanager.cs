using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Tham chiếu đến Panel thông báo game over

    void Start()
    {
        gameOverPanel.SetActive(false); // Đảm bảo panel không hiển thị khi bắt đầu game
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true); // Hiển thị panel thông báo game over
        Time.timeScale = 0; // Dừng game (hoặc tùy chỉnh theo yêu cầu của bạn)
    }
}
