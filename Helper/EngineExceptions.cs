using System;

namespace Monogame_Engine.Engine.Helper
{
    internal sealed class EngineInvalidParameterException : Exception
    {

        public EngineInvalidParameterException(string message) : base(message)
        {

        } 

    }
}
