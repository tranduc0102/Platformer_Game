using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int livePlayer = 3;
    public int idAnimator = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}