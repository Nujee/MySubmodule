using System;

namespace Code.MySubmodule.Analytics
{
    /// <summary>
    /// Interface for adapters initialization
    /// </summary>
    public interface ISDKAdapter : IDisposable
    {
        void Init();
    }
}