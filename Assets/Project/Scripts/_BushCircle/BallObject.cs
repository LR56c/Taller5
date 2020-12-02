using System;
using System.Collections;
using System.Collections.Generic;
using _BushCircle.MyScriptableObject;
using UnityEngine;
using UnityEngine.Events;

public class BallObject : MonoBehaviour
{
    [SerializeField] private ShakeLevel        _shakeLevel;
    public                   UnityEvent        OnBallObjectDestroy;
    public  bool         bFrozen;
    
    private GameObject        _fxPrefab;
    private AudioSource       fxSource;
    private BallObjectManager _ballObjectManager;
    private SpriteRenderer    _spriteRenderer;
    private Collider2D        _collider2D;
    
    private List<BallObject>  frozenG = new List<BallObject>();

    public SpriteRenderer SpriteRenderer
    {
        get => _spriteRenderer;
        set => _spriteRenderer = value;
    }

    private void Awake()
    {
        _ballObjectManager = GetComponentInParent<BallObjectManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _fxPrefab = transform.GetChild(0).gameObject;
        fxSource = _fxPrefab.GetComponent<AudioSource>();
        frozenG.Add(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.GetComponent<ProjectileBase>().IsLaunched) return;
        
        OneEffect();

        if(bFrozen)
        { 
            DeleteAllFriends();
        }
        else
        {
            DisableObject();
        }
    }
    
    public void DeleteAllFriends()
    {
        if(bFrozen)
        {
            for(int i = 0; i < frozenG.Count; i++)
            {
                frozenG[i]?.DisableObject();
            }
        }
        else
        {
            DisableObject();
        }
        
        OneEffect();
    }

    public void AddFrozenFriends(List<BallObject> frozenGroup)
    {
        frozenG = frozenGroup;
    }
    
    public void DisableObject()
    {
        _fxPrefab.SetActive(true);
        OnBallObjectDestroy.Invoke();
        _spriteRenderer.enabled = false;
        _collider2D.enabled = false;
    }

    private void OneEffect()
    {
        CinemachineShake.Instance.ShakeCamera(_shakeLevel.Intensity, _shakeLevel.Duration);
        fxSource.PlayOneShot(fxSource.clip);
    }
}
