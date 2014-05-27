using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class DictionaryExtensionsTests
    {
        [Fact]
        public void GetValueOrDefault_TValueIsRefType_Null()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            object value = d.GetValueOrDefault("some key");

            Assert.Null(value);
        }

        [Fact]
        public void GetValueOrDefault_TValueIsValueType_Zero()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();

            object value = d.GetValueOrDefault("some key");

            Assert.Equal(0, value);
        }

        [Fact]
        public void GetValueOrDefault_ValueExistsForGivenKey_ValueReturned()
        {
            TestThatIfValueExistsValueIsReturned((dic, key) => dic.GetValueOrDefault(key));
        }

        [Fact]
        public void GetValueOrNew_ValueExistsForGivenKey_ValueReturned()
        {
            TestThatIfValueExistsValueIsReturned((dic, s) => dic.GetValueOrNew(s));
        }

        private static void TestThatIfValueExistsValueIsReturned(Func<Dictionary<string, int>, string, int> funcUnderTest)
        {
            const int someValue = 5;
            Dictionary<string, int> d = new Dictionary<string, int>
                {
                    {"some key", someValue}
                };

            int actual = funcUnderTest(d, "some key");

            Assert.Equal(someValue, actual);
        }

        [Fact]
        public void GetValueOrNew_ValueDoesNotExist_DefaultConstructorUsed()
        {
            Dictionary<string, MyType> d = new Dictionary<string, MyType>();

            MyType actual = d.GetValueOrNew("some key");

            Assert.Equal("Default", actual.Value);
        }

        [Fact]
        public void GetValueOrNew_ValueDoesNotExist_AddedIntoDictionary()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();

            d.GetValueOrNew("some key");

            Assert.Contains("some key", d.Keys);
        }

        [Fact]
        public void GetValueOrNew_ValueDoesNotExists_ConstructorFunctionUsed()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            string actual = d.GetValueOrNew("some key", () => "some value");

            Assert.Equal("some value", actual);
        }

        [Fact]
        public void ConvertToNameValueCollection_EmptyDic_EmptyCollection()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();

            NameValueCollection c = d.ToNameValueCollection();

            Assert.Empty(c);
        }

        [Fact]
        public void ConvertToNameValueCollection_MoreElements_SameOrderInCollection()
        {
            Dictionary<int, string> d = new Dictionary<int, string>
                {
                    {1, "Value1"},
                    {2, "Value2"}
                };

            NameValueCollection c = d.ToNameValueCollection();

            Assert.Equal("Value1", c[0]);
            Assert.Equal("Value2", c[1]);
        }

        [Fact]
        public void ConvertToNameValueCollection_NotStringForKey_ToStringForTKeyUsed()
        {
            Dictionary<int, string> d = new Dictionary<int, string>
                {
                    {1, "Value1"}
                };

            NameValueCollection c = d.ToNameValueCollection();

            Assert.Equal("Value1", c["1"]);
        }

        [Fact]
        public void ConvertToNameValueCollection_NullForValue_ConvertToEmptyString()
        {
            Dictionary<int, string> d = new Dictionary<int, string>
                {
                    {1, null}
                };

            NameValueCollection c = d.ToNameValueCollection();

            Assert.Equal(string.Empty, c[0]);
        }

        [Fact]
        public void ConvertToNameValue_SomeRefTypeAsValue_ToStringUsedToConvertTheValue()
        {
            Dictionary<int, MyType> d = new Dictionary<int, MyType>
                {
                    {1, new MyType("Some value")}
                };

            NameValueCollection c = d.ToNameValueCollection();

            Assert.Equal("Some value", c[0]);
        }

        [Fact]
        public void ConvertToNameValue_ConvertFunction_ConvertFunctionUsedToConvertTheValue()
        {
            Dictionary<int, MyType> d = new Dictionary<int, MyType>
                {
                    {1, new MyType("Some value")}
                };

            NameValueCollection c = d.ToNameValueCollection(type => "converted value");

            Assert.Equal("converted value", c[0]);
        }

        private class MyType
        {
            public string Value { get; private set; }

            public MyType()
            {
                Value = "Default";
            }

            public MyType(string value)
            {
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }
    }
}