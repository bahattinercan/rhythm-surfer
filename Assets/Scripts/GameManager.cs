using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public event EventHandler onGamePaused;
    public event EventHandler onGameResumed;

    public MaterialsSO materials;
    public ColorListSO colorList;
    public PrefabSO obsPrefabSO, cristalPrefabSO, colorChangePrefabSO;
    public ParticleSystem collectParticle;
    public GameObject player;
    private ParticleSystem playerBackParticle;
    public AudioClipSO audioClipSO;
    public float spawnDelay = .199f, spawnDistance = 92.85f;
    public EColor spawnColorType, antiColorType;
    public int score;
    private int maxGoal; // maxGoal * .4 -> 1 star | .6 -> 2 star | 1 -> 3 star
    private int[] goals = new int[3];
    public PrefabListSO collectParticleList;
    public AudioClip song;
    private SongListSO songList;
    public AudioSource audioSource;
    public TextMeshProUGUI scoreText;
    private float timer = 1;
    public float time = 1;
    private float changeColorTimer = 0;
    private float changeColorTime = 15f;
    public float bgCubeScaleMultiplier = 4;
    public float obsRotationAngle = 40;
    public int heath = 2;
    public int keys = 1;
    public int obsNormalValue = 4;
    public int cristalValue = 50;
    private KeyPanel keyPanel;
    private HpPanel hpPanel;
    public ScorePanel scorePanel;
    private ProgressSlider progressSlider;
    public EGameState eGameState;

    private void Awake()
    {
        MakeInstance();
        colorList = Resources.Load<ColorListSO>(typeof(ColorListSO).Name);
        collectParticleList = Resources.Load<PrefabListSO>("CollectParticle_" + typeof(PrefabListSO).Name);
        cristalPrefabSO = Resources.Load<PrefabSO>("Cristals_" + typeof(PrefabSO).Name);
        obsPrefabSO = Resources.Load<PrefabSO>("Obs_" + typeof(PrefabSO).Name);
        colorChangePrefabSO = Resources.Load<PrefabSO>("ColorChange_" + typeof(PrefabSO).Name);
        materials = Resources.Load<MaterialsSO>(typeof(MaterialsSO).Name);
        audioClipSO = Resources.Load<AudioClipSO>("Collect_" + typeof(AudioClipSO).Name);
        songList = Resources.Load<SongListSO>(typeof(SongListSO).Name);
        if (!PlayerPrefs.HasKey(Prefs.isStartSameMusic) || PlayerPrefs.GetInt(Prefs.isStartSameMusic) == 0)
        {
            songList.SetRandomClip();
        }
        else if (PlayerPrefs.GetInt(Prefs.isStartSameMusic) == 1)
        {            
            songList.SetClip(PlayerPrefs.GetInt(Prefs.startSameMusicIndex));
        }
        PlayerPrefs.SetInt(Prefs.isStartSameMusic, 0);
    }

    // Start is called before the first frame update
    private void Start()
    {
        eGameState = EGameState.play;
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerBackParticle = GameObject.Find("FX_TireSmoke_Mine").GetComponent<ParticleSystem>();
        spawnColorType = GetRandomColor();
        collectParticle = GameObject.Find("collectParticle").GetComponent<ParticleSystem>();
        ChangeGameColor(spawnColorType);
        ChangePlayerColor(spawnColorType);        
        scoreText = GameObject.Find("scoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
        audioSource = GetComponent<AudioSource>();
        keyPanel = GameObject.Find("keyPanel").GetComponent<KeyPanel>();
        hpPanel = GameObject.Find("hpPanel").GetComponent<HpPanel>();
        hpPanel.UpdateHp(heath);
        progressSlider = GameObject.Find("progressSlider").GetComponent<ProgressSlider>();
        progressSlider.UpdateSlider(score);
        CalculateGoals();
        InvokeRepeating("DecreaseTheSpawnDelay", 60f, 60f);
    }

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += 1;
            time++;
            changeColorTimer++;
        }
        CheckAndSpawnChangeColor();
    }

    private void CalculateGoals()
    {
        goals[0] = (maxGoal * 40) / 100;
        goals[1] = (maxGoal * 60) / 100;
        goals[2] = maxGoal;
    }

    public int GetScoreStar()
    {
        int star = 0;
        for (int i = 0; i < goals.Length; i++)
        {
            if (score > goals[i])
                star = i + 1;
        }
        return star;
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        progressSlider.UpdateSlider(score);
    }

    public void PlayCollectParticle()
    {
        collectParticle.Play();        
    }

    public void PlayCollectSound()
    {
        audioSource.clip = audioClipSO.clip;
        audioSource.Play();
    }

    private void DecreaseTheSpawnDelay()
    {
        if(spawnDelay>.1f)
            spawnDelay -= .1f;

        obsRotationAngle -= 3;
    }

    #region Color Functions

    private void CheckAndSpawnChangeColor()
    {
        if (changeColorTimer == changeColorTime)
        {
            changeColorTimer = 0;
            EColor randomColorType = spawnColorType;
            do
            {
                randomColorType = GetRandomColor();
            } while (spawnColorType == randomColorType);
            spawnColorType = randomColorType;
            GameObject go = Instantiate(colorChangePrefabSO.prefab,
                new Vector3(0, 0, GameObject.FindGameObjectWithTag(Tags.playerMovement).transform.position.z + spawnDistance),
                Quaternion.identity);
            Color color = materials.GetMaterial(spawnColorType).color;
            color = new Color(color.r, color.g, color.b, 0.5f);
            go.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
            go.transform.GetChild(0).GetComponent<ChangeColorObs>().eColor = spawnColorType;
            ChangeGameColor(spawnColorType);
        }
    }

    public EColor GetRandomColor()
    {
        return (EColor)UnityEngine.Random.Range(0, 4);
    }

    public void ChangePlayerColor(EColor eColor)
    {
        player.GetComponent<PlayerColor>().color = eColor;
        player.GetComponent<MeshRenderer>().material = materials.GetMaterial(spawnColorType);
        var backParticleMain = playerBackParticle.main;
        backParticleMain.startColor = materials.GetMaterial(eColor).color;
        var collectMain = collectParticle.main;
        collectMain.startColor = materials.GetMaterial(eColor).color;

    }

    public void ChangeGameColor(EColor eColor)
    {
        antiColorType = eColor;
        do
        {
            antiColorType = GetRandomColor();
        } while (spawnColorType == antiColorType);

        /*
        // change background color
        GameObject[] cubes = GameObject.FindGameObjectsWithTag(Tags.backgroundCube);
        foreach (GameObject cube in cubes)
        {
            cube.GetComponent<MeshRenderer>().material.color = materials.GetMaterial(spawnColorType).color;
        }
        // background resminin rengini deðiþtir
        */
        // spawnlanacak küplerin rengini deðiþtir
        SpawnerWithAudio spawner = GameObject.FindGameObjectWithTag(Tags.audioSpawner).GetComponent<SpawnerWithAudio>();
        spawner.goodMat = materials.GetMaterial(spawnColorType);
        spawner.badMat = materials.GetMaterial(antiColorType);
    }

    #endregion Color Functions

    #region Base Functions

    private void MakeInstance()
    {
        instance = this;
        /*
        if (instance == null)
        {
            
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        */
    }

    public void PauseGame()
    {
        eGameState = EGameState.pause;
        Time.timeScale = 0;
        onGamePaused?.Invoke(this, EventArgs.Empty);
    }

    public void ResumeGame()
    {
        eGameState = EGameState.play;
        Time.timeScale = 1;
        onGameResumed?.Invoke(this, EventArgs.Empty);
    }

    public void RandomStartGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt(Prefs.isStartSameMusic, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt(Prefs.isStartSameMusic, 1);
        PlayerPrefs.SetInt(Prefs.startSameMusicIndex, songList.clipIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Fail()
    {
        /*
        --heath;
        hpPanel.UpdateHp(heath);
        if (heath < 0)
        {
            GameOver();
        }
        */
    }

    public void GameOver()
    {
        PauseGame();
        eGameState = EGameState.gameover;
        scorePanel.UpdateScore();
    }

    public void Win()
    {
        PauseGame();
        eGameState = EGameState.win;
        scorePanel.UpdateScore();        
    }

    #endregion Base Functions
}