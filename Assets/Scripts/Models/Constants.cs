﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class Constants
    {
        public enum Direction { UP, RIGHT, DOWN, LEFT }

        public const int GRID_SIDE_LENGTH = 4;
        public const int TILE_ROWS = 4;
        public const int TILE_COLUMNS = 4;

        public static readonly string[] TILE_COLORS = new string[]
        {
            "#EEE4DA",
            "#EDE0C8",
            "#F2B179",
            "#F59563",
            "#F67C5F",
            "#F65E3B",
            "#EDCF72",
            "#EDCC61",
            "#EDC850",
            "#EDC53F",
            "#EDC22E"
        };

        public static readonly string[] DIRECTIONAL_KEYS = new string[]
        {
            "up",
            "down",
            "left",
            "right",
            "w",
            "a",
            "s",
            "d"
        };
    }
}
