using UnityEngine;
using Bitcraft.StateMachine;
using System;

public abstract class RandomColorizeStateBase : StateBase
{
    public delegate void ArrivedOnStateAction( StateToken state );
    public static event ArrivedOnStateAction OnArrived;

    protected RandomColorizeStateBase( StateToken token )
        : base( token )
    {
    }

    protected override void OnEnter( StateEnterEventArgs e )
    {
        base.OnEnter( e );
        PrintCurrentState();
    }

    protected virtual void PrintCurrentState()
    {
        Debug.Log( "Arrived in the " + Token.ToString() + " state." );
        if ( OnArrived != null )
            OnArrived( Token );
    }
}
