using FluentValidation;
using RestaurantRestApi.Entities;

namespace RestaurantRestApi.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
        private string[] allowedSortByColumNames =
            {nameof(Restaurant.Name), nameof(Restaurant.Description), nameof(Restaurant.Category)};
        public RestaurantQueryValidator()
        {
            RuleFor(r => r.pageNumber).GreaterThanOrEqualTo(1);
            RuleFor(r => r.pageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"Pagesize must be in[{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(r => r.SortBy)
                .Must(value => string.IsNullOrEmpty(value) || allowedSortByColumNames.Contains(value))
                .WithMessage($"Sort by optional or must be in [{string.Join(",", allowedSortByColumNames)}] ");

        }
    }
}
