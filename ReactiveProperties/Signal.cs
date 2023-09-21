using JetBrains.Annotations;

namespace Code.MySubmodule.ReactiveProperties
{
    public struct Signal
    {
        private bool _isSignaled;

        [PublicAPI]
        public void Send()
        {
            _isSignaled = true;
        }

        [PublicAPI]
        public bool Receive()
        {
            var result = _isSignaled;
            _isSignaled = false;
            return result;
        }
    }
}