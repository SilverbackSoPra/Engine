using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelEditor.Engine
{

    /// <summary>
    /// Represents a shadow which can be attached to a light.
    /// </summary>
    class Shadow
    {

        private readonly Light mLight;

        public float mDistance;
        public float mBias;

        public int mNumSamples;
        public float mSampleRange;

        public Matrix mViewMatrix;
        public Matrix mProjectionMatrix;

        public bool mActivated;

        /// <summary>
        /// Constructs a <see cref="Shadow"/>
        /// </summary>
        /// <param name="light">The light where the shadow is attached to.</param>
        public Shadow(Light light)
        {

            mLight = light;

            mDistance = 60.0f;
            mBias = 0.002f;
            mNumSamples = 16;
            mSampleRange = 0.4f;

            mProjectionMatrix = Matrix.CreateOrthographicOffCenter(-25.0f, 25.0f, -25.0f, 25.0f, -100.0f, 100.0f);
            mViewMatrix = Matrix.Identity;

            mActivated = true;

        }

        /// <summary>
        /// Updates the shadow matrices according to the cameras view frustum.
        /// </summary>
        /// <param name="camera">The camera which is used in the scene</param>
        public void Update(Camera camera)
        {

            /*
             * Here is a quick overview of what this method does:
             * First we create the middle point of the shadow map in view space
             * Afterwards we use this information to create a light view matrix.
             * We then calulcate the widths and heights of the nearPlane and the
             * projected shadow distance plane. We then calculate all 8 points of
             * the resulting frustum. After we've done that we loop trough all of
             * these points, transform them into light view space and check the for
             * the mins and max of the X, Y, and Z components of these points. These
             * mins and maxs are needed to calculate the light projection matrix which
             * should fit the view frustum perfectly now.
             */
            var shadowMiddle = camera.mLocation + camera.Direction * mDistance / 2.0f;

            var direction = Vector3.Normalize(shadowMiddle - mLight.mLocation);

            mViewMatrix = Matrix.CreateLookAt(shadowMiddle, shadowMiddle + direction, Vector3.Up);

            var tang = (float)Math.Tan(camera.mFieldOfView * Math.PI / 360.0f);

            var farPlaneHeight = mDistance * tang;
            var farPlaneWidth = camera.mAspectRatio * farPlaneHeight;

            var nearPlaneHeight = camera.mNearPlane * tang;
            var nearPlaneWidth = camera.mAspectRatio * nearPlaneHeight;

            var far = camera.mLocation + camera.Direction * mDistance;
            var near = camera.mLocation + camera.Direction * camera.mNearPlane;

            var farUpperLeft = far + farPlaneHeight * camera.Up - farPlaneWidth * camera.Right;
            var farUpperRight = far + farPlaneHeight * camera.Up + farPlaneWidth * camera.Right;
            var farLowerLeft = far - farPlaneHeight * camera.Up - farPlaneWidth * camera.Right;
            var farLowerRight = far - farPlaneHeight * camera.Up + farPlaneWidth * camera.Right;

            var nearUpperLeft = near + nearPlaneHeight * camera.Up - nearPlaneWidth * camera.Right;
            var nearUpperRight = near + nearPlaneHeight * camera.Up + nearPlaneWidth * camera.Right;
            var nearLowerLeft = near - nearPlaneHeight * camera.Up - nearPlaneWidth * camera.Right;
            var nearLowerRight = near - nearPlaneHeight * camera.Up + nearPlaneWidth * camera.Right;

            var array = new[] {farUpperLeft, farUpperRight, farLowerLeft, farLowerRight, nearUpperLeft, nearUpperRight, nearLowerLeft, nearLowerRight};

            var vec = Vector3.Transform(farUpperLeft, mViewMatrix);

            var maxX = vec.X;
            var minX = vec.X;
            var maxY = vec.Y;
            var minY = vec.Y;
            var maxZ = vec.Z;
            var minZ = vec.Z;

            foreach (var vector in array)
            {

                var transform = Vector3.Transform(vector, mViewMatrix);

                if (transform.X > maxX)
                {
                    maxX = transform.X;
                }
                if (transform.X < minX)
                {
                    minX = transform.X;
                }
                if (transform.Y > maxY)
                {
                    maxY = transform.Y;
                }
                if (transform.Y < minY)
                {
                    minY = transform.Y;
                }
                if (transform.Z > maxZ)
                {
                    maxZ = transform.Z;
                }
                if (transform.Z < minZ)
                {
                    minZ = transform.Z;
                }

            }

            /*
             * We have to reduce -z even further to get the shadows nearer to light rendered correctly
             * To be honest I don't even know why we have to use -maxZ for -z and -minZ for z. My guess
             * is that it has to do with the different orientation of the coordinate system in DirectX.
             */
            mProjectionMatrix = Matrix.CreateOrthographicOffCenter(minX, maxX, minY, maxY, -maxZ - 50.0f, -minZ);

        }

    }
}
