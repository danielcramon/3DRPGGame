using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Image[] hearts;
    public Sprite fullHeartsSprite;
    public Sprite halfHeartsSprite;
    public Sprite emptyHeartsSprite;

    private bool addedHalfHeart;


    public PlayerController thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRendere;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deathEffect;

    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        respawnPoint = thePlayer.transform.position;

        addedHalfHeart = false;

        UpdateHearts();
    }

    // Update is called once per frame
    void Update()
    {

        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRendere.enabled = !playerRendere.enabled;
                flashCounter = flashLength;
            }
        }
        else
        {
            if (!playerRendere.enabled)
            {
                playerRendere.enabled = true;
            }
        }

        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }
        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
        
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < (currentHealth / 2))
            {
                hearts[i].sprite = fullHeartsSprite;
            }
            else
            {
                if (currentHealth % 2 == 1 && addedHalfHeart == false)
                {
                    hearts[i].sprite = halfHeartsSprite;
                    addedHalfHeart = true;
                }
                else
                {
                    hearts[i].sprite = emptyHeartsSprite;
                }
            }
            if (i < maxHealth / 2)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {

            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                UpdateHearts();
                Respawn();
            }
            else
            {


                thePlayer.KnockBack(direction);

                invincibilityCounter = invincibilityLength;

                playerRendere.enabled = false;

                flashCounter = flashLength;

                addedHalfHeart = false;

                UpdateHearts();
            }
        }
    }

    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }

    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;
        isFadeFromBlack = true;

        isRespawning = false;

        thePlayer.gameObject.SetActive(true);
        GameObject player = GameObject.Find("Player");
        CharacterController charController = player.GetComponent<CharacterController>();
        charController.enabled = false;
        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;
        charController.enabled = true;

        invincibilityCounter = invincibilityLength;
        playerRendere.enabled = false;
        flashCounter = flashLength;
        addedHalfHeart = false;
        UpdateHearts();

    }

    public void HealPlayer(int healAmmount)
    {
        currentHealth += healAmmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        addedHalfHeart = false;
        UpdateHearts();
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
