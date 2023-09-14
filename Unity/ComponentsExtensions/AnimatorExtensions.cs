using JetBrains.Annotations;
using UnityEngine;

namespace Code.BlackCubeSubmodule.Unity.ComponentsExtensions
{
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Resets all triggers on this animator.
        /// </summary>
        [PublicAPI]
        public static void ResetAllTriggers(this Animator animator)
        {
            var parameters = animator.parameters;
            for (var i = 0; i < animator.parameters.Length; i++)
            {
                if (parameters[i].type == AnimatorControllerParameterType.Trigger)
                {
                    animator.ResetTrigger(parameters[i].name);
                }
            }
        }
        
        /// <summary>
        /// Sets all boolean values on this animator to false.
        /// </summary>
        [PublicAPI]
        public static void ResetAllBooleans(this Animator animator)
        {
            var parameters = animator.parameters;
            for (var i = 0; i < animator.parameters.Length; i++)
            {
                if (parameters[i].type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameters[i].name, false);
                }
            }
        }
    }
}