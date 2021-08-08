import { useState } from "react";
import emailValidators from "./emailValidators";
import passwordValidators from "./passwordValidators";
import validate from "./validate";

const notEmpty = value => value !== "";
const composeStateProperty = () => {
  const [property, setProperty] = useState("");
  const [propertyError, setPropertyError] = useState("");
  return {
    get: () => property,
    set: setProperty,
    haveError: notEmpty(propertyError),
    setError: setPropertyError,
    getError: () => propertyError
  };
};

const validateProperty = (validators, property) => {
  return validate(validators, property.get(), property.setError);
};

export default function build(loginCallback) {
  const state = {
    login: composeStateProperty(),
    password: composeStateProperty()
  };
  return Object.assign(state, {
    handleSubmit: event => {
      event.preventDefault();
      const emailIsValid = validateProperty(emailValidators, state.login);
      const passIsValid = validateProperty(passwordValidators, state.password);
      if (emailIsValid && passIsValid) {
        loginCallback(state.login.get(), state.password.get());
      } else {
        return false;
      }
    }
  });
}