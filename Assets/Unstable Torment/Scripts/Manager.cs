using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Manager : MonoBehaviour
{
    //gemLabel.text = SaveGameData.current.inventory.gems.ToString("D3");
    public Sprite[] numbers = new Sprite[10];
    public Image[] scoreImages = new Image[6];
    public TilemapCollider2D[] walls = new TilemapCollider2D[3];
    private AnnouncementTextScript flavortext;

    //index 0: innerwalls
    //index 1: outerwalls
    //index 2: outestwalls right
    //index 3: outestwalls left
    public EnemySpawnManager[] spawnManagers = new EnemySpawnManager[4];
    public GameObject[] enemies1 = new GameObject[1];
    public GameObject[] enemies2 = new GameObject[2];
    public GameObject[] enemies3 = new GameObject[3];
    public GameObject gate;
    public GameObject gameoverPanel;
    public GameObject tryagainbutton;
    public EventSystem eventSystem;
    public PlayerScript player;

    public enum GameState
    {
        MENU,
        PLAYERNORMAL,
        PLAYERENRAGED,
        WAITING,
        GAMEOVER,
        GAMEWON
    }

    public GameState curState = GameState.PLAYERNORMAL;




    public void ChangeScoreBoard()
    {
        LookForNextTier();

        string score = StatsTracker.curScore.ToString("000000");
        Debug.Log(score);
        for(int i = 0; i < scoreImages.Length; i++)
        {
            //string cur = score.Substring(i);
            char[] temp = score.ToCharArray();
            int test = (int)char.GetNumericValue(temp[i]);
            scoreImages[i].sprite = numbers[test];
        }
        
    }

    private void LookForNextTier()
    {
        if(StatsTracker.curScore > StatsTracker.scoreForNextTier)
        {
            flavortext.PopUp(StatsTracker.currentTier);
            switch (StatsTracker.currentTier)
            {
                case 0:
                    player.speed += 1f;
                    player.strikeRadius += .15f;

                    IncreaseTier(1000);

                    break;
                case 1:
                    walls[0].gameObject.SetActive(false);
                    walls[1].enabled = true;

                    spawnManagers[1].SetMySpawns(enemies1);
                    spawnManagers[1].ActivateMySpawners();
                    spawnManagers[0].SetMySpawns(enemies2);

                    spawnManagers[0].SetTimers(3, 7);

                    IncreaseTier(2500);
                    break;
                case 2:
                    player.instabilityRate++;

                    IncreaseTier(4000);
                    break;
                case 3:
                    StatsTracker.clawdamage++;
                    StatsTracker.clawtier++;
                    FindObjectOfType<StrikeObject>().transform.localScale += new Vector3(1.15f, 1.15f, 1.15f);

                    IncreaseTier(5500);
                    break;
                case 4:
                    player.instabilityRate++;

                    player.speed += 1f;
                    player.strikeRadius += .15f;

                    walls[1].gameObject.SetActive(false);
                    walls[2].enabled = true;

                    spawnManagers[2].SetMySpawns(enemies3);
                    spawnManagers[2].ActivateMySpawners();
                    spawnManagers[3].SetMySpawns(enemies2);
                    spawnManagers[3].ActivateMySpawners();
                    spawnManagers[0].SetMySpawns(enemies3);
                    spawnManagers[1].SetMySpawns(enemies1);

                    spawnManagers[0].SetTimers(3, 8);
                    spawnManagers[1].SetTimers(2, 13);
                    spawnManagers[2].SetTimers(4, 8);
                    spawnManagers[3].SetTimers(6, 12);


                    IncreaseTier(6500);
                    break;
                case 5:
                    FindObjectOfType<StrikeObject>().transform.localScale += new Vector3(1.15f, 1.15f, 1.15f);

                    IncreaseTier(7500);
                    break;
                case 6:
                    player.instabilityRate++;

                    IncreaseTier(9000);
                    break;
                case 7:
                    StatsTracker.clawdamage++;
                    StatsTracker.clawtier++;
                    FindObjectOfType<StrikeObject>().transform.localScale += new Vector3(1.15f, 1.15f, 1.15f);

                    IncreaseTier(10000);
                    break;
                case 8:
                    player.instabilityRate++;

                    IncreaseTier(11000);
                    break;
                case 9:
                    player.speed += 1f;
                    player.strikeRadius += .15f;


                    IncreaseTier(12000);
                    break;
                case 10:
                    player.instabilityRate++;
                    FindObjectOfType<StrikeObject>().transform.localScale += new Vector3(1.15f, 1.15f, 1.15f);

                    IncreaseTier(999999);
                    break;
                case 11:
                    gate.SetActive(true);
                    break;
            }
            
        }
    }


    private void Awake()
    {
        //DontDestroyOnLoad(this);
        spawnManagers[0].SetMySpawns(enemies1);
        eventSystem = EventSystem.current;
        flavortext = FindObjectOfType<AnnouncementTextScript>();
    }

    private void IncreaseTier(int newScore)
    {
        StatsTracker.scoreForNextTier = newScore;
        StatsTracker.currentTier++;
    }

    public void GameOver()
    {
        player.bc.enabled = false;
        player.canmove = false;
        spawnManagers[0].gameObject.SetActive(false);
        spawnManagers[1].gameObject.SetActive(false);
        spawnManagers[2].gameObject.SetActive(false);
        spawnManagers[3].gameObject.SetActive(false);

        StatsTracker.reset();


        gameoverPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(tryagainbutton);
    }

    public void TryAgain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HellArena");
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void LoadEpicWin()
    {
        StatsTracker.reset();
        UnityEngine.SceneManagement.SceneManager.LoadScene("WinScreen");
    }
}
