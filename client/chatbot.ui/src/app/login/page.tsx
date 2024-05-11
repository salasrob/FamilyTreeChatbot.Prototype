import React from 'react'

export default function LoginPage() {
  return (
    <div className='h-screen w-screen flex justify-center items-center bg-gray-700'>
        <div className='sm:shadow-xl px-8 pb-8 pt-12 space-y-12 rounded-xl bg-gray-500'>
            <h1 className='font-semibold text-2xl text-center'>Log In</h1>
            <p className='text-sm'>Don&apos;t have an account? Register here</p>
        </div>
    </div>
  )
}
