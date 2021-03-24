import { toSeconds } from './time'

describe('TimeSpan to seconds', () => {
    it('Only seconds', () => {
        expect(toSeconds('00:00:12')).toEqual(12)
    })

    it('Only minutes', () => {
        expect(toSeconds('00:12:00')).toEqual(12 * 60)
    })

    it('Only hours', () => {
        expect(toSeconds('12:00:00')).toEqual(12 * 3600)
    })

    it('Complex', () => {
        expect(toSeconds('4:53:1')).toEqual(1 + 53 * 60 + 4 * 3600)
    })

    it('With milliseconds', () => {
        expect(toSeconds('0:0:1.6500000')).toEqual(2)
    })

    it('Invalid', () => {
        expect(toSeconds('invalid value')).toEqual(NaN)
    })
})