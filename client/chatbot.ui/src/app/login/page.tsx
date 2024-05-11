import React from 'react'

export default function Login() {
  return (
    <div className='h-screen w-screen flex justify-center items-center bg-gray-700'>
        <div className='sm:shadow-xl px-8 p-b8 pt-12 space-y-12 rounded-xl bg-gray-500'>
            <h1 className='font-semibold text-2xl'>Log In</h1>
        <div>
            <label>Username</label>
        </div>
        <div>
            <input className='userName' type="text" required></input>
        </div>
        <div>
            <label>Password</label>
        </div>
        <div>
            <input className='password' type="password" required></input>
        </div>
        <button>Login</button>
        </div>
    </div>
  )
}
