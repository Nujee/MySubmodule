using System;
 
namespace Unity.IL2CPP.CompilerServices 
{
    /// <summary>
    /// Use this attribute on a class, method, or property to inform the IL2CPP code conversion utility to override the
    /// global setting for one of a few different runtime checks.
    ///
    /// Example:
    ///
    ///     [Il2CppSetOption(Option.NullChecks, false)]
    ///     public static string MethodWithNullChecksDisabled()
    ///     {
    ///         var tmp = new Object();
    ///         return tmp.ToString();
    ///     }
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public class Il2CppSetOptionAttribute : Attribute {
        public Option Option { get; private set; }
        public object Value { get; private set; }
        public Il2CppSetOptionAttribute(Option option, object value) {
            Option = option;
            Value = value;
        }
    }
}