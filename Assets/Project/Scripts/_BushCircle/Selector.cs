using System;
using System.Collections.Generic;
using _BushCircle.UI;
using DG.Tweening;
using Taller;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _BushCircle
{
    public class Selector : MonoBehaviour
    {
        /*private int _levelSceneIndex = 0;
        private int _projectSceneCount = 0;*/

        public GameObject   ContainerMaster;
        
        public List<GameObject>  PagesLevelsContainer;
        public List<CanvasGroup> PagesCanvasGroups;

        public List<GameObject> Levels;

        public List<GameObject> StarsContainers;
        
        public List<Button> Buttons;

        private Dictionary<string, bool> MyLevelsUnlocked      = new Dictionary<string, bool>();
        private Dictionary<string, int>  MyStarsLevelsUnlocked = new Dictionary<string, int>();

        public int _projectSceneCount;

        public TextMeshProUGUI pageCounter;
        public GameObject      starTotalContainer;
        public TextMeshProUGUI starTotalText;

        public int pageIndex = 0;
        
        private void Start()
        {
            GetInitialComponents();
            
            Inizialize();
            ReOrder();
            Setup();
            
            StarCounter();
            GetCanvasGroupPages();
            HideInitialCanvasGroup();
            UpdateArrow();
            //DebugSaves();
        }

        public void ChangeArrow(int dir)
        {
            pageIndex += dir;
        }

        public void UpdateArrow()
        {
            CheckLimitPage();
            FadeCanvasGroups();
            UpdatePageNumberText();
        }

        private void CheckLimitPage()
        {
            const int pageIndexMin = 0;
            const int pageIndexMax = 2;

            
            if(pageIndex > pageIndexMax)
            {
                pageIndex = 2;
            }
            else if(pageIndex < pageIndexMin)
            {
                pageIndex = 0;
            }
        }

        private void FadeCanvasGroups()
        {
            for(int i = 0; i < PagesCanvasGroups.Count; i++)
            {
                if(i == pageIndex)
                {
                    PagesCanvasGroups[i].DOFade(1f, .25f);
                    PagesCanvasGroups[i].blocksRaycasts = true;
                }
                else
                {
                    PagesCanvasGroups[i].DOFade(0f, .25f);
                    PagesCanvasGroups[i].blocksRaycasts = false;
                }
            }
        }

        private void UpdatePageNumberText()
        {
            int totalPages = PagesLevelsContainer.Count;
            string pageCounterText = $"Page\n{pageIndex + 1} / {totalPages}"; 
            pageCounter.SetText(pageCounterText);
        }

        private void GetInitialComponents()
        {
            pageCounter = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            //arrowContainer = transform.GetChild(2).gameObject;
            
            starTotalContainer = transform.GetChild(2).gameObject;
            starTotalText = starTotalContainer.GetComponentInChildren<TextMeshProUGUI>();
            
            _projectSceneCount = SceneManager.sceneCountInBuildSettings - 1;

            MyLevelsUnlocked = ES3.Load<Dictionary<string, bool>>("LevelsUnlocked");
            MyStarsLevelsUnlocked = ES3.Load<Dictionary<string, int>>("StarsLevelsUnlocked");
        }

        private void GetCanvasGroupPages()
        {
            for(int i = 0; i < PagesLevelsContainer.Count; i++)
            {
                var canvasGroup = PagesLevelsContainer[i].GetComponent<CanvasGroup>();
                PagesCanvasGroups.Add(canvasGroup);
            }
        }

        private void HideInitialCanvasGroup()
        {
            for(int i = 1; i < PagesCanvasGroups.Count; i++)
            {
                PagesCanvasGroups[i].alpha = 0f;
            }
        }
        
        private void StarCounter()
        {
            int countSaved = 0;
            for(int i = 0; i < MyStarsLevelsUnlocked.Count; i++)
            {
                var count = MyStarsLevelsUnlocked[$"Level{i + 1}"];
                countSaved += count;
            }

            int starTotal = _projectSceneCount * 3;

            string starText = $"{countSaved} / {starTotal}";
            starTotalText.SetText(starText);
        }
        
        private void DebugSaves()
        {
            for(int i = 1; i < MyLevelsUnlocked.Count; i++)
            {
                var msj = MyLevelsUnlocked[$"Level{i}"];
                //Debug.Log($"Level {i}: {msj}");
            }

            for(int j = 1; j < MyStarsLevelsUnlocked.Count; j++)
            {
                var msj = MyStarsLevelsUnlocked[$"Level{j}"];
                //Debug.Log($"Level{j}: {msj}");
            }
        }

        private void Inizialize()
        {
            ContainerMaster = transform.GetChild(0).gameObject;
            
            int countLevelTotal = ContainerMaster.transform.childCount - 1; // 32 +2
            int countLevelTmp = countLevelTotal                        - 2; // 30 +2
            
            for(int i = 0; i <= countLevelTotal; i++)
            {
                if(i < countLevelTmp)
                {
                    var levelTmp = ContainerMaster.transform.GetChild(i).gameObject;
                    Levels.Add(levelTmp);
                    
                    var starContainerTmp = levelTmp.transform.GetChild(1).gameObject;
                    StarsContainers.Add(starContainerTmp);
                    
                    var buttonLevelTmp = levelTmp.GetComponentInChildren<Button>();
                    Buttons.Add(buttonLevelTmp);

                    var buttonTextLevelTmp = buttonLevelTmp.GetComponentInChildren<TextMeshProUGUI>();
                    buttonTextLevelTmp.SetText(levelTmp.name);
                }

                if(i >= countLevelTmp)
                {
                    var pages = ContainerMaster.transform.GetChild(i).gameObject;
                    PagesLevelsContainer.Add(pages);
                }
            }
        }

        private void ReOrder()
        {
            const int primerTramo = 11;
            const int segundoTramo = 22;

            const int page1index = 0;
            const int page2index = 1;
            const int page3index = 2;
            
            for(int i = 0; i < Levels.Count; i++)
            {
                if(i < primerTramo)
                {
                    Levels[i].transform.SetParent(PagesLevelsContainer[page1index].transform);
                }

                if(i >= primerTramo && i < segundoTramo)
                {
                    Levels[i].transform.SetParent(PagesLevelsContainer[page2index].transform);
                }
                
                if(i >= segundoTramo)
                {
                    Levels[i].transform.SetParent(PagesLevelsContainer[page3index].transform);
                }
            }
        }

        private void Setup()
        {
            CheckStars();
            CheckButtons();
        }

        private void CheckStars()
        {
            for(int i = 0; i < StarsContainers.Count; i++)
            {
                var starsUnlocked = MyStarsLevelsUnlocked[$"Level{i+1}"];
                var imagesCount = StarsContainers[i].transform.childCount;
                
                for(int j = 1; j < imagesCount; j++)
                {
                    const int unaEstrella = 1;
                    const int dosEstrella = 2;
                    const int tresEstrella = 3;
                    
                    var imageChild = StarsContainers[i].transform.GetChild(j).gameObject;

                    switch(starsUnlocked)
                    {
                        case 0: break;
                        
                        case unaEstrella:
                            if(j <= unaEstrella)
                            {
                                imageChild.SetActive(true);
                            } break;
                        
                        case dosEstrella:
                            if(j <= dosEstrella)
                            {
                                imageChild.SetActive(true);
                            } break;
                        
                        case tresEstrella:
                            if(j <= tresEstrella)
                            {
                                imageChild.SetActive(true);
                            } break;
                    }
                }
            }
        }

        private void CheckButtons()
        {
            for(int j = 0; j < Buttons.Count; j++)
            {
                var levelUnlocked = MyLevelsUnlocked[$"Level{j + 1}"];
                Buttons[j].interactable = levelUnlocked;

                var buttonText = Buttons[j].GetComponentInChildren<TextMeshProUGUI>();
                var buttonParent = Buttons[j].transform.parent.name;
                buttonText.SetText(buttonParent);
                
                var level = int.Parse(buttonParent);
                Buttons[j].onClick.AddListener((() => GoLevel(level)));
            }
        }

        public void GoLevel(int level)
        {
            GameManager.Instance.LoadLevel(level);
        }

        private void DisableButtons()
        {
            for(int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].onClick.RemoveAllListeners();
            }
        }

        private void OnDisable()
        {
            DisableButtons();
        }
    }
}