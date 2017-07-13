using UnityEngine;
using System.Collections;
using Bitcraft.StateMachine;
using System;

public class ValidateColorState : RandomColorizeStateBase
{
    public delegate void OnValidateColorAction( bool isValid );
    public static event OnValidateColorAction OnValidate;

    public ValidateColorState()
        : base( RandomColorizeStateTokens.ValidateColor )
    {
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        RegisterActionHandler( RandomColorizeActionTokens.Validate, OnValidateCheck );

    }

    protected override void OnEnter( StateEnterEventArgs e )
    {
        base.OnEnter( e );
    }

    private void OnValidateCheck( object data, Action<StateToken> cb )
    {
        ColorHandler infos = GetContext<ColorHandler>();

        bool isValid = infos.CurrentColor != infos.PreviousColor;

        // If the current color is equal to previous color => return to the GenerateColor state
        if ( !isValid )
            cb( RandomColorizeStateTokens.GenerateColor );

        // If the current color is different to previous color => go to the DisplayColor state
        else
        {
            infos.PreviousColor = infos.CurrentColor; 
            cb( RandomColorizeStateTokens.DisplayColor );
        }

        if ( OnValidate != null)
        {
            OnValidate( isValid );
        }
    }
}
