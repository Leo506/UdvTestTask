using FluentAssertions;
using UdvTestTask.Extensions;
using UdvTestTask.Models;
using UdvTestTask.Services;

namespace UdvTestTask.UnitTests;

public class LetterCounterTests
{
    [Fact]
    public void Count_AllGood_ReturnsDictNotNull()
    {
        // arrange
        var sut = new LetterCounter();

        // act
        var result = sut.Count(new PostModel()
        {
            Content = "post content"
        });


        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void Count_AllGood_ReturnsDictNotEmpty()
    {
        // arrange
        var sut = new LetterCounter();

        // act
        var result = sut.Count(new PostModel()
        {
            Content = "post content"
        });


        // assert
        result.Should().NotBeEmpty();
    }

    [Fact]
    public void Count_AllGood_ReturnsCorrectDict()
    {
        // arrange
        var expectedDict = new Dictionary<char, int>()
        {
            ['p'] = 1,
            ['o'] = 2,
            ['s'] = 1,
            ['t'] = 3,
            ['c'] = 1,
            ['n'] = 2,
            ['e'] = 1
        };
        var sut = new LetterCounter();

        
        // act
        var actualDict = sut.Count(new PostModel()
        {
            Content = "post content"
        });

        var result = actualDict.IsEqualsToDict(expectedDict);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Count_DifferentRegisters_IgnoreRegister()
    {
        // arrange
        var expectedDict = new Dictionary<char, int>()
        {
            ['p'] = 1,
            ['o'] = 2,
            ['s'] = 1,
            ['t'] = 3,
            ['c'] = 1,
            ['n'] = 2,
            ['e'] = 1
        };

        var sut = new LetterCounter();

        // act
        var actualDict = sut.Count(new PostModel()
        {
            Content = "PoSt CoNtEnT"
        });

        var result = actualDict.IsEqualsToDict(expectedDict);
        
        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Count_AllGood_ReturnsDictInAlphabeticOrder()
    {
        // arrange
        var expectedDict = new Dictionary<char, int>()
        {
            ['a'] = 1,
            ['b'] = 1,
            ['c'] = 1,
            ['d'] = 1
        };

        var sut = new LetterCounter();

        // act
        var actualDict = sut.Count(new PostModel()
        {
            Content = "dcba"
        });

        var result = actualDict.SequenceEqual(expectedDict);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Count_NoPosts_ReturnsEmpty()
    {
        // arrange
        var sut = new LetterCounter();

        // act
        var result = sut.Count();

        // assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Count_NoContent_ReturnsEmpty()
    {
        // arrange
        var sut = new LetterCounter();

        // act
        var result = sut.Count(new PostModel());

        // assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Count_PostsIsNull_ReturnsEmpty()
    {
        // arrange
        var sut = new LetterCounter();

        // act
        var result = sut.Count(null);
        
        // assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Count_PostsContainsDigits_IgnoreDigits()
    {
        // arrange
        var expectedDict = new Dictionary<char, int>()
        {
            ['a'] = 1,
            ['b'] = 1,
            ['c'] = 1
        };
        
        var sut = new LetterCounter();

        // act
        var resultDict = sut.Count(new PostModel()
        {
            Content = "a0b1c2"
        });

        var result = resultDict.SequenceEqual(expectedDict);

        // assert
        result.Should().BeTrue();
    }
}