export function setSuccessResponse(data, statusCode = 200) {
    return {
        isSuccess : true,
        result : data,
        headers: {},
        errors : null,
        statusCode
    }
}

export function setErrorResponse(error, statusCode) {
    return {
        isSuccess : false,
        result : null,
        headers: {},
        errors : error,
        statusCode
    }
}