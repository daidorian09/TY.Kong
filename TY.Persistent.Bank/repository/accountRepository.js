import { model } from 'mongoose';

import logger from '../utils/logger/logger';

const Account = model('Account')
export async function createAccount(request) {

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

        return true
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/create -> ${error.message}`)
        return null
    }

}

export async function getAccountById(id) {

    try {
        const account = await Account.findOne({
                _id: id,
                isActive: true
            }, {
                password: 0
            })
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
            $set: {
                isActive: false,
                updatedAt: Date.now()
            }
        }
        const account = await Account.findByIdAndUpdate(id, $set, {
            new: true
        }).lean().exec()
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/deleteAndUpdate repository -> ${error.message}`)
        return null
    }

}

export async function updateAccount(request) {

    try {
        const account = await Account.findByIdAndUpdate(request.id, request.data, {
                new: true
            })
            .select({
                '_id': 1,
                'firstName': 1,
                'lastName': 1,
                'age': 1,
                'createdAt': 1,
                'updatedAt': 1,
                'address': 1,
                'balance': 1,
                'isActive': 1,
                'phoneNumber': 1,
                'email': 1
            })
            .lean()
            .exec()
        return account
    } catch (error) {
        logger.error(`Exception thrown in AccountRepository/update repository -> ${error.message}`)
        return null
    }
}