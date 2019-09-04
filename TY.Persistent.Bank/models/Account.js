import { set, Schema, model } from 'mongoose'
import bcrypt from 'bcrypt'
set('debug', true)

const AccountSchema = new Schema({
    firstName: {
        type: String,
        required: true,
        min: 3,
        max: 50
    },
    lastName: {
        type: String,
        required: true,
        min: 3,
        max: 100,
        trim: true
    },
    address: {
        type: String,
        trim: true,
        max: 255
    },
    email: {
        unique: true,
        type: String,
        trim: true,
        index: true
    },
    phoneNumber: {
        type: String,
        trim: true,
        default: null
    },
    age: {
        type: Number,
        default: 0,
        required: true
    },
    salt: {
        type: String,
        default: null,
        trim: true
    },
    password: {
        type: String,
        min: 6,
        required: true,
        trim: true
    },
    balance: {
        type: Number,
        required: true,
        default: 0
    },
    isActive: {
        type: Boolean,
        default: true
    }
}, {
    timestamps: true,
    toJSON: {
        virtuals: true
    }
})

AccountSchema.set('toJSON', {
    transform: function(doc, ret, opt) {
        delete ret['password']
        return ret
    }
})

model('Account', AccountSchema)