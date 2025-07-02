import { Link } from '@tanstack/react-router'
import React, { useState } from 'react'

export const BaseLayout = ({ children }: { children: React.ReactNode }) => {
  const [sidebarOpen, setSidebarOpen] = useState(false)

  return (
    <div className="grid grid-rows-[48px_auto_1fr] grid-cols-1 md:grid-rows-[48px_1fr] md:grid-cols-[240px_1fr] min-h-screen bg-indigo-50 gap-2 md:gap-4">
      {/* Topbar */}
      <div className="row-start-1 col-span-1 md:col-start-2 md:col-span-1 md:row-start-1 p-4 flex items-center min-h-[48px]">
        <header className="bg-indigo-100 text-indigo-900 shadow border-b border-indigo-200 w-full flex items-center justify-between rounded-sm p-2">
          <span className="text-lg font-semibold">Topbar</span>
          {/* Hamburger for mobile */}
          <button
            className="md:hidden p-2 rounded hover:bg-indigo-200"
            onClick={() => setSidebarOpen((open) => !open)}
            aria-label="Toggle sidebar"
          >
            <svg
              width="24"
              height="24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
            >
              <path d="M4 6h16M4 12h16M4 18h16" />
            </svg>
          </button>
        </header>
      </div>
      {/* Sidebar */}
      <aside
        className={`
                    bg-indigo-50 text-indigo-900
                    md:row-start-1 md:row-span-2 md:col-start-1
                    p-4 md:max-w-xs md:w-full flex flex-col gap-6 shadow-md border-r border-indigo-100
                    transition-all duration-300 ease-in-out
                    fixed top-0 right-0 h-full z-40
                    ${sidebarOpen ? 'translate-x-0' : 'translate-x-full'}
                    md:static md:translate-x-0 md:block
                    row-start-2 col-span-1
                `}
        style={{ maxWidth: '240px', width: '100%' }}
      >
        <section>
          <div className="text-2xl font-bold tracking-wide mb-4">AI Chats</div>
          <ul className="flex flex-col gap-2">
            <li className="flex bg-indigo-200 border-2 border-indigo-300 rounded-sm p-1 hover:bg-indigo-300 transition-colors cursor-pointer">
              <Link to={`/chat-view`}>Random Id</Link>
            </li>
          </ul>
        </section>
        {/* Add more sidebar items here */}
      </aside>
      {/* Overlay for mobile */}
      {sidebarOpen && (
        <div
          className="fixed inset-0 bg-transparent backdrop-blur-xs z-30 md:hidden transition-opacity duration-300"
          onClick={() => setSidebarOpen(false)}
        />
      )}
      {/* Main content */}
      <main className="row-start-3 col-span-1 md:col-start-2 md:row-start-2 bg-white p-6 rounded-tl-2xl shadow-inner">
        {children}
      </main>
    </div>
  )
}
