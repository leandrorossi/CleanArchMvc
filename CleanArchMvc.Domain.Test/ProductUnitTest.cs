using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace CleanArchMvc.Domain.Tests;
public class ProductUnitTest
{
    [Fact(DisplayName = "Create product with valid state")]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "product image");
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create product with negative id")]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product name", "Product description", 9.99m, 99, "product image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact(DisplayName = "Create product with short name")]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product description", 9.99m, 99, "product image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact(DisplayName = "Create product with long image name")]
    public void CreateProduct_LongImageNameValue_DomainExceptionLongImageName()
    {
        Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99,
            "product image tooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo loooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnggggggggggggggggggggggggggggggggggggggggg");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image, too long, maximum 250 characters");
    }

    [Fact(DisplayName = "Create product with null image name")]
    public void CreateProduct_WithNullImageNameValue_NoDomainException()
    {
        Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create product with null image name")]
    public void CreateProduct_WithNullImageNameValue_NoNullReferenceException()
    {
        Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, null);
        action.Should()
            .NotThrow<NullReferenceException>();
    }

    [Fact(DisplayName = "Create product with empty image name")]
    public void CreateProduct_WithEmptyImageNameValue_NoDomainException()
    {
        Action action = () => new Product(1, "Product name", "Product description", 9.99m, 99, "");
        action.Should()
            .NotThrow<Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName = "Create product with invalid price")]
    public void CreateProduct_InvalidPriceValue_DomainExceptionNegativeValue()
    {
        Action action = () => new Product(1, "Product name", "Product description", -99.9m, 99, "product image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }

    [Theory(DisplayName = "Create product with invalid stock")]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_DomainExceptionNegativeValue(int value)
    {
        Action action = () => new Product(1, "Product name", "Product description", 9.99m, value, "product image");
        action.Should()
            .Throw<Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }
}
