using System.Diagnostics;
using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player character lands after being airborne.
    /// </summary>
    /// <typeparam name="PlayerLanded"></typeparam>
    public class PlayerLanded : Simulation.Event<PlayerLanded>
    {
        public PlayerController player;

        public override void Execute()
        {
            
        }
    }
}