using Bitcraft.StateMachine;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class FSMManager : MonoBehaviour
{
    private static readonly int VALIDATE_STATE_DISPLAY_TIME = 1;

    private RandomColorizeStateMachine sm;

    public Image generatedColorImage;
    public Material displayedColorMaterial;

    public Text currentStateText;
    public Text remainingTimeText;
    public GameObject isValidGO;
    public GameObject isInvalidGO;

    public Button generateNextColorButton;
    public Button validateColorButton;

    void Start()
    {
        // Set Callback used to display the current state on screen
        RandomColorizeStateBase.OnArrived += SetCurrentStateLabel;
        ValidateColorState.OnValidate += SetIsValidLabel;
        GenerateColorState.OnNewColor += SetNewColor;
        DisplayColorState.OnDisplayColor += SetValidatedColor;

        //Add UI Button Callbacks
        generateNextColorButton.onClick.AddListener( PerformNextAction );
        validateColorButton.onClick.AddListener( PerformValidateAction );

        // Set default displayed color to transparent
        displayedColorMaterial.color = new Color( 0, 0, 0, 0 );

        // Set default validate button to not interactable
        validateColorButton.interactable = false;

        // Set default color info values and start the FSM
        var colorHandler = new ColorHandler();
        sm = new RandomColorizeStateMachine( colorHandler );
    }

    // Button Callback
    public void PerformNextAction()
    {
        sm.PerformAction( RandomColorizeActionTokens.Next );
        generateNextColorButton.interactable = false;
        validateColorButton.interactable = true;
    }

    // Button Callback
    public void PerformValidateAction()
    {
        sm.PerformAction( RandomColorizeActionTokens.Validate );
        validateColorButton.interactable = false;
    }

    public void ComeBackToGenerateState()
    {
        sm.PerformAction( RandomColorizeActionTokens.Next );
        generateNextColorButton.interactable = true;
        validateColorButton.interactable = false;
    }

    // Set label to current state when arrived
    public void SetCurrentStateLabel( StateToken token )
    {
        if ( currentStateText )
            currentStateText.text = token.ToString();
    }

    // Display remaining time before coming back to the GenerateState
    public void SetRemainingTimeLabel( float remainingTime )
    {
        if ( remainingTimeText )
        {
            if( !remainingTimeText.isActiveAndEnabled)
                remainingTimeText.enabled = true;
            remainingTimeText.text = "( Remaining time = " + string.Format( "{0:N2}", remainingTime ) + " )";
        }
    }

    // Display if color is valid or not
    public void SetIsValidLabel( bool isValid )
    {
        if ( !isValid )
            generateNextColorButton.interactable = true;

        StartCoroutine( DisplayColorValidationResult( VALIDATE_STATE_DISPLAY_TIME, isValid ? isValidGO : isInvalidGO ) );
    }

    IEnumerator DisplayColorValidationResult( float displayTime, GameObject go )
    {
        go.SetActive( true );
        yield return new WaitForSeconds( displayTime );
        go.SetActive( false );
    }

    // Display generated color
    public void SetNewColor( Color? color )
    {
        // If the color is null we display a transparent color
        generatedColorImage.color = color ?? new Color( 0, 0, 0, 0 );
    }

    // Display valid color
    public void SetValidatedColor( int duration, Color? color )
    {
        StartCoroutine( DisplayValidatedColor( duration, color ) );
    }

    IEnumerator DisplayValidatedColor( float displayTime, Color? color )
    {
        // Display validated color
        displayedColorMaterial.color = color ?? new Color( 0, 0, 0, 0 );

        // Display remaining time
        bool complete = false;
        float elapsedTime = 0f;

        DOTween.To( () => { return elapsedTime; }, ( v ) => { elapsedTime = v; }, displayTime, displayTime )
        .SetEase( Ease.Linear )
        .OnComplete( () =>
        {
            complete = true;
            Debug.Log( "DISPLAY TIME COMPLETE" );
        } );
        while ( !complete )
        {
            SetRemainingTimeLabel( displayTime - elapsedTime );
            yield return null;
        }
        if( remainingTimeText )
            remainingTimeText.enabled = false;

        // Perform Action to comeback to the generate state
        ComeBackToGenerateState();

        generateNextColorButton.interactable = true;
    }
}
