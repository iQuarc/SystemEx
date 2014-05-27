SystemEx
========

A set of extensions methods over standard .Net types

### Dictionary Extensions

A set of extensions methods over `IDictionary<TKey, TValue>`

**`DictionaryExtensions.GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)`**

Gets the value if it exists in the dictionary or the default of TValue.

```cs
	Dictionary<string, int> d = new Dictionary<string, int>();
	int value = d.GetValueOrDefault("some key");
```

**`DictionaryExtensions.GetValueOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)`**

Gets the value if it exists in the dictionary or it creates a new instance which is added and returned.

```cs
	Dictionary<string, MyType> d = new Dictionary<string, MyType>();
	MyType actual = d.GetValueOrNew("some key");
```

**`DictionaryExtensions.GetValueOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)`**

Gets the value if it exists in the dictionary or it creates a new instance using the specified factory which is added and returned.

```cs
	Dictionary<string, string> d = new Dictionary<string, string>();
	string actual = d.GetValueOrNew("some key", () => "some value");
```

**`DictionaryExtensions.ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TValue, string> convertFunc = null)`**

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

A set of extensions methods over `IEnumerable<T>`

**`EnumerableExtensions.ReplaceAll<T>(this IEnumerable<T> collection, T source, T replacement)`**

Replaces all occurrences of source element in given IEnumerable with replacement element.
If more occurrences are found all are replaced, with ONE replacement. Therefore, the resulted IEnumerable has less elements.
Default EqualityComparer is used to determine equality.
Null element is handled as any other element

```cs
	char[] chars = {'a', 'b', 'c'};
	IEnumerable<char> actual = chars.ReplaceAll('b', 'x');
```

**`EnumerableExtensions.ReplaceAll<T>(this IEnumerable<T> collection, T source, T replacement, IEqualityComparer<T> comparer)`**

Replaces all occurrences of source element in given IEnumerable with replacement element.
If more occurrences are found all are replaced, with ONE replacement. Therefore, the resulted IEnumerable has less elements. The specified IEqualityComparer is used to determine equality.
Null element is handled as any other element

```cs
	char[] chars = {'a', 'b', 'c'};
	IEqualityComparer<char> comparer = new CustomEqualityComparer();
	IEnumerable<char> actual = chars.ReplaceAll('b', 'x', comparer);
```

**`EnumerableExtensions.Replace<T>(this IEnumerable<T> collection, T source, T replacement)`**

Replaces each occurrences of source element in given IEnumerable with replacement element.
If more occurrences are found each one is replaced, resulting same number of elements in output IEnumerable
Default EqualityComparer is used to determine equality.
Null element is handled as any other element

```cs
	char[] chars = {'a', 'b', 'c'};
	IEnumerable<char> actual = chars.Replace('b', 'x');
```


**`EnumerableExtensions.Replace<T>(this IEnumerable<T> collection, T source, T replacement, IEqualityComparer<T> comparer)`**

Replaces each occurrences of source element in given IEnumerable with replacement element.
If more occurrences are found each one is replaced, resulting same number of elements in output IEnumerable.
The specified IEqualityComparer is used to determine equality.
Null element is handled as any other element

```cs
	char[] chars = {'a', 'b', 'c'};
	IEqualityComparer<char> comparer = new CustomEqualityComparer();
	IEnumerable<char> actual = chars.Replace('b', 'x', comparer);
```

**`EnumerableExtensions.ForEach<T>(this IEnumerable<T> collection, Action<T> action)`**

Executes the action on each element from the enumerable.

```cs
	List<int> actual = new List<int>();
	int[] e = { 1, 2 };
	e.ForEach(x => actual.Add(x));
```

**`EnumerableExtensions.Includes<T>(this IEnumerable<T> superset, IEnumerable<T> subset)`**

Verifies if every element of the subset is also an element of the superset.

```cs
	char[] superset = new[] {'a', 'b', 'c'};
	char[] subset = new[] {'b', 'c'};
	bool included = superset.Includes(subset);
```

### Exception Extensions

A set of extensions methods over `Exception`

**`ExceptionExtensions.FirstInner<T>(this Exception exception)`**

Gets the first inner exception of type T. Returns null if not found.

```cs
	ArgumentNullException ane = new ArgumentNullException();
	Exception e = new Exception("", ane);
	ArgumentNullException actual = e.FirstInner<ArgumentNullException>();
```

**`ExceptionExtensions.InnerMostException(this Exception exception)`**

Gets the deepest inner exception.

```cs
	ArgumentException ae = new ArgumentException();
    ArgumentNullException ane = new ArgumentNullException("", ae);
    Exception e = new Exception("", ane);

    Exception actual = e.InnerMostException();
	Assert.Same(ae, actual);
```

### String Extensions

A set of extensions methods over `String`

**`String.MatchesWildcard(this string input, string wildcard)`**

Returns true if the input matches the wildcard pattern. It is case sensitive.

```cs
	string input = "BXgin_SomeText_End";
	bool match = input.MatchesWildcard("B?gin*End");
```

### Type Extensions

A set of extensions methods over `Type`

**`Type.GetGenericInterface(this Type type, Type genericInterfaceTypeDefinition)`**

Gets the generic interface implemented by this type, which complies with given interface type definition.
An interface type definition is an open generic type which is an interface.

```cs
	Type result = typeof (GenericInterfaceImpl<int, string>).GetGenericInterface(typeof (IGenericInterface<,>));
	if (result != null) 
	{
		// type implements IGenericInterface<,>
	}
```

**`Type.GetGenericInterface(this Type type, Type genericInterfaceTypeDefinition)`**

Gets the type arguments of the generic interface implemented by this type. The generic interface should match the given interface type definition.
An interface type definition is an open generic type which is an interface.

```cs
	Type[] result = typeof(GenericInterfaceImpl<int, string>).GetGenericInterfaceArguments(typeof (IGenericInterface<,>));
	// result is [ typeof(int), typeof(string) ]
```

### Reflection Extensions

**`ReflectionExtensions.GetAttribute<TAttribute>(object instance)`**

Gets the attribute of type TAttribute from the instance type. Returns null if the attribute was not found.

```cs
	MyType instance = new MyType();
	TableAttribute attribute = instance.GetAttribute<TableAttribute>();
```

**`ReflectionExtensions.GetAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)`**

Gets the attribute of type TAttribute from the attribute provider. Returns null if the attribute was not found.

```cs
	TableAttribute table = typeof(MyClass).GetAttribute<TableAttribute>();
	
	foreach (PropertyInfo property in typeof(MyClass).GetProperties()) 
	{
		ColumnAttribute column = property.GetAttribute<ColumnAttribute>();
	}
```

**`ReflectionExtensions.GetAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider)`**

Gets all the attributes of type TAttribute from the attribute provider.

```cs
	IEnumerable<ServiceAttribute> services = typeof(MyClass).GetAttributes<ServiceAttribute>();

	PropertyInfo property = typeof(MyClass).GetProperty("Name");
	IEnumerable<ValidationAttribute> validations = property.GetAttributes<ValidationAttribute>();
```

**`ReflectionExtensions.IsSimpleType(this Type type)`**

A type is simple if it is primitive or value type, or enum or string

```cs
	IEnumerable<PropertyInfo> simpleProperties = typeof(MyClass).GetProperties()
		.Where(p => p.PropertyType.IsSimpleType())
```

## Priority Extensions

A set of extensions to order elements by their type priority

**`PriorityExtensions.OrderByPriority<T>(this IEnumerable<T> items)`**

 Orders the elements by priority attribute on their type.

 ```cs
	interface IPrio 
	{
	}

	[Priority(1)]
	public class Prio1 : IPrio
	{
    }

	[Priority(2)]
	public class Prio2 : IPrio
	{
	}

	IPrio p1 = new Prio1();
	IPrio p2 = new Prio2();

	IPrio[] items = new IPrio[] { p1, p2 };
	IPrio[] ordered = items.OrderByPriority();
 ```