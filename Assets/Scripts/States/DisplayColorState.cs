using UnityEngine;
using Bitcraft.StateMachine;
using System;

public class DisplayColorState : RandomColorizeStateBase
{
    private static readonly int MAX_DURATION = 3;

    public delegate void OnDisplayAction( int second, Color? colorToDisplay );
    public static event OnDisplayAction OnDisplayColor;

    public DisplayColorState()
        : base( RandomColorizeStateTokens.DisplayColor )
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

        ColorHandler infos = GetContext<ColorHandler>();

        int displayDurationInSeconds = new System.Random().Next( 1, MAX_DURATION + 1 );

        if( OnDisplayColor != null )
        {
            OnDisplayColor( displayDurationInSeconds, infos.CurrentColor );
        }
    }

    private void OnNext( object data, Action<StateToken> cb )
    {
        cb( RandomColorizeStateTokens.GenerateColor );
    }

}
