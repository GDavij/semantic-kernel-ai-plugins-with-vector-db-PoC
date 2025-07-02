import { createFileRoute } from '@tanstack/react-router'
import { BaseLayout } from '../layouts/base.layout'
import logo from '../logo.svg'
import React, { useRef, useEffect, useState } from 'react'
import { useForm, useStore } from '@tanstack/react-form'
import { Store } from '@tanstack/store'
import { chatMessagesStore } from '@/data/chat-message.store'
import { v4 as uuidv4 } from 'uuid'

export const Route = createFileRoute('/chat-view')({
  component: ChatView,
})

function ChatView() {
  const [chatId, setChatId] = useState<string | null>(null)
  const messages = useStore(chatMessagesStore)

  const messagesEndRef = useRef<HTMLDivElement>(null)

  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' })
  }, [messages])

  const form = useForm({
    defaultValues: {
      userMessage: '',
    },
    onSubmit: async ({ value }) => {
      const input = value.userMessage.trim()
      if (!input) return
      messages.push(
        { id: uuidv4(), locutor: 'Human', message: input },
        { id: uuidv4(), locutor: 'AI System', message: "I'm just a demo bot!" },
      )
      form.reset()
    },
  })

  return (
    <>
      <div className="flex flex-col max-w-2xl mx-auto w-full max-h-screen h-full min-h-0">
        {/* Chat header */}
        <div className="flex items-center gap-3 mb-4 flex-shrink-0 bg-white z-10">
          <img src={logo} alt="Logo" className="w-10 h-10 animate-spin-slow" />
          <span className="text-xl font-bold text-indigo-700">
            Realtime Chat
          </span>
        </div>
        {/* Chat messages */}
        <div className="flex-1 min-h-0 overflow-y-auto overflow-x-hidden bg-indigo-50 rounded-lg p-4 shadow-inner transition-all duration-300">
          {messages.map((msg) => (
            <div
              key={msg.id}
              className={`
        flex mb-3 transition-all duration-300
        ${msg.locutor === 'Human' ? 'justify-end' : 'justify-start'}
          `}
            >
              <div
                className={`
          px-4 py-2 rounded-xl max-w-[70%] shadow break-words whitespace-pre-wrap
          ${
            msg.locutor === 'Human'
              ? 'bg-indigo-600 text-white rounded-br-none animate-fade-in-right'
              : 'bg-white text-indigo-900 rounded-bl-none animate-fade-in-left border border-indigo-100'
          }
        `}
              >
                {msg.message}
              </div>
            </div>
          ))}
          <div ref={messagesEndRef} />
        </div>
        {/* Chat input */}
        <form
          onSubmit={(e) => {
            e.preventDefault()
            e.stopPropagation()
            form.handleSubmit()
          }}
          className="flex gap-2 mt-4 flex-shrink-0 bg-white z-10"
          autoComplete="off"
        >
          <form.Field
            name="userMessage"
            children={(field) => (
              <input
                className="flex-1 px-4 py-2 rounded-lg border border-indigo-200 focus:outline-none focus:ring-2 focus:ring-indigo-400 transition"
                placeholder="Type your message..."
                onBlur={field.handleBlur}
                value={field.state.value}
                onChange={(e) => field.handleChange(e.target.value)}
                autoComplete="off"
              />
            )}
          />
          <button
            type="submit"
            className="bg-indigo-600 text-white px-5 py-2 rounded-lg font-semibold shadow hover:bg-indigo-700 transition"
          >
            Send
          </button>
        </form>
      </div>
      {/* Animations */}
      <style>
        {`
          @keyframes fadeInLeft {
            from { opacity: 0; transform: translateX(-30px);}
            to { opacity: 1; transform: translateX(0);}
            }
            @keyframes fadeInRight {
              from { opacity: 0; transform: translateX(30px);}
              to { opacity: 1; transform: translateX(0);}
              }
              .animate-fade-in-left {
                animation: fadeInLeft 0.4s;
                }
                .animate-fade-in-right {
                  animation: fadeInRight 0.4s;
                  }
                  .animate-spin-slow {
                    animation: spin 4s linear infinite;
                    }
                    `}
      </style>
    </>
  )
}
