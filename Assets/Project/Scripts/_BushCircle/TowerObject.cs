using System;
using System.Collections;
using System.Collections.Generic;
using _BushCircle.MyScriptableObject;
using UnityEngine;

public enum ETowersType {CRUZ_TOWER, CIRCLE_TOWER, BOUNCE_TOWER , FREEZE_TOWER, X_TOWER, NONE}
public enum ETowerState {NONE, FROZEN, EXPLOTED}
public enum ETowerColliderCount {ONE, TWO}

public class TowerObject : MonoBehaviour
{
    private Vector3                   _myCenter = Vector2.zero;
    private Vector2                   _vSize1   = Vector2.zero;
    private Vector2                   _vSize2   = Vector2.zero;
    private float                     _angle    = 0f;
    private Animator                  _animator;
    private GameObject                _fxPrefab;
    private AudioSource               _fxSource;
    private ParticleSystem.MainModule p_main;
    
    private Collider2D[] touchedObjects;
    private Collider2D[] touchedObjectsExtra;
    
    private List<TowerObject> towerObjectsList = new List<TowerObject>();
    private List<BallObject> ballObjectsList = new List<BallObject>();

    public List<BallObject> BallObjectsList {get => ballObjectsList; set => ballObjectsList = value;}
    
    [SerializeField] private ShakeLevel          _shakeLevel;
    
    public                   ETowersType         TowerType  = ETowersType.NONE;
    public                   ETowerState         TowerState = ETowerState.NONE;
    public                   ETowerColliderCount TowerColliderCount;
    
    public                   float               TweenDuration = 1f;

    // magenta = error
    public Color FXColor     = Color.magenta;
    public Color TargetColor = Color.magenta;
    
    public void ApplyEffect(Collision2D other = null)
    {
        switch(TowerType)
        {
            case ETowersType.BOUNCE_TOWER: FXEffect(); return;
            
            case ETowersType.FREEZE_TOWER:
                FreezeTower(); break;
            
            default: RouteTowerState(); break;
        }

        DestroyProjectile(other);
        FXEffect();
    }

    private void Awake()
    {
        _myCenter = transform.position;
        _fxPrefab = transform.GetChild(0).gameObject;
        _fxSource = _fxPrefab.GetComponent<AudioSource>();
        p_main = _fxPrefab.GetComponent<ParticleSystem>().main;
        p_main.startColor = FXColor;
        TowerTypeSetup();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.gameObject.GetComponent<ProjectileBase>().IsLaunched) return;
        
        CinemachineShake.Instance.ShakeCamera(_shakeLevel.Intensity,_shakeLevel.Duration);
        ApplyEffect(other);
    }
    
    private void FXEffect()
    {
        _fxPrefab.SetActive(true);
        
        _fxSource.PlayOneShot(_fxSource.clip);
    }

    private void TowerTypeSetup()
    {
        switch(TowerType)
        {
            case ETowersType.CRUZ_TOWER:
                TowerColliderCount = ETowerColliderCount.TWO;
                _vSize1 = new Vector2(4f,  .5f);
                _vSize2 = new Vector2(.5f, 4f);
                break;
            
            case ETowersType.CIRCLE_TOWER:
                TowerColliderCount = ETowerColliderCount.ONE;
                _vSize1 = new Vector2(2f, 2f);
                break;
            
            case ETowersType.BOUNCE_TOWER: break;
            
            case ETowersType.FREEZE_TOWER:
                TowerColliderCount = ETowerColliderCount.ONE;
                _vSize1 = new Vector2(2f, 2f);
                _animator = GetComponent<Animator>();
                break;
            
            case ETowersType.X_TOWER:
                TowerColliderCount = ETowerColliderCount.TWO;
                _vSize1 = new Vector2(4f, .5f);
                _vSize2 = new Vector2(4f, .5f);
                _angle = 45f;
                break;
            
            case ETowersType.NONE: Debug.LogError("TowerType no seleccionado"); break;
            default:               Debug.LogError("TowerType no encontrado"); break;
        }
    }
    
    private void RouteTowerState()
     {
         switch(TowerState)
         {
             case ETowerState.NONE:
                 GlobalStepOne(); break;
             
             case ETowerState.FROZEN: break;
             
             case ETowerState.EXPLOTED: break;
         }
     }
     
     private void GlobalStepOne()
     {
         TowerState = ETowerState.EXPLOTED;
         
         switch(TowerColliderCount)
         {
             case ETowerColliderCount.ONE:
                 SingleArray(false); break;
             
             case ETowerColliderCount.TWO:
                 DoubleArray(); break;
         }
     }
     
     private void SingleArray(bool addToList)
     {
         touchedObjects = Physics2D.OverlapBoxAll(_myCenter, _vSize1, _angle);
         
         for(int i = 0; i < touchedObjects.Length; i++)
         {
             var ballObject = touchedObjects[i].GetComponent<BallObject>();
             var towerObject = touchedObjects[i].GetComponent<TowerObject>();
             ApplyColor(ballObject, towerObject, addToList);
         }
     }

     private void DoubleArray()
     {
         var xAngle = CheckTowerType(ETowersType.X_TOWER) ? -_angle : _angle ;
         
         touchedObjects = Physics2D.OverlapBoxAll(_myCenter,      _vSize1, _angle);
         touchedObjectsExtra = Physics2D.OverlapBoxAll(_myCenter, _vSize2, xAngle);
 
         for(int i = 0; i < touchedObjects.Length; i++)
         {
             var ballObject = touchedObjects[i].GetComponent<BallObject>();
             var towerObject = touchedObjects[i].GetComponent<TowerObject>();
             ApplyColor(ballObject, towerObject, false);
         }
        
         for(int i = 0; i < touchedObjectsExtra.Length; i++)
         {
             var ballObject = touchedObjectsExtra[i].GetComponent<BallObject>();
             var towerObject = touchedObjectsExtra[i].GetComponent<TowerObject>();
             ApplyColor(ballObject, towerObject, false);
         }
     }
     
     private bool CheckTowerType(ETowersType param1) => TowerType == param1;
     
     private void ApplyColor(BallObject ballObject, TowerObject nearbyTowerObject , bool addToList)
     {
         if(ballObject)
         {
             if(addToList)
             {
                 ballObjectsList.Add(ballObject);
                 
                 /*ballObject.SpriteRenderer.DOColor(TargetColor, TweenDuration).OnComplete(() =>
                 {
                     ballObject.bFrozen = true;
                 });*/
                 
                 LeanTween.color(ballObject.gameObject, TargetColor, TweenDuration).setOnComplete((() =>
                 {
                     ballObject.bFrozen = true;
                 }));
             }
             else
             {
                 //ballObject.SpriteRenderer.DOColor(TargetColor, TweenDuration).OnComplete((ballObject.DeleteAllFriends));
                 LeanTween.color(ballObject.gameObject, TargetColor, TweenDuration).setOnComplete((ballObject.DeleteAllFriends));
             }
         }
         
         if(nearbyTowerObject)
         {
             if(addToList)
             {
                 if(nearbyTowerObject.GetInstanceID() != this.GetInstanceID())
                 {
                     towerObjectsList.Add(nearbyTowerObject);
                 }
             }
             else
             {
                 ApplyEffectIfIsDifferentTower(nearbyTowerObject);
             }
         }
     }
     
     private void FreezeTower()
     {
         //la misma freezeTower actua como router porque es unica
         switch(TowerState)
         {
             case ETowerState.NONE:
                 FreezeStepOne(); break;
             
             case ETowerState.FROZEN:
                 FreezeStepTwo(); break;
             
             case ETowerState.EXPLOTED: break;
         }
     }
     
     private void FreezeStepOne()
     {
         TowerState = ETowerState.FROZEN;
         _animator.SetBool("Frozen", true);
         SingleArray(true);
         CombineBothBallsInTowers();
         InjectBallsToFriends();
     }

     public void FreezeStepTwo()
     {
         TowerState = ETowerState.EXPLOTED;
         _animator.SetBool("Frozen", false);
         ballObjectsList[0]?.DeleteAllFriends();
         CheckTowerList();
     }

     private void CheckTowerList()
     {
         for(int j = 0; j < towerObjectsList.Count; j++)
         {
             var tower = towerObjectsList[j];
             ApplyEffectIfIsDifferentTower(tower);
         }
     }
     
     private void ApplyEffectIfIsDifferentTower(TowerObject towerObject)
     {
         if(towerObject)
         {
             if(towerObject.GetInstanceID() != this.GetInstanceID())
             {
                 switch(towerObject.TowerState)
                 {
                     case ETowerState.NONE:
                         towerObject.ApplyEffect();
                         break;
                     
                     case ETowerState.FROZEN:
                         towerObject.FreezeStepTwo();
                         break;
                     
                     case ETowerState.EXPLOTED: break;
                 }
             }
         }
     }
     
     private void InjectBallsToFriends()
     {
         //inyecta la lista de compañeros a cada pelotita
         for(int i = 0; i < ballObjectsList.Count; i++)
         {
             ballObjectsList[i].AddFrozenFriends(ballObjectsList);
         }
     }
    
     private void CombineBothBallsInTowers()
     {
         for(int i = 0; i < towerObjectsList.Count; i++)
         {
             var tower = towerObjectsList[i];
             if(tower) CheckNearbyTowerIsFrozen(tower);
         }
     }
     
     private void CheckNearbyTowerIsFrozen(TowerObject nearbyTower)
     {
         if(nearbyTower.TowerState == ETowerState.FROZEN)
         {
             for(var j = 0; j < ballObjectsList.Count; j++)
             {
                 var ball = ballObjectsList[j];
                 AddMyBallsToNearbyTower(ball, nearbyTower);
             }

             for(int k = 0; k < nearbyTower.BallObjectsList.Count; k++)
             {
                 var ballInTower = nearbyTower.BallObjectsList[k];
                 AddNearbyTowerBallsToThis(ballInTower);
             }
         }
     }

     private void AddNearbyTowerBallsToThis(BallObject ballInTower)
     {
         if(!this.ballObjectsList.Contains(ballInTower))
         {
             //si no tengo sus bolitas, las agrego a mi mismo
             ballObjectsList.Add(ballInTower);
         }
     }

     private void AddMyBallsToNearbyTower(BallObject myBallObject, TowerObject nearbyTower)
     {
         if(!nearbyTower.BallObjectsList.Contains(myBallObject))
         {
             //si no contiene mis bolitas, las agrega a la lista de la torre cercana
             nearbyTower.BallObjectsList.Add(myBallObject);
         }
     }

     private void DestroyProjectile(Collision2D other)
     {
         if(other != null)
         {
             Destroy(other.gameObject);
         }
     }
}
