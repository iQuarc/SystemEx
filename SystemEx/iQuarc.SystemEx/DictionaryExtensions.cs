using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace iQuarc.SystemEx
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the value if it exists in the dictionary or the default of <see cref="TValue"/>.
        /// </summary>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }

        /// <summary>
        /// Gets the value if it exists in the dictionary or it creates a new instance which is added and returned.
        /// </summary>
        public static TValue GetValueOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            TValue value;

            if (!dictionary.TryGetValue(key, out value))
            {
                value = new TValue();
                dictionary.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// Gets the value if it exists in the dictionary or it creates a new instance which is added and returned.
        /// </summary>
        public static TValue GetValueOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            TValue value;

            if (!dictionary.TryGetValue(key, out value))
            {
                value = factory();
                dictionary.Add(key, value);
            }

            return value;
        }

        /// <summary>
        /// Converts the dictionary to a name value collection.
        /// </summary>
        /// <remarks>
        /// The keys are converted to string using ToString function.
        /// By default ToString is used to convert a value, if a conversion function is not provided
        /// </remarks>
        public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TValue, string> convertFunc = null)
            where TValue : class
        {
            if (convertFunc == null)
                convertFunc = t => t.ToString();

            NameValueCollection nameValueCollection = new NameValueCollection();

            foreach (var kvp in dictionary)
            {
                string value = string.Empty;

                if (kvp.Value != null)
                    value = convertFunc(kvp.Value);

                nameValueCollection.Add(kvp.Key.ToString(), value);
            }

            return nameValueCollection;
        }
    }
}