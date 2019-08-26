import repository from '../repository'
import { setErrorResponse, setSuccesResponse} from '../shared/models/response/response'

module.exports = async function create(request) {
    const result = await repository.TransactionRepository.create(request) 

    if(!result) {
        return setErrorResponse('Error on inserting a new transaction', 500)
    }

    return setSuccesResponse(result, 201)
}

module.exports = async function getById(id) {     
    const result = await repository.TransactionRepository.getById(id) 

    if(!result) {
        return setErrorResponse('Account is not found', 400)
    }

    return setSuccesResponse(result, 200)
}

module.exports = async function deleteAndUpdate(id) {

    const result = await repository.TransactionRepository.deleteAndUpdate(id) 

    return setSuccesResponse(result, 204) 

}

module.exports = async function update(request) {

    const result = await repository.TransactionRepository.update(request) 

    if(!result) {
        return setErrorResponse('Error on updating transaction', 500)
    }

    return setSuccesResponse(result, 200)
}