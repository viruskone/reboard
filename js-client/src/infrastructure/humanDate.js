const isMoreLikeAges = milliseconds => {
  const ages = new Date(0);
  ages.setHours(2);
  return milliseconds > ages.getTime();
};

const ranges = [
  {
    text: () => "poniżej minuty",
    range: m => m == 0
  },
  {
    text: () => "powyżej 1 minuty",
    range: m => m == 1
  },
  {
    text: m => `${m} minuty`,
    range: m => (m > 1 && m <= 4) || (m >= 22 && m % 10 >= 2 && m % 10 < 5)
  },
  {
    text: m => `${m} minut`,
    range: m => m >= 5
  }
];

export function formatTimeSpan(milliseconds) {
  if (isMoreLikeAges(milliseconds)) return "wieki";

  const date = new Date(milliseconds);
  const minutes = date.getMinutes();

  const inRange = ranges.find(range => range.range(minutes));
  return inRange ? inRange.text(minutes) : "";
}
