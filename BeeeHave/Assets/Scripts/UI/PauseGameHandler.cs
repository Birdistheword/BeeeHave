using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameHandler : MonoBehaviour
{
  [SerializeField] Canvas pauseMenuPrefab;

  bool gameIsPaused = false;

  private void Start()
  {
    pauseMenuPrefab.enabled = false;
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape) && !gameIsPaused)
    {
      Pausegame();
    }
    else if (Input.GetKeyDown(KeyCode.Escape) && gameIsPaused)
    {
      ResumeGame();
    }
  }

  public void Pausegame()
  {
    Time.timeScale = 0;
    pauseMenuPrefab.enabled = true;
  }

  public void ResumeGame()
  {
    Time.timeScale = 1;
    pauseMenuPrefab.enabled = false;
  }
}
