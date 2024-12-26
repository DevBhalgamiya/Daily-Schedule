using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] State startDayState;
    [SerializeField] State midDayState;
    [SerializeField] State nightDayState;

    State state;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SetInitialState();
    }

    void Update()
    {
        ManageState();
    }

    private void SetInitialState()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if (activeScene == "Start Day")
        {
            state = startDayState;
        }
        else if (activeScene == "Mid Day")
        {
            state = midDayState;
        }
        else if (activeScene == "Night Day")
        {
            state = nightDayState;
        }

        if (textComponent != null && state != null)
        {
            textComponent.text = state.GetStateStory();
        }
    }

    private void ManageState()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if (activeScene == "Mid Day")
        {
            var nextStates = state.GetNextStates();
            for (int index = 0; index < nextStates.Length; index++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + index))
                {
                    state = nextStates[index];
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Night Day");
            }

            if (textComponent != null && state != null)
            {
                textComponent.text = state.GetStateStory();
            }
        }
        else if (activeScene == "Night Day" && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Game Over. Thanks for playing!");
            Application.Quit();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit();
    }

    public void LoadMidDayScene()
    {
        Debug.Log("Loading Mid Day Scene...");
        SceneManager.LoadScene("Mid Day");
    }
}
