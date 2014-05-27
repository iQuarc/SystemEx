SystemEx
========

A set of extensions methods over standard .Net types

### Dictionary Extensions

A set of extensions methods over IDictionary<TKey, TValue>

**DictionaryExtensions.GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)**

Gets the value if it exists in the dictionary or the default of TValue.

```cs
	Dictionary<string, int> d = new Dictionary<string, int>();
	int value = d.GetValueOrDefault("some key");
```

**DictionaryExtensions.GetValueOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)**

Gets the value if it exists in the dictionary or it creates a new instance which is added and returned.

```cs
	Dictionary<string, MyType> d = new Dictionary<string, MyType>();
	MyType actual = d.GetValueOrNew("some key");
```

**DictionaryExtensions.GetValueOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)**

Gets the value if it exists in the dictionary or it creates a new instance using the specified factory which is added and returned.

```cs
	Dictionary<string, string> d = new Dictionary<string, string>();
	string actual = d.GetValueOrNew("some key", () => "some value");
```

**DictionaryExtensions.ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TValue, string> convertFunc = null)**

Converts the dictionary to a name value collection.
The keys are converted to string using ToString() function. By default ToString() is used to convert a value, if a conversion function is not provided

```cs
	 Dictionary<int, string> d = new Dictionary<int, string>
				{
					{ 1, "Value1" },
                    { 2, "Value2" }
				};

	NameValueCollection c = d.ToNameValueCollection();
```

### Enumerable Extensions

A set of extensions methods over IEnumerable<T>