using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Rolficopter.LD34.Assets.Scripts;

public class TutorialLogic : MonoBehaviour {

    private enum TutorialStep
    {
        Start,
        IntroducePlayer,
        CameraStartedFollowing
    }

    private TutorialStep currentStep = TutorialStep.Start;

    private const float playerIntroDelay = 2.0f;
    private const float cameraFollowDelay = playerIntroDelay + 1.0f;

    // Use this for initialization
    void Start () {
        GameObject[] allTexts = GameObject.FindGameObjectsWithTag("Tutorial");
        foreach (var text in allTexts)
        {
            Text textComponent = text.GetComponent<Text>();
            if ( textComponent != null )
            {
                Color color = textComponent.color;
                // transparent
                color.a = 0.0f;

                textComponent.color = color;
            }
        }

        this.MakeTextVisible("Welcome");
	}
	
	// Update is called once per frame
	void Update () {
        float time = Time.timeSinceLevelLoad;

        switch (this.currentStep)
        {
            case TutorialStep.Start:
                if ( time >= playerIntroDelay )
                {
                    this.MakeTextVisible("PlayerIntro");
                    this.MakeSpriteVisible("Player");

                    this.currentStep = TutorialStep.IntroducePlayer;
                }

                break;

            case TutorialStep.IntroducePlayer:
                if ( time >= cameraFollowDelay )
                {
                    GameObject mainCamera = GameObject.Find("Main Camera");
                    if ( mainCamera != null )
                    {
                        CameraSideFollow followScript = mainCamera.GetComponent<CameraSideFollow>();
                        followScript.enabled = true;
                    }

                    this.currentStep = TutorialStep.CameraStartedFollowing;
                }

                break;
        }
	}

    private void MakeTextVisible(string gameObjectName)
    {
        GameObject text = GameObject.Find(gameObjectName);
        if ( text != null )
        {
            Text textComponent = text.GetComponent<Text>();
            if ( textComponent != null )
            {
                Color newColor = textComponent.color;
                newColor.a = 255.0f;
                textComponent.color = newColor;
            }
        }
    }

    private void MakeSpriteVisible(string gameObjectName)
    {
        GameObject text = GameObject.Find(gameObjectName);
        if (text != null)
        {
            SpriteRenderer spriteRenderer = text.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color newColor = spriteRenderer.color;
                newColor.a = 255.0f;
                spriteRenderer.color = newColor;
            }
        }
    }
}
