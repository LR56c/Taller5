using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using Taller;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRoundOverSubCanvas : UISubCanvas
{
    public bool bClicked;
    public bool bCanChange;

    [SerializeField]             private GameObject _starsContainer;
    [SerializeField] private TextMeshProUGUI          levelCompleteText;
    [SerializeField] private TextMeshProUGUI          dragsText;
    
    
    [SerializeField] private Image[]     _starsImages;
    private                  AudioSource source;

    
    private int _levelSceneIndex = 0;
    
    private Dictionary<string, int>  MyStarsLevelsUnlocked = new Dictionary<string, int>();

    protected override void Awake()
    {
        _starsImages = _starsContainer.GetComponentsInChildren<Image>();
        //_levelSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        _levelSceneIndex = SceneManager.GetActiveScene().buildIndex;
        source = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();
        for(int i = 0; i < _starsImages.Length; i++)
        {
            _starsImages[i].gameObject.SetActive(false);
        }
        
        if(ES3.KeyExists("StarsLevelsUnlocked"))
        {
            MyStarsLevelsUnlocked = ES3.Load<Dictionary<string, int>>("StarsLevelsUnlocked");
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LeanTouch.OnFingerDown += OnFingerDown;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        LeanTouch.OnFingerDown -= OnFingerDown;
    }
    
    private void OnFingerDown(LeanFinger obj)
    {
        if(obj.IsOverGui) return;
        RoundOver();
    }
    
    private void RoundOver()
    {
        if(!bCanChange) return;
        if (bClicked) return;
        bClicked = true;

        GameManager.Instance.LoadNextLevel();
    }
    
    protected override void GameManager_OnGameStateChange(EGameStates NewGameState)
    {
        switch (NewGameState)
        {
            case EGameStates.ROUND_OVER:
                ShowContainer();
                ChangeTexts();
                ShowStars();
                source.PlayOneShot(source.clip);
                bCanChange = true;
                break;

            default: HideContainer(); break;
        }
    }

    private void ChangeTexts()
    {
        string levelText = $"Level {_levelSceneIndex}\nComplete!";
        
        levelCompleteText.SetText(levelText);
        
        string dragCount = GameManager.Instance.DragCount.ToString();
        
        string dragText = $"Launched\nObjects: {dragCount}";
        
        dragsText.SetText(dragText);
    }

    private void ShowStars()
    {
        int currentDragCount = GameManager.Instance.DragCount;
        int[] stars = GameManager.Instance.Stars;
        int counter = 0;
        for(int i = 0; i < stars.Length; i++)
        {
            if(stars[i] >= currentDragCount)
            {
                counter++;
                _starsImages[i].gameObject.SetActive(true);
            }
        }
        
        var actualCounter = MyStarsLevelsUnlocked[$"Level{_levelSceneIndex}"];
        if(actualCounter < counter)
        {
            if(MyStarsLevelsUnlocked.ContainsKey($"Level{_levelSceneIndex}"))
            {
                MyStarsLevelsUnlocked.Remove($"Level{_levelSceneIndex}");
            }
        
            MyStarsLevelsUnlocked.Add($"Level{_levelSceneIndex}", counter);
        
            ES3.Save("StarsLevelsUnlocked", MyStarsLevelsUnlocked);
        }
    }
}