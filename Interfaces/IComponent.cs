using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match3Game.Interfaces
{   
    /// <summary>
    /// Represents game element that can be contain other game elements/components. 
    /// </summary>
    public interface IComponent : IGameElement
    {
        /// <summary>
        /// Adds game element for update and drawing.
        /// </summary>
        /// <param name="component"></param>
        void AddChild(IGameElement component);

        /// <summary>
        /// Removes game element.
        /// </summary>
        void RemoveChild(IGameElement component);
    }
}
