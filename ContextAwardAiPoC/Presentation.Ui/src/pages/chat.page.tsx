import { useState } from "react";
import { useForm } from "react-hook-form";
import { twMerge as tw } from 'tailwind-merge';
import { v4 as uuidv4 } from 'uuid';
type ChatMessage = {
  id: string;
  locutor: string;
  content: string;
}

type ChatForm = {
  message: string;
}

export function ChatPage() {
  const [chatMessages, setChatMessages] = useState<ChatMessage[]>([
    {
      id: uuidv4(),
      locutor: "Ai",
      content: "Hello how can i help you today"
    },
    {
      id: uuidv4(),
      locutor: "Human",
      content: "Hello can you connect to my alexa and make me a coffe?"
    }
  ]);

  const { register, formState: { isValid }, handleSubmit, reset } = useForm<ChatForm>({
    values: {
      message: ''
    }
  });

  const handleSendMessage = (chatForm: ChatForm) => {
    console.log({chatForm})
    setChatMessages([...chatMessages, {
      id: uuidv4(),
      locutor: "Human",
      content: chatForm.message
    }])
  }


  return (<section className="grid grid-rows-8 gap-6 h-screen p-6">
      <section className="w-full row-span-7 bg-gray-100 shadow-lg rounded-2xl p-6 border-gray-200 border-2">
        <ul className="flex flex-col gap-2">
        { chatMessages.map(message => (
          <li key={message.id} className="grid columns-8 gap-6 border-2 border-gray-200">
            {message.locutor === "Ai" ? (
              <span className="grid grid-rows-1 grid-cols-8 gap-6 w-full">

            <span className=" col-span-1 bg-gray-200"> { message.locutor }</span>
            <span className="col-span-7 text-wrap wrap-anywhere">{ message.content }</span>
              </span>
            ) : (
              <span className="grid grid-rows-1 grid-cols-8 gap-6 w-full">

            <span className="col-span-7 text-wrap wrap-anywhere">{ message.content }</span>
            <span className=" col-span-1 bg-gray-200"> { message.locutor }</span>
              </span>
              )}
          </li>
        ))
        }
        </ul>
      </section>
      <form onSubmit={handleSubmit(handleSendMessage)} className="flex gap-6 items-center w-full row-span-2 bg-gray-100 shadow-lg rounded-2xl p-6 border-gray-200 border-2">
        <input {...register("message")} className="w-full h-full bg-white p-6 text-gray-500 outline-0 focus:border-blue-700 border-2 rounded-xl border-gray-50 transition-all"/>
      <button className={ tw(isValid ? "bg-blue-700 hover:bg-blue-500" : "bg-red-400", "hover:cursor-pointer transition-all p-2.5 text-white font-semibold text-xl rounded-2xl")  }>
        { isValid ? "Send" : "Clear" }
      </button>
      </form>
    </section>)
}
