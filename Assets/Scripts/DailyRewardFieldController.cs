using UnityEngine;
using UnityEngine.UI;

using TMPro;

// This controller may change further
// Most likely by adding some particle systems to claiming, defining reward type and generally altering deactivation/claim logic
public class DailyRewardFieldController : MonoBehaviour
{
    private Button _button;
    private Animator _animator;
    private Image _image;

    private TMP_Text _text;

    private Vector3 startingScale;

    public Sprite higlightedSprite;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        startingScale = transform.localScale;
    }

    public void Highlight()
    {
        _animator.enabled = true;
        _image.sprite = higlightedSprite;
    }

    public void RewardDeactivate()
    {
        _image.sprite = higlightedSprite;
        _button.interactable = false;
        _text.text = "";
    }

    public void RewardClaimedAction()
    {
        _animator.enabled = false;
        transform.localScale = startingScale;

        _button.interactable = false;
        _text.text = "";
    }
}
