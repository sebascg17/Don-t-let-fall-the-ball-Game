using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Panel to show when the game is over
    public GameObject gameWinPanel; // Panel to show when the game is over

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start ()
    {
        // Deseleccionar cualquier UI seleccionada al inicio
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ShowGameOver ()
    {
        StartCoroutine(GamePanelRoutine()); // Start the game over routine
        Debug.Log("Moriste wee!"); // Log game over message
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausar el tiempo
    }
    public void ShowGameWin ()
    {
        StartCoroutine(GamePanelRoutine()); // Start the game over routine
        Time.timeScale = 0f; // Pausar el tiempo
    }

    IEnumerator GamePanelRoutine ()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds
    }

    public void RestartGame ()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame ()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
