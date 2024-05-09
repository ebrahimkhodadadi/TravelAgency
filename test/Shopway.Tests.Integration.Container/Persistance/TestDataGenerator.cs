using TravelAgency.Domain.Products;
using TravelAgency.Domain.Products.ValueObjects;
using TravelAgency.Persistence.Abstractions;
using TravelAgency.Persistence.Framework;
using TravelAgency.Tests.Integration.Container.Abstractions;

namespace TravelAgency.Tests.Integration.Container.Persistance;

/// <summary>
/// Contains methods to add entities to the database and utility methods for test data
/// </summary>
public sealed class TestDataGenerator(IUnitOfWork<TravelAgencyDbContext> unitOfWork) : TestDataGeneratorBase(unitOfWork)
{
    public async Task<Product> AddProductAsync
    (
        ProductName? productName = null,
        Revision? revision = null,
        Price? price = null,
        UomCode? uomCode = null
    )
    {
        productName ??= ProductName.Create(TestString(20)).Value;
        price ??= Price.Create(TestInt(1, 10)).Value;
        uomCode ??= UomCode.Create(UomCode.AllowedUomCodes.ElementAt(_random.Next(0, UomCode.AllowedUomCodes.Length - 1))).Value;
        revision ??= Revision.Create(TestString(2)).Value;

        var product = Product.Create
        (
            id: ProductId.New(),
            productName: productName,
            price: price,
            uomCode: uomCode,
            revision: revision
        );

        await AddEntityAsync(product);

        return product;
    }
}