using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public Game game;
    public Button mainButton;
    public SpriteRenderer bg;
    public Sprite[] sprites;
    public Transform[] movableObjects;
    public float sceneMoveDuration = 0.5f;
    public float sceneStepY = 1.0f;

    public Transform gameScene;
    public Transform spawnMover;
    public Transform spawnPoint;
    public GameObject blockPrefab;
    public GameObject fakePrefab;

    public List<GameObject> blocks = new List<GameObject>();

    public bool canPress;
    private bool canMove;
    public bool gameStarted;

    private bool canIncreasSpeed = true;

    public float speed = 2f;

    public float leftLimit = -5f;
    public float rightLimit = 5f;

    private int direction = 1;


    private List<Vector3> startMovableObjectPos = new();

    private void Start()
    {
        foreach (Transform t in movableObjects)
        {
            startMovableObjectPos.Add(t.position);
        }

        mainButton.onClick.AddListener(MainButtonPressed);
    }

    private void OnDestroy()
    {
        mainButton.onClick.RemoveListener(MainButtonPressed);
    }
    public void CleanGame()
    {
        StopAllCoroutines();
        fakePrefab.SetActive(true);
        canPress = false;
        canMove = false;    
        gameStarted = false;
        canIncreasSpeed = false;

        for (int i = 0;i <  movableObjects.Length; i++)
        {
            movableObjects[i].transform.position = startMovableObjectPos[i];
        }


        foreach(var block in blocks)
        {
            Destroy(block.gameObject);
        }
        blocks.Clear();

        speed = 2;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        canPress = true;
        canMove = true;
        canIncreasSpeed = true;
        gameStarted = true;

        bg.sprite = sprites[PlayerPrefs.GetInt("CurrentBG")];
    }

    private void Update()
    {
        if (!gameStarted) return;

        if(blocks.Count > 0)
            CheckLoseCondition(blocks[^1]);

        if (!canMove) return;

        MoveSpawnPoint();
    }
    private void CheckLoseCondition(GameObject block)
    {
        float blockY = block.transform.position.y;
        float cameraY = Camera.main.transform.position.y;

        if (blockY < cameraY -25)
        { 
            Lose();
        }
    }

    private void Lose()
    {
        Time.timeScale = 0;




        int newScore = PlayerPrefs.GetInt("Score");
        newScore += Mathf.RoundToInt(blocks.Count * speed);
        PlayerPrefs.SetInt("Score", newScore);
        PlayerPrefs.Save();
        game.SetText();

        WinGame winGame = (WinGame) UIManager.Instance.GetPopup(PopupTypes.WinGame);
        winGame.SetText(Mathf.RoundToInt(blocks.Count * speed));
        UIManager.Instance.ShowPopup(PopupTypes.WinGame);
        gameStarted = false;
    }
    private void MoveSpawnPoint()
    {

        spawnMover.transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        if (spawnMover.transform.position.x >= rightLimit)
        {
            direction = -1;
        }
        else if (spawnMover.transform.position.x <= leftLimit)
        {
            direction = 1;
        }
    }

    private void MainButtonPressed()
    {
        if (!canPress) return;
        StartCoroutine(DropBlock());
    }
    private IEnumerator DropBlock()
    {
        if (speed < 100 && canIncreasSpeed)
        {
            speed += 2f;
        }
        canPress = false;
        fakePrefab.SetActive(false);
        GameObject newBlock = Instantiate(blockPrefab, gameScene);
        newBlock.transform.position = spawnPoint.transform.position;
        blocks.Add(newBlock);

        yield return new WaitForSeconds(2);
        canMove = false;
        StartCoroutine(MoveBlocksUp());
        fakePrefab.SetActive(true);
        canPress = true;

        string achievekey = "Achieve" + 0;
        if (blocks.Count >= 30 && !PlayerPrefs.HasKey(achievekey))
        {

            PlayerPrefs.SetInt(achievekey, 1);
        }


        achievekey = "Achieve" + 1;
        if (blocks.Count > 0 && !PlayerPrefs.HasKey(achievekey))
        {
            PlayerPrefs.SetInt(achievekey, 1);
        }
    }

    private IEnumerator MoveBlocksUp()
    {
        float elapsed = 0f;
        float duration = sceneMoveDuration;

        Vector3[] startPositions = new Vector3[movableObjects.Length];
        Vector3[] targetPositions = new Vector3[movableObjects.Length];

        for (int i = 0; i < movableObjects.Length; i++)
        {
            if (movableObjects[i] != null)
            {
                startPositions[i] = movableObjects[i].position;
                targetPositions[i] = startPositions[i] + new Vector3(0, sceneStepY, 0);
            }
        }

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            for (int i = 0; i < movableObjects.Length; i++)
            {
                if (movableObjects[i] != null)
                {
                    movableObjects[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
                }
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < movableObjects.Length; i++)
        {
            if (movableObjects[i] != null)
            {
                movableObjects[i].position = targetPositions[i];
            }
        }

        canMove = true;
    }

    public void Freez()
    {
        if (blocks.Count == 0) return;
        int freezes = PlayerPrefs.GetInt("Freez");
        if (freezes > 0)
        {
            freezes--;
            PlayerPrefs.SetInt("Freez", freezes);
            game.SetText();
            StartCoroutine(FreezTime());
        }
    }

    private IEnumerator FreezTime()
    {
        List<GameObject> freezedBlocks = new List<GameObject>(blocks); 

        foreach (GameObject block in freezedBlocks)
        {
            Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static; 
            }
        }

        yield return new WaitForSeconds(10f);

        foreach (GameObject block in freezedBlocks)
        {
            Rigidbody2D rb = block.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
    public void SlowMo()
    {
        if (blocks.Count == 0) return;
        int slowmo = PlayerPrefs.GetInt("Slowmo");
        if (slowmo > 0)
        {
            slowmo--;
            PlayerPrefs.SetInt("Slowmo", slowmo);
            game.SetText();
            StartCoroutine(SlowMoTime());
        }
    }
    private IEnumerator SlowMoTime()
    {
        canIncreasSpeed = false;    
        float prevSpeed = speed;
        speed = 2;
        yield return new WaitForSeconds(5);

        speed = prevSpeed;
        canIncreasSpeed = true;
    }
    public void Magnet()
    {
        if (blocks.Count == 0) return;

        int magnet = PlayerPrefs.GetInt("Magnet");
        if (magnet > 0)
        {
            magnet--;
            PlayerPrefs.SetInt("Magnet", magnet);
            game.SetText();


            StartCoroutine(DropBlock());

            GameObject lastBlock = blocks[blocks.Count - 1];
            GameObject preLastBlock = blocks[blocks.Count - 2];
            lastBlock.transform.position = preLastBlock.GetComponent<BlockController>().upPoint.position;

        }
    }
}