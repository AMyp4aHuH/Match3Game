using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.MatrixElements.Destroyers
{
    public enum DestroyerState : byte
    {
        Create,
        Move,
        Destroy,
        Empty
    }
}
