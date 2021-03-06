import repository from '../repository'
import { setErrorResponse, setSuccessResponse} from '../shared/models/response/response'

export async function createAccount(request) {
    const result = await repository.AccountRepository.createAccount(request) 

    if(!result) {
        return setErrorResponse('Error on inserting a new account', 500)
    }

    return setSuccessResponse(result, 201)
}

export async function getAccountById(id) {     
    const result = await repository.AccountRepository.getAccountById(id) 

    if(!result) {
        return setErrorResponse('Account is not found', 400)
    }

    return setSuccessResponse(result, 200)
}

export async function getAccountByEmail(email) {     
    const result = await repository.AccountRepository.getAccountByEmail(email) 

    if(!result) {
        return setErrorResponse('Account is not found', 400)
    }

    return setSuccessResponse(result, 200)
}

export async function deleteAndUpdateAccount(id) {

    const result = await repository.AccountRepository.deleteAndUpdateAccount(id) 

    return setSuccessResponse(result, 204) 

}

export async function updateAccount(request) {

    const result = await repository.AccountRepository.updateAccount(request) 

    if(!result) {
        return setErrorResponse('Error on updating account', 500)
    }

    return setSuccessResponse(result, 200)
}