using System;
using LavaLeak.Diplomata.Models;
using LavaLeak.Diplomata.Persistence;
using UnityEngine;

namespace LavaLeak.Diplomata.Persistence
{
    /// <summary>
    /// The abstract class to get the persistent models data.
    /// </summary>
    [Serializable]
    public abstract class Persistent
    {
        /// <summary>
        /// Override to return the object as json.
        /// </summary>
        /// <returns>A json string.</returns>
        public override string ToString() => JsonUtility.ToJson(this);
    }
}