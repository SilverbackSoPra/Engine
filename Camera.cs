using Microsoft.Xna.Framework;
using static System.Math;

namespace Monogame_Engine.Engine
{
    internal sealed class Camera
    {

        public Vector3 mLocation;
        public Vector2 mRotation;

        public float mFieldOfView;
        public float mAspectRatio;
        public float mNearPlane;
        public float mFarPlane;

        public Matrix mViewMatrix;
        public Matrix mProjectionMatrix;

        public Camera(Vector3 location = default(Vector3),
            Vector2 rotation = default(Vector2),
            float fieldOfView = 45.0f,
            float aspectRatio = 2.0f,
            float nearPlane = 1.0f,
            float farPlane = 100.0f)
        {
            mViewMatrix = new Matrix();
            mProjectionMatrix = new Matrix();

            mLocation = location;
            mRotation = rotation;

            mFieldOfView = fieldOfView;
            mAspectRatio = aspectRatio;

            mNearPlane = nearPlane;
            mFarPlane = farPlane;
        }

        public void UpdateView()
        {
            // TODO: We need to change this to a 3rd person camera
            var direction = Vector3.Normalize(new Vector3((float) (Cos(mRotation.Y) * Sin(mRotation.X)),
                (float) Sin(mRotation.Y),
                (float) (Cos(mRotation.Y) * Cos(mRotation.X))));

            var right = new Vector3((float) Sin(mRotation.X - 3.14 / 2.0), 0.0f, (float) Cos(mRotation.X - 3.14 / 2.0));

            var up = Vector3.Cross(direction, right);

            mViewMatrix = Matrix.CreateLookAt(mLocation, mLocation + direction, up);
        }

        public void UpdatePerspective()
        {
            mProjectionMatrix =
                Matrix.CreatePerspectiveFieldOfView(mFieldOfView / 180.0f * 3.14f, mAspectRatio, mNearPlane, mFarPlane);
        }
    }
}