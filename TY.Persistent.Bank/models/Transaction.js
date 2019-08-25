var mongoose = require('mongoose')
mongoose.set('debug', true)

var TransactionSchema = new mongoose.Schema({
    account: {
        type: mongoose.Schema.Types.ObjectId,
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
}, {
    timestamps: true,
    toJSON: true
});

mongoose.model('Transaction', TransactionSchema)