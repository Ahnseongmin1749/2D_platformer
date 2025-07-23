using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public Move player;
    public GameObject[] Stages;

    public Image[] UIhealth;
    public TextMeshProUGUI UIPoint;
    public TextMeshProUGUI UIStage;
    public GameObject RestartBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
        UIStage.text = "STAGE " + (stageIndex + 1).ToString();
    }

    public void NextStage()
    {
        //Change Stage
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
            
        }
        else
        {
            //Game Clear
            //Player Control Lock
            Time.timeScale = 0f;

            //Result UI
            Debug.Log("게임 클리어!");

            //Restart Button UI
            TextMeshProUGUI btnText = RestartBtn.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = "Game Clear!";
            RestartBtn.SetActive(true);

        }

            //Calculate Point
            totalPoint += stagePoint;
            stagePoint = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            /*//Health Down
            HealthDown();*/

            //Player Reposition
            if (health > 1)
            {
                PlayerReposition();
                HealthDown();
            }

            else if (health == 1)
            {
                HealthDown();
            }

        }
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(0, 0, -1);
        player.VelocityZero();
    }

    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            UIhealth[0].color = new Color(1, 0, 0, 0.4f);

            //Player Die Effect
            player.OnDie();

            //Retry Button Ui
            RestartBtn.SetActive(true);

            


        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
