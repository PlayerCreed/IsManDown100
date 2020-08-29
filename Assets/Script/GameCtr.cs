using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCtr : MonoBehaviour
{
    public static GameCtr instance;
    public float StartWait;
    public GameObject[] Paltforms;
    public GameObject[] Tarps;
    public int PaltformCount;
    public int TarpCount;
    public Transform SpawnPosition;
    public float SpawnDelay;
    private float[] fanposition = new float[2] { -2.35f, 2.35f };
    public int score = 0;
    public bool GameOverFlag;
    public Text ScoreText, TimeText;
    public GameObject gameoverUI;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnUnit());
        GameOverFlag = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();
        ScoreText.text = "Score: " + score.ToString("000");
        TimeText.text = "Time: " + Time.timeSinceLevelLoad.ToString("000");//获取场景运行时的时间转换为000整数格式

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新加载目前场景
        Time.timeScale = 1;//启动游戏
    }

    public void Quit()
    {
        Application.Quit();//关闭游戏
    }

    void GameOver()
    {
        if (GameOverFlag == true)
        {
            instance.gameoverUI.SetActive(true);
            Time.timeScale = 0f;//暂停游戏
            GameOverFlag = false;
        }
    }

    IEnumerator SpawnUnit()
    {
        yield return new WaitForSeconds(StartWait);

        while (true)
        {
            Vector3 OldPosition = new Vector3(0, 0, 0);
            for (int i=0;i< PaltformCount; i++)
            {
                GameObject Paltform = Paltforms[Random.Range(0,Paltforms.Length)];
                Vector3 position = new Vector3(SpawnPosition.position.x+Random.Range(-1.9f,1.9f),SpawnPosition.position.y, SpawnPosition.position.z);
                if (position.x>=OldPosition.x+ 1|| position.x <= OldPosition.x - 1)
                {
                    Instantiate(Paltform, position, Quaternion.identity);
                    OldPosition = position;
                }
            }
            yield return new WaitForSeconds(SpawnDelay);

            for (int i = 0; i < TarpCount; i++)
            {
                int unitnum = Random.Range(0, Tarps.Length);
                if (unitnum == 0)
                {
                    GameObject Tarp = Tarps[unitnum];
                    Vector3 position = new Vector3(SpawnPosition.position.x + fanposition[Random.Range(0, 2)], SpawnPosition.position.y, SpawnPosition.position.z);
                    if (position.x == -2.35f)
                    {
                        Instantiate(Tarp, position, Quaternion.identity);
                        OldPosition = position;
                    }
                    else
                    {
                        Instantiate(Tarp, position, Quaternion.Euler(0, 180, 0));
                        OldPosition = position;
                    }
                }
                else
                {
                    GameObject Tarp = Tarps[unitnum];
                    Vector3 position = new Vector3(SpawnPosition.position.x + Random.Range(-1.9f, 1.9f), SpawnPosition.position.y, SpawnPosition.position.z);
                    if (position.x >= OldPosition.x + 1 || position.x <= OldPosition.x - 1)
                    {
                        Instantiate(Tarp, position, Quaternion.identity);
                        OldPosition = position;
                    }
                }
            }
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}
