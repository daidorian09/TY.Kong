import { model } from 'mongoose'
const Transaction = model('Transaction')
import logger from '../utils/logger/logger'

module.exports = async function create(request) {

    try {
        const transaction = new Transaction()

        transaction.account = request.account
        transaction.amount = request.amount
        transaction.oldBalance = request.oldBalance
        transaction.currentBalance = request.currentBalance
        transaction.transactionType = request.transactionType
    
        await transaction.save()
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/create -> ${error.message}`)
        return null
    }  

}

module.exports = async function getById(id) {

    try {
        const transaction = await Transaction.findOne({id, isActive : true})
        .lean()
        .exec()        
        return transaction;
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/getById -> ${error.message}`)
        return null
    }  

}

module.exports = async function deleteAndUpdate(id) {

    try {
        const $set = {
            $set : {
                isActive : false,
                updatedAt : Date.now()
            }
        }
        const transaction = await Transaction.findByIdAndUpdate(id, $set, {new: true}).lean().exec()
        return transaction;
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/deleteAndUpdate -> ${error.message}`)
        return null
    }  

}

module.exports = async function update(request) {

    try {
        const transaction = await Transaction.findByIdAndUpdate(request.id, request.data, {new: true}).lean().exec()
        return transaction;
    } catch (error) {
        logger.error(`Exception thrown in TransactionRepository/update -> ${error.message}`)
        return null
    }
}