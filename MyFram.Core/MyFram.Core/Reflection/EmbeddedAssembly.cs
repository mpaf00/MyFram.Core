namespace MyFram.Core.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Security.Cryptography;

    public static class EmbeddedAssembly
    {
        private static Dictionary<string, Assembly> _assemblies;

        public static Assembly Get(string assemblyFullName)
        {
            Assembly returnObject = null;

            if (IsValidAssemblies() && _assemblies.ContainsKey(assemblyFullName))
            {
                returnObject = _assemblies[assemblyFullName];
            }

            return returnObject;
        }

        public static void Load(string embeddedResource, string fileName)
        {
            if (!IsValidAssemblies())
            {
                _assemblies = new Dictionary<string, Assembly>();
            }

            byte[] memory;
            Assembly assembly;
            var currentAssembly = Assembly.GetExecutingAssembly();

            using (var stream = currentAssembly.GetManifestResourceStream(embeddedResource))
            {
                if (stream == null)
                    throw new Exception(embeddedResource + " is not found in Embedded Resources.");

                memory = new byte[(int)stream.Length];
                stream.Read(memory, 0, (int)stream.Length);

                try
                {
                    assembly = Assembly.Load(memory);

                    _assemblies.Add(assembly.FullName, assembly);
                    return;
                }
                catch
                {
                    // Purposely do nothing
                    // Unmanaged dll or assembly cannot be loaded directly from byte[]
                    // Let the process fall through for next part
                }
            }

            var fileOk = false;
            string tempFile;

            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                var fileHash = BitConverter.ToString(sha1.ComputeHash(memory)).Replace("-", string.Empty);

                // Define the temporary storage location of the DLL/assembly
                tempFile = Path.GetTempPath() + fileName;

                // Determines whether the DLL/assembly is existed or not
                if (File.Exists(tempFile))
                {
                    // Get the hash value of the existed file
                    var bb = File.ReadAllBytes(tempFile);
                    var fileHash2 = BitConverter.ToString(sha1.ComputeHash(bb)).Replace("-", string.Empty);

                    // Compare the existed DLL/assembly with the Embedded DLL/assembly
                    fileOk = fileHash == fileHash2;
                }
            }

            // Create the file on disk
            if (!fileOk)
            {
                File.WriteAllBytes(tempFile, memory);
            }

            // Load it into memory
            assembly = Assembly.LoadFile(tempFile);

            // Add the loaded DLL/assembly into dictionary
            _assemblies.Add(assembly.FullName, assembly);
        }

        private static bool IsValidAssemblies()
        {
            return (_assemblies != null && _assemblies.Count > 0);
        }
    }
}