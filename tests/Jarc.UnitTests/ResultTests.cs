namespace Bcm.Jarc.UnitTests;

public class ResultTests
{
    [Fact]
    public void Result_Exception_Value_Should_Have_Failed_State()
    {
        // Arrange
        Result data = new ArgumentException();

        // Assert
        Assert.False(data.IsSuccess);
    }

    [Fact]
    public void Result_Non_Exception_Value_Should_Have_Success_State()
    {
        // Arrange
        Result data = Result.Empty;

        // Assert
        Assert.True(data.IsSuccess);
        Assert.Null(data.Error);
    }

    [Fact]
    public void Result_Match_Exception_Value_Should_Call_Error_Func()
    {
        // Arrange
        Result data = new ArgumentException();

        // Act
        var result = data.Match(() => true, _ => false);

        // Assert
        Assert.False(data.IsSuccess);
        Assert.False(result);
    }

    [Fact]
    public void Result_Non_Exception_Value_Should_Call_Success_Func()
    {
        // Arrange
        Result data = Result.Empty;

        // Act
        var result = data.Match(() => true, _ => false);

        // Assert
        Assert.True(data.IsSuccess);
        Assert.True(result);
    }

    [Fact]
    public void Generic_Result_Exception_Value_Should_Have_Failed_State()
    {
        // Arrange
        Result<int> data = new ArgumentException();

        // Assert
        Assert.False(data.IsSuccess);
        Assert.True(data.Value == default);
    }

    [Fact]
    public void Generic_Result_Non_Exception_Value_Should_Have_Success_State()
    {
        // Arrange
        var value = 5;
        Result<int> data = value;

        // Assert
        Assert.True(data.IsSuccess);
        Assert.Equal(value, data.Value);
        Assert.Null(data.Error);
    }

    [Fact]
    public void Generic_Result_Match_Exception_Value_Should_Call_Error_Func()
    {
        // Arrange
        var indicator = -1;
        var expected = 1;

        Result<int> data = new ArgumentException();

        // Act
        var result = data.Match(_ => indicator, _ => indicator = expected);

        // Assert
        Assert.False(data.IsSuccess);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Generic_Result_Match_Non_Exception_Value_Should_Call_Success_Func()
    {
        // Arrange
        var expected = 1;

        Result<int> data = expected;

        // Act
        var result = data.Match(value => value, _ => 0);

        // Assert
        Assert.True(data.IsSuccess);
        Assert.Equal(expected, result);
    }
}
