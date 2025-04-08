using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Mappers
{
    public static class ReviewMapper
    {
        public static Review ToModel(this ReviewRequestDto review)
        {
            return new Review
            {
                Rating = review.Rating,
                Comment = review.Comment,
                Date = review.Date,
                ReviewerName = review.ReviewerName,
                ReviewerEmail = review.ReviewerEmail,
                ProductId = review.ProductId,
            };
        }

        public static ReviewResponseDto ToDto(this Review review)
        {
            return new ReviewResponseDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                Date = review.Date,
                ReviewerName = review.ReviewerName,
                ReviewerEmail = review.ReviewerEmail,
                ProductId = review.ProductId,
            };
        }

        public static ICollection<Review> ToModel(this IEnumerable<ReviewRequestDto> reviews) => reviews.Select(x => x.ToModel()).ToList();

        public static ICollection<ReviewResponseDto> ToDto(this IEnumerable<Review> reviews) => reviews.Select(x => x.ToDto()).ToList();
    }

}
