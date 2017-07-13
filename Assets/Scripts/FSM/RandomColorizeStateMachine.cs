using UnityEngine;
using Bitcraft.StateMachine;

public class RandomColorizeStateMachine : StateManager
{
    public RandomColorizeStateMachine( ColorHandler infos )
        : base( infos )
    {
        RegisterState( new DisplayColorState() );
        RegisterState( new GenerateColorState() );
        RegisterState( new ValidateColorState() );

        SetInitialState( RandomColorizeStateTokens.GenerateColor );
    }
}
