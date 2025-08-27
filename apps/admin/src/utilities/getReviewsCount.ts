export const getReviewsCount = (reviewsCount: number) => {
  if (reviewsCount === 1) {
    return `${reviewsCount} User Review`;
  } else if (reviewsCount > 1 && reviewsCount < 1000) {
    return `${reviewsCount} User Reviews`;
  } else if (reviewsCount >= 1000) {
    return `${(reviewsCount / 1000).toFixed(0)}k+ User Reviews`;
  }
  return `${reviewsCount} User Reviews`;
};
