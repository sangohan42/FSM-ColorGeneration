using UnityEngine;
using Bitcraft.StateMachine;

public static class RandomColorizeActionTokens
{
    public static readonly ActionToken Next = new ActionToken("Next");
    public static readonly ActionToken Validate = new ActionToken("Validate Color");
}
