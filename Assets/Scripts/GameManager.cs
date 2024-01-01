using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform SphereParent;

    public AudioSource audiosource;
    public AudioSource BGaudiosource;
    public AudioClip Greenscolliedaudio;
    public AudioClip Redscolliedaudio;
    public AudioClip GameOveraudio;

   
    public GameObject GameOverPanel;
    public GameObject GreenSpherePrefab;
    public GameObject RedSpherePrefab;
    public GameObject BOXPrefab;
    public GameObject Loading;

    public Text LoadingTxt;
    public Text Timertxt;
    public Text ScoreText;
    public Text BestScore;

    public float Score;
    public int numberOfSpheres;
 
    private float timerDuration = 120.0f; 
    private float timer;
    public Image StarFillImage;

    public bool GamePlay = true;
    private void Awake()
    {
        Instance = this;
    }
   

    private void Update()
    {
        if ((timer <= 0 || Score == 50 )&&GamePlay)
        {
            Handheld.Vibrate();
            Timertxt.text = "GameOver";
            GameOverPanel.SetActive(true);
            StarFillImage.fillAmount = Mathf.Clamp01(Score / 50.0f);
            GamePlay = false;
            BGaudiosource.volume = 0;
            GameManager.Instance.audiosource.PlayOneShot(GameOveraudio);
            if (PlayerPrefs.GetInt("Score") != null)
            {
                BestScore.text = "BestScore: " + PlayerPrefs.GetInt("Score").ToString();

            }
            else
            {
                BestScore.text = Score.ToString();
            }
        }
        

    }

 
    public void Restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Start()
    {
        InstantiateSpheres();
        Loading.SetActive(true);
        timer = timerDuration;

      StartCoroutine(LoadingCountdownCoroutine());

    }
    IEnumerator LoadingCountdownCoroutine()
    {
       

        float Timer = 7;

        while (Timer > 0)
        {
            Timer -= Time.deltaTime;
            LoadingTxt.text = "Loading... Let me cook [" + Mathf.Ceil(Timer).ToString() + "]";
            yield return null;
        }

        Loading.SetActive(false);
        StartCoroutine(CountdownTimer());

    }
    IEnumerator CountdownTimer()
    {
        

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerText();
                yield return null;
            }
        

       
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        Timertxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void InstantiateSpheres()
    {
        

            for (int i = 0; i < numberOfSpheres; i++)
            {
                InstantiateSphere(GreenSpherePrefab);
                InstantiateSphere(RedSpherePrefab);
            }
            for (int i = 0; i < numberOfSpheres / 10; i++)
            {
                InstantiateSphere(BOXPrefab);
            }

        
            
    }


    void InstantiateSphere(GameObject prefab)
    {
        Vector3 randomPosition = GetRandomPositionWithinNavMesh();
        GameObject sphere = Instantiate(prefab, randomPosition, Quaternion.identity);
        sphere.transform.SetParent(SphereParent);
    }

    Vector3 GetRandomPositionWithinNavMesh()
    {
       
            NavMeshHit hit;
            Vector3 randomPoint;

            do
            {
                randomPoint = Random.insideUnitSphere * 350f;
                randomPoint.y = 0;

            } while (!NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas));

            return hit.position;
        
    }
}
