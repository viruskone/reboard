import { formatTimeSpan } from "./humanDate";
describe("In PL", () => {
  describe("Proper format", () => {
    const getAsMilliseconds = (minutes, seconds = 0) => {
      var d = new Date(0);
      d.setMinutes(minutes);
      d.setSeconds(seconds);
      return d.getTime();
    };
    const buildCase = (test, text, minutes, seconds) => ({
      test,
      time: { minutes, seconds },
      text
    });
    [
      buildCase("over one minute", "poniżej minuty", 0, 59),
      buildCase("1:25", "powyżej 1 minuty", 1, 25),
      buildCase("1:35", "powyżej 1 minuty", 1, 35),
      buildCase("2:25", "2 minuty", 2, 25),
      buildCase("4:00", "4 minuty", 4, 0),
      buildCase("5:00", "5 minut", 5, 0),
      buildCase("22:00", "22 minuty", 22, 0),
      buildCase("24:00", "24 minuty", 24, 0),
      buildCase("25:00", "25 minut", 25, 0),
      buildCase("ages", "wieki", 240, 0),
      buildCase("ages with days", "wieki", 1444, 0),

    ].forEach(value => {
      it(value.test, () => {
        expect(
          formatTimeSpan(getAsMilliseconds(value.time.minutes, value.time.seconds))
        ).toEqual(value.text);
      });
    });
  });
});
