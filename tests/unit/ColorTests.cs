using System;
using FluentAssertions;
using Reboard.Colors;
using Xunit;

namespace Reboard.UnitTests
{
    public class ColorTests
    {
        [Fact]
        public void check_generating_proper_rgb_color()
        {
            var generator = new HsvColorGenerator(new RangeDouble(1), new RangeDouble(1));
            var color = generator.Generate();
            color.Red.Should().BeInRange(0, 255).And.NotBe(color.Green);
            color.Green.Should().BeInRange(0, 255).And.NotBe(color.Blue);
            color.Blue.Should().BeInRange(0, 255);
            Console.WriteLine($"[{color.Red}, {color.Green}, {color.Blue}]");
        }

        [Fact]
        public void verify_contrast_ratio_test()
        {
            ContrastRatio
               .Contrast(new Rgb(255, 255, 255), new Rgb(0, 0, 0))
               .Should().Be(21);
        }

        [Fact]
        public void verify_contrast_ratio_revert_test()
        {
            ContrastRatio
               .Contrast(new Rgb(0, 0, 0), new Rgb(255, 255, 255))
               .Should().Be(21);
        }

        [Fact]
        public void verify_contrast_ratio_middle_test()
        {
            ContrastRatio
               .Contrast(new Rgb(68, 238, 119), new Rgb(255, 0, 187))
               .Should().BeApproximately(2.3, 0.01);
        }

    }
}