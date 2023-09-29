using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {
    public class PauseMenu : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform menu;

        //SCREEM COMPONENTS POSITIONS
        Vector2 menuAnchored;

//******************************************************************************************************************************************// 
        public override void SetParameters(){ //stablish initial positions
            menuAnchored = menu.anchoredPosition;
        }
        public override void ResetPositions(){
            menu.anchoredPosition = menuAnchored;
        }

//*****LOGIC OF THE BUTTONS*****************************************************************************************************************//
        public void ContinueGame(){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GameManager.Instance.SetPauseState(false);
            ScreenManager.Instance.RemoveScreen(myTypeScreen);
        }
        public void OpenSettings(){
            //ScreenManager.Instance.ChangeScreen(GameScreens.Settings,myTypeScreen);
        }
        public void MainMenu(){
            Application.Quit();
        }

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            menu.anchoredPosition = new Vector2(menuAnchored.x - Screen.width * 2,menuAnchored.y);

            yield return new WaitForEndOfFrame();

            menu.DOAnchorPosX(menuAnchored.x,0.8f).SetEase(Ease.InOutSine);
                
            isAnimationRunning = false;
        }

//*****HIDE SCREEN LOGIC********************************************************************************************************************// 
        public override void Hide(){
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            isAnimationRunning = true;
            yield return new WaitForEndOfFrame();
            menu.DOAnchorPosX(-Screen.width * 2,0.8f).SetEase(Ease.InOutSine).OnComplete(() => {
                isAnimationRunning = false;
                myScreen.SetActive(false);
            });     
        }
    }
}