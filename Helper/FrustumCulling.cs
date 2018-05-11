using Microsoft.Xna.Framework;
using static System.Math;
using static Microsoft.Xna.Framework.Vector3;

namespace Monogame_Engine.Engine.Helper
{
    class FrustumCulling
    {
        private double mTang;

        private Vector3 mX;
        private Vector3 mY;
        private Vector3 mZ;

        public FrustumCulling()
        {

        }


        public int CullActorsOutsideFrustum(Scene scene, Camera camera)
        {

            var visibleActors = 0;

            CalculateFrustum(camera);

            foreach (var actorBatch in scene.mActorBatches)
            {

                var radius = actorBatch.mMesh.mRadius;

                foreach (var actor in actorBatch.mActors)
                {

                    var scale = actor.mModelMatrix.Scale.Length();

                    actor.mRender = IsSphereVisible(actor.mModelMatrix.Translation, radius, 1.0f, camera);

                    if (actor.mRender)
                    {
                        visibleActors++;
                    }

                }
            }

            return visibleActors;

        }

        private bool IsSphereVisible(Vector3 point, float radius, float scale, Camera camera)
        {

            var d = radius * scale;

            point = point - camera.mLocation;

            var z = Dot(point, mZ);

            if (z - d > camera.mFarPlane || z + d < camera.mNearPlane)
            {
                return false;
            }

            var y = Dot(point, mY);
            var localHeight = z * mTang;

            if (y - d > localHeight || y + d < -localHeight)
            {
                return false;
            }

            var x = Dot(point, mX);
            var localWidth = localHeight * camera.mAspectRatio;

            if (x - d > localWidth || x + d < -localWidth)
            {
                return false;
            }

            return true;

        }

        private void CalculateFrustum(Camera camera)
        {
            mTang = Tan(camera.mFieldOfView * PI / 360.0f);

            mZ = Normalize(camera.Direction);
            mX = Normalize(Cross(mZ, camera.Up));
            mY = Normalize(Cross(mX, mZ));
        }

    }
}
