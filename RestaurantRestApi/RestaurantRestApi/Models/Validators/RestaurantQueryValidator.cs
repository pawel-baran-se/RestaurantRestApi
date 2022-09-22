﻿using FluentValidation;

namespace RestaurantRestApi.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = new[] { 5, 10, 15 };
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
        }
    }
}
