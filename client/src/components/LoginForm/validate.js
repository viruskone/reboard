export default (validators, value, setErrorFunction) => {
    var invalid = validators.find(element => element.testInvalid(value))
    if (invalid) {
        setErrorFunction(invalid.message)
        return false
    }
    setErrorFunction('')
    return true
}