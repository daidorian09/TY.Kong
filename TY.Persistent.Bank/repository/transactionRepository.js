import { model } from 'mongoose'
const Transaction = model('Transaction')
import logger from '../utils/logger/logger'

export async function createTransaction(request) {

    try {
        const transaction = new Transaction()

        transaction.account = request.account
        transaction.amount = request.amount
        transaction.oldBalance = request.oldBalance
        transaction.currentBalance = request.currentBalance
        transaction.transactionType = request.transactionType
    
        await transaction.save()

        return true
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/create -> ${error.message}`)
        return null
    }  
}

export async function getTransactionById(id) {

    try {
        const transaction = await Transaction.findOne({_id : id, isActive : true})
        .populate('account', '-password')
        .lean()
        .exec()        
        return transaction
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/getById -> ${error.message}`)
        return null
    }  

}

export async function deleteAndUpdateTransaction(id) {

    try {
        const $set = {
            $set : {
                isActive : false,
                updatedAt : Date.now()
            }
        }
        const transaction = await Transaction.findByIdAndUpdate(id, $set, {new: true}).lean().exec()
        return transaction
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/deleteAndUpdate -> ${error.message}`)
        return null
    }  

}

export async function updateTransaction(request) {

    try {
        const transaction = await Transaction.findByIdAndUpdate(request.id, request.data, {new: true}).lean().exec()
        return transaction
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/update -> ${error.message}`)
        return null
    }
}