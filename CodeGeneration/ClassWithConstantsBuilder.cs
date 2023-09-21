using System;
using System.Collections.Generic;
using System.IO;

namespace Code.MySubmodule.CodeGeneration
{
    public static class ClassWithConstantsBuilder
    {
        /// <summary>
        /// Builds enum like static class with constants from Dictionary(int, string).
        /// </summary>
        [Obsolete]
        public static void Build(string className, string namespaceName, string path, Dictionary<int, string> layersDictionary)
        {
            using (var streamWriter = new StreamWriter($"{path}{className}.cs"))
            {
                streamWriter.WriteLine($"namespace {namespaceName}");
                streamWriter.WriteLine("{");
                streamWriter.WriteLine($"\tpublic static class {className}");
                streamWriter.WriteLine("\t// DON'T EDIT MANUALLY!!! CREATED WITH CODE GENERATOR.");
                streamWriter.WriteLine("\t{");
                foreach (var keyValuePair in layersDictionary)
                {
                    var layerName = keyValuePair.Value.Replace(" ", "");
                    var line = $"\t\tpublic const int {layerName} = {keyValuePair.Key};";
                    streamWriter.WriteLine(line);
                }
                streamWriter.WriteLine("\t}");
                streamWriter.WriteLine("}");
            }
        }
    }
}