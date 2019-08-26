import repository from '../repository'
import { setErrorResponse, setSuccesResponse} from '../shared/models/response/response'

module.exports = async function create(request) {
    const result = await repository.AccountRepository.create(request) 

    if(!result) {
        return setErrorResponse('Error on inserting a new account', 500)
    }

    return setSuccesResponse(result, 201)
}

module.exports = async function getById(id) {     
    const result = await repository.AccountRepository.getById(id) 

    if(!result) {
        return setErrorResponse('Account is not found', 400)
    }

    return setSuccesResponse(result, 200)
}

module.exports = async function deleteAndUpdate(id) {

    const result = await repository.AccountRepository.deleteAndUpdate(id) 

    return setSuccesResponse(result, 204) 

}

module.exports = async function update(request) {

    const result = await repository.AccountRepository.update(request) 

    if(!result) {
        return setErrorResponse('Error on updating account', 500)
    }

    return setSuccesResponse(result, 200)
}