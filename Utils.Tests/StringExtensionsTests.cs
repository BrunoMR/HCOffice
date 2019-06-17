using System;
using System.Linq;

namespace Utils.Tests
{
    using Xunit;

    public class StringExtensionsTests
    {
        #region Prefix

        [Fact]
        public void Should_catch_prefix_with_2_Characters_of_marca()
        {
            //var result = "Axe".Ortografar().GetPrefix();

            var text = "NANOVIN A CRESCIMENTO CAPILAR - CAVALO DE OURO";

            var splitw = text.SplitWords();
            var result = text.Spell();
            var result2 = text.NotSpellManyWords();
            var result4 = text.NotSpellManyWords().GetStringReverse();
            var result3 = text.NotSpellOutNoSpaceManyWords();
            var result5 = text.NotSpellOutNoSpaceManyWords().GetStringReverse();

            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void Should_catch_prefix_with_3_Characters_of_marca()
        {
            var result = "machado".Spell().GetPrefix();

            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void Should_catch_prefix_with_4_Characters_of_marca()
        {
            var result = "AMIL SUL NW".Spell().GetPrefix();

            Assert.Equal(4, result.Length);
        }

        [Fact]
        public void Should_catch_prefix_with_5_Characters_of_marca()
        {
            var result = "CATERPILLAR".Spell().GetPrefix();

            Assert.Equal(5, result.Length);
        }

        [Fact]
        public void Should_catch_prefix_with_6_Characters_of_marca()
        {
            var result = "A GRANDE FAMILIA".Spell().GetPrefix();

            Assert.Equal(6, result.Length);
        }

        #endregion Prefix

        #region Suffix

        [Fact]
        public void Should_catch_suffix_with_2_Characters_of_marca()
        {
            var result = "Axe".Spell().GetSuffix();

            Assert.Equal(2, result.Length);
        }

        [Fact]
        public void Should_catch_suffix_with_2_Characters_and_match()
        {
            var result = "Axe".Spell().GetSuffix();

            Assert.Equal("XE", result);
        }

        [Fact]
        public void Should_catch_suffix_with_3_Characters_of_marca()
        {
            var result = "machado".Spell().GetSuffix();

            Assert.Equal(3, result.Length);
        }

        [Fact]
        public void Should_catch_suffix_with_3_Characters_and_match()
        {
            var result = "machado".Spell().GetSuffix();

            Assert.Equal("ADO", result);
        }

        [Fact]
        public void Should_catch_suffix_with_4_Characters_of_marca()
        {
            var result = "AMIL SUL NW".Spell().GetSuffix();

            Assert.Equal(4, result.Length);
        }

        [Fact]
        public void Should_catch_suffix_with_4_Characters_and_match()
        {
            var result = "AMIL SUL NW".Spell().GetSuffix();

            Assert.Equal("SUMW", result);
        }

        [Fact]
        public void Should_catch_suffix_with_5_Characters_of_marca()
        {
            var result = "CATERPILLAR".Spell().GetSuffix();

            Assert.Equal(5, result.Length);
        }

        [Fact]
        public void Should_catch_suffix_with_5_Characters_and_match()
        {
            var result = "CATERPILLAR".Spell().GetSuffix();

            Assert.Equal("IULAR", result);
        }

        [Fact]
        public void Should_catch_suffix_with_6_Characters_of_marca()
        {
            var result = "A GRANDE FAMILIA".Spell().GetSuffix();
            
            Assert.Equal(6, result.Length);
        }

        [Fact]
        public void Should_catch_suffix_with_6_Characters_and_match()
        {
            var result = "A GRANDE FAMILIA".Spell().GetSuffix();

            Assert.Equal("AMILIA", result);
        }

        #endregion Suffix

        #region Radical

        [Fact]
        public void Should_catch_radicals_with_1_Characters_of_marca()
        {
            var result = "A".Spell().GetRadical();
            
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 1));
        }

        [Fact]
        public void Should_catch_radicals_with_2_Characters_of_marca()
        {
            var result = "MC".Spell().GetRadical();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 2));
        }

        [Fact]
        public void Should_catch_radicals_with_3_Characters_of_marca()
        {
            var result = "Axe".Spell().GetRadical();
            
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 3));
        }

        [Fact]
        public void Should_catch_radicals_with_4_Characters_of_marca()
        {
            var result = "Machado".Spell().GetRadical();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 4));
        }

        [Fact]
        public void Should_catch_radicals_with_6_Characters_of_marca()
        {
            var result = "AMIL SUL NW".Spell().GetRadical();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 6));
        }

        [Fact]
        public void Should_catch_radicals_with_7_Characters_of_marca()
        {
            var result = "CATERPILLAR".Spell().GetRadical();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 7));
        }

        [Fact]
        public void Should_catch_radicals_with_8_Characters_of_marca()
        {
            var result = "A GRANDE FAMILIA".Spell().GetRadical();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 8));
        }

        [Fact]
        public void Should_catch_radicals_with_9_Characters_of_marca()
        {
            var result = "A GRANDE FAMILIA de novo na tv".Spell().GetRadical();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.TrueForAll(x => x.Length == 9));
        }

        #endregion Radical
    }
}
