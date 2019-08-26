import { set, Schema, model } from 'mongoose'
set('debug', true)

const TransactionSchema = new Schema({
    account: {
        type: Schema.Types.ObjectId,
        ref: 'account'
    },
    amount: {
        type: Number,
        required: true,
        default: 0
    },
    oldBalance: {
        type: Number,
        required: true,
        default: 0
    },
    currentBalance: {
        type: Number,
        required: true,
        default: 0
    },
    transactionType: {
        type: Number,
        required: true,
        min: 0,
        max: 1,
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
});

model('Transaction', TransactionSchema)