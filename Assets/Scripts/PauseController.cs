using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController instance;
    public bool IsPaused { get; set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
