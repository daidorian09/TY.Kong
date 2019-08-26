import { set, Schema, model } from 'mongoose'
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

model('Account', AccountSchema)