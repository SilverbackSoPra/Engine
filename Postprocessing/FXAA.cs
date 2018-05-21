using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Engine.Postprocessing
{

    /// <summary>
    /// Represents the fast approximate anti-aliasing algorithm.
    /// </summary>
    class Fxaa
    {

        public float mLumaThreshold;
        public float mLumaThresholdMin;

        public bool mDebugMode;
        public bool mActivated;

        /// <summary>
        /// Constructs a <see cref="Fxaa"/>
        /// </summary>
        public Fxaa()
        {

            mLumaThreshold = 1.0f / 8.0f;
            mLumaThresholdMin = 1 / 32.0f;

            mDebugMode = false;
            mActivated = true;

        }

    }
}
