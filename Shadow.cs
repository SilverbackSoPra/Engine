using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Engine
{
    class Shadow
    {

        private readonly Light mLight;

        public float mDistance;
        public float mBias;

        public int mNumSamples;
        public float mSampleRange;

        public Matrix mViewMatrix;
        public Matrix mProjectionMatrix;

        public Shadow(Light light)
        {

            mLight = light;

            mDistance = 40.0f;
            mBias = 0.002f;
            mNumSamples = 16;
            mSampleRange = 0.2f;

            mProjectionMatrix = Matrix.CreateOrthographicOffCenter(-25.0f, 25.0f, -25.0f, 25.0f, -100.0f, 100.0f);
            mViewMatrix = Matrix.Identity;

        }

        public void Update(Vector3 location)
        {

            var direction = Vector3.Normalize(-mLight.mLocation);

            mViewMatrix = Matrix.CreateLookAt(location, location + direction, Vector3.Up);

        }

    }
}
