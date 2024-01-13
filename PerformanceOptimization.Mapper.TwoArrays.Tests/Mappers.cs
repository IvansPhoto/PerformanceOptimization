namespace PerformanceOptimization.Mapper.TwoArrays.Tests;

public class Tests
{
    private Input _input = null!;
    private Output[] _output = null!;
    private readonly Generator _generator = new();

    [SetUp]
    public void Setup()
    {
        (_input, _output) = _generator.GetInput(100);
    }

    [Test]
    public void OriginalMappingIsCorrect()
    {
        //Arrange, Act
        var outputs = Original.Map(_input);

        CollectionAssert.AreEquivalent(outputs, _output);
    }

    [Test]
    public void OptimizedMappingIsCorrect()
    {
        //Arrange, Act
        var optimized = Optimized.Map(_input);
        
        //Assert
        CollectionAssert.AreEquivalent(optimized, _output);
    }

    [Test]
    public void MapperOutputAreEqual()
    {
        //Arrange, Act
        var original = Original.Map(_input);
        var optimized = Optimized.Map(_input);

        //Assert
        CollectionAssert.AreEquivalent(original, optimized);
    }
}