using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    
    //public static UI_Controller Instance;

    private AudioSource _audioSource;
    
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private Slider _healthBar;
    /*[SerializeField] private Slider _honeyFiller;
    [SerializeField] private TextMeshProUGUI _honeyPercentage;*/
    [SerializeField] private TextMeshProUGUI _honeyNumber;
    [SerializeField] private GameObject _scroll;

    [SerializeField] private Image _healthFill;
    [SerializeField] private Image _healthBackground;

    [SerializeField] private GameObject player;
    /*private Image _honeyBackground;
    private Image _honeyAddition;
    private Image _honeyFill;
    private Image _honeyHandle;
    private TextMeshProUGUI _textHoney;*/

    [Header("Technical Variables")]
    private bool _menuOpened;
    private bool _hpFading;
    private bool _honeyFading;

  /*  private void Awake() {

        if(!Instance) Instance = this;

    }*/

    public bool HPFading { get => _hpFading; }
    public bool HoneyFading { get => _honeyFading; }
    public GameObject Scroll { get => _scroll; set => _scroll = value; }

    private void Start() {

        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(FadeHoney());
    }

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Escape)) ChangeMenuState();

        _healthBar.value = player.GetComponent<HP>().currentHp;
        _healthBar.maxValue = player.GetComponent<HP>().maxHp;
        //_honeyFiller.value = Mathf.Floor((((float)PlayerInformation.Instance.CollectedHoney) / ((float)PlayerInformation.Instance.MaxHoneyAmount)) * 100);
        //_honeyPercentage.text = _honeyFiller.value.ToString() + "%";
        //_honeyNumber.text =  player.GetComponent<CollectCoins>().coins.ToString() + " монет";

    }

    public void ChangeMenuState() {

        if(!_menuOpened) {
            Time.timeScale = 0f;
            _pauseUI.SetActive(true);
            _menuOpened = true;
            Cursor.visible = true;
            /*AudioHandler.Instance.MuteForMenu();
            MovingController.Instance.enabled = false;*/
        } else {
            Time.timeScale = 1f;
            _pauseUI.SetActive(false);
            _menuOpened = false;
            Cursor.visible = false;
            /*AudioHandler.Instance.Unmute();
            MovingController.Instance.enabled = true;*/
        }

    }

   /*public void ToMap() {

        if(GameInformation.Instance.LastUnlockedLevel < 1) return;

        SceneManager.LoadScene(1);
        Time.timeScale = 1f;

    }*/
   
   public void ToMenu () {

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }

   /* public void MakeClick() {

        _audioSource.PlayOneShot(AudioHandler.Instance.menuPress);

    }*/

    public IEnumerator FadeHP() {

        _hpFading = true;

        for(float alpha = 0; alpha < 1.5f; alpha += 0.04f) {
            _healthFill.color = new Color(_healthFill.color.r, _healthFill.color.g, _healthFill.color.b, alpha);
            _healthBackground.color = new Color(_healthBackground.color.r, _healthBackground.color.g, _healthBackground.color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(7);

        for(float alpha = 1; alpha > -0.5f; alpha -= 0.005f) {
            _healthFill.color = new Color(_healthFill.color.r, _healthFill.color.g, _healthFill.color.b, alpha);
            _healthBackground.color = new Color(_healthBackground.color.r, _healthBackground.color.g, _healthBackground.color.b, alpha);
            yield return null;
        }

        _hpFading = false;

    }

    public IEnumerator FadeHoney() {

        _honeyFading = true;

        for(float alpha = 0; alpha < 1.5f; alpha += 0.04f) {
            /*_honeyBackground.color = new Color(_honeyBackground.color.r, _honeyBackground.color.g, _honeyBackground.color.b, alpha);
            _honeyFill.color = new Color(_honeyFill.color.r, _honeyFill.color.g, _honeyFill.color.b, alpha);
            _honeyAddition.color = new Color(_honeyAddition.color.r, _honeyAddition.color.g, _honeyAddition.color.b, alpha);
            _honeyHandle.color = new Color(_honeyHandle.color.r, _honeyHandle.color.g, _honeyHandle.color.b, alpha);
            _textHoney.color = new Color(_textHoney.color.r, _textHoney.color.g, _textHoney.color.b, alpha);
            _honeyPercentage.color = new Color(_honeyPercentage.color.r, _honeyPercentage.color.g, _honeyPercentage.color.b, alpha);*/
            _honeyNumber.color = new Color(_honeyNumber.color.r, _honeyNumber.color.g, _honeyNumber.color.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(1);

        for(float alpha = 1; alpha > -0.5f; alpha -= 0.005f) {
            /*_honeyBackground.color = new Color(_honeyBackground.color.r, _honeyBackground.color.g, _honeyBackground.color.b, alpha);
            _honeyFill.color = new Color(_honeyFill.color.r, _honeyFill.color.g, _honeyFill.color.b, alpha);
            _honeyAddition.color = new Color(_honeyAddition.color.r, _honeyAddition.color.g, _honeyAddition.color.b, alpha);
            _honeyHandle.color = new Color(_honeyHandle.color.r, _honeyHandle.color.g, _honeyHandle.color.b, alpha);
            _textHoney.color = new Color(_textHoney.color.r, _textHoney.color.g, _textHoney.color.b, alpha);
            _honeyPercentage.color = new Color(_honeyPercentage.color.r, _honeyPercentage.color.g, _honeyPercentage.color.b, alpha);*/
            _honeyNumber.color = new Color(_honeyNumber.color.r, _honeyNumber.color.g, _honeyNumber.color.b, alpha);
            yield return null;
        }

        _honeyFading = false;

    }

}
