using System;
using System.Linq;
using System.Security.Cryptography;

namespace Encryptor
{
    public static class SymetricAlgorithmTypes
    {
        private static readonly Lazy<Type[]> Types = new Lazy<Type[]>(() =>
        {
            var assemblies = new[]
            {
                typeof(AesCryptoServiceProvider).Assembly,
                typeof(SymmetricAlgorithm).Assembly
            };

            var saType = typeof(SymmetricAlgorithm);

            return assemblies.SelectMany( a => a.GetTypes().Where(t => t.IsSubclassOf(saType) && !t.IsAbstract)).OrderBy(t => t.Name).ToArray();
        });

        public static Type[] GetTypes()
        {
            return Types.Value;
        }
    }
}