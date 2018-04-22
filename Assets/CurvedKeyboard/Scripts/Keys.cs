﻿//namespace HoloLensKeyboard
//{
    public enum KeySet
    {
        LowerCase,
        UpperCase,
        Special
    }

    public enum KeyState
    {
        Default,
        Focused,
        Pressed,
        Selected
    }

    public static class Keys
    {
        public const string SPACE = " ";
        public const string BACK = "Back";
        public const string ABC = "ABC";
        public const string QEH = "123\n?!#";
        public const string UP = "UP";
        public const string LOW = "low";

        public const int CENTER_ITEM = 15;
        public const int KEY_NUMBER = 30;
        public const int POSITION_SPACE = 28;

        // Feel free to change (but do not write strings in place of
        // special signs, change variables values instead).
        // Remember to always have 30 values
        public static readonly string[] LowerCase = new string[]
        {
        "q","w","e","r","t","y","u","i","o","p",
        "a","s","d","f","g","h","j","k","l",
        UP,"z","x","c","v","b","n","m",
        QEH,SPACE,BACK
        };

        // Feel free to change (but do not write strings in place of
        // special signs, change variables values instead)
        // Remember to always have 30 values
        public static readonly string[] UpperCase = new string[]
        {
        "Q","W","E","R","T","Y","U","I","O","P",
        "A","S","D","F","G","H","J","K","L",
        LOW,"Z","X","C","V","B","N","M",
        QEH,SPACE,BACK
        };

        // Feel free to change (but do not write strings in place of
        // special signs, change variables values instead)
        // Remember to always have 30 values
        public static readonly string[] Specials = new string[]
        {
        "1","2","3","4","5","6","7","8","9","0",
        "@","#","£","_","&","-","+","(",")",
        "*","\"","'",":",";","/","!","?",
        ABC,SPACE,BACK
        };

        // Number of items in a row
        public static readonly int[] LettersInRowsCount = new int[] { 10, 9, 8, 6 };

    }

//}

