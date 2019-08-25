import { model } from 'mongoose'
const Account = model('Account')
import { setErrorResponse, setSuccesResponse } from '../models/response/response';

export default async function create(request) {

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
        
    }  

}

export default async function getById(id) {

    try {
        const account = await Account.findOne({id, isActive : true})
        .lean()
        .exec()

        if(!account) {
            
        }
        
        return account;
    } catch (error) {
        
    }  

}

export default async function deleteAndUpdate(id) {

    try {
        const $set = {
            $set : {
                isActive : false,
                updatedAt : Date.now()
            }
        }
        const account = await Account.findByIdAndUpdate(id, $set, {new: true}).lean().exec()
        return account;
    } catch (error) {
        
    }  

}

export default async function update(request) {

    try {
        const account = await Account.findByIdAndUpdate(request.id, request.data, {new: true}).lean().exec()
        return account;
    } catch (error) {
        
    }
}