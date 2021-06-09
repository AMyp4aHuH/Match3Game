using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Sprites
{
    public enum AnimationState : byte
    {
        Create,
        Idle,
        Select,
        Destroy,
        WaitDestroy
    }
}
