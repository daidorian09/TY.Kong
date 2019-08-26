import { model } from 'mongoose'
const Account = model('Account')
import logger from '../utils/logger/logger'

module.exports = async function create(request) {

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
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/create -> ${error.message}`)
        return null
    }  

}

module.exports = async function getById(id) {

    try {
        const account = await Account.findOne({id, isActive : true})
        .lean()
        .exec()
        
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/getById repository -> ${error.message}`)
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
        const account = await Account.findByIdAndUpdate(id, $set, {new: true}).lean().exec()
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/deleteAndUpdate repository -> ${error.message}`)
        return null
    }  

}

export default async function update(request) {

    try {
        const account = await Account.findByIdAndUpdate(request.id, request.data, {new: true}).lean().exec()
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/update repository -> ${error.message}`)
        return null
    }
}