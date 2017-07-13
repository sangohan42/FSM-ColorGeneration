using UnityEngine;
using Bitcraft.StateMachine;
using System;

public class GenerateColorState : RandomColorizeStateBase
{
    public delegate void OnNewColorGenerated( Color? color );
    public static event OnNewColorGenerated OnNewColor;

    public GenerateColorState()
        : base( RandomColorizeStateTokens.GenerateColor )
    {
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        RegisterActionHandler( RandomColorizeActionTokens.Next, OnNext );
    }

    protected override void OnEnter( StateEnterEventArgs e )
    {
        base.OnEnter( e );

        // Could have generated the random Color here and passed it to the next state (ValidateColorState) by using :
        //e.Redirect.TargetStateToken = RandomColorizeStateTokens.ValidateColor; 
        //e.Redirect.TargetStateData = generatedColor;
        //but let's wait for the user to click on the Generate Next Color button to perform the RandomColorizeActionTokens.Next action
    }

    private void OnNext( object data, Action<StateToken> cb )
    {
        // Save generated color
        ColorHandler infos = GetContext<ColorHandler>();
        infos.CurrentColor = GenerateRandomColor();

        if ( OnNewColor != null )
            OnNewColor( infos.CurrentColor );

        cb( RandomColorizeStateTokens.ValidateColor );
    }

    private Color? GenerateRandomColor()
    {
        int randomKey = new System.Random().Next( 0, PossibleColors.colorDict.Count );

        return PossibleColors.GetColor( ( ColorNames )randomKey );
    }
}
