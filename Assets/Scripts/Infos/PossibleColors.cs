using UnityEngine;
using System.Collections.Generic;

public enum ColorNames
{
    Red,
    Green,
    Blue,
    Orange,
    Yellow,
    Pink,
    BattleshipGrey,
    Black,
    White,
    Purple
}

public static class PossibleColors
{
    // 10 clearly different values to see when it changed
    public static readonly Dictionary<ColorNames, Color> colorDict = new Dictionary<ColorNames, Color> {
         { ColorNames.Red,              new Color( 254, 9, 0, 255 )     /255f },
         { ColorNames.Green,            new Color( 0, 254, 111, 255 )   /255f },
         { ColorNames.Blue,             new Color( 0, 122, 254, 255 )   /255f },
         { ColorNames.Orange,           new Color( 254, 161, 0, 255 )   /255f },
         { ColorNames.Yellow,           new Color( 254, 224, 0, 255 )   /255f },
         { ColorNames.Pink,             new Color( 232, 0, 254, 255 )   /255f },
         { ColorNames.BattleshipGrey,   new Color( 132, 132, 130, 255 ) /255f },
         { ColorNames.Black,            new Color( 0, 0, 0, 1 ) },
         { ColorNames.White,            new Color( 1, 1, 1, 1 ) },
         { ColorNames.Purple,           new Color( 102, 51, 153, 255 )   /255f }
     };

    public static Color? GetColor( ColorNames colorName )
    {
        // Try to get the result in the static Dictionary
        Color result;
        if ( colorDict.TryGetValue( colorName, out result ) )
        {
            return result;
        }
        else
        {
            return null;
        }
    }
}
