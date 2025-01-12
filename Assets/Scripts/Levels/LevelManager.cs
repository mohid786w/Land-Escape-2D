using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] Text coinText;
    [SerializeField] Image healthImage;
    [SerializeField] HealthController adventurerHealth;
    [SerializeField] GameObject healthPrefab;
    [SerializeField] GameObject keyPrefab;
    [SerializeField] GameObject jumperPrefab;
    [SerializeField] GameObject keyIcon;
    [SerializeField] GameObject jumperIcon;

    [SerializeField] GameObject doorMid;  
    [SerializeField] GameObject doorTop; 
    [SerializeField] Sprite doorClosedMid; 
    [SerializeField] Sprite doorClosedTop; 
    [SerializeField] Sprite doorOpenMid; 
    [SerializeField] Sprite doorOpenTop;

    private int coins = 0;  
    private bool hasKey = false;  

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeCollectables();
        UpdateDoorState();
    }
    
    void InitializeCollectables()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");

        if (coins.Length > 0)
        {
            int keyIndex = Random.Range(0, coins.Length);
            int healthIndex, jumperIndex;

            do
            {
                healthIndex = Random.Range(0, coins.Length);
            } while (healthIndex == keyIndex);
            
            do
            {
                jumperIndex = Random.Range(0, coins.Length);
            } while (jumperIndex == keyIndex || jumperIndex == healthIndex);  // Ensure jumper is not placed in key or health spot
            
            GameObject randomKeyCoin = coins[keyIndex];
            GameObject keyObject = Instantiate(keyPrefab, randomKeyCoin.transform.position, Quaternion.identity);
            keyObject.SetActive(true);
            Destroy(randomKeyCoin);
    
            GameObject randomHealthCoin = coins[healthIndex];
            GameObject healthObject = Instantiate(healthPrefab, randomHealthCoin.transform.position, Quaternion.identity);
            healthObject.SetActive(true);
            Destroy(randomHealthCoin);
            
            GameObject randomJumperCoin = coins[jumperIndex];
            GameObject jumperObject = Instantiate(jumperPrefab, randomJumperCoin.transform.position, Quaternion.identity); 
            jumperObject.SetActive(true);
            Destroy(randomJumperCoin);
        }
        else
        {
            Debug.LogWarning("No coins found to replace with a key or health collectible.");
        }
    }
    
    void UpdateDoorState()
    {
        if (hasKey)
        {
            SoundManager.Instance.PlayDoorUnlockSound();
            doorMid.GetComponent<SpriteRenderer>().sprite = doorOpenMid;
            doorTop.GetComponent<SpriteRenderer>().sprite = doorOpenTop;
        }
        else
        {
            doorMid.GetComponent<SpriteRenderer>().sprite = doorClosedMid;
            doorTop.GetComponent<SpriteRenderer>().sprite = doorClosedTop;
        }
    }
    
    public void AddHealth(int health)
    {
        float h = (float)health / adventurerHealth.vitality;
        healthImage.fillAmount = h;
    }

    public void AddCoins(int amount)
    {
        SoundManager.Instance.PlayCoinCollectSound();
        coins += amount;
        coinText.text = coins.ToString();
    }

    public void AddKey()
    {
        hasKey = true;
        keyIcon.SetActive(true);
        UpdateDoorState(); 
    }

    public bool HasKey()
    {
        return hasKey;
    }    
    
    public void ShowJumperIcon()
    {
        jumperIcon.SetActive(true); 
    }

    public void HideJumperIcon()
    {
        jumperIcon.SetActive(false);
    }

    private void OnEnable()
    {
        adventurerHealth.HealthEvent += AddHealth;
    }

    private void OnDisable()
    {
        adventurerHealth.HealthEvent -= AddHealth;
    }
}
