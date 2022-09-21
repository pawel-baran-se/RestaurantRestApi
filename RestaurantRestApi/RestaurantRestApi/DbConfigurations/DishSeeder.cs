using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantRestApi.Entities;

namespace RestaurantRestApi.DbConfigurations
{
    public class DishSeeder : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasData(
                new Dish()
                {
                    Id = 1,
                    Name = "Nashville Hot Chicken",
                    Price = 10.30M,
                    RestaurantId = 1,
                },
                new Dish()
                {
                    Id = 2,
                    Name = "Chicken Nuggets",
                    Price = 5.30M,
                    RestaurantId = 1,
                }
                );
        }
    }
}