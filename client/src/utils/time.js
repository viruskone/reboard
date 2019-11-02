export const toSeconds = (timeSpan) => 
    Math.round(timeSpan
        .split(':').map((s, i) => Number(s) * Math.pow(60, 2 - i))
        .reduce((a, b) => a + b, 0))