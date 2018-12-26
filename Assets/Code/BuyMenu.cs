using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : MonoBehaviour {

    public static bool isPaused = false;

    public GameObject buyMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        buyMenuUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        Game.overallPlayer.canMove = true;
    }

    public void Pause()
    {
        buyMenuUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
        Game.overallPlayer.canMove = false;
    }

    public void Health()
    {
        Game.overallPlayer.HealthUpdate(25);
    }

    public void Damage()
    {
        Game.overallPlayer.damageUpdate(0.10f);
    }

    public void Jump()
    {
        Game.overallPlayer.jumpUpdate(0.10f);
    }

}
