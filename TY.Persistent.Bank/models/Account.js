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

AccountSchema.pre('save', async function (next) {
    try {
      if (!this.isModified('password')) {
        // Skip it & stop this function from running
        return next()
      }
  
      // Generate a salt
      const salt = await bcrypt.genSalt(Number(process.env.SALT_ROUNDS))
  
      // Hash the password along with our new salt
      const hash = await bcrypt.hash(this.password, salt)
  
      // Override the cleartext password with the hashed one
      this.password = hash
  
      return next()
    } catch (e) {
      return next(e)
    }
})

AccountSchema.set('toJSON', {
    transform: function(doc, ret, opt) {
        delete ret['password']
        return ret
    }
})

model('Account', AccountSchema)