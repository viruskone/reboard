const validateEmail = email => {
    // eslint-disable-next-line no-useless-escape
    var re = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    return re.test(String(email).toLowerCase());
}

const emailValidators = [
    { testInvalid: value => value === '', message: "You must type your e-mail" },
    { testInvalid: value => !validateEmail(value), message: "Type correct e-mail address" }
]

export default emailValidators