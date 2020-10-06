using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Matrix
{
    public enum TileState
    {
        Idle,
        Select,
        Move,
        WaitDestroy,
        Destroy,
        Empty
    }
}
