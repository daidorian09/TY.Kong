import { model } from 'mongoose'
const Account = model('Account')
import logger from '../utils/logger/logger'

export async function createAccount(request){

    try {
        const account = new Account()

        account.firstName = request.firstName
        account.lastName = request.lastName
        account.address = request.address
        account.email = request.email
        account.phoneNumber = request.phoneNumber
        account.age = request.age
        account.password = request.password
        account.balance = request.balance
    
        await account.save()

        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/create -> ${error.message}`)
        return null
    }  

}

export async function getAccountById(id) {

    try {
        const account = await Account.findOne({_id : id, isActive : true})
        .lean()
        .exec()
        
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/getById repository -> ${error.message}`)
        return null
    } 
}

export async function deleteAndUpdateAccount(id) {

    try {
        const $set = {
            $set : {
                isActive : false,
                updatedAt : Date.now()
            }
        }
        const account = await Account.findByIdAndUpdate(id, $set, {new: true}).lean().exec()
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/deleteAndUpdate repository -> ${error.message}`)
        return null
    }  

}

export async function updateAccount(request) {

    try {
        const account = await Account.findByIdAndUpdate(request.id, request.data, {new: true}).lean().exec()
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/update repository -> ${error.message}`)
        return null
    }
}