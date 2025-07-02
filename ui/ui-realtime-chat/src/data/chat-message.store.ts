import type { ChatMessage } from '@/@types/chat-message'
import { Store } from '@tanstack/store'
import { v4 as uuidv4 } from 'uuid'

export const chatMessagesStore = new Store<ChatMessage[]>([
  {
    id: uuidv4(),
    locutor: 'Human',
    message:
      'Hello you are an very cool Ai Chat bot that automate tasks with Semantic Kernel Function Calls and MCP',
  },
  {
    id: uuidv4(),
    locutor: 'AI system',
    message:
      'Thank you for your kind words! I am an advanced AI chatbot designed to help you automate a wide range of tasks using Semantic Kernel Function Calls and Microsoft Copilot Platform (MCP). Whether you need to process data, integrate with external APIs, or streamline your workflow, I am here to assist you. Just let me know what you would like to accomplish, and I will leverage my capabilities to provide efficient and intelligent solutions. How can I help you today?',
  },
])
