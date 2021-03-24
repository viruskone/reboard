import Logger from "js-logger"
import * as errors from "../constants/errors"

export function failurePayload(code, error) {
    const result = {
        code,
        details: error ? error.message : '',
        error
    }
    Logger.error(result)
    return result
}

export const getErrorMessage = error => {
    switch (error.code) {
        case errors.LOGIN_WRONG_CREDENTIALS:
            return "Niepoprawne dane. Sprawdź ich poprawność i spróbuj ponownie"
        case errors.FETCH_ERROR:
            return "Wystąpił problem połączenia z serwerem"
            break;
    }
}