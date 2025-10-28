'use client'
import "./page.css"

export default function HomePage() {
    return(
        <main className="flex flex-col items-center justify-center min-h-screen space-y-6">
            <h1 className="text-4xl font-bold mb-4 text-center text-top text-[#243C80]"> 
                Welcome back, User! 
            </h1>
            <div className="flex space-x-6">
                <div className="box-wrapper box-wrapper--small">
                    <h1 className="text-2xl font-bold mb-4 text-center text-top text-[#1B2D5E]">Competition overview</h1>
                </div>

                <div className="box-wrapper box-wrapper--small">
                    <p className="text-[#1B2D5E]">Competition Status</p>
                </div>

                <div className="box-wrapper box-wrapper--small">
                    <p className="text-[#1B2D5E]">Quick Actions</p>
                </div>
            </div>

            <div className="">
                <div className="box-wrapper box-wrapper--info">
                    <p>competition progess</p>
                </div>
            </div>

            <div className="flex space-x-6">
                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">participants</p>
                </div>

                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">schedule</p>
                </div>
            </div>

            

            <div className="flex space-x-6">
                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">results</p>
                </div>

                <div className="box-wrapper">
                    <p className="text-[#1B2D5E]">judges & officials</p>
                </div>
            </div>
            
        </main> 
    )
}