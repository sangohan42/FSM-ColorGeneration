using UnityEngine;
using Bitcraft.StateMachine;

public static class RandomColorizeStateTokens
{
    public static readonly StateToken GenerateColor = new StateToken("Generate Color");
    public static readonly StateToken ValidateColor = new StateToken("Validate Color");
    public static readonly StateToken DisplayColor = new StateToken("Display Color");
}
