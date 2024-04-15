using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool startBossPhase = false;
    [SerializeField] private CinemachineFreeLook freeLookCam;
    [SerializeField] private GameObject toHideInBossPhase;
    [SerializeField] private GameObject BossHpBar;
    public GameObject EText;

    [Space]
    public GameObject endScreen;
    private Light directionalLight;
    private Camera cam;
    private bool started = false;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
        directionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
        cam = Camera.main;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startBossPhase && !started)
        {
            started = true;
            toHideInBossPhase.SetActive(false);
            directionalLight.color = new Color(255f / 255f, 0f / 255f, 0f / 255f);
            cam.backgroundColor = new Color(125f / 255f, 30f / 255f, 15f / 255f);
            freeLookCam.m_YAxis.Value = 0.5f;
            freeLookCam.m_YAxis.m_MaxSpeed = 0;
            AudioManager.instance.PlayBgm(1);
            Boss.instance.enabled = true;
            Boss.instance.transform.Find("Model").gameObject.SetActive(true);
            BossHpBar.SetActive(true);
        }
        else if (!startBossPhase)
        {
            toHideInBossPhase.SetActive(true);
            directionalLight.color = new Color(210f / 255f, 210f / 255f, 150f / 255f);
            cam.backgroundColor = new Color(134f / 255f, 238f / 255f, 255f / 255f);
            freeLookCam.m_YAxis.m_MaxSpeed = .5f;
            BossHpBar.SetActive(false);
        }
        if (EText.activeInHierarchy && Input.GetKeyDown(KeyCode.E) && !startBossPhase && !Boss.instance.isDead)
        {
            startBossPhase = true;
        }
    }
    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
