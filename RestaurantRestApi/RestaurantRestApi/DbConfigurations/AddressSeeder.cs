using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantRestApi.Entities;

namespace RestaurantRestApi.DbConfigurations
{
    public class AddressSeeder : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(
                new Address()
                {
                    Id = 1,
                    City = "Kraków",
                    Street = "Długa 5",
                    PostalCode = "30-001"
                },
                new Address()
                {
                    Id = 2,
                    City = "Kraków",
                    Street = "Szewska 2",
                    PostalCode = "30-001"
                }
                );
        }
    }
}