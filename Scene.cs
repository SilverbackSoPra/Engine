﻿using System.Collections.Generic;
using Monogame_Engine.Engine.Mesh;

namespace Monogame_Engine.Engine
{
    /// <summary>
    /// The Scene class is used to represent a scene. This is useful
    /// if we want many actors in one place e.g for a level.
    /// </summary>
    class Scene
    {

        public readonly List<ActorBatch> mActorBatches;
        public readonly Light mDirectionalLight;

        /// <summary>
        /// Constructs a <see cref="Scene"/>.
        /// </summary>
        public Scene()
        {
            mActorBatches = new List<ActorBatch>();
            mDirectionalLight = new Light();
        }

        /// <summary>
        /// Adds an actor to the scene.
        /// </summary>
        /// <param name="actor">The actor which you want to add to the scene</param>
        public void Add(Actor actor)
        {

            // Search for the ActorBatch
            var actorBatch = mActorBatches.Find(ele => ele.mMesh == actor.mMesh);

            // If there is no ActorBatch which already uses the mesh of the Actor we
            // need to create a new ActorBatch and add it to mActorBatches
            if (actorBatch == null)
            {
                actorBatch = new ActorBatch(actor.mMesh);
                mActorBatches.Add(actorBatch);
            }

            actorBatch.Add(actor);

        }

        /// <summary>
        /// Removes an actor from the scene.
        /// </summary>
        /// <param name="actor"></param>
        /// <returns>A boolean whether the actor was found in the scene.
        /// If there is no ActorBatch existing the return value will be null</returns>
        public bool? Remove(Actor actor)
        {

            var actorBatch = mActorBatches.Find(ele => ele.mMesh == actor.mMesh);

            return actorBatch?.Remove(actor);

        }

    }
}