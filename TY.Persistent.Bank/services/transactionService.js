import repository from '../repository';
import { setErrorResponse, setSuccessResponse } from '../shared/models/response/response';

export async function createTransaction(request) {
    const result = await repository.TransactionRepository.createTransaction(request) 

    if(!result) {
        return setErrorResponse('Error on inserting a new transaction', 500)
    }

    return setSuccessResponse(result, 201)
}

export async function getTransactionById(id) {     
    const result = await repository.TransactionRepository.getTransactionById(id) 

    if(!result) {
        return setErrorResponse('Transaction is not found', 400)
    }

    return setSuccessResponse(result, 200)
}

export async function deleteAndUpdateTransaction(id) {

    const result = await repository.TransactionRepository.deleteAndUpdateTransaction(id) 

    return setSuccessResponse(result, 204) 

}

export async function updateTransaction(request) {

    const result = await repository.TransactionRepository.updateTransaction(request) 

    if(!result) {
        return setErrorResponse('Error on updating transaction', 500)
    }

    return setSuccessResponse(result, 200)
}