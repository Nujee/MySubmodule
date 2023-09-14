using System;

namespace Code.BlackCubeSubmodule.Analytics
{
    /// <summary>
    /// Interface for adapters initialization
    /// </summary>
    public interface ISDKAdapter : IDisposable
    {
        void Init();
    }
}