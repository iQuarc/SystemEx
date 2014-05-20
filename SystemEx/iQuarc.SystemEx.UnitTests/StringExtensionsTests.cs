using Xunit;

namespace iQuarc.SystemEx.UnitTests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void MatchesWildcard_StarInMiddleWithMatchForBeginEnd_Match()
        {
            ShouldMatch(@"Begin_Any .\ ./&^@123 Characters_End", "Begin*End");
        }

        [Fact]
        public void MatchesWildcard_StarInMiddleNoMatchForBeginEnd_NoMatch()
        {
            ShouldNotMatch("NO.AnyCharacters.End", "Begin*End");
        }

        [Fact]
        public void MatchesWildcard_StarInMiddleWithMatchOtherCaseForBeginEnd_NoMatch()
        {
            ShouldNotMatch("BEGIN_AnyCharacters_End", "Begin*End");
        }

        [Fact]
        public void MatchesWildcard_StarAtEndAndNoOtherCharsInInput_Match()
        {
            ShouldMatch("Begin", "Begin*");
        }

        [Fact]
        public void MatchesWildcard_StarAtBeginAndNoOtherCharsInInput_Match()
        {
            ShouldMatch("End", "*End*");
        }

        [Fact]
        public void MatchesWildcard_QuestionMarkToReplaceOneChar_Match()
        {
            ShouldMatch("BeginXEnd", "Begin?End");
        }

        [Fact]
        public void MatchesWildcard_QuestionMarkToReplaceMoreChars_NoMatch()
        {
            ShouldNotMatch("BeginXXXEnd", "Begin?End");
        }

        [Fact]
        public void MatchesWildcard_TwoQuestionMarksToReplaceTwoChars_Match()
        {
            ShouldMatch("BeginXXEnd", "Begin??End");
        }

        [Fact]
        public void MatchesWildcard_StarAndQuestionMarkToMatch_Match()
        {
            ShouldMatch("BXgin_SomeText_End", "B?gin*End");
        }

        [Fact]
        public void MatchesWildcard_StarAndQuestionMarkNotToMatch_NoMatch()
        {
            ShouldNotMatch("Bgin_SomeText_End", "B?gin*End");
        }

        private static void ShouldMatch(string text, string wildcard)
        {
            bool result = text.MatchesWildcard(wildcard);

            Assert.True(result);
        }

        private static void ShouldNotMatch(string text, string wildcard)
        {
            bool result = text.MatchesWildcard(wildcard);

            Assert.False(result);
        }
    }
}